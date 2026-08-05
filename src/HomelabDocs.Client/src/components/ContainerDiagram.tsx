import { useEffect, useMemo } from 'react'
import {
  Background,
  BackgroundVariant,
  Controls,
  MiniMap,
  ReactFlow,
  useEdgesState,
  useNodesState,
  type Edge,
  type Node,
  type NodeTypes,
} from '@xyflow/react'
import '@xyflow/react/dist/style.css'

import type { Container } from '../types/containers'
import { ContainerNode, type ContainerNodeType } from './ContainerNode'
import { StackNode, type StackNodeType } from './StackNode'

const nodeTypes: NodeTypes = {
  container: ContainerNode,
  stack: StackNode,
}

const NODE_WIDTH = 220
const NODE_HEIGHT = 72
const STACK_NODE_WIDTH = 200
const GAP_X = 64
const GAP_Y = 40
const STACK_GAP_Y = 56
const ORIGIN_X = 40
const ORIGIN_Y = 40
const STANDALONE_COLUMN_COUNT = 4

type DiagramNode = ContainerNodeType | StackNodeType

function stackNodeId(stack: string): string {
  return `stack:${stack}`
}

function toGraph(containers: Container[]): {
  nodes: DiagramNode[]
  edges: Edge[]
} {
  const stacks = new Map<string, Container[]>()
  const standalone: Container[] = []

  for (const container of containers) {
    if (container.stack) {
      const members = stacks.get(container.stack) ?? []
      members.push(container)
      stacks.set(container.stack, members)
    } else {
      standalone.push(container)
    }
  }

  const nodes: DiagramNode[] = []
  const edges: Edge[] = []
  let cursorY = ORIGIN_Y

  const sortedStacks = [...stacks.entries()].sort(([left], [right]) =>
    left.localeCompare(right),
  )

  for (const [stack, members] of sortedStacks) {
    const stackId = stackNodeId(stack)
    const groupHeight = Math.max(members.length, 1) * (NODE_HEIGHT + GAP_Y) - GAP_Y

    nodes.push({
      id: stackId,
      type: 'stack',
      position: {
        x: ORIGIN_X,
        y: cursorY + Math.max((groupHeight - NODE_HEIGHT) / 2, 0),
      },
      data: {
        name: stack,
      },
    })

    members.forEach((container, index) => {
      nodes.push({
        id: container.id,
        type: 'container',
        position: {
          x: ORIGIN_X + STACK_NODE_WIDTH + GAP_X,
          y: cursorY + index * (NODE_HEIGHT + GAP_Y),
        },
        data: {
          name: container.name,
          state: container.state,
        },
      })

      edges.push({
        id: `${stackId}->${container.id}`,
        source: stackId,
        target: container.id,
        type: 'smoothstep',
      })
    })

    cursorY += groupHeight + STACK_GAP_Y
  }

  if (standalone.length > 0) {
    const standaloneOriginX =
      stacks.size > 0
        ? ORIGIN_X + STACK_NODE_WIDTH + GAP_X + NODE_WIDTH + GAP_X
        : ORIGIN_X

    standalone.forEach((container, index) => {
      const column = index % STANDALONE_COLUMN_COUNT
      const row = Math.floor(index / STANDALONE_COLUMN_COUNT)

      nodes.push({
        id: container.id,
        type: 'container',
        position: {
          x: standaloneOriginX + column * (NODE_WIDTH + GAP_X),
          y: ORIGIN_Y + row * (NODE_HEIGHT + GAP_Y),
        },
        data: {
          name: container.name,
          state: container.state,
        },
      })
    })
  }

  return { nodes, edges }
}

type ContainerDiagramProps = {
  containers: Container[]
}

export function ContainerDiagram({ containers }: ContainerDiagramProps) {
  const graph = useMemo(() => toGraph(containers), [containers])
  const [nodes, setNodes, onNodesChange] = useNodesState<Node>([])
  const [edges, setEdges, onEdgesChange] = useEdgesState<Edge>([])

  useEffect(() => {
    setNodes(graph.nodes)
    setEdges(graph.edges)
  }, [graph, setNodes, setEdges])

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
        nodeColor={(node) =>
          node.type === 'stack' ? '#243044' : '#1c2330'
        }
        maskColor="rgba(11, 15, 20, 0.7)"
      />
    </ReactFlow>
  )
}
