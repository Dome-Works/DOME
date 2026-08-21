import { Outlet } from 'react-router'

import { AppHeader } from '@/components/layout/AppHeader'
import { AppSidebar } from '@/components/layout/AppSidebar'
import { SidebarInset, SidebarProvider } from '@/components/ui/sidebar'

export function AppLayout() {
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
