import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'

import type { DiagramStackKind } from '../types/diagram'
import { Badge } from './ui/badge'
import { formatBytes } from '../lib/bytes'

export type StackNodeData = {
  name: string
  kind: DiagramStackKind
  totalBytes?: number
}

export type StackNodeType = Node<StackNodeData, 'stack'>

export function StackNode({ data, selected }: NodeProps<StackNodeType>) {
  const kindLabel = data.kind === 'readonly' ? 'Read-only' : 'Stack'
  const totalBytesLabel = formatBytes(data.totalBytes ?? 0)
  const className = [
    'stack-node',
    data.kind === 'readonly' ? 'stack-node-readonly' : '',
    selected ? 'stack-node-selected' : '',
  ]
    .filter(Boolean)
    .join(' ')

  return (
    <div className={className}>
      <div className="stack-node-body">
        <span className="stack-node-kind">{kindLabel}</span>
        <span className="stack-node-name">{data.name}</span>
        <Badge variant="outline" className="container-node-storage-badge">
          {totalBytesLabel}
        </Badge>
      </div>
      <Handle type="source" position={Position.Bottom} className="container-node-handle" />
    </div>
  )
}
