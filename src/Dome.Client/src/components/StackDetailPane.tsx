import { useEffect, useState } from 'react'
import { Pencil, Rocket, XIcon } from 'lucide-react'
import { toast } from 'sonner'

import { fetchDeviceStack, updateDeviceStack } from '@/api/stacks'
import { ComposeEditorDialog } from '@/components/ComposeEditorDialog'
import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip'
import type { DiagramStack } from '@/types/diagram'
import type { DeviceStack } from '@/types/stacks'

type StackDetailPaneProps = {
  deviceName: string
  stack: DiagramStack
  onClose: () => void
  isActionPending: boolean
  onDeploy: () => void
}

type StackLoadState =
  | { status: 'idle' }
  | { status: 'loading' }
  | { status: 'ready'; stack: DeviceStack }
  | { status: 'error'; message: string }

export function StackDetailPane({
  deviceName,
  stack,
  onClose,
  isActionPending,
  onDeploy,
}: StackDetailPaneProps) {
  const [loadState, setLoadState] = useState<StackLoadState>({ status: 'idle' })
  const [isEditorOpen, setIsEditorOpen] = useState(false)
  const [isSaving, setIsSaving] = useState(false)
  const isManaged = stack.kind === 'managed' && stack.id !== null

  useEffect(() => {
    if (!isManaged || stack.id === null) {
      setLoadState({ status: 'idle' })
      return
    }

    const controller = new AbortController()
    setLoadState({ status: 'loading' })

    void fetchDeviceStack(deviceName, stack.id, controller.signal)
      .then((loaded) => {
        if (!controller.signal.aborted) {
          setLoadState({ status: 'ready', stack: loaded })
        }
      })
      .catch((error: unknown) => {
        if (controller.signal.aborted) {
          return
        }

        setLoadState({
          status: 'error',
          message:
            error instanceof Error
              ? error.message
              : 'Unable to load this stack.',
        })
      })

    return () => controller.abort()
  }, [deviceName, isManaged, stack.id])

  async function saveCompose(composeYaml: string) {
    if (loadState.status !== 'ready') {
      return
    }

    setIsSaving(true)
    try {
      const saved = await updateDeviceStack(deviceName, {
        id: loadState.stack.id,
        projectName: loadState.stack.projectName,
        composeYaml,
      })
      setLoadState({ status: 'ready', stack: saved })
      setIsEditorOpen(false)
      toast.success(`Saved ${saved.projectName}.`)
    } catch (error) {
      toast.error(
        error instanceof Error ? error.message : 'Unable to save the compose file.',
      )
    } finally {
      setIsSaving(false)
    }
  }

  const canEdit = isManaged && loadState.status === 'ready'
  const canDeploy = isManaged && loadState.status === 'ready'

  return (
    <TooltipProvider>
      <aside className="container-detail-pane">
        <Card className="container-detail-card">
          <CardHeader className="container-detail-header">
            <div className="container-detail-header-row">
              <div className="container-detail-header-copy">
                <CardDescription>Stack</CardDescription>
                <CardTitle className="container-detail-title">
                  {stack.projectName}
                </CardTitle>
              </div>
              <Button
                type="button"
                variant="ghost"
                size="icon-sm"
                onClick={onClose}
                aria-label="Close stack details"
              >
                <XIcon />
              </Button>
            </div>
            <div className="container-detail-header-badges">
              <Badge variant="outline">
                {stack.kind === 'readonly' ? 'Read-only' : 'Managed'}
              </Badge>
            </div>
            {isManaged ? (
              <div className="container-detail-actions">
                <Tooltip>
                  <TooltipTrigger asChild>
                    <Button
                      type="button"
                      variant="outline"
                      size="icon-sm"
                      disabled={isActionPending || isSaving || !canEdit}
                      onClick={() => setIsEditorOpen(true)}
                      aria-label="Edit compose file"
                    >
                      <Pencil />
                    </Button>
                  </TooltipTrigger>
                  <TooltipContent side="top">Edit</TooltipContent>
                </Tooltip>
                <Tooltip>
                  <TooltipTrigger asChild>
                    <Button
                      type="button"
                      variant="outline"
                      size="icon-sm"
                      disabled={isActionPending || isSaving || !canDeploy}
                      onClick={onDeploy}
                      aria-label="Deploy stack"
                    >
                      <Rocket />
                    </Button>
                  </TooltipTrigger>
                  <TooltipContent side="top">Deploy</TooltipContent>
                </Tooltip>
              </div>
            ) : null}
          </CardHeader>
          <CardContent className="container-detail-content">
            {stack.kind === 'readonly' ? (
              <p className="container-detail-empty">
                This stack was discovered from Docker and is not managed by
                DOME. Edit and deploy are unavailable.
              </p>
            ) : null}
            {isManaged && loadState.status === 'loading' ? (
              <p className="container-detail-empty">Loading compose file…</p>
            ) : null}
            {isManaged && loadState.status === 'error' ? (
              <p className="container-detail-empty">{loadState.message}</p>
            ) : null}
            {loadState.status === 'ready' ? (
              <p className="container-detail-empty">
                {loadState.stack.composeYaml.trim() === 'services: {}'
                  ? 'This stack has an empty compose file. Edit it, then deploy.'
                  : 'Edit the compose file, then deploy to run docker compose up on this device.'}
              </p>
            ) : null}
          </CardContent>
        </Card>
      </aside>
      {loadState.status === 'ready' ? (
        <ComposeEditorDialog
          open={isEditorOpen}
          projectName={loadState.stack.projectName}
          composeYaml={loadState.stack.composeYaml}
          isSaving={isSaving}
          onOpenChange={setIsEditorOpen}
          onSave={saveCompose}
        />
      ) : null}
    </TooltipProvider>
  )
}
