export type AuthStatus = {
  setupRequired: boolean
  authenticated: boolean
  username: string | null
  onboardingCompleted: boolean
}

export type AuthCredentials = {
  username: string
  password: string
}
