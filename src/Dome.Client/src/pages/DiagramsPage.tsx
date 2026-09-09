import { useCallback, useEffect, useState } from 'react'
import { Link } from 'react-router'

import {
  fetchDeviceDiagram,
  startDeviceContainer,
  stopDeviceContainer,
} from '@/api/containers'
import { fetchDevices } from '@/api/devices'
import { createDeviceStack, deployDeviceStack } from '@/api/stacks'
import { ContainerDetailPane } from '@/components/ContainerDetailPane'
import { ContainerDiagram } from '@/components/ContainerDiagram'
import { CreateStackDialog } from '@/components/CreateStackDialog'
import { DeviceTabs } from '@/components/DeviceTabs'
import { StackDetailPane } from '@/components/StackDetailPane'
import { Button } from '@/components/ui/button'
import { toast } from 'sonner'
import type { Container } from '@/types/containers'
import type { DeviceDiagram } from '@/types/diagram'
import { stackCanvasId } from '@/types/diagram'
import type { Device } from '@/types/devices'
import '@/App.css'

type DevicesState =
  | { status: 'loading' }
  | { status: 'ready'; devices: Device[] }
  | { status: 'empty' }
  | { status: 'error'; message: string }

type DiagramState =
  | { status: 'idle' }
  | { status: 'loading' }
  | { status: 'ready'; diagram: DeviceDiagram }
  | { status: 'empty' }
  | { status: 'error'; message: string }

export function DiagramsPage() {
  const [devicesState, setDevicesState] = useState<DevicesState>({
    status: 'loading',
  })
  const [selectedDeviceName, setSelectedDeviceName] = useState<string | null>(
    null,
  )
  const [selectedContainerId, setSelectedContainerId] = useState<string | null>(
    null,
  )
  const [selectedStackCanvasId, setSelectedStackCanvasId] = useState<
    string | null
  >(null)
  const [diagramState, setDiagramState] = useState<DiagramState>({
    status: 'idle',
  })
  const [isRefreshing, setIsRefreshing] = useState(false)
  const [isActionPending, setIsActionPending] = useState(false)
  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isCreating, setIsCreating] = useState(false)

  const loadDevices = useCallback(async (signal?: AbortSignal) => {
    setDevicesState({ status: 'loading' })

    try {
      const response = await fetchDevices(signal)
      const devices = response.devices ?? []

      if (devices.length === 0) {
        setSelectedDeviceName(null)
        setSelectedContainerId(null)
        setSelectedStackCanvasId(null)
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

  const loadDiagram = useCallback(
    async (deviceName: string, signal?: AbortSignal) => {
      setIsRefreshing(true)
      setDiagramState({ status: 'loading' })

      try {
        const diagram = await fetchDeviceDiagram(deviceName, signal)

        if (signal?.aborted) {
          return
        }

        if (diagram.stacks.length === 0 && diagram.containers.length === 0) {
          setDiagramState({ status: 'empty' })
        } else {
          setDiagramState({ status: 'ready', diagram })
        }
      } catch {
        if (signal?.aborted) {
          return
        }

        setDiagramState({
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
      setSelectedContainerId(null)
      setSelectedStackCanvasId(null)
      setDiagramState({ status: 'idle' })
      return
    }

    const controller = new AbortController()
    void loadDiagram(selectedDeviceName, controller.signal)
    return () => controller.abort()
  }, [selectedDeviceName, loadDiagram])

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

    switch (diagramState.status) {
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
          text: 'No stacks or containers were found on this device.',
          showRetry: false,
          onRetry: undefined as (() => void) | undefined,
        }
      case 'error':
        return {
          className: 'status-banner status-banner-error',
          text: diagramState.message,
          showRetry: true,
          onRetry: selectedDeviceName
            ? () => void loadDiagram(selectedDeviceName)
            : undefined,
        }
      default:
        return null
    }
  })()

  const diagram =
    diagramState.status === 'ready'
      ? diagramState.diagram
      : { stacks: [], containers: [] }
  const devices = devicesState.status === 'ready' ? devicesState.devices : []
  const selectedContainer =
    selectedContainerId === null
      ? null
      : diagram.containers.find((container) => container.id === selectedContainerId) ??
        null
  const selectedStack =
    selectedStackCanvasId === null
      ? null
      : diagram.stacks.find((stack) => stackCanvasId(stack) === selectedStackCanvasId) ??
        null
  const canRefresh =
    selectedDeviceName !== null && devicesState.status === 'ready'
  const canCreateStack =
    selectedDeviceName !== null && devicesState.status === 'ready'

  const runContainerAction = useCallback(
    async (action: 'start' | 'stop', container: Container) => {
      if (!selectedDeviceName) {
        return
      }

      setIsActionPending(true)
      try {
        if (action === 'start') {
          await startDeviceContainer(selectedDeviceName, container.id)
          toast.success(`Started ${container.name}.`)
        } else {
          await stopDeviceContainer(selectedDeviceName, container.id)
          toast.success(`Stopped ${container.name}.`)
        }

        await loadDiagram(selectedDeviceName)
      } catch (error) {
        toast.error(
          error instanceof Error
            ? error.message
            : `Unable to ${action} container.`,
        )
      } finally {
        setIsActionPending(false)
      }
    },
    [loadDiagram, selectedDeviceName],
  )

  const createStack = useCallback(
    async (projectName: string) => {
      if (!selectedDeviceName) {
        return
      }

      setIsCreating(true)
      try {
        const created = await createDeviceStack(selectedDeviceName, projectName)
        setIsCreateOpen(false)
        toast.success(`Created ${created.projectName}.`)
        await loadDiagram(selectedDeviceName)
        setSelectedContainerId(null)
        setSelectedStackCanvasId(
          stackCanvasId({
            id: created.id,
            projectName: created.projectName,
            kind: 'managed',
          }),
        )
      } catch (error) {
        toast.error(
          error instanceof Error ? error.message : 'Unable to create stack.',
        )
      } finally {
        setIsCreating(false)
      }
    },
    [loadDiagram, selectedDeviceName],
  )

  const deployStack = useCallback(
    async (stackId: string, projectName: string) => {
      if (!selectedDeviceName) {
        return
      }

      setIsActionPending(true)
      try {
        await deployDeviceStack(selectedDeviceName, stackId)
        toast.success(`Deployed ${projectName}.`)
        await loadDiagram(selectedDeviceName)
      } catch (error) {
        toast.error(
          error instanceof Error ? error.message : 'Unable to deploy stack.',
        )
      } finally {
        setIsActionPending(false)
      }
    },
    [loadDiagram, selectedDeviceName],
  )

  useEffect(() => {
    if (diagramState.status !== 'ready' && diagramState.status !== 'empty') {
      return
    }

    const listedContainers =
      diagramState.status === 'ready' ? diagramState.diagram.containers : []
    const listedStacks =
      diagramState.status === 'ready' ? diagramState.diagram.stacks : []

    if (
      selectedContainerId &&
      !listedContainers.some((container) => container.id === selectedContainerId)
    ) {
      setSelectedContainerId(null)
    }

    if (
      selectedStackCanvasId &&
      !listedStacks.some((stack) => stackCanvasId(stack) === selectedStackCanvasId)
    ) {
      setSelectedStackCanvasId(null)
    }
  }, [diagramState, selectedContainerId, selectedStackCanvasId])

  const detailOpen = selectedContainer !== null || selectedStack !== null

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
              void loadDiagram(selectedDeviceName)
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

      <main
        className={`diagram-shell${detailOpen ? ' diagram-shell-with-detail' : ''}`}
      >
        <section className="diagram-canvas">
          {selectedDeviceName ? (
            <ContainerDiagram
              key={selectedDeviceName}
              diagram={diagram}
              selectedContainerId={selectedContainerId}
              selectedStackCanvasId={selectedStackCanvasId}
              canCreateStack={canCreateStack}
              onContainerSelect={setSelectedContainerId}
              onStackSelect={setSelectedStackCanvasId}
              onCreateStack={() => setIsCreateOpen(true)}
            />
          ) : null}
        </section>
        {selectedContainer ? (
          <ContainerDetailPane
            container={selectedContainer}
            onClose={() => setSelectedContainerId(null)}
            isActionPending={isActionPending || isRefreshing}
            onStart={() => {
              void runContainerAction('start', selectedContainer)
            }}
            onStop={() => {
              void runContainerAction('stop', selectedContainer)
            }}
          />
        ) : null}
        {selectedStack && selectedDeviceName && !selectedContainer ? (
          <StackDetailPane
            deviceName={selectedDeviceName}
            stack={selectedStack}
            onClose={() => setSelectedStackCanvasId(null)}
            isActionPending={isActionPending || isRefreshing}
            onDeploy={() => {
              if (selectedStack.id) {
                void deployStack(selectedStack.id, selectedStack.projectName)
              }
            }}
          />
        ) : null}
      </main>

      <CreateStackDialog
        open={isCreateOpen}
        isSaving={isCreating}
        onOpenChange={setIsCreateOpen}
        onCreate={createStack}
      />
    </div>
  )
}
