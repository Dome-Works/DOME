import { useEffect, useState, type FormEvent, type ReactNode } from 'react'
import {
  CircleCheckBig,
  KeyRound,
  PartyPopper,
  ShieldCheck,
  type LucideIcon,
} from 'lucide-react'
import { useNavigate } from 'react-router'
import { toast } from 'sonner'

import { fetchAuthStatus, setupAdmin } from '@/api/auth'
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
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Skeleton } from '@/components/ui/skeleton'
import { cn } from '@/lib/utils'
import type { AuthStatus } from '@/types/auth'
import type { SystemSettings } from '@/types/settings'

const issuesUrl = 'https://github.com/Dome-Works/DOME/issues'
const accountFormId = 'onboarding-account-form'
const minPasswordLength = 12

type OnboardingStep = {
  icon: LucideIcon
  title: string
  description: ReactNode
  content?: ReactNode
  isAccount?: boolean
}

export function OnboardingPage() {
  const navigate = useNavigate()
  const [status, setStatus] = useState<AuthStatus | null>(null)
  const [authenticated, setAuthenticated] = useState(false)
  const [settings, setSettings] = useState<SystemSettings | null>(null)
  const [stepIndex, setStepIndex] = useState(0)
  const [finishing, setFinishing] = useState(false)
  const [username, setUsername] = useState('')
  const [usernameTouched, setUsernameTouched] = useState(false)
  const [password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')
  const [submitting, setSubmitting] = useState(false)

  useEffect(() => {
    const controller = new AbortController()
    fetchAuthStatus(controller.signal)
      .then((result) => {
        if (!result.authenticated && !result.setupRequired) {
          void navigate('/login', { replace: true })
          return
        }
        setStatus(result)
        setAuthenticated(result.authenticated)
      })
      .catch((error: unknown) => {
        if (controller.signal.aborted) return
        toast.error(
          error instanceof Error ? error.message : 'Failed to load auth status',
        )
      })
    return () => controller.abort()
  }, [navigate])

  const isUpgrade = !!status?.onboardingCompleted && status.setupRequired

  useEffect(() => {
    if (!authenticated || isUpgrade) return
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
  }, [authenticated, isUpgrade])

  const usernameError =
    usernameTouched && !username.trim() ? 'Username is required.' : null
  const passwordError =
    password && password.length < minPasswordLength
      ? `Password must be at least ${minPasswordLength} characters.`
      : null
  const confirmError =
    confirmPassword && confirmPassword !== password
      ? 'Passwords do not match.'
      : null
  const accountValid =
    !!username.trim() &&
    password.length >= minPasswordLength &&
    password === confirmPassword

  async function createAdmin(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    if (!accountValid || submitting) return
    setSubmitting(true)
    try {
      await setupAdmin({ username: username.trim(), password })
      setAuthenticated(true)
      setPassword('')
      setConfirmPassword('')
      setStepIndex((index) => index + 1)
    } catch (error) {
      toast.error(
        error instanceof Error
          ? error.message
          : 'Failed to create admin account',
      )
    } finally {
      setSubmitting(false)
    }
  }

  const accountContent = authenticated ? (
    <p className="text-muted-foreground">
      Admin account <strong className="text-foreground">{username}</strong>{' '}
      created. You are signed in.
    </p>
  ) : (
    <form
      id={accountFormId}
      className="w-full max-w-sm space-y-4"
      onSubmit={(event) => void createAdmin(event)}
    >
      <div className="space-y-2">
        <Label htmlFor="onboarding-username">Username</Label>
        <Input
          id="onboarding-username"
          autoComplete="username"
          required
          value={username}
          aria-invalid={!!usernameError}
          aria-describedby={usernameError ? 'onboarding-username-error' : undefined}
          onBlur={() => setUsernameTouched(true)}
          onChange={(event) => setUsername(event.target.value)}
        />
        {usernameError && (
          <p id="onboarding-username-error" className="text-sm text-destructive">
            {usernameError}
          </p>
        )}
      </div>
      <div className="space-y-2">
        <Label htmlFor="onboarding-password">Password</Label>
        <Input
          id="onboarding-password"
          type="password"
          autoComplete="new-password"
          required
          minLength={minPasswordLength}
          value={password}
          aria-invalid={!!passwordError}
          aria-describedby="onboarding-password-hint"
          onChange={(event) => setPassword(event.target.value)}
        />
        <p
          id="onboarding-password-hint"
          className={cn(
            'text-sm text-muted-foreground',
            passwordError && 'text-destructive',
          )}
        >
          {passwordError ?? `At least ${minPasswordLength} characters.`}
        </p>
      </div>
      <div className="space-y-2">
        <Label htmlFor="onboarding-confirm-password">Confirm password</Label>
        <Input
          id="onboarding-confirm-password"
          type="password"
          autoComplete="new-password"
          required
          value={confirmPassword}
          aria-invalid={!!confirmError}
          aria-describedby={confirmError ? 'onboarding-confirm-error' : undefined}
          onChange={(event) => setConfirmPassword(event.target.value)}
        />
        {confirmError && (
          <p id="onboarding-confirm-error" className="text-sm text-destructive">
            {confirmError}
          </p>
        )}
      </div>
    </form>
  )

  const doneStep: OnboardingStep = {
    icon: CircleCheckBig,
    title: "You're all set",
    description: isUpgrade
      ? 'Your DOME is secured. Open the Sockets page to add the key for each socket.'
      : 'Onboarding is finished. You can revisit these steps at any time from Settings.',
  }

  const upgradeSteps: OnboardingStep[] = [
    {
      icon: KeyRound,
      title: 'Secure your DOME',
      description: (
        <>
          <p>
            DOME now requires you to sign in. Create an admin account to keep
            using it.
          </p>
          <p>
            Sockets also need a key now. You will be asked for it on the
            Sockets page.
          </p>
        </>
      ),
      content: accountContent,
      isAccount: true,
    },
    doneStep,
  ]

  // Add new onboarding steps here; the stepper adapts to the array length.
  const freshSteps: OnboardingStep[] = [
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
    ...(status?.setupRequired
      ? [
          {
            icon: KeyRound,
            title: 'Create admin account',
            description:
              'This account protects your DOME. You will use it to sign in.',
            content: accountContent,
            isAccount: true,
          },
        ]
      : []),
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
    doneStep,
  ]

  if (!status) {
    return (
      <div className="flex h-full flex-col items-center p-6">
        <Skeleton className="mt-10 h-64 w-full max-w-2xl" />
      </div>
    )
  }

  const steps = isUpgrade ? upgradeSteps : freshSteps
  const step = steps[stepIndex]
  const isLast = stepIndex === steps.length - 1
  const submitsAccount = !!step.isAccount && !authenticated

  async function finish() {
    if (isUpgrade) {
      void navigate('/', { replace: true })
      return
    }
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
            submitsAccount ? (
              <Button
                type="submit"
                form={accountFormId}
                disabled={!accountValid || submitting}
              >
                {submitting ? 'Creating account…' : 'Next'}
              </Button>
            ) : (
              <Button onClick={() => setStepIndex(stepIndex + 1)}>Next</Button>
            )
          )}
        </div>
      </div>
    </div>
  )
}
