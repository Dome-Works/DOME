# Changelog

This file records notable changes to DOME for people who run and use the
application. Changes are grouped by release.

## [Unreleased]

## [v0.3.0] - 2026-09-03

### Added

- Start and stop Docker containers from the container details panel, without
  switching to the Docker command line.

### Breaking Changes

- The project is renamed from HomelabDocs to DOME (Docker Orchestration &
  Management Engine). The GitHub organization is `Dome-Works` and the
  repository is `DOME`.
- Published images are now `ghcr.io/dome-works/{server,socket,client}`. Update
  Compose and pull commands that still use `ghcr.io/homelabdocs/...`.
- Server SQLite settings and paths changed: use `ConnectionStrings:Dome`
  (`ConnectionStrings__Dome`), Compose volume `dome-data` at
  `/var/lib/dome/dome.db`, and local app-data `Dome/dome.db`. Existing
  HomelabDocs databases are not picked up automatically; copy or remount the
  old file if you need to keep Socket registrations.

## [v0.2.2] - 2026-09-01

### Added

- Inspect an individual container from a details panel on the Diagrams page.
- View mounted Docker volumes for a container, including name, mount
  destination, and storage usage.
- See whether each mounted volume is read-only or read-write.
- See whether each registered Socket is reachable from HomelabDocs.Server.

### Improved

- Make container state easier to scan with clearer status indicators.
- Show total mounted-volume size on each container in the diagram.

## [v0.2.1] - 2026-08-27

### Added

- Published Docker images now support ARM64 as well as AMD64, so HomelabDocs
  is easier to run on ARM-based homelab hardware such as a Raspberry Pi.

## [v0.2.0] - 2026-08-27

### Added

- Discover containers through Socket agents instead of connecting HomelabDocs.Server
  directly to Docker Engine.
- Register, edit, and delete Docker hosts from the Sockets page.
- Persist Socket registrations in SQLite, including a named Docker volume in
  the published Compose stack so data survives image updates.
- Dashboard counts for registered Sockets, containers, running containers, and
  unreachable Sockets.
- A separate diagram tab for each registered Docker host.
- A published `socket` image alongside the existing Server and Client images.
- A development Compose file for building and running the full stack from source.

### Improved

- Navigation now uses a header and sidebar, with dedicated Home, Sockets, and
  Diagrams pages.

### Breaking Changes

- HomelabDocs.Server no longer talks to Docker Engine. Each Docker host needs a
  HomelabDocs.Socket agent, and you must register that Socket in the UI.
- Docker hosts are no longer configured with `Docker:Connections` in Server
  settings. Register them on the Sockets page instead.
- Remote Docker Engine `tcp://` endpoints are no longer supported. Only a local
  Unix Docker socket on the Socket agent is supported.
- The published Compose stack now includes a `socket` service. The Docker
  socket is mounted there, not on the Server container. Register the Compose
  Socket as `http://socket:8080`.

## [v0.1.0] - 2026-08-13

### Added

- First public release of HomelabDocs as a self-hosted Docker infrastructure
  viewer.
- Diagram of containers grouped by Docker Compose stack.
- Container state shown in the diagram, including containers that are not
  running.
- Published Docker images for HomelabDocs.Server and HomelabDocs.Client.
- Support for more than one Docker Engine through configuration, using a local
  Unix socket or a remote `tcp://` endpoint.

## Release process

1. Add new notable changes under `[Unreleased]`.
2. When a version is released, move those changes into a versioned section such
   as `[v0.3.0] - YYYY-MM-DD`.
3. Record the release date on that section.
4. Create a new empty `[Unreleased]` section at the top for the next cycle.
