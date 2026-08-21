import { useCallback, useEffect, useState } from 'react'
import { Link } from 'react-router'

import { fetchDeviceContainers } from '@/api/containers'
import { fetchDevices } from '@/api/devices'
import { ContainerDiagram } from '@/components/ContainerDiagram'
import { DeviceTabs } from '@/components/DeviceTabs'
import { Button } from '@/components/ui/button'
import type { Container } from '@/types/containers'
import type { Device } from '@/types/devices'
import '@/App.css'

type DevicesState =
  | { status: 'loading' }
  | { status: 'ready'; devices: Device[] }
  | { status: 'empty' }
  | { status: 'error'; message: string }

type ContainersState =
  | { status: 'idle' }
  | { status: 'loading' }
  | { status: 'ready'; containers: Container[] }
  | { status: 'empty' }
  | { status: 'error'; message: string }

export function DiagramsPage() {
  const [devicesState, setDevicesState] = useState<DevicesState>({
    status: 'loading',
  })
  const [selectedDeviceName, setSelectedDeviceName] = useState<string | null>(
    null,
  )
  const [containersState, setContainersState] = useState<ContainersState>({
    status: 'idle',
  })
  const [isRefreshing, setIsRefreshing] = useState(false)

  const loadDevices = useCallback(async (signal?: AbortSignal) => {
    setDevicesState({ status: 'loading' })

    try {
      const response = await fetchDevices(signal)
      const devices = response.devices ?? []

      if (devices.length === 0) {
        setSelectedDeviceName(null)
        setDevicesState({ status: 'empty' })
        return
      }

      setDevicesState({ status: 'ready', devices })
      setSelectedDeviceName((current) => {
        if (current && devices.some((device) => device.name === current)) {
          return current
        }

        return devices[0].name
      })
    } catch {
      if (signal?.aborted) {
        return
      }

      setSelectedDeviceName(null)
      setDevicesState({
        status: 'error',
        message: 'Unable to load devices. Confirm that the API is running.',
      })
    }
  }, [])

  const loadContainers = useCallback(
    async (deviceName: string, signal?: AbortSignal) => {
      setIsRefreshing(true)
      setContainersState({ status: 'loading' })

      try {
        const response = await fetchDeviceContainers(deviceName, signal)
        const containers = response.containers ?? []

        if (signal?.aborted) {
          return
        }

        if (containers.length === 0) {
          setContainersState({ status: 'empty' })
        } else {
          setContainersState({ status: 'ready', containers })
        }
      } catch {
        if (signal?.aborted) {
          return
        }

        setContainersState({
          status: 'error',
          message:
            'Unable to load containers. Confirm that Docker is reachable for this device.',
        })
      } finally {
        if (!signal?.aborted) {
          setIsRefreshing(false)
        }
      }
    },
    [],
  )

  useEffect(() => {
    const controller = new AbortController()
    void loadDevices(controller.signal)
    return () => controller.abort()
  }, [loadDevices])

  useEffect(() => {
    if (!selectedDeviceName) {
      setContainersState({ status: 'idle' })
      return
    }

    const controller = new AbortController()
    void loadContainers(selectedDeviceName, controller.signal)
    return () => controller.abort()
  }, [selectedDeviceName, loadContainers])

  const statusBanner = (() => {
    if (devicesState.status === 'loading') {
      return {
        className: 'status-banner status-banner-info',
        text: 'Loading devices…',
        showRetry: false,
        onRetry: undefined as (() => void) | undefined,
      }
    }

    if (devicesState.status === 'empty') {
      return {
        className: 'status-banner status-banner-info',
        text: 'No devices are configured.',
        showRetry: false,
        onRetry: undefined as (() => void) | undefined,
      }
    }

    if (devicesState.status === 'error') {
      return {
        className: 'status-banner status-banner-error',
        text: devicesState.message,
        showRetry: true,
        onRetry: () => void loadDevices(),
      }
    }

    switch (containersState.status) {
      case 'loading':
        return {
          className: 'status-banner status-banner-info',
          text: 'Loading containers…',
          showRetry: false,
          onRetry: undefined as (() => void) | undefined,
        }
      case 'empty':
        return {
          className: 'status-banner status-banner-info',
          text: 'No containers were found on this device.',
          showRetry: false,
          onRetry: undefined as (() => void) | undefined,
        }
      case 'error':
        return {
          className: 'status-banner status-banner-error',
          text: containersState.message,
          showRetry: true,
          onRetry: selectedDeviceName
            ? () => void loadContainers(selectedDeviceName)
            : undefined,
        }
      default:
        return null
    }
  })()

  const containers =
    containersState.status === 'ready' ? containersState.containers : []
  const devices = devicesState.status === 'ready' ? devicesState.devices : []
  const canRefresh =
    selectedDeviceName !== null && devicesState.status === 'ready'

  return (
    <div className="app-page">
      <div className="flex shrink-0 items-center justify-end gap-2 border-b px-4 py-2">
        {devicesState.status === 'empty' ? (
          <Button asChild variant="outline" size="sm">
            <Link to="/sockets">Register a socket</Link>
          </Button>
        ) : null}
        <Button
          type="button"
          variant="outline"
          size="sm"
          disabled={!canRefresh || isRefreshing}
          onClick={() => {
            if (selectedDeviceName) {
              void loadContainers(selectedDeviceName)
            }
          }}
        >
          Refresh containers
        </Button>
      </div>

      {devices.length > 0 && selectedDeviceName ? (
        <DeviceTabs
          devices={devices}
          selectedDeviceName={selectedDeviceName}
          onSelect={setSelectedDeviceName}
        />
      ) : null}

      {statusBanner ? (
        <div className={statusBanner.className} role="status">
          <span>{statusBanner.text}</span>
          {statusBanner.showRetry && statusBanner.onRetry ? (
            <Button
              type="button"
              variant="outline"
              size="sm"
              disabled={isRefreshing}
              onClick={statusBanner.onRetry}
            >
              Retry
            </Button>
          ) : null}
        </div>
      ) : null}

      <main className="diagram-canvas">
        {selectedDeviceName ? (
          <ContainerDiagram
            key={selectedDeviceName}
            containers={containers}
          />
        ) : null}
      </main>
    </div>
  )
}
