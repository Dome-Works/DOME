export type SocketRecord = {
  id: string
  name: string
  address: string
  createdAt: string
}

export type SocketState =
  | 'Online'
  | 'Offline'
  | 'NeedsPairing'
  | 'Unauthorized'
  | 'CertificateChanged'

export type SocketStatusRecord = {
  id: string
  isReachable: boolean
  state: SocketState
  version: string | null
  needsUpdate: boolean
}

export type GetSocketsResponse = {
  sockets: SocketRecord[]
}

export type GetSocketStatusesResponse = {
  statuses: SocketStatusRecord[]
}

export type CreateSocketRequest = {
  name: string
  address: string
  key: string
}

export type SetSocketKeyRequest = {
  id: string
  key: string
}

export type UpdateSocketRequest = {
  id: string
  name: string
  address: string
}
