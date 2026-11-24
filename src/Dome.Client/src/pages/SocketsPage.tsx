import { useCallback, useEffect, useState } from 'react'
import { ArrowUpCircle, MoreHorizontal } from 'lucide-react'
import { toast } from 'sonner'

import {
  createSocket,
  deleteSocket,
  fetchSockets,
  fetchSocketStatuses,
  setSocketKey,
  updateSocket,
} from '@/api/sockets'
import { SocketKeyHelpDialog } from '@/components/SocketKeyHelpDialog'
import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { cn } from '@/lib/utils'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import type {
  SocketRecord,
  SocketState,
  SocketStatusRecord,
} from '@/types/sockets'

type SocketsState =
  | { status: 'loading' }
  | { status: 'ready'; sockets: SocketRecord[] }
  | { status: 'error'; message: string }

type EditorState =
  | { mode: 'closed' }
  | { mode: 'create' }
  | { mode: 'edit'; socket: SocketRecord }

type SocketStatuses = Record<string, SocketStatusRecord>

const socketUpdateMessage =
  'Updating the socket is essential to prevent errors.'

const keyStateBadges: Partial<
  Record<SocketState, { label: string; tooltip: string }>
> = {
  NeedsPairing: {
    label: 'Needs pairing',
    tooltip:
      "Check the key from the socket's logs and make sure the socket is reachable over https.",
  },
  Unauthorized: {
    label: 'Key rejected',
    tooltip:
      "The socket refuses this key. Use Change key with the key from the socket's logs.",
  },
  CertificateChanged: {
    label: 'Certificate changed',
    tooltip:
      "The socket presents a new certificate, for example after its data volume was recreated. If you expected this, use Change key with the key from the socket's logs to pair again.",
  },
}

function keyStateBadge(status: SocketStatusRecord | undefined) {
  return status ? keyStateBadges[status.state] : undefined
}

function unreachableStatus(id: string): SocketStatusRecord {
  return {
    id,
    isReachable: false,
    state: 'Offline',
    version: null,
    needsUpdate: false,
  }
}

function socketVersionLabel(status: SocketStatusRecord | undefined): string {
  if (!status?.isReachable) {
    return '—'
  }

  if (!status.version) {
    return 'Unknown'
  }

  return status.version.startsWith('v') || status.version.startsWith('V')
    ? status.version
    : `v${status.version}`
}

export function SocketsPage() {
  const [state, setState] = useState<SocketsState>({ status: 'loading' })
  const [editor, setEditor] = useState<EditorState>({ mode: 'closed' })
  const [socketToDelete, setSocketToDelete] = useState<SocketRecord | null>(
    null,
  )
  const [socketStatuses, setSocketStatuses] = useState<SocketStatuses>({})
  const [name, setName] = useState('')
  const [address, setAddress] = useState('')
  const [key, setKey] = useState('')
  const [isKeyHelpOpen, setIsKeyHelpOpen] = useState(false)
  const [formError, setFormError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)
  const [isDeleting, setIsDeleting] = useState(false)
  const [socketToRekey, setSocketToRekey] = useState<SocketRecord | null>(
    null,
  )
  const [isSavingKey, setIsSavingKey] = useState(false)
  const [keyError, setKeyError] = useState<string | null>(null)

  const loadSockets = useCallback(async (signal?: AbortSignal) => {
    try {
      const response = await fetchSockets(signal)
      if (signal?.aborted) {
        return
      }

      setState({ status: 'ready', sockets: response.sockets ?? [] })
    } catch {
      if (signal?.aborted) {
        return
      }

      setState({
        status: 'error',
        message: 'Unable to load sockets. Confirm that the API is running.',
      })
    }
  }, [])

  useEffect(() => {
    const controller = new AbortController()

    void (async () => {
      try {
        const response = await fetchSockets(controller.signal)
        if (!controller.signal.aborted) {
          setState({ status: 'ready', sockets: response.sockets ?? [] })
        }
      } catch {
        if (!controller.signal.aborted) {
          setState({
            status: 'error',
            message: 'Unable to load sockets. Confirm that the API is running.',
          })
        }
      }
    })()

    return () => controller.abort()
  }, [])

  const readySockets = state.status === 'ready' ? state.sockets : undefined

  useEffect(() => {
    if (!readySockets || readySockets.length === 0) {
      return
    }

    const sockets = readySockets
    const controller = new AbortController()

    void (async () => {
      try {
        const response = await fetchSocketStatuses(controller.signal)
        if (!controller.signal.aborted) {
          setSocketStatuses(
            Object.fromEntries(
              response.statuses.map((status) => [status.id, status]),
            ),
          )
        }
      } catch {
        if (!controller.signal.aborted) {
          setSocketStatuses(
            Object.fromEntries(
              sockets.map((socket) => [socket.id, unreachableStatus(socket.id)]),
            ),
          )
        }
      }
    })()

    return () => controller.abort()
  }, [readySockets])

  const openCreate = () => {
    setName('')
    setAddress('')
    setKey('')
    setFormError(null)
    setEditor({ mode: 'create' })
  }

  const openEdit = (socket: SocketRecord) => {
    setName(socket.name)
    setAddress(socket.address)
    setFormError(null)
    setEditor({ mode: 'edit', socket })
  }

  const closeEditor = () => {
    setEditor({ mode: 'closed' })
    setFormError(null)
  }

  const saveSocket = async () => {
    const trimmedName = name.trim()
    const trimmedAddress = address.trim()

    const trimmedKey = key.trim()

    if (!trimmedName || !trimmedAddress) {
      setFormError('Name and address are required.')
      return
    }

    if (editor.mode === 'create' && !trimmedKey) {
      setFormError("Key is required. Copy it from the socket's logs.")
      return
    }

    setIsSaving(true)
    setFormError(null)

    try {
      if (editor.mode === 'edit') {
        await updateSocket({
          id: editor.socket.id,
          name: trimmedName,
          address: trimmedAddress,
        })
        toast.success('Socket updated.')
      } else {
        await createSocket({
          name: trimmedName,
          address: trimmedAddress,
          key: trimmedKey,
        })
        toast.success('Socket registered.')
      }

      closeEditor()
      await loadSockets()
    } catch (error) {
      setFormError(
        error instanceof Error ? error.message : 'Unable to save socket.',
      )
    } finally {
      setIsSaving(false)
    }
  }

  const confirmDelete = async () => {
    if (!socketToDelete) {
      return
    }

    setIsDeleting(true)

    try {
      await deleteSocket(socketToDelete.id)
      toast.success(`Deleted ${socketToDelete.name}.`)
      setSocketToDelete(null)
      await loadSockets()
    } catch (error) {
      toast.error(
        error instanceof Error ? error.message : 'Unable to delete socket.',
      )
    } finally {
      setIsDeleting(false)
    }
  }

  const openRekey = (socket: SocketRecord) => {
    setKey('')
    setKeyError(null)
    setSocketToRekey(socket)
  }

  const saveKey = async () => {
    if (!socketToRekey) {
      return
    }

    const trimmedKey = key.trim()
    if (!trimmedKey) {
      setKeyError("Key is required. Copy it from the socket's logs.")
      return
    }

    setIsSavingKey(true)
    setKeyError(null)

    try {
      await setSocketKey({ id: socketToRekey.id, key: trimmedKey })
      toast.success(`Key changed for ${socketToRekey.name}.`)
      setSocketToRekey(null)
      await loadSockets()
    } catch (error) {
      setKeyError(
        error instanceof Error ? error.message : 'Unable to change the key.',
      )
    } finally {
      setIsSavingKey(false)
    }
  }

  const sockets = state.status === 'ready' ? state.sockets : []
  const socketsNeedingKey = sockets.filter((socket) =>
    keyStateBadge(socketStatuses[socket.id]),
  ).length

  return (
    <div className="flex flex-1 flex-col gap-4 overflow-auto p-6">
      <div className="flex items-start justify-between gap-4">
        <div>
          <h2 className="text-2xl font-semibold tracking-tight">Sockets</h2>
          <p className="text-sm text-muted-foreground">
            Register DOME Socket agents the server should query.
          </p>
        </div>
        <Button type="button" onClick={openCreate}>
          Register socket
        </Button>
      </div>

      {socketsNeedingKey > 0 ? (
        <div className="rounded-lg border border-chart-3/50 bg-chart-3/10 p-4 text-sm">
          <p className="font-medium">
            {socketsNeedingKey === 1
              ? '1 socket needs a key'
              : `${socketsNeedingKey} sockets need a key`}
          </p>
          <p className="text-muted-foreground">
            Copy the key from each socket's logs and enter it with Change key
            in that socket's actions menu.{' '}
            <Button
              type="button"
              variant="link"
              size="xs"
              onClick={() => setIsKeyHelpOpen(true)}
            >
              How to get the socket key?
            </Button>
          </p>
        </div>
      ) : null}

      {state.status === 'loading' ? (
        <p className="text-sm text-muted-foreground">Loading sockets…</p>
      ) : null}

      {state.status === 'error' ? (
        <div className="flex items-center gap-3 text-sm text-destructive">
          <span>{state.message}</span>
          <Button
            variant="outline"
            size="sm"
            onClick={() => {
              setState({ status: 'loading' })
              void loadSockets()
            }}
          >
            Retry
          </Button>
        </div>
      ) : null}

      {state.status === 'ready' ? (
        <div className="rounded-lg border">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-16">
                  <span className="sr-only">Status</span>
                </TableHead>
                <TableHead>Name</TableHead>
                <TableHead>Address</TableHead>
                <TableHead>Version</TableHead>
                <TableHead>Created</TableHead>
                <TableHead className="w-12">
                  <span className="sr-only">Actions</span>
                </TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {sockets.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={6} className="h-24 text-center">
                    No sockets are registered yet.
                  </TableCell>
                </TableRow>
              ) : (
                sockets.map((socket) => (
                  <TableRow key={socket.id}>
                    <TableCell>
                      {keyStateBadge(socketStatuses[socket.id]) ? (
                        <TooltipProvider>
                          <Tooltip>
                            <TooltipTrigger asChild>
                              <Badge variant="destructive">
                                {keyStateBadge(socketStatuses[socket.id])?.label}
                              </Badge>
                            </TooltipTrigger>
                            <TooltipContent side="top">
                              {keyStateBadge(socketStatuses[socket.id])?.tooltip}
                            </TooltipContent>
                          </Tooltip>
                        </TooltipProvider>
                      ) : (
                        <span
                          aria-label={
                            socketStatuses[socket.id] === undefined
                              ? 'Checking status'
                              : socketStatuses[socket.id].isReachable
                                ? 'Socket reachable'
                                : 'Socket unreachable'
                          }
                          className={cn(
                            'inline-flex h-2.5 w-2.5 rounded-full',
                            socketStatuses[socket.id] === undefined
                              ? 'bg-muted-foreground/40'
                              : socketStatuses[socket.id].isReachable
                                ? 'bg-emerald-500'
                                : 'bg-destructive',
                          )}
                        />
                      )}
                    </TableCell>
                    <TableCell className="font-medium">{socket.name}</TableCell>
                    <TableCell>{socket.address}</TableCell>
                    <TableCell>
                      <span className="inline-flex items-center gap-1.5">
                        {socketVersionLabel(socketStatuses[socket.id])}
                        {socketStatuses[socket.id]?.needsUpdate ? (
                          <TooltipProvider>
                            <Tooltip>
                              <TooltipTrigger asChild>
                                <span
                                  className="image-version-check-icon"
                                  aria-label={socketUpdateMessage}
                                >
                                  <ArrowUpCircle
                                    className="size-3.5"
                                    aria-hidden="true"
                                  />
                                </span>
                              </TooltipTrigger>
                              <TooltipContent side="top">
                                {socketUpdateMessage}
                              </TooltipContent>
                            </Tooltip>
                          </TooltipProvider>
                        ) : null}
                      </span>
                    </TableCell>
                    <TableCell>
                      {new Date(socket.createdAt).toLocaleString()}
                    </TableCell>
                    <TableCell>
                      <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                          <Button
                            variant="ghost"
                            size="icon-sm"
                            aria-label={`Actions for ${socket.name}`}
                          >
                            <MoreHorizontal />
                          </Button>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end">
                          <DropdownMenuItem onClick={() => openEdit(socket)}>
                            Edit
                          </DropdownMenuItem>
                          <DropdownMenuItem onClick={() => openRekey(socket)}>
                            Change key
                          </DropdownMenuItem>
                          <DropdownMenuItem
                            variant="destructive"
                            onClick={() => setSocketToDelete(socket)}
                          >
                            Delete
                          </DropdownMenuItem>
                        </DropdownMenuContent>
                      </DropdownMenu>
                    </TableCell>
                  </TableRow>
                ))
              )}
            </TableBody>
          </Table>
        </div>
      ) : null}

      <Dialog
        open={editor.mode !== 'closed'}
        onOpenChange={(open) => {
          if (!open) {
            closeEditor()
          }
        }}
      >
        <DialogContent>
          <DialogHeader>
            <DialogTitle>
              {editor.mode === 'edit' ? 'Edit socket' : 'Register socket'}
            </DialogTitle>
            <DialogDescription>
              Name is unique and used as the device label. Address must be an
              https URL, for example https://host:5110.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4">
            <div className="grid gap-2">
              <Label htmlFor="socket-name">Name</Label>
              <Input
                id="socket-name"
                value={name}
                onChange={(event) => setName(event.target.value)}
                placeholder="Local"
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="socket-address">Address</Label>
              <Input
                id="socket-address"
                value={address}
                onChange={(event) => setAddress(event.target.value)}
                placeholder="https://host:5110"
              />
            </div>
            {editor.mode === 'create' ? (
              <div className="grid gap-2">
                <Label htmlFor="socket-key">Key</Label>
                <Input
                  id="socket-key"
                  value={key}
                  onChange={(event) => setKey(event.target.value)}
                  autoComplete="off"
                  spellCheck={false}
                  placeholder="Paste the key from the socket's logs"
                />
                <Button
                  type="button"
                  variant="link"
                  size="xs"
                  className="justify-self-start"
                  onClick={() => setIsKeyHelpOpen(true)}
                >
                  How to get the socket key?
                </Button>
              </div>
            ) : null}
            {formError ? (
              <p className="text-sm text-destructive">{formError}</p>
            ) : null}
          </div>
          <DialogFooter>
            <Button type="button" variant="outline" onClick={closeEditor}>
              Cancel
            </Button>
            <Button type="button" disabled={isSaving} onClick={() => void saveSocket()}>
              {isSaving ? 'Saving…' : 'Save'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <Dialog
        open={socketToDelete !== null}
        onOpenChange={(open) => {
          if (!open) {
            setSocketToDelete(null)
          }
        }}
      >
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Delete socket</DialogTitle>
            <DialogDescription>
              {socketToDelete
                ? `Remove ${socketToDelete.name} (${socketToDelete.address}) from the server?`
                : null}
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => setSocketToDelete(null)}
            >
              Cancel
            </Button>
            <Button
              type="button"
              variant="destructive"
              disabled={isDeleting}
              onClick={() => void confirmDelete()}
            >
              {isDeleting ? 'Deleting…' : 'Delete'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <Dialog
        open={socketToRekey !== null}
        onOpenChange={(open) => {
          if (!open) {
            setSocketToRekey(null)
          }
        }}
      >
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Change key</DialogTitle>
            <DialogDescription>
              {socketToRekey
                ? `Enter the key that ${socketToRekey.name} shows in its logs. DOME pairs with the socket again.`
                : null}
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4">
            <div className="grid gap-2">
              <Label htmlFor="socket-new-key">Key</Label>
              <Input
                id="socket-new-key"
                value={key}
                onChange={(event) => setKey(event.target.value)}
                autoComplete="off"
                spellCheck={false}
                placeholder="Paste the key from the socket's logs"
              />
              <Button
                type="button"
                variant="link"
                size="xs"
                className="justify-self-start"
                onClick={() => setIsKeyHelpOpen(true)}
              >
                How to get the socket key?
              </Button>
            </div>
            {keyError ? (
              <p className="text-sm text-destructive">{keyError}</p>
            ) : null}
          </div>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              disabled={isSavingKey}
              onClick={() => setSocketToRekey(null)}
            >
              Cancel
            </Button>
            <Button
              type="button"
              disabled={isSavingKey}
              onClick={() => void saveKey()}
            >
              {isSavingKey ? 'Saving…' : 'Save'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <SocketKeyHelpDialog
        open={isKeyHelpOpen}
        onOpenChange={setIsKeyHelpOpen}
      />
    </div>
  )
}
