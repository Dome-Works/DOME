import { useEffect, useState } from 'react'

import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { validateStackProjectName } from '@/lib/stackProjectName'

type CreateStackDialogProps = {
  open: boolean
  isSaving: boolean
  onOpenChange: (open: boolean) => void
  onCreate: (projectName: string) => Promise<void>
}

export function CreateStackDialog({
  open,
  isSaving,
  onOpenChange,
  onCreate,
}: CreateStackDialogProps) {
  const [projectName, setProjectName] = useState('')
  const [formError, setFormError] = useState<string | null>(null)

  useEffect(() => {
    if (open) {
      setProjectName('')
      setFormError(null)
    }
  }, [open])

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Create stack</DialogTitle>
          <DialogDescription>
            The project name is the canvas label and the Compose project name.
          </DialogDescription>
        </DialogHeader>
        <form
          className="grid gap-4"
          onSubmit={(event) => {
            event.preventDefault()
            const error = validateStackProjectName(projectName)
            if (error) {
              setFormError(error)
              return
            }

            setFormError(null)
            void onCreate(projectName.trim())
          }}
        >
          <div className="grid gap-2">
            <Label htmlFor="stack-project-name">Project name</Label>
            <Input
              id="stack-project-name"
              value={projectName}
              onChange={(event) => setProjectName(event.target.value)}
              placeholder="my-stack"
              autoFocus
              autoComplete="off"
            />
          </div>
          {formError ? (
            <p className="text-sm text-destructive">{formError}</p>
          ) : null}
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={isSaving}>
              {isSaving ? 'Creating…' : 'Create'}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
