import { useEffect, useState, type ReactNode } from 'react'
import {
  CircleCheckBig,
  PartyPopper,
  ShieldCheck,
  type LucideIcon,
} from 'lucide-react'
import { useNavigate } from 'react-router'
import { toast } from 'sonner'

import { fetchSettings, updateSettings } from '@/api/settings'
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
import { cn } from '@/lib/utils'
import type { SystemSettings } from '@/types/settings'

const issuesUrl = 'https://github.com/Dome-Works/DOME/issues'

type OnboardingStep = {
  icon: LucideIcon
  title: string
  description: ReactNode
  content?: ReactNode
}

export function OnboardingPage() {
  const navigate = useNavigate()
  const [settings, setSettings] = useState<SystemSettings | null>(null)
  const [stepIndex, setStepIndex] = useState(0)
  const [finishing, setFinishing] = useState(false)

  useEffect(() => {
    const controller = new AbortController()
    fetchSettings(controller.signal)
      .then(setSettings)
      .catch((error: unknown) => {
        if (controller.signal.aborted) return
        toast.error(
          error instanceof Error ? error.message : 'Failed to load settings',
        )
      })
    return () => controller.abort()
  }, [])

  // Add new onboarding steps here; the stepper adapts to the array length.
  const steps: OnboardingStep[] = [
    {
      icon: PartyPopper,
      title: 'Welcome to DOME',
      description: (
        <>
          Thank you for trying out DOME! It is still young, so bug reports
          and feature requests are very welcome. Share them on{' '}
          <a
            href={issuesUrl}
            target="_blank"
            rel="noreferrer"
            className="font-medium text-primary underline underline-offset-4"
          >
            GitHub Issues
          </a>
          .
        </>
      ),
    },
    {
      icon: ShieldCheck,
      title: 'Anonymous usage tracking',
      description: (
        <>
          <p>
            <strong className="text-foreground">Why.</strong> Knowing how many
            installations, sockets and stacks are in use helps the maintainers
            decide what to work on next. Sharing these insights is very
            important for DOME&apos;s development.
          </p>
          <p>
            <strong className="text-foreground">What.</strong> The DOME
            version, the environment, the number of sockets and stacks, and
            their random IDs. We never collect anything that can identify you
            or your device: no IP addresses, hostnames, networks or stack
            contents. The only identifiers are random IDs generated on first
            launch, such as the installation ID shown below.
          </p>
          <p>
            <strong className="text-foreground">How.</strong> The server sends
            a heartbeat when it starts and every 24 hours after. When tracking
            is disabled, no request is sent at all.
          </p>
        </>
      ),
      content: settings ? (
        <>
          <Card className="w-full max-w-2xl">
            <CardHeader>
              <CardTitle>Privacy</CardTitle>
              <CardDescription>
                You can change this at any time in Settings.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <TelemetryToggle
                settings={settings}
                onSettingsChange={setSettings}
              />
            </CardContent>
          </Card>
          <AboutCard settings={settings} />
        </>
      ) : (
        <Skeleton className="h-40 w-full" />
      ),
    },
    {
      icon: CircleCheckBig,
      title: "You're all set",
      description:
        'Onboarding is finished. You can revisit these steps at any time from Settings.',
    },
  ]

  const step = steps[stepIndex]
  const isLast = stepIndex === steps.length - 1

  async function finish() {
    setFinishing(true)
    try {
      await updateSettings({ onboardingCompleted: true })
      void navigate('/', { replace: true })
    } catch (error) {
      setFinishing(false)
      toast.error(
        error instanceof Error ? error.message : 'Failed to update settings',
      )
    }
  }

  return (
    <div className="flex h-full flex-col items-center overflow-auto p-6">
      <div className="flex w-full max-w-2xl flex-1 flex-col items-center gap-6 py-10 text-center">
        <div className="flex gap-2">
          <span className="sr-only">
            Step {stepIndex + 1} of {steps.length}
          </span>
          {steps.map((s, index) => (
            <span
              key={s.title}
              aria-hidden
              className={cn(
                'h-1.5 w-8 rounded-full bg-muted',
                index <= stepIndex && 'bg-primary',
              )}
            />
          ))}
        </div>

        <div className="flex size-24 items-center justify-center rounded-3xl bg-primary/10 text-primary">
          <step.icon className="size-12" />
        </div>

        <h1 className="text-2xl font-semibold">{step.title}</h1>
        <div className="space-y-3 text-muted-foreground">{step.description}</div>

        {step.content && (
          <div className="flex w-full flex-col items-center gap-4 text-left">
            {step.content}
          </div>
        )}

        <div className="mt-auto flex w-full justify-between pt-6">
          <Button
            variant="outline"
            disabled={stepIndex === 0}
            onClick={() => setStepIndex(stepIndex - 1)}
          >
            Back
          </Button>
          {isLast ? (
            <Button disabled={finishing} onClick={() => void finish()}>
              Get started
            </Button>
          ) : (
            <Button onClick={() => setStepIndex(stepIndex + 1)}>Next</Button>
          )}
        </div>
      </div>
    </div>
  )
}
