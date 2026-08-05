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
const STACK_NODE_WIDTH = 200
const STACK_NODE_HEIGHT = 72
const GAP_X = 48
const GAP_Y = 40
const STACK_GAP_X = 80
const ORIGIN_X = 40
const ORIGIN_Y = 40

type DiagramNode = ContainerNodeType | StackNodeType

function stackNodeId(stack: string): string {
  return `stack:${stack}`
}

function groupWidth(memberCount: number): number {
  if (memberCount <= 0) {
    return STACK_NODE_WIDTH
  }

  const membersWidth = memberCount * NODE_WIDTH + (memberCount - 1) * GAP_X
  return Math.max(STACK_NODE_WIDTH, membersWidth)
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
  let cursorX = ORIGIN_X

  const sortedStacks = [...stacks.entries()].sort(([left], [right]) =>
    left.localeCompare(right),
  )

  for (const [stack, members] of sortedStacks) {
    const stackId = stackNodeId(stack)
    const width = groupWidth(members.length)
    const stackX = cursorX + (width - STACK_NODE_WIDTH) / 2
    const membersWidth =
      members.length > 0
        ? members.length * NODE_WIDTH + (members.length - 1) * GAP_X
        : 0
    const membersOriginX = cursorX + (width - membersWidth) / 2

    nodes.push({
      id: stackId,
      type: 'stack',
      position: {
        x: stackX,
        y: ORIGIN_Y,
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
          x: membersOriginX + index * (NODE_WIDTH + GAP_X),
          y: ORIGIN_Y + STACK_NODE_HEIGHT + GAP_Y,
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

    cursorX += width + STACK_GAP_X
  }

  standalone.forEach((container, index) => {
    nodes.push({
      id: container.id,
      type: 'container',
      position: {
        x: cursorX + index * (NODE_WIDTH + STACK_GAP_X),
        y: ORIGIN_Y,
      },
      data: {
        name: container.name,
        state: container.state,
      },
    })
  })

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
