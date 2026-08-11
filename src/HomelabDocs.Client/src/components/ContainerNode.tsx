import { Handle, Position, type Node, type NodeProps } from '@xyflow/react'

export type ContainerNodeData = {
  name: string
  state: string
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

export function ContainerNode({ data }: NodeProps<ContainerNodeType>) {
  return (
    <div className="container-node">
      <Handle type="target" position={Position.Top} className="container-node-handle" />
      <div className="container-node-body">
        <span className="container-node-name">{data.name}</span>
        <span className={stateClassName(data.state)}>{data.state}</span>
      </div>
    </div>
  )
}
