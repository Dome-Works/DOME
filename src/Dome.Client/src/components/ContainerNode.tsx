import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'
import { Badge } from './ui/badge'
import { formatBytes } from '../lib/bytes'

export type ContainerNodeData = {
  name: string
  state: string
  totalBytes: number
}

export type ContainerNodeType = Node<ContainerNodeData, 'container'>

function stateClassName(state: string): string {
  const normalized = state.trim().toLowerCase()

  switch (normalized) {
    case 'running':
      return 'container-node-state container-node-state-running'
    case 'paused':
      return 'container-node-state container-node-state-paused'
    case 'restarting':
      return 'container-node-state container-node-state-restarting'
    case 'created':
      return 'container-node-state container-node-state-created'
    case 'exited':
    case 'dead':
    case 'removing':
      return 'container-node-state container-node-state-stopped'
    default:
      return 'container-node-state'
  }
}

export function ContainerNode({ data, selected }: NodeProps<ContainerNodeType>) {
  const totalBytesLabel = formatBytes(data.totalBytes)

  return (
    <div className={`container-node${selected ? ' container-node-selected' : ''}`}>
      <Handle type="target" position={Position.Top} className="container-node-handle" />
      <div className="container-node-body">
        <div className="container-node-header">
          <span className="container-node-name">{data.name}</span>
          <span className={stateClassName(data.state)}>{data.state}</span>
        </div>
        <Badge variant="outline" className="container-node-storage-badge">
          {totalBytesLabel}
        </Badge>
      </div>
    </div>
  )
}
