# HomelabDocs

Infrastructure visualization for Docker environments. A Vite + React client renders a [React Flow](https://reactflow.dev/) diagram of running containers loaded from a local Docker Engine through a read-only ASP.NET Core API.

## Repository layout

```text
src/
  HomelabDocs.Server/   .NET solution root (API, Business, Shared)
  HomelabDocs.Client/   Vite + React + React Flow frontend
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (see `src/HomelabDocs.Server/global.json`)
- [Node.js](https://nodejs.org/) 22+ and npm
- A running [Docker Engine](https://docs.docker.com/engine/) reachable via Unix socket or TCP

## Docker endpoint

The API reads the Docker Engine URI from configuration (`Docker:Endpoint`).

Default:

```text
unix:///var/run/docker.sock
```

Override with `appsettings`, environment variables, or Compose:

| Source | Example |
| --- | --- |
| `appsettings.json` | `"Docker": { "Endpoint": "tcp://192.168.1.10:2375" }` |
| Environment | `Docker__Endpoint=tcp://192.168.1.10:2375` |
| Compose / `.env` | `DOCKER_ENDPOINT=tcp://192.168.1.10:2375` |

Linux and macOS commonly use the default Unix socket. Docker Desktop environments can differ, and socket permissions may prevent access. Windows named-pipe support is not included in this phase. TLS-protected remote endpoints are not configured yet (plain `tcp://` only).

## Installation

From the repository root:

```bash
dotnet restore src/HomelabDocs.Server/HomelabDocs.slnx
cd src/HomelabDocs.Client
npm install
```

## Run locally

Start the API and Client separately.

### API

```bash
dotnet run --project src/HomelabDocs.Server/HomelabDocs.Api --launch-profile http
```

- API base URL: `http://localhost:5100`
- HTTPS profile also available: `https://localhost:7100` (and `http://localhost:5100`)
- Swagger UI: [http://localhost:5100/swagger](http://localhost:5100/swagger)
- Containers endpoint: `GET /api/containers`

### Client

```bash
cd src/HomelabDocs.Client
npm run dev
```

Typical Client URL: [http://localhost:5173](http://localhost:5173)

The Vite dev server proxies `/api` to `http://localhost:5100`. Only the API process accesses the Docker socket.

## Run with Docker Compose

From the repository root:

```bash
docker compose up --build
```

This starts two containers:

| Service | Image role | Host URL |
| --- | --- | --- |
| `api` | ASP.NET Core API | [http://localhost:5100](http://localhost:5100) (Swagger at `/swagger`) |
| `client` | Nginx static UI + `/api` reverse proxy | [http://localhost:8080](http://localhost:8080) |

The API connects to Docker using `Docker__Endpoint` (default `unix:///var/run/docker.sock`) and mounts the host socket read-only when using that local path. The client container proxies browser `/api` requests to the `api` service on the Compose network, so the UI keeps using relative `/api` paths.

### Remote Docker Engine

Point Compose at a remote Engine over TCP (for example an exposed Docker API on another host):

```bash
DOCKER_ENDPOINT=tcp://192.168.1.10:2375 docker compose up --build
```

Or set `DOCKER_ENDPOINT` in a `.env` file next to `docker-compose.yml`. When using a remote `tcp://` endpoint, you can remove the `api` service socket volume mount from `docker-compose.yml` (it is unused).

If the API cannot list containers with the local socket, check socket permissions on the host (the container process must be able to read the mounted socket).

## Current limitations

- No TLS support for remote Docker TCP endpoints
- No background synchronization or Docker events
- No persistence, database, authentication, or configuration UI
- Diagram shows container nodes only (no hosts, networks, volumes, or edges)
- No Windows named-pipe support
