# HomelabDocs

Infrastructure visualization for Docker environments. A Blazor Web App renders a Cytoscape.js diagram of running containers loaded from a local Docker Engine through a read-only ASP.NET Core API.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (see `global.json`)
- [Node.js](https://nodejs.org/) and npm (only to install and copy Cytoscape.js assets for the Web project)
- A running [Docker Engine](https://docs.docker.com/engine/) that exposes the local Unix socket

## Docker socket

The API connects to a hardcoded Docker endpoint:

```text
unix:///var/run/docker.sock
```

Defined in `HomelabDocs.Business` as `DockerConnectionOptions.DefaultDockerSocket`.

Linux and macOS commonly use this path. Docker Desktop environments can differ, and socket permissions may prevent access. Windows named-pipe support is not included in this phase.

## Installation

From the repository root:

```bash
dotnet restore
cd src/HomelabDocs.Web
npm install
```

`npm install` installs Cytoscape.js and copies its ESM build into `wwwroot/lib/cytoscape/`.

## Run

Start the API and Web projects separately.

### API

```bash
dotnet run --project src/HomelabDocs.Api --launch-profile http
```

- API base URL: `http://localhost:5100`
- HTTPS profile also available: `https://localhost:7100` (and `http://localhost:5100`)
- Swagger UI: [http://localhost:5100/swagger](http://localhost:5100/swagger)
- Containers endpoint: `GET /api/containers`

### Web

```bash
dotnet run --project src/HomelabDocs.Web
```

Typical Web URLs:

- `https://localhost:7193`
- `http://localhost:5172`

The Blazor server calls the API over HTTP at `http://localhost:5100` through the shared Refit interface. Only the API process accesses the Docker socket.

You can also start both projects from Rider or Visual Studio by selecting multiple startup projects.

## Current limitations

- Hardcoded Docker socket and API base URL
- No Docker Compose configuration
- No background synchronization or Docker events
- No persistence, database, authentication, or configuration UI
- Diagram shows container nodes only (no hosts, networks, volumes, or edges)
- No Windows named-pipe support

Configuration is expected to move into Docker Compose environment variables in a later phase.
