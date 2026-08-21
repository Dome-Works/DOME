
[![OpenSSF Best Practices](https://www.bestpractices.dev/projects/14152/badge)](https://www.bestpractices.dev/projects/14152)

# HomelabDocs

Infrastructure visualization for Docker environments. A Vite + React client renders a [React Flow](https://reactflow.dev/) diagram of containers (with status) loaded from HomelabDocs.Server. The Server never talks to Docker Engine. Each host runs HomelabDocs.Socket, which has access to the local Docker socket and is queried by the Server over HTTP (Refit).

> [!CAUTION]
> This application is intended to be used in secure and private homelab-networks. There is no authentication (yet). HomelabDocs.Socket has full access to the local Docker Engine. The application is prone to vulnerabilities as the development is still in its early stages.

> [!NOTE]
> Don't be afraid to give feedback or make feature requests by creating [Issues](https://github.com/HomelabDocs/HomelabDocs/issues). I am a solo-engineer with a wild idea, and would love to tailor this idea into an application that is beginner friendly and usable by everyone (with beginner docker knowledge).

> [!WARNING]
> This repository / application is still a work-in-progress, and is prone to breaking changes. Untill V1.0.0 releases, this should be considered NOT PRODUCTION READY.

## Repository layout

```text
HomelabDocs.slnx            Single .NET solution (Server + Socket)
Directory.Build.props       Shared SDK / language settings
Directory.Packages.props    Central package versions
global.json                 .NET SDK pin
src/
  HomelabDocs.Server/       API, Business, Domain, Shared
  HomelabDocs.Socket/       Privileged Docker socket agent (FastEndpoints, no database)
  HomelabDocs.Client/       Vite + React + React Flow frontend
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (see `global.json`)
- [Node.js](https://nodejs.org/) 22+ and npm
- A running [Docker Engine](https://docs.docker.com/engine/) on each host where HomelabDocs.Socket is deployed (Unix socket only)

## Architecture

The Client talks only to HomelabDocs.Server. The Server stores socket registrations (id, name, HTTP address) in SQLite and calls one or more HomelabDocs.Socket instances via Refit. Each Socket process always uses the local Docker endpoint (`unix:///var/run/docker.sock` by default). Remote Docker Engine URLs (`tcp://`) are not supported.

Register sockets on the Server after they are running. Example:

```bash
curl -X POST http://localhost:5100/api/sockets \
  -H 'Content-Type: application/json' \
  -d '{"name":"Local","address":"http://127.0.0.1:5110"}'
```

On Docker Compose, the Socket service is reachable as `http://socket:8080` from the `api` container.

`Name` must be unique (case-insensitive). It is used as the device key in `GET /api/devices` and as the tab label in the UI.

## Persistence

SQLite is the database. All EF Core types and migrations live in `HomelabDocs.Domain`. The API applies pending migrations on every startup, including after pulling a new Docker image and running `docker compose up`. Seed data, when needed, should go into those migrations so `__EFMigrationsHistory` is the only apply log.

The database is a save directory containing `homelabdocs.db`. Copy or share that whole directory for backups.

| Environment | Location |
| --- | --- |
| Docker | `/var/lib/homelabdocs/homelabdocs.db` (named volume `homelabdocs-data`) |
| Local Development | `%LOCALAPPDATA%/HomelabDocs/homelabdocs.db` (Windows) or `$HOME/.local/share/HomelabDocs/homelabdocs.db` |

Override the path with `ConnectionStrings:HomelabDocs` / `ConnectionStrings__HomelabDocs`. An empty value in Development falls back to local application data.

To add a migration from the repository root:

```bash
dotnet ef migrations add <Name> --project src/HomelabDocs.Server/HomelabDocs.Domain --startup-project src/HomelabDocs.Server/HomelabDocs.Domain
```

## Installation

From the repository root:

```bash
dotnet restore HomelabDocs.slnx
cd src/HomelabDocs.Client
npm install
```

## Run locally

Start Socket, Server, and Client separately.

### Socket

```bash
dotnet run --project src/HomelabDocs.Socket/HomelabDocs.Socket.Api --launch-profile http
```

- Socket base URL: `http://localhost:5110`
- HTTPS profile also available: `https://localhost:7110` (and `http://localhost:5110`)
- Swagger UI: [http://localhost:5110/swagger](http://localhost:5110/swagger)
- Health: `GET /api/health`
- Containers: `GET /api/containers`

Override the local Docker Unix socket with `Docker:Endpoint` / `Docker__Endpoint` (unix scheme only).

### Server (API)

```bash
dotnet run --project src/HomelabDocs.Server/HomelabDocs.Api --launch-profile http
```

- API base URL: `http://localhost:5100`
- HTTPS profile also available: `https://localhost:7100` (and `http://localhost:5100`)
- Swagger UI: [http://localhost:5100/swagger](http://localhost:5100/swagger)
- Sockets: `GET/POST /api/sockets`, `GET/PUT/DELETE /api/sockets/{id}`
- Devices endpoint: `GET /api/devices` (registered sockets)
- Containers endpoint: `GET /api/devices/{name}/containers`

### Client

```bash
cd src/HomelabDocs.Client
npm run dev
```

Typical Client URL: [http://localhost:5173](http://localhost:5173)

The Vite dev server proxies `/api` to `http://localhost:5100`. Only HomelabDocs.Socket accesses Docker.

## Run with Docker Compose

Published images are built and pushed to the GitHub Container Registry when a [GitHub Release](https://github.com/HomelabDocs/HomelabDocs/releases) is created:

| Service | Image |
| --- | --- |
| `api` | [`ghcr.io/homelabdocs/server`](https://ghcr.io/homelabdocs/server) |
| `socket` | [`ghcr.io/homelabdocs/socket`](https://ghcr.io/homelabdocs/socket) |
| `client` | [`ghcr.io/homelabdocs/client`](https://ghcr.io/homelabdocs/client) |

Stable releases also publish `latest`, `MAJOR.MINOR`, and `MAJOR` tags. Pre-releases publish only the exact version tag (for example `0.1.0-beta.1`).

From the repository root, pull and start the published images:

```bash
docker compose up
```

Pin a specific release by setting the image tags in `docker-compose.yml`, for example `ghcr.io/homelabdocs/server:0.1.0`, `ghcr.io/homelabdocs/socket:0.1.0`, and `ghcr.io/homelabdocs/client:0.1.0`.

To build images locally instead of pulling, use the Dockerfiles under `src/` (see `.github/workflows/docker.yml` for the build contexts). Server and Socket images use the repository root as context so they can restore from the shared `Directory.Build.props` and `Directory.Packages.props`.

This starts three containers:

| Service | Image role | Host URL |
| --- | --- |
| `api` | ASP.NET Core API | [http://localhost:5100](http://localhost:5100) (Swagger at `/swagger`) |
| `socket` | Local Docker agent | [http://localhost:5110](http://localhost:5110) (Swagger at `/swagger`) |
| `client` | Nginx static UI + `/api` reverse proxy | [http://localhost:5200](http://localhost:5200) |

SQLite is stored in the named volume `homelabdocs-data` at `/var/lib/homelabdocs` so it survives container recreate and image pulls. For a host-visible folder (for example a later rclone/Google Drive sidecar), replace that volume with a bind mount such as `./data:/var/lib/homelabdocs`. The `socket` service mounts the host Docker socket. The client container proxies browser `/api` requests to the `api` service on the Compose network, so the UI keeps using relative `/api` paths.

Register the Compose socket once the stack is up (from another container on the Compose network use `http://socket:8080`; from the host use `http://127.0.0.1:5110`):

```bash
curl -X POST http://localhost:5100/api/sockets \
  -H 'Content-Type: application/json' \
  -d '{"name":"Local","address":"http://socket:8080"}'
```

If the Socket cannot list containers, check socket permissions on the host (the Socket process must be able to access the mounted Docker socket).

### Multiple Docker Engines

Run HomelabDocs.Socket on each machine that has a Docker Engine, then register each Socket’s HTTP address on the Server (`POST /api/sockets`). The Server does not connect to Docker Engine ports.

## Current limitations

- No TLS or authentication between Server and Socket
- No background synchronization or Docker events
- No authentication or configuration UI
- Diagram shows container nodes only (no hosts, networks, volumes, or edges)
- No Windows named-pipe support
- Socket talks only to a local Unix Docker socket (no remote `tcp://` Engine)
