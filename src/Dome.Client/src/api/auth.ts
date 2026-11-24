import type { AuthCredentials, AuthStatus } from '../types/auth'
import { readApiError } from './errors'

const tooManyAttempts = 'Too many attempts, try again in a minute.'

async function postAuth(
  path: string,
  fallback: string,
  credentials?: AuthCredentials,
): Promise<void> {
  const response = await fetch(`/api/auth/${path}`, {
    method: 'POST',
    headers: credentials ? { 'Content-Type': 'application/json' } : undefined,
    body: credentials ? JSON.stringify(credentials) : undefined,
  })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        response.status === 429
          ? tooManyAttempts
          : `${fallback} (${response.status})`,
      ),
    )
  }
}

export async function fetchAuthStatus(
  signal?: AbortSignal,
): Promise<AuthStatus> {
  const response = await fetch('/api/auth/status', { signal })

  if (!response.ok) {
    throw new Error(
      await readApiError(
        response,
        `Failed to load auth status (${response.status})`,
      ),
    )
  }

  return (await response.json()) as AuthStatus
}

export function setupAdmin(credentials: AuthCredentials): Promise<void> {
  return postAuth('setup', 'Failed to create admin account', credentials)
}

export function login(credentials: AuthCredentials): Promise<void> {
  return postAuth('login', 'Failed to log in', credentials)
}

export function logout(): Promise<void> {
  return postAuth('logout', 'Failed to log out')
}
