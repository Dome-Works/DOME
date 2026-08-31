import type { GetDeviceContainersResponse } from '../types/containers'

export async function fetchDeviceContainers(
  deviceName: string,
  signal?: AbortSignal,
): Promise<GetDeviceContainersResponse> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/containers`,
    { signal },
  )

  if (!response.ok) {
    throw new Error(`Failed to load containers (${response.status})`)
  }

  return (await response.json()) as GetDeviceContainersResponse
}
