import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'

export type ContainerNodeData = {
  name: string
  state: string
}

export type ContainerNodeType = Node<ContainerNodeData, 'container'>

export function ContainerNode({ data }: NodeProps<ContainerNodeType>) {
  return (
    <div className="container-node">
      <Handle type="target" position={Position.Left} className="container-node-handle" />
      <div className="container-node-body">
        <span className="container-node-name">{data.name}</span>
        <span className="container-node-state">{data.state}</span>
      </div>
      <Handle type="source" position={Position.Right} className="container-node-handle" />
    </div>
  )
}
