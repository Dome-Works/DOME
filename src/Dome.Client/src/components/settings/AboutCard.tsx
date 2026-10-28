import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import type { SystemSettings } from '@/types/settings'

const clientVersion = __APP_VERSION__

export function AboutCard({ settings }: { settings: SystemSettings }) {
  return (
    <Card className="w-full max-w-2xl">
      <CardHeader>
        <CardTitle>About</CardTitle>
      </CardHeader>
      <CardContent>
        <dl className="grid grid-cols-[10rem_1fr] gap-y-2 text-sm">
          <dt className="text-muted-foreground">Server version</dt>
          <dd>{settings.version}</dd>
          <dt className="text-muted-foreground">Client version</dt>
          <dd>{clientVersion}</dd>
          <dt className="text-muted-foreground">Environment</dt>
          <dd>{settings.environment}</dd>
          <dt className="text-muted-foreground">Installation ID</dt>
          <dd className="font-mono break-all">{settings.installationId}</dd>
        </dl>
      </CardContent>
    </Card>
  )
}
