import type { SystemSettings } from '../types/settings'
import { readApiError } from './errors'

export async function fetchSettings(
  signal?: AbortSignal,
): Promise<SystemSettings> {
  const response = await fetch('/api/settings', { signal })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to load settings (${response.status})`,
      ),
    )
  }

  return (await response.json()) as SystemSettings
}

export async function updateSettings(
  request: Partial<
    Pick<SystemSettings, 'telemetryEnabled' | 'onboardingCompleted'>
  >,
): Promise<SystemSettings> {
  const response = await fetch('/api/settings', {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(request),
  })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to update settings (${response.status})`,
      ),
    )
  }

  return (await response.json()) as SystemSettings
}
