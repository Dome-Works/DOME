import { Eye, Pencil, XIcon } from 'lucide-react'

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
}

function volumeName(volume: ContainerVolume): string {
  return volume.name?.trim() || volume.source?.trim() || volume.destination
}

function AccessIcon({ readOnly }: { readOnly: boolean }) {
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
}: ContainerDetailPaneProps) {
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
              <div className="container-detail-header-badges">
                <Badge variant="outline">{formatBytes(container.totalBytes)}</Badge>
                <Badge variant="secondary">{container.state}</Badge>
              </div>
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
