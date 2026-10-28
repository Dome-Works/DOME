import { useEffect, useState } from 'react'
import { Info } from 'lucide-react'
import { toast } from 'sonner'

import { fetchSettings, updateSettings } from '@/api/settings'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import { Label } from '@/components/ui/label'
import { Skeleton } from '@/components/ui/skeleton'
import { Switch } from '@/components/ui/switch'
import type { SystemSettings } from '@/types/settings'

const clientVersion = __APP_VERSION__

type SettingsState =
  | { status: 'loading' }
  | { status: 'ready'; settings: SystemSettings }
  | { status: 'error'; message: string }

export function SettingsPage() {
  const [state, setState] = useState<SettingsState>({ status: 'loading' })

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
    return () => controller.abort()
  }, [])

  async function handleTelemetryChange(
    settings: SystemSettings,
    telemetryEnabled: boolean,
  ) {
    setState({ status: 'ready', settings: { ...settings, telemetryEnabled } })
    try {
      const saved = await updateSettings({ telemetryEnabled })
      setState({ status: 'ready', settings: saved })
    } catch (error) {
      setState({ status: 'ready', settings })
      toast.error(
        error instanceof Error ? error.message : 'Failed to update settings',
      )
    }
  }

  return (
    <div className="flex-1 space-y-4 overflow-auto p-6">
      <h1 className="text-xl font-semibold">Settings</h1>

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
              <div className="flex items-center justify-between gap-4">
                <div className="space-y-1">
                  <Label htmlFor="telemetry">Anonymous usage tracking</Label>
                  <p className="text-sm text-muted-foreground">
                    Send a daily anonymous heartbeat with the version and the
                    number of sockets and stacks.
                  </p>
                </div>
                <Switch
                  id="telemetry"
                  checked={state.settings.telemetryEnabled}
                  onCheckedChange={(checked) =>
                    void handleTelemetryChange(state.settings, checked)
                  }
                />
              </div>
            </CardContent>
          </Card>

          <Card className="max-w-2xl">
            <CardHeader>
              <CardTitle>About</CardTitle>
            </CardHeader>
            <CardContent>
              <dl className="grid grid-cols-[10rem_1fr] gap-y-2 text-sm">
                <dt className="text-muted-foreground">Server version</dt>
                <dd>{state.settings.version}</dd>
                <dt className="text-muted-foreground">Client version</dt>
                <dd>{clientVersion}</dd>
                <dt className="text-muted-foreground">Environment</dt>
                <dd>{state.settings.environment}</dd>
                <dt className="text-muted-foreground">Installation ID</dt>
                <dd className="font-mono break-all">
                  {state.settings.installationId}
                </dd>
              </dl>
            </CardContent>
          </Card>
        </>
      )}
    </div>
  )
}
