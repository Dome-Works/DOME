import type { GetRunningContainersResponse } from '../types/containers'

export async function fetchRunningContainers(
  signal?: AbortSignal,
): Promise<GetRunningContainersResponse> {
  const response = await fetch('/api/containers', { signal })

  if (!response.ok) {
    throw new Error(`Failed to load containers (${response.status})`)
  }

  return (await response.json()) as GetRunningContainersResponse
}
