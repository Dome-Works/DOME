import type { GetDeviceImageVersionChecksResponse } from '../types/imageVersionChecks'

export async function fetchDeviceImageVersionChecks(
  deviceName: string,
  signal?: AbortSignal,
): Promise<GetDeviceImageVersionChecksResponse> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/image-version-checks`,
    { signal },
  )

  if (!response.ok) {
    throw new Error(`Failed to load image version checks (${response.status})`)
  }

  return (await response.json()) as GetDeviceImageVersionChecksResponse
}
