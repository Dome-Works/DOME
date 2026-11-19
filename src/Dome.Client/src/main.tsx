import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App'
import './index.css'

const originalFetch = window.fetch.bind(window)
window.fetch = async (input, init) => {
  const response = await originalFetch(input, init)
  const url = new URL(
    input instanceof Request ? input.url : input.toString(),
    window.location.origin,
  )
  if (
    response.status === 401 &&
    url.pathname.startsWith('/api/') &&
    !url.pathname.startsWith('/api/auth/') &&
    !['/login', '/onboarding'].includes(window.location.pathname)
  ) {
    window.location.assign('/login')
  }
  return response
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
