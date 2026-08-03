# HomelabDocs

Infrastructure visualization for Docker environments, built as a .NET Blazor Web App with Cytoscape.js.

This initial version is a standalone interactive diagram foundation. It renders a hardcoded example graph and does not yet connect to Docker or load external configuration.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) and npm (required only to install and copy frontend assets)

## Installation

From the repository root:

```bash
dotnet restore
cd src/HomelabDocs.Web
npm install
```

`npm install` installs Cytoscape.js and copies its ESM build into `wwwroot/lib/cytoscape/` via the `postinstall` script.

## How Cytoscape.js is included

- Cytoscape.js is installed from npm (`cytoscape`), not loaded from a CDN.
- After install, `node_modules/cytoscape/dist/cytoscape.esm.min.mjs` is copied to `wwwroot/lib/cytoscape/cytoscape.esm.min.js`.
- The Blazor project also copies that file before `dotnet build` as a safety net. If `npm install` has not been run, the build fails with a clear error.
- The browser loads the diagram through the ES module at `wwwroot/js/cytoscapeDiagram.js`, which imports the local Cytoscape build.
- At runtime only ASP.NET Core / Blazor is required. Node is not needed to run the app.

## Run

```bash
cd src/HomelabDocs.Web
dotnet run
```

Open the URL printed by `dotnet run` (typically `https://localhost:7xxx`).

## Current limitations

- Example graph data is hardcoded in C#
- No Docker daemon or Compose integration
- No configuration screens
- No authentication, database, or persistence
- No API endpoints beyond what Blazor needs

Docker integration and configuration via Docker Compose environment variables will be added later.
