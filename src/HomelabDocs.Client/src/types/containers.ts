export type Container = {
  id: string
  name: string
  state: string
}

export type GetRunningContainersResponse = {
  containers: Container[]
}
