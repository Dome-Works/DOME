export type ImageVersionKind =
  | 'none'
  | 'unknown'
  | 'digest'
  | 'patch'
  | 'minor'
  | 'major'

export type ContainerImageVersionCheck = {
  containerId: string
  image: string
  currentTag: string | null
  newestTag: string | null
  kind: ImageVersionKind
  checkedAt: string | null
}

export type GetDeviceImageVersionChecksResponse = {
  checks: ContainerImageVersionCheck[]
}
