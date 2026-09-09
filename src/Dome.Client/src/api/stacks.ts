import type { DeviceStack } from '../types/stacks'
import { readApiError } from './errors'

export async function fetchDeviceStack(
  deviceName: string,
  stackId: string,
  signal?: AbortSignal,
): Promise<DeviceStack> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/stacks/${encodeURIComponent(stackId)}`,
    { signal },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(response, `Failed to load stack (${response.status})`),
    )
  }

  return (await response.json()) as DeviceStack
}

export async function createDeviceStack(
  deviceName: string,
  projectName: string,
): Promise<DeviceStack> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/stacks`,
    {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ projectName }),
    },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to create stack (${response.status})`,
      ),
    )
  }

  return (await response.json()) as DeviceStack
}

export async function updateDeviceStack(
  deviceName: string,
  stack: Pick<DeviceStack, 'id' | 'projectName' | 'composeYaml'>,
): Promise<DeviceStack> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/stacks/${encodeURIComponent(stack.id)}`,
    {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        projectName: stack.projectName,
        composeYaml: stack.composeYaml,
      }),
    },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to save stack (${response.status})`,
      ),
    )
  }

  return (await response.json()) as DeviceStack
}

export async function deployDeviceStack(
  deviceName: string,
  stackId: string,
): Promise<void> {
  const response = await fetch(
    `/api/devices/${encodeURIComponent(deviceName)}/stacks/${encodeURIComponent(stackId)}/deploy`,
    { method: 'POST' },
  )

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to deploy stack (${response.status})`,
      ),
    )
  }
}
