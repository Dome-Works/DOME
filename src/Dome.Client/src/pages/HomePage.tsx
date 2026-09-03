import { useCallback, useEffect, useState } from 'react'
import { Link } from 'react-router'

import { fetchDeviceContainers } from '@/api/containers'
import { fetchSockets } from '@/api/sockets'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import type { SocketRecord } from '@/types/sockets'

type HomeState =
  | { status: 'loading' }
  | { status: 'error'; message: string }
  | {
      status: 'ready'
      socketCount: number
      containerCount: number
      runningCount: number
      unreachableCount: number
    }

export function HomePage() {
  const [state, setState] = useState<HomeState>({ status: 'loading' })

  const loadSummary = useCallback(async (signal?: AbortSignal) => {
    setState({ status: 'loading' })

    try {
      const response = await fetchSockets(signal)
      const sockets = response.sockets ?? []

      if (signal?.aborted) {
        return
      }

      const results = await Promise.all(
        sockets.map(async (socket: SocketRecord) => {
          try {
            const containersResponse = await fetchDeviceContainers(
              socket.name,
              signal,
            )
            const containers = containersResponse.containers ?? []
            const runningCount = containers.filter(
              (container) => container.state.toLowerCase() === 'running',
            ).length

            return {
              containerCount: containers.length,
              runningCount,
              unreachable: false,
            }
          } catch {
            return { containerCount: 0, runningCount: 0, unreachable: true }
          }
        }),
      )

      if (signal?.aborted) {
        return
      }

      setState({
        status: 'ready',
        socketCount: sockets.length,
        containerCount: results.reduce(
          (sum, result) => sum + result.containerCount,
          0,
        ),
        runningCount: results.reduce(
          (sum, result) => sum + result.runningCount,
          0,
        ),
        unreachableCount: results.filter((result) => result.unreachable).length,
      })
    } catch {
      if (signal?.aborted) {
        return
      }

      setState({
        status: 'error',
        message: 'Unable to load dashboard data. Confirm that the API is running.',
      })
    }
  }, [])

  useEffect(() => {
    const controller = new AbortController()
    void loadSummary(controller.signal)
    return () => controller.abort()
  }, [loadSummary])

  return (
    <div className="flex flex-1 flex-col gap-6 overflow-auto p-6">
      <div>
        <h2 className="text-2xl font-semibold tracking-tight">Dashboard</h2>
        <p className="text-sm text-muted-foreground">
          Summary of registered sockets and containers they report.
        </p>
      </div>

      {state.status === 'loading' ? (
        <p className="text-sm text-muted-foreground">Loading summary…</p>
      ) : null}

      {state.status === 'error' ? (
        <div className="flex items-center gap-3 text-sm text-destructive">
          <span>{state.message}</span>
          <Button variant="outline" size="sm" onClick={() => void loadSummary()}>
            Retry
          </Button>
        </div>
      ) : null}

      {state.status === 'ready' ? (
        <>
          <div className="grid grid-cols-4 gap-4">
            <Card>
              <CardHeader>
                <CardDescription>Registered sockets</CardDescription>
                <CardTitle className="text-3xl">{state.socketCount}</CardTitle>
              </CardHeader>
            </Card>
            <Card>
              <CardHeader>
                <CardDescription>Containers</CardDescription>
                <CardTitle className="text-3xl">{state.containerCount}</CardTitle>
              </CardHeader>
            </Card>
            <Card>
              <CardHeader>
                <CardDescription>Running</CardDescription>
                <CardTitle className="text-3xl">{state.runningCount}</CardTitle>
              </CardHeader>
            </Card>
            <Card>
              <CardHeader>
                <CardDescription>Unreachable sockets</CardDescription>
                <CardTitle className="text-3xl">
                  {state.unreachableCount}
                </CardTitle>
              </CardHeader>
            </Card>
          </div>

          {state.socketCount === 0 ? (
            <Card>
              <CardHeader>
                <CardTitle>No sockets registered</CardTitle>
                <CardDescription>
                  Register a HomelabDocs Socket so the server can query Docker
                  on that host.
                </CardDescription>
              </CardHeader>
              <CardContent>
                <Button asChild>
                  <Link to="/sockets">Register a socket</Link>
                </Button>
              </CardContent>
            </Card>
          ) : (
            <div className="flex gap-2">
              <Button asChild>
                <Link to="/diagrams">Open diagrams</Link>
              </Button>
              <Button asChild variant="outline">
                <Link to="/sockets">Manage sockets</Link>
              </Button>
            </div>
          )}
        </>
      ) : null}
    </div>
  )
}
