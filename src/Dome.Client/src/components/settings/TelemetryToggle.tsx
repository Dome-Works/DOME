import { toast } from 'sonner'

import { updateSettings } from '@/api/settings'
import { Label } from '@/components/ui/label'
import { Switch } from '@/components/ui/switch'
import type { SystemSettings } from '@/types/settings'

type TelemetryToggleProps = {
  settings: SystemSettings
  onSettingsChange: (settings: SystemSettings) => void
}

export function TelemetryToggle({
  settings,
  onSettingsChange,
}: TelemetryToggleProps) {
  async function handleChange(telemetryEnabled: boolean) {
    onSettingsChange({ ...settings, telemetryEnabled })
    try {
      onSettingsChange(await updateSettings({ telemetryEnabled }))
    } catch (error) {
      onSettingsChange(settings)
      toast.error(
        error instanceof Error ? error.message : 'Failed to update settings',
      )
    }
  }

  return (
    <div className="flex items-center justify-between gap-4">
      <div className="space-y-1">
        <Label htmlFor="telemetry">Anonymous usage tracking</Label>
        <p className="text-sm text-muted-foreground">
          Send a daily anonymous heartbeat with the version and the number of
          sockets and stacks.
        </p>
      </div>
      <Switch
        id="telemetry"
        checked={settings.telemetryEnabled}
        onCheckedChange={(checked) => void handleChange(checked)}
      />
    </div>
  )
}
