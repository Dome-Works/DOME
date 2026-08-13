# HomelabDocs

Infrastructure visualization for Docker environments. A Vite + React client renders a [React Flow](https://reactflow.dev/) diagram of containers (with status) loaded from one or more Docker Engines through a read-only ASP.NET Core API.

> [!CAUTION]
> This application is intended to be used in secure and private homelab-networks. There is no authentication (yet), the communication to the Docker socket is not covered by TLS (for now), and the application is prone to vulnerabilities as the development is still in it's early stages.

> [!NOTE]
> Don't be afraid to give feedback or make feature requests by creating [Issues](https://github.com/HomelabDocs/HomelabDocs/issues). I am a solo-engineer with a wild idea, and would love to tailor this idea into an application that is beginner friendly and usable by everyone (with beginner docker knowledge).


## Repository layout

```text
src/
  HomelabDocs.Server/   .NET solution root (API, Business, Shared)
  HomelabDocs.Client/   Vite + React + React Flow frontend
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (see `src/HomelabDocs.Server/global.json`)
- [Node.js](https://nodejs.org/) 22+ and npm
- One or more running [Docker Engine](https://docs.docker.com/engine/) instances reachable via Unix socket or TCP

## Docker devices

The API reads Docker Engine connections from configuration (`Docker:Connections`). Each entry becomes a **device** in the UI (one dashboard tab per device).

There is no implicit default: only the connections you configure are loaded, and the API fails to start when the list is empty. Add the local socket explicitly when you want it.

```json
{
  "Docker": {
    "Connections": [
      {
        "Name": "Local",
        "Endpoint": "unix:///var/run/docker.sock"
      },
      {
        "Name": "raspberrypi",
        "Endpoint": "tcp://ip-address:2375"
      }
    ]
  }
}
```

`Name` must be unique (case-insensitive). It is used as the device key in the API and as the tab label in the UI.

Configure with `appsettings`, environment variables, or Compose:

| Source | Example |
| --- | --- |
| `appsettings.json` | `"Docker": { "Connections": [ { "Name": "Remote", "Endpoint": "tcp://192.168.1.10:2375" } ] }` |
| Environment | `Docker__Connections__0__Name=Local`, `Docker__Connections__0__Endpoint=unix:///var/run/docker.sock` |
| Compose / `.env` | Commented `Docker__Connections__*` entries under the `api` service |

Add more devices with the next index (`Docker__Connections__1__*`, `__2__`, and so on). An index that also exists in `appsettings.json` overrides that entry rather than adding a new one. You can mix a local Unix socket with remote `tcp://` endpoints.

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
- Devices endpoint: `GET /api/devices`
- Containers endpoint: `GET /api/devices/{name}/containers`

### Client

```bash
cd src/HomelabDocs.Client
npm run dev
```

Typical Client URL: [http://localhost:5173](http://localhost:5173)

The Vite dev server proxies `/api` to `http://localhost:5100`. Only the API process accesses Docker.

## Run with Docker Compose

Published images are built and pushed to the GitHub Container Registry when a [GitHub Release](https://github.com/HomelabDocs/HomelabDocs/releases) is created:

| Service | Image |
| --- | --- |
| `api` | [`ghcr.io/homelabdocs/server`](https://ghcr.io/homelabdocs/server) |
| `client` | [`ghcr.io/homelabdocs/client`](https://ghcr.io/homelabdocs/client) |

Stable releases also publish `latest`, `MAJOR.MINOR`, and `MAJOR` tags. Pre-releases publish only the exact version tag (for example `0.1.0-beta.1`).

From the repository root, pull and start the published images:

```bash
docker compose up
```

Pin a specific release by setting the image tags in `docker-compose.yml`, for example `ghcr.io/homelabdocs/server:0.1.0` and `ghcr.io/homelabdocs/client:0.1.0`.

To build images locally instead of pulling, use the Dockerfiles under `src/` (see `.github/workflows/docker.yml` for the build contexts).

This starts two containers:

| Service | Image role | Host URL |
| --- | --- | --- |
| `api` | ASP.NET Core API | [http://localhost:5100](http://localhost:5100) (Swagger at `/swagger`) |
| `client` | Nginx static UI + `/api` reverse proxy | [http://localhost:5200](http://localhost:5200) |

The API connects to Docker using `Docker:Connections` (Compose defaults to one local device on `unix:///var/run/docker.sock`) and mounts the host socket read-only when using that local path. The client container proxies browser `/api` requests to the `api` service on the Compose network, so the UI keeps using relative `/api` paths.

### Multiple Docker Engines

Compose adds no devices of its own. Either edit `Docker:Connections` in `appsettings.json`, or uncomment the `Docker__Connections__*` block in `docker-compose.yml` and set values (env or `.env`):

```bash
DOCKER_DEVICE_1_NAME=Remote \
DOCKER_DEVICE_1_ENDPOINT=tcp://192.168.1.10:2375 \
docker compose up
```

When using only remote `tcp://` endpoints, you can remove the `api` service socket volume mount from `docker-compose.yml` (it is unused).

If the API cannot list containers with the local socket, check socket permissions on the host (the container process must be able to read the mounted socket).

## Current limitations

- No TLS support for remote Docker TCP endpoints
- No background synchronization or Docker events
- No persistence, database, authentication, or configuration UI
- Diagram shows container nodes only (no hosts, networks, volumes, or edges)
- No Windows named-pipe support
