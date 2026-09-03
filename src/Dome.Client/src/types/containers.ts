export type Container = {
  id: string
  name: string
  state: string
  stack: string | null
  totalBytes: number
  volumes: ContainerVolume[]
}

export type ContainerVolume = {
  name: string | null
  source: string | null
  destination: string
  type: string | null
  readOnly: boolean
  sizeBytes: number | null
}

export type GetDeviceContainersResponse = {
  containers: Container[]
}
