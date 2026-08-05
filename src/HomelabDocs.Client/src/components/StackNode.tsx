import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'

export type StackNodeData = {
  name: string
}

export type StackNodeType = Node<StackNodeData, 'stack'>

export function StackNode({ data }: NodeProps<StackNodeType>) {
  return (
    <div className="stack-node">
      <div className="stack-node-body">
        <span className="stack-node-kind">Stack</span>
        <span className="stack-node-name">{data.name}</span>
      </div>
      <Handle type="source" position={Position.Right} className="container-node-handle" />
    </div>
  )
}
