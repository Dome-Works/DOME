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

type SocketKeyHelpDialogProps = {
  open: boolean
  onOpenChange: (open: boolean) => void
}

const logsCommand = 'docker logs dome-socket'
const keyFileCommand =
  'docker exec dome-socket cat /var/lib/dome-socket/socket.key'
async function copyText(text: string) {
  try {
    await navigator.clipboard.writeText(text)
    toast.success('Command copied.')
  } catch {
    toast.error('Unable to copy the command. Copy it manually.')
  }
}

function Command({ command }: { command: string }) {
  return (
    <div className="flex items-center gap-2">
      <code className="flex-1 select-all break-all rounded-md border bg-muted px-3 py-2 font-mono text-xs">
        {command}
      </code>
      <Button
        type="button"
        variant="outline"
        size="sm"
        onClick={() => void copyText(command)}
      >
        Copy
      </Button>
    </div>
  )
}

export function SocketKeyHelpDialog({
  open,
  onOpenChange,
}: SocketKeyHelpDialogProps) {
  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>How to get the socket key</DialogTitle>
          <DialogDescription>
            The key is in the socket container's logs, under DOME SOCKET KEY.
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-3 text-sm">
          <Command command={logsCommand} />
          <p className="text-muted-foreground">
            Key not in the logs? Restart the socket container and check again.
            If it still isn't there, read it directly:
          </p>
          <Command command={keyFileCommand} />
        </div>
        <DialogFooter>
          <Button type="button" onClick={() => onOpenChange(false)}>
            Got it
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
