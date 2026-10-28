import { BrowserRouter, Route, Routes } from 'react-router'

import { AppLayout } from '@/components/layout/AppLayout'
import { Toaster } from '@/components/ui/sonner'
import { DiagramsPage } from '@/pages/DiagramsPage'
import { HomePage } from '@/pages/HomePage'
import { OnboardingPage } from '@/pages/OnboardingPage'
import { SettingsPage } from '@/pages/SettingsPage'
import { SocketsPage } from '@/pages/SocketsPage'

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="onboarding" element={<OnboardingPage />} />
        <Route element={<AppLayout />}>
          <Route index element={<HomePage />} />
          <Route path="diagrams" element={<DiagramsPage />} />
          <Route path="sockets" element={<SocketsPage />} />
          <Route path="settings" element={<SettingsPage />} />
        </Route>
      </Routes>
      <Toaster theme="dark" />
    </BrowserRouter>
  )
}
