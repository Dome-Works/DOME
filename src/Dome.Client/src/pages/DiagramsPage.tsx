import { useCallback, useEffect, useState } from 'react'
import { Link } from 'react-router'

import {
  deleteDeviceContainer,
  fetchDeviceDiagram,
  startDeviceContainer,
  stopDeviceContainer,
} from '@/api/containers'
import { fetchDeviceImageVersionChecks } from '@/api/imageVersionChecks'
import { fetchDevices } from '@/api/devices'
import { createDeviceStack, deployDeviceStack } from '@/api/stacks'
import { ContainerDetailPane } from '@/components/ContainerDetailPane'
import { ContainerDiagram } from '@/components/ContainerDiagram'
import { DiagramDetailSlot } from '@/components/DiagramDetailSlot'
import { CreateStackDialog } from '@/components/CreateStackDialog'
import { DeviceTabs } from '@/components/DeviceTabs'
import { StackDetailPane } from '@/components/StackDetailPane'
import { Button } from '@/components/ui/button'
import { toast } from 'sonner'
import type { Container } from '@/types/containers'
import type { DeviceDiagram, DiagramStack } from '@/types/diagram'
import type { ContainerImageVersionCheck } from '@/types/imageVersionChecks'
import { stackCanvasId } from '@/types/diagram'
import type { Device } from '@/types/devices'

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

type RenderedDetail =
  | { kind: 'container'; container: Container }
  | { kind: 'stack'; stack: DiagramStack; deviceName: string }

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
  const [diagramDeviceName, setDiagramDeviceName] = useState<string | null>(null)
  const [isRefreshing, setIsRefreshing] = useState(false)
  const [isActionPending, setIsActionPending] = useState(false)
  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isCreating, setIsCreating] = useState(false)
  const [renderedDetail, setRenderedDetail] = useState<RenderedDetail | null>(
    null,
  )
  const [versionChecks, setVersionChecks] = useState<ContainerImageVersionCheck[]>([])

  if (selectedDeviceName !== diagramDeviceName) {
    setDiagramDeviceName(selectedDeviceName)
    setSelectedContainerId(null)
    setSelectedStackCanvasId(null)
    setDiagramState(selectedDeviceName ? { status: 'loading' } : { status: 'idle' })
    setVersionChecks([])
    setIsRefreshing(false)
  }

  const loadDevices = useCallback(async (signal?: AbortSignal) => {
    try {
      const response = await fetchDevices(signal)
      const devices = response.devices ?? []

      if (signal?.aborted) {
        return
      }

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

  const loadDiagram = useCallback(
    async (deviceName: string, signal?: AbortSignal) => {
      try {
        const diagram = await fetchDeviceDiagram(deviceName, signal)

        if (signal?.aborted) {
          return
        }

        if (diagram.stacks.length === 0 && diagram.containers.length === 0) {
          setDiagramState({ status: 'empty' })
          setVersionChecks([])
        } else {
          setDiagramState({ status: 'ready', diagram })
          try {
            const checks = await fetchDeviceImageVersionChecks(deviceName, signal)
            if (!signal?.aborted) {
              setVersionChecks(checks.checks ?? [])
            }
          } catch {
            if (!signal?.aborted) {
              setVersionChecks([])
            }
          }
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
        setVersionChecks([])
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

    void (async () => {
      try {
        const response = await fetchDevices(controller.signal)
        const devices = response.devices ?? []

        if (controller.signal.aborted) {
          return
        }

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
        if (controller.signal.aborted) {
          return
        }

        setSelectedDeviceName(null)
        setDevicesState({
          status: 'error',
          message: 'Unable to load devices. Confirm that the API is running.',
        })
      }
    })()

    return () => controller.abort()
  }, [])

  useEffect(() => {
    if (!selectedDeviceName) {
      return
    }

    const deviceName = selectedDeviceName
    const controller = new AbortController()

    void (async () => {
      try {
        const diagram = await fetchDeviceDiagram(deviceName, controller.signal)

        if (controller.signal.aborted) {
          return
        }

        if (diagram.stacks.length === 0 && diagram.containers.length === 0) {
          setDiagramState({ status: 'empty' })
          setVersionChecks([])
        } else {
          setDiagramState({ status: 'ready', diagram })
          try {
            const checks = await fetchDeviceImageVersionChecks(
              deviceName,
              controller.signal,
            )
            if (!controller.signal.aborted) {
              setVersionChecks(checks.checks ?? [])
            }
          } catch {
            if (!controller.signal.aborted) {
              setVersionChecks([])
            }
          }
        }
      } catch {
        if (controller.signal.aborted) {
          return
        }

        setDiagramState({
          status: 'error',
          message:
            'Unable to load containers. Confirm that Docker is reachable for this device.',
        })
        setVersionChecks([])
      } finally {
        if (!controller.signal.aborted) {
          setIsRefreshing(false)
        }
      }
    })()

    return () => controller.abort()
  }, [selectedDeviceName])

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
        onRetry: () => {
          setDevicesState({ status: 'loading' })
          void loadDevices()
        },
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
            ? () => {
                setIsRefreshing(true)
                setDiagramState({ status: 'loading' })
                void loadDiagram(selectedDeviceName)
              }
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
  const detailOpen = selectedContainer !== null || selectedStack !== null

  if (selectedContainer) {
    if (
      renderedDetail?.kind !== 'container' ||
      renderedDetail.container !== selectedContainer
    ) {
      setRenderedDetail({ kind: 'container', container: selectedContainer })
    }
  } else if (selectedStack && selectedDeviceName) {
    if (
      renderedDetail?.kind !== 'stack' ||
      renderedDetail.stack !== selectedStack ||
      renderedDetail.deviceName !== selectedDeviceName
    ) {
      setRenderedDetail({
        kind: 'stack',
        stack: selectedStack,
        deviceName: selectedDeviceName,
      })
    }
  }

  const runContainerAction = useCallback(
    async (
      action: 'start' | 'stop' | 'delete',
      container: Container,
      removeVolumes = false,
    ) => {
      if (!selectedDeviceName) {
        return
      }

      setIsActionPending(true)
      try {
        if (action === 'start') {
          await startDeviceContainer(selectedDeviceName, container.id)
          toast.success(`Started ${container.name}.`)
        } else if (action === 'stop') {
          await stopDeviceContainer(selectedDeviceName, container.id)
          toast.success(`Stopped ${container.name}.`)
        } else {
          await deleteDeviceContainer(
            selectedDeviceName,
            container.id,
            removeVolumes,
          )
          toast.success(`Deleted ${container.name}.`)
          setSelectedContainerId(null)
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
              setIsRefreshing(true)
              setDiagramState({ status: 'loading' })
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
        <div className={statusBanner.className} aria-live="polite">
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

      <main className="diagram-shell">
        <section className="diagram-canvas">
          {selectedDeviceName ? (
            <ContainerDiagram
              key={selectedDeviceName}
              diagram={diagram}
              versionChecks={versionChecks}
              selectedContainerId={selectedContainerId}
              selectedStackCanvasId={selectedStackCanvasId}
              canCreateStack={canCreateStack}
              onContainerSelect={setSelectedContainerId}
              onStackSelect={setSelectedStackCanvasId}
              onCreateStack={() => setIsCreateOpen(true)}
            />
          ) : null}
        </section>
        <DiagramDetailSlot
          open={detailOpen}
          onCloseAnimationEnd={() => {
            if (!detailOpen) {
              setRenderedDetail(null)
            }
          }}
        >
          {renderedDetail?.kind === 'container' ? (
            <ContainerDetailPane
              container={renderedDetail.container}
              versionCheck={
                versionChecks.find(
                  (check) => check.containerId === renderedDetail.container.id,
                ) ?? null
              }
              onClose={() => setSelectedContainerId(null)}
              isActionPending={isActionPending || isRefreshing}
              onStart={() => {
                void runContainerAction('start', renderedDetail.container)
              }}
              onStop={() => {
                void runContainerAction('stop', renderedDetail.container)
              }}
              onDelete={(removeVolumes) =>
                runContainerAction('delete', renderedDetail.container, removeVolumes)
              }
            />
          ) : null}
          {renderedDetail?.kind === 'stack' ? (
            <StackDetailPane
              deviceName={renderedDetail.deviceName}
              stack={renderedDetail.stack}
              onClose={() => setSelectedStackCanvasId(null)}
              isActionPending={isActionPending || isRefreshing}
              onDeploy={() => {
                if (renderedDetail.stack.id) {
                  void deployStack(
                    renderedDetail.stack.id,
                    renderedDetail.stack.projectName,
                  )
                }
              }}
            />
          ) : null}
        </DiagramDetailSlot>
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
