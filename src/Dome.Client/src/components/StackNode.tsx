import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'

import type { DiagramStackKind } from '../types/diagram'

export type StackNodeData = {
  name: string
  kind: DiagramStackKind
}

export type StackNodeType = Node<StackNodeData, 'stack'>

export function StackNode({ data, selected }: NodeProps<StackNodeType>) {
  const kindLabel = data.kind === 'readonly' ? 'Read-only' : 'Stack'
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
      </div>
      <Handle type="source" position={Position.Bottom} className="container-node-handle" />
    </div>
  )
}
