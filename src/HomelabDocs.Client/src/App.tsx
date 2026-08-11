import { useCallback, useEffect, useState } from 'react'
import { fetchRunningContainers } from './api/containers'
import { ContainerDiagram } from './components/ContainerDiagram'
import type { Container } from './types/containers'
import './App.css'

type LoadState =
  | { status: 'loading' }
  | { status: 'ready'; containers: Container[] }
  | { status: 'empty' }
  | { status: 'error'; message: string }

export default function App() {
  const [loadState, setLoadState] = useState<LoadState>({ status: 'loading' })
  const [isRefreshing, setIsRefreshing] = useState(false)

  const loadContainers = useCallback(async (signal?: AbortSignal) => {
    setIsRefreshing(true)

    try {
      const response = await fetchRunningContainers(signal)
      const containers = response.containers ?? []

      if (containers.length === 0) {
        setLoadState({ status: 'empty' })
      } else {
        setLoadState({ status: 'ready', containers })
      }
    } catch (error) {
      if (signal?.aborted) {
        return
      }

      setLoadState({
        status: 'error',
        message:
          'Unable to load containers. Confirm that Docker is running and that the API can access the Docker socket.',
      })
    } finally {
      if (!signal?.aborted) {
        setIsRefreshing(false)
      }
    }
  }, [])

  useEffect(() => {
    const controller = new AbortController()
    void loadContainers(controller.signal)
    return () => controller.abort()
  }, [loadContainers])

  const statusBanner = (() => {
    switch (loadState.status) {
      case 'loading':
        return {
          className: 'status-banner status-banner-info',
          text: 'Loading containers…',
          showRetry: false,
        }
      case 'empty':
        return {
          className: 'status-banner status-banner-info',
          text: 'No containers were found.',
          showRetry: false,
        }
      case 'error':
        return {
          className: 'status-banner status-banner-error',
          text: loadState.message,
          showRetry: true,
        }
      default:
        return null
    }
  })()

  const containers =
    loadState.status === 'ready' ? loadState.containers : []

  return (
    <div className="app-page">
      <header className="app-bar">
        <div className="app-bar-brand">
          <h1 className="app-bar-title">HomelabDocs</h1>
          <p className="app-bar-subtitle">Infrastructure visualization</p>
        </div>
        <div className="app-bar-actions">
          <button
            type="button"
            className="refresh-button"
            disabled={isRefreshing}
            onClick={() => void loadContainers()}
          >
            Refresh containers
          </button>
        </div>
      </header>

      {statusBanner ? (
        <div className={statusBanner.className} role="status">
          <span>{statusBanner.text}</span>
          {statusBanner.showRetry ? (
            <button
              type="button"
              className="refresh-button refresh-button-inline"
              disabled={isRefreshing}
              onClick={() => void loadContainers()}
            >
              Retry
            </button>
          ) : null}
        </div>
      ) : null}

      <main className="diagram-canvas">
        <ContainerDiagram containers={containers} />
      </main>
    </div>
  )
}
