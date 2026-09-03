import { Eye, Pencil, Play, Square, XIcon } from 'lucide-react'

import type { Container, ContainerVolume } from '@/types/containers'
import { formatBytes } from '@/lib/bytes'
import { Badge } from './ui/badge'
import { Button } from './ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from './ui/card'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from './ui/table'
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from './ui/tooltip'

type ContainerDetailPaneProps = {
  container: Container | null
  onClose: () => void
  onStart: () => void
  onStop: () => void
  isActionPending: boolean
}

function normalizedState(state: string): string {
  return state.trim().toLowerCase()
}

function canStart(state: string): boolean {
  const normalized = normalizedState(state)
  return (
    normalized !== 'running' &&
    normalized !== 'paused' &&
    normalized !== 'restarting' &&
    normalized !== 'removing'
  )
}

function canStop(state: string): boolean {
  const normalized = normalizedState(state)
  return normalized === 'running' || normalized === 'paused'
}

function stateBadgeClassName(state: string): string {
  const normalized = normalizedState(state)
  const modifier = (() => {
    switch (normalized) {
      case 'running':
        return 'running'
      case 'paused':
        return 'paused'
      case 'restarting':
        return 'restarting'
      case 'created':
        return 'created'
      case 'exited':
      case 'dead':
      case 'removing':
        return 'stopped'
      default:
        return 'unknown'
    }
  })()

  return `container-detail-state-badge container-detail-state-badge-${modifier}`
}

function volumeName(volume: ContainerVolume): string {
  return volume.name?.trim() || volume.source?.trim() || volume.destination
}

function AccessIcon({ readOnly }: Readonly<{ readOnly: boolean }>) {
  const Icon = readOnly ? Eye : Pencil
  const label = readOnly ? 'Read-Only' : 'Read-Write'

  return (
    <Tooltip>
      <TooltipTrigger asChild>
        <span className="container-volume-access-icon" aria-label={label} role="img">
          <Icon className="size-3.5" />
        </span>
      </TooltipTrigger>
      <TooltipContent side="top">{label}</TooltipContent>
    </Tooltip>
  )
}

export function ContainerDetailPane({
  container,
  onClose,
  onStart,
  onStop,
  isActionPending,
}: Readonly<ContainerDetailPaneProps>) {
  return (
    <TooltipProvider>
      <aside className="container-detail-pane">
        <Card className="container-detail-card">
          <CardHeader className="container-detail-header">
            <div className="container-detail-header-row">
              <div className="container-detail-header-copy">
                <CardDescription>Container</CardDescription>
                <CardTitle className="container-detail-title">
                  {container?.name ?? 'Select a container'}
                </CardTitle>
              </div>
              {container ? (
                <Button
                  type="button"
                  variant="ghost"
                  size="icon-sm"
                  onClick={onClose}
                  aria-label="Close container details"
                >
                  <XIcon />
                </Button>
              ) : null}
            </div>
            {container ? (
              <>
                <div className="container-detail-header-badges">
                  <Badge variant="outline" className={stateBadgeClassName(container.state)}>
                    {container.state}
                  </Badge>
                  <Badge variant="outline">{formatBytes(container.totalBytes)}</Badge>
                </div>
                <div className="container-detail-actions">
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <Button
                        type="button"
                        variant="outline"
                        size="icon-sm"
                        disabled={isActionPending || !canStart(container.state)}
                        onClick={onStart}
                        aria-label="Start container"
                      >
                        <Play />
                      </Button>
                    </TooltipTrigger>
                    <TooltipContent side="top">Start</TooltipContent>
                  </Tooltip>
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <Button
                        type="button"
                        variant="destructive"
                        size="icon-sm"
                        disabled={isActionPending || !canStop(container.state)}
                        onClick={onStop}
                        aria-label="Stop container"
                      >
                        <Square />
                      </Button>
                    </TooltipTrigger>
                    <TooltipContent side="top">Stop</TooltipContent>
                  </Tooltip>
                </div>
              </>
            ) : null}
          </CardHeader>

          <CardContent className="container-detail-content">
            {container ? (
              container.volumes.length > 0 ? (
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Volume</TableHead>
                      <TableHead>Storage</TableHead>
                      <TableHead className="w-20">Access</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {container.volumes.map((volume) => (
                      <TableRow key={`${volume.destination}-${volumeName(volume)}`}>
                        <TableCell>
                          <div className="container-volume-name">
                            <span className="container-volume-name-primary">
                              {volumeName(volume)}
                            </span>
                            <span className="container-volume-name-secondary">
                              {volume.destination}
                            </span>
                          </div>
                        </TableCell>
                        <TableCell>{formatBytes(volume.sizeBytes)}</TableCell>
                        <TableCell>
                          <AccessIcon readOnly={volume.readOnly} />
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              ) : (
                <p className="container-detail-empty">This container has no mounted volumes.</p>
              )
            ) : (
              <p className="container-detail-empty">
                Click a container node to inspect its attached volumes.
              </p>
            )}
          </CardContent>
        </Card>
      </aside>
    </TooltipProvider>
  )
}
