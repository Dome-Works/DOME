import type { GetDeviceContainersResponse } from '../types/containers'
import { readApiError } from './errors'

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

export async function startDeviceContainer(
  deviceName: string,
  containerId: string,
): Promise<void> {
  await postContainerLifecycle(deviceName, containerId, 'start')
}

export async function stopDeviceContainer(
  deviceName: string,
  containerId: string,
): Promise<void> {
  await postContainerLifecycle(deviceName, containerId, 'stop')
}

async function postContainerLifecycle(
  deviceName: string,
  containerId: string,
  action: 'start' | 'stop',
): Promise<void> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/containers/${encodeURIComponent(containerId)}/${action}`,
    { method: 'POST' },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to ${action} container (${response.status})`,
      ),
    )
  }
}

