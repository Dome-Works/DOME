import type {
  CreateSocketRequest,
  GetSocketsResponse,
  SocketRecord,
  UpdateSocketRequest,
} from '../types/sockets'
import { readApiError } from './errors'

export async function fetchSockets(
  signal?: AbortSignal,
): Promise<GetSocketsResponse> {
  const response = await fetch('/api/sockets', { signal })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to load sockets (${response.status})`,
      ),
    )
  }

  return (await response.json()) as GetSocketsResponse
}

export async function createSocket(
  request: CreateSocketRequest,
): Promise<SocketRecord> {
  const response = await fetch('/api/sockets', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(request),
  })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to register socket (${response.status})`,
      ),
    )
  }

  return (await response.json()) as SocketRecord
}

export async function updateSocket(
  request: UpdateSocketRequest,
): Promise<SocketRecord> {
  const response = await fetch(
    `/api/sockets/${encodeURIComponent(request.id)}`,
    {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to update socket (${response.status})`,
      ),
    )
  }

  return (await response.json()) as SocketRecord
}

export async function deleteSocket(id: string): Promise<void> {
  const response = await fetch(`/api/sockets/${encodeURIComponent(id)}`, {
    method: 'DELETE',
  })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to delete socket (${response.status})`,
      ),
    )
  }
}
