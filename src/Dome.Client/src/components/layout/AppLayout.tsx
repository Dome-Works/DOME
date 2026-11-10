import { useEffect, useState } from 'react'
import { Outlet, useNavigate } from 'react-router'

import { fetchAuthStatus } from '@/api/auth'

import { AppHeader } from '@/components/layout/AppHeader'
import { AppSidebar } from '@/components/layout/AppSidebar'
import { SidebarInset, SidebarProvider } from '@/components/ui/sidebar'

export function AppLayout() {
  const navigate = useNavigate()
  const [authenticated, setAuthenticated] = useState(false)

  useEffect(() => {
    const controller = new AbortController()
    fetchAuthStatus(controller.signal)
      .then((status) => {
        if (
          status.setupRequired ||
          (status.authenticated && !status.onboardingCompleted)
        ) {
          void navigate('/onboarding', { replace: true })
          return
        }
        if (!status.authenticated) {
          void navigate('/login', { replace: true })
          return
        }
        setAuthenticated(true)
      })
      .catch(() => {
        if (!controller.signal.aborted) {
          void navigate('/login', { replace: true })
        }
      })
    return () => controller.abort()
  }, [navigate])

  if (!authenticated) return null

  return (
    <SidebarProvider className='h-full min-h-0'>
      <AppSidebar />
      <SidebarInset className='min-h-0 overflow-hidden'>
        <AppHeader />
        <div className='flex min-h-0 flex-1 flex-col overflow-hidden'>
          <Outlet />
        </div>
      </SidebarInset>
    </SidebarProvider>
  )
}
