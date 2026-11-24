import { useEffect, useState } from 'react'
import { Info } from 'lucide-react'
import { useNavigate } from 'react-router'
import { toast } from 'sonner'

import { fetchAuthStatus, logout } from '@/api/auth'
import { fetchSettings } from '@/api/settings'
import { AboutCard } from '@/components/settings/AboutCard'
import { TelemetryToggle } from '@/components/settings/TelemetryToggle'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import { Skeleton } from '@/components/ui/skeleton'
import type { SystemSettings } from '@/types/settings'

type SettingsState =
  | { status: 'loading' }
  | { status: 'ready'; settings: SystemSettings }
  | { status: 'error'; message: string }

export function SettingsPage() {
  const navigate = useNavigate()
  const [state, setState] = useState<SettingsState>({ status: 'loading' })
  const [username, setUsername] = useState<string | null>(null)
  const [loggingOut, setLoggingOut] = useState(false)

  useEffect(() => {
    const controller = new AbortController()
    fetchSettings(controller.signal)
      .then((settings) => setState({ status: 'ready', settings }))
      .catch((error: unknown) => {
        if (controller.signal.aborted) return
        setState({
          status: 'error',
          message: error instanceof Error ? error.message : 'Failed to load settings',
        })
      })
    fetchAuthStatus(controller.signal)
      .then((status) => setUsername(status.username))
      .catch(() => {})
    return () => controller.abort()
  }, [])

  async function handleLogout() {
    setLoggingOut(true)
    try {
      await logout()
      void navigate('/login', { replace: true })
    } catch (error) {
      toast.error(error instanceof Error ? error.message : 'Failed to log out')
      setLoggingOut(false)
    }
  }

  function setSettings(settings: SystemSettings) {
    setState({ status: 'ready', settings })
  }

  return (
    <div className="flex-1 space-y-4 overflow-auto p-6">
      <h1 className="text-xl font-semibold">Settings</h1>

      <Card className="max-w-2xl">
        <CardHeader>
          <CardTitle>Account</CardTitle>
          <CardDescription>
            {username ? `Signed in as ${username}.` : 'Signed in.'}
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Button variant="outline" onClick={handleLogout} disabled={loggingOut}>
            Log out
          </Button>
        </CardContent>
      </Card>

      {state.status === 'loading' && <Skeleton className="h-40 w-full max-w-2xl" />}
      {state.status === 'error' && (
        <p className="text-sm text-destructive">{state.message}</p>
      )}
      {state.status === 'ready' && (
        <>
          <Card className="max-w-2xl">
            <CardHeader>
              <CardTitle>Privacy</CardTitle>
              <CardDescription>
                Control what DOME shares with its developers.
              </CardDescription>
              <div
                role="note"
                className="mt-2 flex items-start gap-2 rounded-md border border-blue-500/30 bg-blue-500/10 p-3 text-sm text-blue-700 dark:text-blue-300"
              >
                <Info className="mt-0.5 size-4 shrink-0" />
                <p>
                  We never collect data that could identify you or your
                  machine. Installations are recognised only by a random
                  identifier generated when DOME is first installed.
                </p>
              </div>
            </CardHeader>
            <CardContent>
              <TelemetryToggle
                settings={state.settings}
                onSettingsChange={setSettings}
              />
            </CardContent>
          </Card>

          <Card className="max-w-2xl">
            <CardHeader>
              <CardTitle>Onboarding</CardTitle>
              <CardDescription>
                Walk through the welcome steps shown on first launch again.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <Button variant="outline" onClick={() => navigate('/onboarding')}>
                Re-run onboarding
              </Button>
            </CardContent>
          </Card>

          <AboutCard settings={state.settings} />
        </>
      )}
    </div>
  )
}
