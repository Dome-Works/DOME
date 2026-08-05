import { useEffect, useMemo } from 'react'
import {
  Background,
  BackgroundVariant,
  Controls,
  MiniMap,
  ReactFlow,
  useEdgesState,
  useNodesState,
  type NodeTypes,
} from '@xyflow/react'
import '@xyflow/react/dist/style.css'

import type { Container } from '../types/containers'
import { ContainerNode, type ContainerNodeType } from './ContainerNode'

const nodeTypes: NodeTypes = {
  container: ContainerNode,
}

const COLUMN_COUNT = 4
const NODE_WIDTH = 220
const NODE_HEIGHT = 72
const GAP_X = 48
const GAP_Y = 40
const ORIGIN_X = 40
const ORIGIN_Y = 40

function toNodes(containers: Container[]): ContainerNodeType[] {
  return containers.map((container, index) => {
    const column = index % COLUMN_COUNT
    const row = Math.floor(index / COLUMN_COUNT)

    return {
      id: container.id,
      type: 'container',
      position: {
        x: ORIGIN_X + column * (NODE_WIDTH + GAP_X),
        y: ORIGIN_Y + row * (NODE_HEIGHT + GAP_Y),
      },
      data: {
        name: container.name,
        state: container.state,
      },
    }
  })
}

type ContainerDiagramProps = {
  containers: Container[]
}

export function ContainerDiagram({ containers }: ContainerDiagramProps) {
  const initialNodes = useMemo(() => toNodes(containers), [containers])
  const [nodes, setNodes, onNodesChange] = useNodesState<ContainerNodeType>([])
  const [edges, , onEdgesChange] = useEdgesState([])

  useEffect(() => {
    setNodes(initialNodes)
  }, [initialNodes, setNodes])

  return (
    <ReactFlow
      nodes={nodes}
      edges={edges}
      onNodesChange={onNodesChange}
      onEdgesChange={onEdgesChange}
      nodeTypes={nodeTypes}
      fitView
      fitViewOptions={{ padding: 0.2 }}
      minZoom={0.25}
      maxZoom={1.75}
      proOptions={{ hideAttribution: true }}
      nodesConnectable={false}
      elementsSelectable
    >
      <Background
        variant={BackgroundVariant.Dots}
        gap={20}
        size={1}
        color="#2a3340"
      />
      <Controls showInteractive={false} />
      <MiniMap
        pannable
        zoomable
        nodeColor="#1c2330"
        maskColor="rgba(11, 15, 20, 0.7)"
      />
    </ReactFlow>
  )
}
