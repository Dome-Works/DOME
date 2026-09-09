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

export function stackCanvasId(stack: DiagramStack): string {
  return stack.id == null
    ? `stack:readonly:${stack.projectName}`
    : `stack:${stack.id}`
}
