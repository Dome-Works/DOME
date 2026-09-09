import type { Container } from './containers'

export type DiagramStackKind = 'managed' | 'readonly'

export type DiagramStack = {
  id: string | null
  projectName: string
  kind: DiagramStackKind
}

export type DeviceDiagram = {
  stacks: DiagramStack[]
  containers: Container[]
}
