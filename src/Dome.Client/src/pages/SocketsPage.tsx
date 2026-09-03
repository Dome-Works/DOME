import { useCallback, useEffect, useState } from 'react'
import { MoreHorizontal } from 'lucide-react'
import { toast } from 'sonner'

import {
  createSocket,
  deleteSocket,
  fetchSockets,
  fetchSocketStatuses,
  updateSocket,
} from '@/api/sockets'
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
import type { SocketRecord } from '@/types/sockets'

type SocketsState =
  | { status: 'loading' }
  | { status: 'ready'; sockets: SocketRecord[] }
  | { status: 'error'; message: string }

type EditorState =
  | { mode: 'closed' }
  | { mode: 'create' }
  | { mode: 'edit'; socket: SocketRecord }

type SocketStatuses = Record<string, boolean>

export function SocketsPage() {
  const [state, setState] = useState<SocketsState>({ status: 'loading' })
  const [editor, setEditor] = useState<EditorState>({ mode: 'closed' })
  const [socketToDelete, setSocketToDelete] = useState<SocketRecord | null>(
    null,
  )
  const [socketStatuses, setSocketStatuses] = useState<SocketStatuses>({})
  const [name, setName] = useState('')
  const [address, setAddress] = useState('')
  const [formError, setFormError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)
  const [isDeleting, setIsDeleting] = useState(false)

  const loadSockets = useCallback(async (signal?: AbortSignal) => {
    setState({ status: 'loading' })

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

  const loadSocketStatuses = useCallback(
    async (sockets: SocketRecord[], signal?: AbortSignal) => {
      if (sockets.length === 0) {
        setSocketStatuses({})
        return
      }

      try {
        const response = await fetchSocketStatuses(signal)
        if (signal?.aborted) {
          return
        }

        setSocketStatuses(
          Object.fromEntries(
            response.statuses.map((status) => [status.id, status.isReachable]),
          ),
        )
      } catch {
        if (signal?.aborted) {
          return
        }

        setSocketStatuses(
          Object.fromEntries(sockets.map((socket) => [socket.id, false])),
        )
      }
    },
    [],
  )

  useEffect(() => {
    const controller = new AbortController()
    void loadSockets(controller.signal)
    return () => controller.abort()
  }, [loadSockets])

  const readySockets = state.status === 'ready' ? state.sockets : undefined

  useEffect(() => {
    if (!readySockets) {
      setSocketStatuses({})
      return
    }

    const controller = new AbortController()
    void loadSocketStatuses(readySockets, controller.signal)
    return () => controller.abort()
  }, [loadSocketStatuses, readySockets])

  const openCreate = () => {
    setName('')
    setAddress('')
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

    if (!trimmedName || !trimmedAddress) {
      setFormError('Name and address are required.')
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

  const sockets = state.status === 'ready' ? state.sockets : []

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

      {state.status === 'loading' ? (
        <p className="text-sm text-muted-foreground">Loading sockets…</p>
      ) : null}

      {state.status === 'error' ? (
        <div className="flex items-center gap-3 text-sm text-destructive">
          <span>{state.message}</span>
          <Button variant="outline" size="sm" onClick={() => void loadSockets()}>
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
                <TableHead>Created</TableHead>
                <TableHead className="w-12">
                  <span className="sr-only">Actions</span>
                </TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {sockets.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={5} className="h-24 text-center">
                    No sockets are registered yet.
                  </TableCell>
                </TableRow>
              ) : (
                sockets.map((socket) => (
                  <TableRow key={socket.id}>
                    <TableCell>
                      <span
                        aria-label={
                          socketStatuses[socket.id] === undefined
                            ? 'Checking status'
                            : socketStatuses[socket.id]
                              ? 'Socket reachable'
                              : 'Socket unreachable'
                        }
                        className={cn(
                          'inline-flex h-2.5 w-2.5 rounded-full',
                          socketStatuses[socket.id] === undefined
                            ? 'bg-muted-foreground/40'
                            : socketStatuses[socket.id]
                              ? 'bg-emerald-500'
                              : 'bg-destructive',
                        )}
                      />
                    </TableCell>
                    <TableCell className="font-medium">{socket.name}</TableCell>
                    <TableCell>{socket.address}</TableCell>
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
              absolute http or https URL.
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
                placeholder="http://127.0.0.1:5110"
              />
            </div>
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
    </div>
  )
}
