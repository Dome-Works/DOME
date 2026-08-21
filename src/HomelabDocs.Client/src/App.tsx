import { BrowserRouter, Route, Routes } from 'react-router'

import { AppLayout } from '@/components/layout/AppLayout'
import { Toaster } from '@/components/ui/sonner'
import { DiagramsPage } from '@/pages/DiagramsPage'
import { HomePage } from '@/pages/HomePage'
import { SocketsPage } from '@/pages/SocketsPage'

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AppLayout />}>
          <Route index element={<HomePage />} />
          <Route path="diagrams" element={<DiagramsPage />} />
          <Route path="sockets" element={<SocketsPage />} />
        </Route>
      </Routes>
      <Toaster theme="dark" />
    </BrowserRouter>
  )
}
