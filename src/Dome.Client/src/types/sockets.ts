export type SocketRecord = {
  id: string
  name: string
  address: string
  createdAt: string
}

export type SocketStatusRecord = {
  id: string
  isReachable: boolean
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
}

export type UpdateSocketRequest = {
  id: string
  name: string
  address: string
}
