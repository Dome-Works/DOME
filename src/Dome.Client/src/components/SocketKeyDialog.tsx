import { toast } from 'sonner'

import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Label } from '@/components/ui/label'
import type { SocketCreated } from '@/types/sockets'

type SocketKeyDialogProps = {
  socket: SocketCreated | null
  onClose: () => void
}

async function copyText(text: string, label: string) {
  try {
    await navigator.clipboard.writeText(text)
    toast.success(`${label} copied.`)
  } catch {
    toast.error(`Unable to copy ${label.toLowerCase()}. Copy it manually.`)
  }
}

export function SocketKeyDialog({ socket, onClose }: SocketKeyDialogProps) {
  const snippet = socket
    ? `    environment:\n      DOME_SOCKET_KEY: ${socket.apiKey}`
    : ''

  return (
    <Dialog
      open={socket !== null}
      onOpenChange={(open) => {
        if (!open) {
          onClose()
        }
      }}
    >
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Key for {socket?.name}</DialogTitle>
          <DialogDescription>
            Copy this key now — it won't be shown again.
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-4">
          <div className="grid gap-2">
            <Label id="socket-key-label">Socket key</Label>
            <div className="flex gap-2">
              <code
                aria-labelledby="socket-key-label"
                className="flex-1 select-all break-all rounded-md border bg-muted px-3 py-2 font-mono text-sm"
              >
                {socket?.apiKey}
              </code>
              <Button
                type="button"
                variant="outline"
                onClick={() => socket && void copyText(socket.apiKey, 'Key')}
              >
                Copy
              </Button>
            </div>
          </div>
          <div className="grid gap-2">
            <div className="flex items-center justify-between">
              <Label>Add to the socket's service in its compose file</Label>
              <Button
                type="button"
                variant="ghost"
                size="sm"
                onClick={() => void copyText(snippet, 'Snippet')}
              >
                Copy
              </Button>
            </div>
            <pre className="overflow-x-auto rounded-md border bg-muted p-3 font-mono text-xs">
              {snippet}
            </pre>
          </div>
          <p className="text-sm text-muted-foreground">
            Restart the socket afterwards. DOME pairs automatically once the
            socket is reachable.
          </p>
          <p className="text-sm text-muted-foreground">
            The socket must run the latest image to accept a key.
          </p>
        </div>
        <DialogFooter>
          <Button type="button" onClick={onClose}>
            Done
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
