import { useId, useState, type ReactNode } from 'react'

import { Button } from '@/components/ui/button'
import { Checkbox } from '@/components/ui/checkbox'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Label } from '@/components/ui/label'

type ConfirmDeleteDialogProps = {
  open: boolean
  title: string
  description: ReactNode
  volumesLabel: string
  isPending: boolean
  onOpenChange: (open: boolean) => void
  onConfirm: (removeVolumes: boolean) => void
}

export function ConfirmDeleteDialog({
  open,
  title,
  description,
  volumesLabel,
  isPending,
  onOpenChange,
  onConfirm,
}: ConfirmDeleteDialogProps) {
  const volumesId = useId()
  const [removeVolumes, setRemoveVolumes] = useState(false)
  const [wasOpen, setWasOpen] = useState(open)

  if (open !== wasOpen) {
    setWasOpen(open)
    if (open) {
      setRemoveVolumes(false)
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
          <DialogDescription>{description}</DialogDescription>
        </DialogHeader>
        <div className="flex items-center gap-2">
          <Checkbox
            id={volumesId}
            checked={removeVolumes}
            onCheckedChange={(checked) => setRemoveVolumes(checked === true)}
          />
          <Label htmlFor={volumesId}>{volumesLabel}</Label>
        </div>
        <DialogFooter>
          <Button
            type="button"
            variant="outline"
            disabled={isPending}
            onClick={() => onOpenChange(false)}
          >
            Cancel
          </Button>
          <Button
            type="button"
            variant="destructive"
            disabled={isPending}
            onClick={() => onConfirm(removeVolumes)}
          >
            Delete
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
