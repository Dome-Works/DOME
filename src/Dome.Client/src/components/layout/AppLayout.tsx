import { useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router'

import { fetchSettings } from '@/api/settings'

import { AppHeader } from '@/components/layout/AppHeader'
import { AppSidebar } from '@/components/layout/AppSidebar'
import { SidebarInset, SidebarProvider } from '@/components/ui/sidebar'

export function AppLayout() {
  const navigate = useNavigate()

  useEffect(() => {
    const controller = new AbortController()
    fetchSettings(controller.signal)
      .then((settings) => {
        if (!settings.onboardingCompleted) {
          void navigate('/onboarding', { replace: true })
        }
      })
      // Never lock users out of the app when settings cannot be loaded.
      .catch(() => {})
    return () => controller.abort()
  }, [navigate])

  return (
    <SidebarProvider className="h-full min-h-0">
      <AppSidebar />
      <SidebarInset className="min-h-0 overflow-hidden">
        <AppHeader />
        <div className="flex min-h-0 flex-1 flex-col overflow-hidden">
          <Outlet />
        </div>
      </SidebarInset>
    </SidebarProvider>
  )
}
