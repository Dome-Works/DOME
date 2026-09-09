import { lazy, Suspense, useEffect, useState } from 'react'

import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

const ComposeYamlEditor = lazy(async () => {
  const module = await import('@/components/ComposeYamlEditor')
  return { default: module.ComposeYamlEditor }
})

type ComposeEditorDialogProps = {
  open: boolean
  projectName: string
  composeYaml: string
  isSaving: boolean
  onOpenChange: (open: boolean) => void
  onSave: (composeYaml: string) => Promise<void>
}

export function ComposeEditorDialog({
  open,
  projectName,
  composeYaml,
  isSaving,
  onOpenChange,
  onSave,
}: ComposeEditorDialogProps) {
  const [draft, setDraft] = useState(composeYaml)

  useEffect(() => {
    if (open) {
      setDraft(composeYaml)
    }
  }, [open, composeYaml])

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="flex max-h-[90vh] max-w-5xl flex-col sm:max-w-5xl">
        <DialogHeader>
          <DialogTitle>Edit compose file</DialogTitle>
          <DialogDescription>
            {projectName} — docker-compose.yml stored on the server.
          </DialogDescription>
        </DialogHeader>
        <div className="min-h-0 overflow-hidden rounded-md border">
          {open ? (
            <Suspense
              fallback={
                <p className="text-muted-foreground p-4 text-sm">
                  Loading editor…
                </p>
              }
            >
              <ComposeYamlEditor value={draft} onChange={setDraft} />
            </Suspense>
          ) : null}
        </div>
        <DialogFooter>
          <Button
            type="button"
            variant="outline"
            onClick={() => onOpenChange(false)}
          >
            Cancel
          </Button>
          <Button
            type="button"
            disabled={isSaving}
            onClick={() => void onSave(draft)}
          >
            {isSaving ? 'Saving…' : 'Save'}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
