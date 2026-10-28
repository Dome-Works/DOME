import { useState } from 'react'

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

type DeleteContainerDialogProps = {
  open: boolean
  containerName: string
  isPending: boolean
  onOpenChange: (open: boolean) => void
  onConfirm: (removeVolumes: boolean) => void
}

export function DeleteContainerDialog({
  open,
  containerName,
  isPending,
  onOpenChange,
  onConfirm,
}: DeleteContainerDialogProps) {
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
          <DialogTitle>Delete container?</DialogTitle>
          <DialogDescription>
            This permanently deletes {containerName}. Running containers are
            force-stopped. This action cannot be undone.
          </DialogDescription>
        </DialogHeader>
        <div className="flex items-center gap-2">
          <Checkbox
            id="delete-container-volumes"
            checked={removeVolumes}
            onCheckedChange={(checked) => setRemoveVolumes(checked === true)}
          />
          <Label htmlFor="delete-container-volumes">
            Also delete anonymous volumes
          </Label>
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
