export type Container = {
  id: string
  name: string
  state: string
  stack: string | null
}

export type GetRunningContainersResponse = {
  containers: Container[]
}
