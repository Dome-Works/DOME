import { useLocation } from 'react-router'

import { Separator } from '@/components/ui/separator'
import { SidebarTrigger } from '@/components/ui/sidebar'

const pageTitles: Record<string, string> = {
  '/': 'Home',
  '/diagrams': 'Diagrams',
  '/sockets': 'Sockets',
}

export function AppHeader() {
  const location = useLocation()
  const title = pageTitles[location.pathname] ?? 'DOME'

  return (
    <header className="flex h-14 shrink-0 items-center gap-2 border-b px-4">
      <SidebarTrigger className="-ml-1" />
      <Separator orientation="vertical" className="mr-2 h-4" />
      <h1 className="text-sm font-medium">{title}</h1>
    </header>
  )
}
