export type SocketRecord = {
  id: string
  name: string
  address: string
  createdAt: string
}

export type GetSocketsResponse = {
  sockets: SocketRecord[]
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
