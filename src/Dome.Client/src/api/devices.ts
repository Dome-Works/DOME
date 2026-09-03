import type { GetDevicesResponse } from '../types/devices'

export async function fetchDevices(
  signal?: AbortSignal,
): Promise<GetDevicesResponse> {
  const response = await fetch('/api/devices', { signal })

  if (!response.ok) {
    throw new Error(`Failed to load devices (${response.status})`)
  }

  return (await response.json()) as GetDevicesResponse
}
