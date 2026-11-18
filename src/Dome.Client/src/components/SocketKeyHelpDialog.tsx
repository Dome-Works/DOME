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

const logsCommand = 'docker compose logs socket'
const keyFileCommand =
  'docker compose exec socket cat /var/lib/dome-socket/socket.key'
const exampleBanner = `################################################################
#  DOME SOCKET KEY
#
#  Paste this key into DOME when you register this socket:
#
#      q3Zx…the key…8Fk
#
#  This message repeats at every start until DOME has paired.
################################################################`

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
      <DialogContent className="sm:max-w-xl">
        <DialogHeader>
          <DialogTitle>How to get the socket key</DialogTitle>
          <DialogDescription>
            Each socket creates its own key on first start and prints it in its
            logs at every start until DOME has paired with it.
          </DialogDescription>
        </DialogHeader>
        <ol className="grid list-decimal gap-4 pl-5 text-sm marker:text-muted-foreground">
          <li className="grid gap-2">
            <span>
              Open a terminal on the host where the socket runs, in the folder
              with its compose file.
            </span>
          </li>
          <li className="grid gap-2">
            <span>Show the socket's logs:</span>
            <Command command={logsCommand} />
            <span className="text-muted-foreground">
              Without Compose, use <code>docker logs &lt;container&gt;</code>.
              Running from Rider? Check the Dome.Socket.Api run console.
            </span>
          </li>
          <li className="grid gap-2">
            <span>
              Find the <strong>DOME SOCKET KEY</strong> block and copy the key
              in it:
            </span>
            <pre className="overflow-x-auto rounded-md border bg-muted p-3 font-mono text-xs">
              {exampleBanner}
            </pre>
          </li>
          <li>
            Paste the key into the Key field and save. DOME pairs with the
            socket automatically.
          </li>
        </ol>
        <div className="grid gap-2 rounded-md border p-3 text-sm">
          <p className="font-medium">No key in the logs?</p>
          <p className="text-muted-foreground">
            A socket that has paired before stops printing its key. Read it
            from the socket's data volume instead. If you set{' '}
            <code>DOME_SOCKET_KEY</code> on the socket yourself, use that value.
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
