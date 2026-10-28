# Graph Report - DOME  (2026-10-28)

## Corpus Check
- 304 files · ~58,423 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2246 nodes · 5139 edges · 124 communities (116 shown, 8 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 371 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `aed05a1d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IDomeSocketApi
- RecordingProcessRunner
- IStackRepository
- Client Layout and Routing
- Task
- DateTimeOffset
- InlineData
- Socket and Compose Editor UI
- Build Workflow
- Container API Client
- CodeQL Workflow
- Endpoint
- Stack Service
- Docker Workflow
- SocketsPage.tsx
- Dev Compose Config
- Scorecard Workflow
- GetDeviceStackRequest
- HTTP Request Primitives
- Container Node Diagram UI
- DeleteContainersResponse
- Device Container API and Errors
- Compose Sample Config
- Endpoint
- Client Lint and Tooling Config
- Zizmor Workflow Config
- TypeScript App Config
- Image Version Check Service
- ContainerVolumeDto
- Dome.Business.Devices
- ContainerVolumeViewModel
- GetDeviceStacksEndpoint
- ContainerVolumeResponse
- The Design System Page
- fastendpoints
- shadcn UI Config
- TypeScript Node Config
- GhcrRegistryProvider
- Docker Registry Tests and Image Endpoints
- ContainerMapperProperties
- Docker Build Args Config
- DeleteDeviceStackRequest
- Oxlint Config
- Dome.Api.Endpoints.Devices
- GetDevicesEndpoint
- DeleteDeviceUnmanagedStackEndpoint
- Fact
- ImageVersionCache
- Docker Hub Registry Provider
- StackViewModel
- ISocketRepository
- CreateDeviceStackEndpoint
- Design Systems from DESIGN.md
- Client UI Utilities
- ImageReference
- Dome.Business.Stacks
- Dome.Shared.Containers
- AGENTS.md Guidelines
- Dependabot Config
- Dependency Review Workflow
- Dome.Domain/DependencyInjection.cs
- ContainerViewModel
- DockerEngineClient.cs
- DiagramStackViewModel
- Commands
- DeviceImageVersionCheckerTests.cs
- DomeDbContext
- Release Workflow
- Vite and Tailwind Config
- Device Diagram Endpoint
- ContainerResponse
- Input
- Customization & Theming
- Step 4: Map Tokens to the Theme
- Device Stack Update Endpoint
- Socket Status Response
- Dome.Socket.Api.Docker.Clients
- Image Version Check Response
- README Feature Summary
- Release Notes Highlights
- UpdateSocketEndpoint
- Publish Workflow Attestations
- Security Notes
- README.md
- Docker.DotNet and FastEndpoints Deps
- Test Tooling Deps
- Config and DI Deps
- Monaco Editor Deps
- devDependencies
- OCI Token and Tag Responses
- Socket agents discover containers (breaking)
- Solution and API Project
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Build Context Config
- Business Test Deps
- Tools
- EF Core Design Deps
- HTTP and Logging Deps
- Client npm Scripts
- Graphify Rules and Workflow
- Component Composition
- ScriptedHttpMessageHandler
- Docker Env Variables
- Client Dependency Overrides
- TypeScript Project References
- Graphify Skill Docs
- Favicon Icon
- Styling & Customization
- Document a DOME release
- shadcn/ui
- Socket
- Chat & Messaging
- Registry Authoring and Addresses
- DeviceContainerCommandResult
- Regex
- Base vs Radix
- showcase-coverage.mjs
- ContainerDto
- Icons
- Client index.html
- shadcn
- DeleteDeviceContainerEndpoint

## God Nodes (most connected - your core abstractions)
1. `cn()` - 88 edges
2. `Socket` - 51 edges
3. `IDomeSocketApi` - 47 edges
4. `Stack` - 37 edges
5. `Button()` - 35 edges
6. `SocketsPage()` - 34 edges
7. `Dome.Api.Endpoints.Devices` - 34 edges
8. `ContainerDetailPane()` - 30 edges
9. `react` - 28 edges
10. `IStackRepository` - 28 edges

## Surprising Connections (you probably didn't know these)
- `Message surfaces use Bubble` --references--> `Badge()`  [INFERRED]
  .agents/skills/shadcn/rules/chat.md → src/Dome.Client/src/components/ui/badge.tsx
- `Button / trigger as non-button element (base only)` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/rules/base-vs-radix.md → src/Dome.Client/src/components/ui/button.tsx
- `Option sets (2–7 choices) use ToggleGroup` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/rules/forms.md → src/Dome.Client/src/components/ui/button.tsx
- `Icons → [icons.md](./rules/icons.md)` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/SKILL.md → src/Dome.Client/src/components/ui/button.tsx
- `Card structure` --references--> `CardContent()`  [INFERRED]
  .agents/skills/shadcn/rules/composition.md → src/Dome.Client/src/components/ui/card.tsx

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **Security and no-authentication model** — readme_no_auth, security_scope, security_out_of_scope, security_deployment_guidance, instructions_security_constraints [INFERRED 0.75]

## Communities (124 total, 8 thin omitted)

### Community 0 - "IDomeSocketApi"
Cohesion: 0.13
Nodes (10): DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess, GetContainersResponse, Containers (+2 more)

### Community 1 - "RecordingProcessRunner"
Cohesion: 0.05
Nodes (35): DockerComposeClient, DockerComposeResult, Error, IsInvalid, Succeeded, IDockerComposeClient, DockerComposeOptions, DockerCliPath (+27 more)

### Community 2 - "IStackRepository"
Cohesion: 0.11
Nodes (16): StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService, IStackRepository (+8 more)

### Community 3 - "Client Layout and Routing"
Cohesion: 0.07
Nodes (54): Known traps, Step 2: Install Everything, Composition: asChild (radix) vs render (base), Chat & Messaging → [chat.md](./rules/chat.md), Use Components, Not Custom Markup → [composition.md](./rules/composition.md), lucide-react, react-router, sonner (+46 more)

### Community 4 - "Task"
Cohesion: 0.07
Nodes (9): IStackService, SocketRepository, DockerContainerLifecycleResult, NotFound, Succeeded, DockerEngineClient, IDockerEngineClient, GetContainersEndpoint (+1 more)

### Community 5 - "DateTimeOffset"
Cohesion: 0.07
Nodes (21): Dome.Domain, Dome.Domain.Migrations, ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind (+13 more)

### Community 6 - "InlineData"
Cohesion: 0.07
Nodes (13): Dome.Socket.Tests.Docker.Configuration, SocketAddressValidator, DependencyInjection, DatabaseInitializer, IDatabaseInitializer, SqlitePath, DockerEndpointValidator, ImageReferenceParser (+5 more)

### Community 7 - "Socket and Compose Editor UI"
Cohesion: 0.19
Nodes (23): class-variance-authority, radix-ui, react, ComposeEditorDialog(), ComposeEditorDialogProps, ComposeYamlEditor, ConfirmDeleteDialog(), ConfirmDeleteDialogProps (+15 more)

### Community 8 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 9 - "Container API Client"
Cohesion: 0.09
Nodes (36): deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), readApiError(), createDeviceStack() (+28 more)

### Community 10 - "CodeQL Workflow"
Cohesion: 0.05
Nodes (36): name, permissions, runs-on, steps, strategy, concurrency, cancel-in-progress, group (+28 more)

### Community 11 - "Endpoint"
Cohesion: 0.08
Nodes (16): DeleteSocketEndpoint, GetSocketEndpoint, ISocketService, SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess (+8 more)

### Community 12 - "Stack Service"
Cohesion: 0.15
Nodes (9): Stack, ComposeYaml, CreatedAt, Id, ProjectName, Socket, SocketId, UpdatedAt (+1 more)

### Community 13 - "Docker Workflow"
Cohesion: 0.06
Nodes (31): name, permissions, runs-on, steps, strategy, env, PLATFORMS, context (+23 more)

### Community 14 - "SocketsPage.tsx"
Cohesion: 0.08
Nodes (40): Assistant activity uses Trace, Items always inside their Group component, No sizing classes on icons inside components, No manual z-index on overlay components, createSocket(), deleteSocket(), fetchSocketStatuses(), updateSocket() (+32 more)

### Community 15 - "Dev Compose Config"
Cohesion: 0.08
Nodes (29): build, environment, image, ports, restart, volumes, context, dockerfile (+21 more)

### Community 16 - "Scorecard Workflow"
Cohesion: 0.07
Nodes (29): name, permissions, runs-on, steps, jobs, analysis, name, permissions (+21 more)

### Community 17 - "GetDeviceStackRequest"
Cohesion: 0.29
Nodes (4): DeployDeviceStackEndpoint, GetDeviceStackRequest, SocketName, StackId

### Community 18 - "HTTP Request Primitives"
Cohesion: 0.15
Nodes (7): DependencyInjection, OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, OciTagListResponse, Tags

### Community 19 - "Container Node Diagram UI"
Cohesion: 0.12
Nodes (36): @xyflow/react, fetchDeviceImageVersionChecks(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState() (+28 more)

### Community 20 - "DeleteContainersResponse"
Cohesion: 0.32
Nodes (6): DeleteContainersEndpoint, DeleteContainersRequest, ContainerIds, RemoveVolumes, DeleteContainersResponse, Results

### Community 21 - "Device Container API and Errors"
Cohesion: 0.26
Nodes (16): Component Structure → [composition.md](./rules/composition.md), fetchDeviceContainers(), fetchSockets(), StackDetailPane(), StackDetailPaneProps, StackLoadState, Card(), CardAction() (+8 more)

### Community 22 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 23 - "Endpoint"
Cohesion: 0.17
Nodes (6): DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint, DownComposeEndpoint

### Community 24 - "Client Lint and Tooling Config"
Cohesion: 0.11
Nodes (18): clsx, cn, oxlint, react-dom, shadcn, @shadcn/lint, tailwind-merge, tailwindcss (+10 more)

### Community 25 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 26 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 27 - "Image Version Check Service"
Cohesion: 0.15
Nodes (10): IImageVersionCheckService, ImageVersionCheckService, CheckImageVersionsEndpoint, CheckImageVersionsRequest, Images, CheckImageVersionsResponse, Results, ImageVersionCheckRequestItem (+2 more)

### Community 28 - "ContainerVolumeDto"
Cohesion: 0.22
Nodes (7): ContainerVolumeDto, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 29 - "Dome.Business.Devices"
Cohesion: 0.20
Nodes (7): Dome.Socket.Contracts.Compose, Dome.Socket.Contracts, Dome.Business.Devices, Dome.Business.Tests.Devices, Dome.Business.Sockets, Dome.Socket.Contracts.Health, Dome.Business.Tests.Sockets

### Community 30 - "ContainerVolumeViewModel"
Cohesion: 0.22
Nodes (7): ContainerVolumeViewModel, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 31 - "GetDeviceStacksEndpoint"
Cohesion: 0.22
Nodes (5): GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName, GetDeviceStacksResponse, Stacks

### Community 32 - "ContainerVolumeResponse"
Cohesion: 0.22
Nodes (7): ContainerVolumeResponse, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 33 - "The Design System Page"
Cohesion: 0.08
Nodes (29): ColorPair(), contrast(), luminance(), toHex(), toRgb(), defaultStates, previewState(), StateMatrix() (+21 more)

### Community 34 - "fastendpoints"
Cohesion: 0.22
Nodes (6): Dome.Shared.Sockets, Dome.Api.Endpoints.Sockets, CreateSocketEndpoint, CreateSocketRequest, Address, Name

### Community 35 - "shadcn UI Config"
Cohesion: 0.11
Nodes (17): aliases, components, hooks, lib, ui, utils, iconLibrary, rsc (+9 more)

### Community 36 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 37 - "GhcrRegistryProvider"
Cohesion: 0.28
Nodes (5): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, GhcrRegistryProviderTests

### Community 38 - "Docker Registry Tests and Image Endpoints"
Cohesion: 0.17
Nodes (6): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images, ImageVersionCheckServiceTests

### Community 39 - "ContainerMapperProperties"
Cohesion: 0.10
Nodes (6): Dome.Socket.Tests.Docker.Clients, ContainerMapper, ContainerMapperProperties, FallbackNameSample, NameResolutionSample, StackSample

### Community 40 - "Docker Build Args Config"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 41 - "DeleteDeviceStackRequest"
Cohesion: 0.25
Nodes (5): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId

### Community 42 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 43 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.13
Nodes (11): Dome.Api.Endpoints.Devices, GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse, Containers, GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest (+3 more)

### Community 44 - "GetDevicesEndpoint"
Cohesion: 0.19
Nodes (7): Dome.Shared.Containers, Dome.Shared.Devices, GetDevicesEndpoint, DeviceResponse, Name, GetDevicesResponse, Devices

### Community 45 - "DeleteDeviceUnmanagedStackEndpoint"
Cohesion: 0.25
Nodes (5): DeleteDeviceUnmanagedStackEndpoint, DeleteDeviceUnmanagedStackRequest, ProjectName, RemoveVolumes, SocketName

### Community 46 - "Fact"
Cohesion: 0.06
Nodes (22): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix, ImageVersionClassification (+14 more)

### Community 47 - "ImageVersionCache"
Cohesion: 0.17
Nodes (9): ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest, RemoteTags, Unsupported (+1 more)

### Community 48 - "Docker Hub Registry Provider"
Cohesion: 0.29
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 49 - "StackViewModel"
Cohesion: 0.20
Nodes (8): GetDeviceStackEndpoint, StackViewModel, ComposeYaml, CreatedAt, Id, ProjectName, SocketId, UpdatedAt

### Community 50 - "ISocketRepository"
Cohesion: 0.06
Nodes (32): Dome.Socket.Api, AssemblyApplicationVersion, Current, DependencyInjection, DeviceDiagramService, DeviceQueryService, DeviceStackDeployService, IDeviceDiagramService (+24 more)

### Community 51 - "CreateDeviceStackEndpoint"
Cohesion: 0.29
Nodes (4): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName

### Community 52 - "Design Systems from DESIGN.md"
Cohesion: 0.20
Nodes (10): Contents, Coverage check, Design Systems from DESIGN.md, Inputs to Confirm, Picking a style, Report Back, Step 1: Scaffold, Step 3: Read the DESIGN.md (+2 more)

### Community 53 - "Client UI Utilities"
Cohesion: 0.14
Nodes (14): dependencies, class-variance-authority, clsx, cn, lucide-react, monaco-editor, @monaco-editor/react, radix-ui (+6 more)

### Community 54 - "ImageReference"
Cohesion: 0.14
Nodes (10): IRegistryProvider, RegistryVersionInfo, RemoteDigest, Tags, ImageReference, Canonical, Digest, Host (+2 more)

### Community 56 - "Dome.Shared.Containers"
Cohesion: 0.14
Nodes (10): Dome.Shared.Diagrams, DeviceDiagramDto, Containers, Stacks, DiagramStackDto, Id, Kind, ProjectName (+2 more)

### Community 57 - "AGENTS.md Guidelines"
Cohesion: 0.24
Nodes (8): Completion report requirements, DOME Development Instructions, Frontend conventions, Layer responsibilities and boundaries, One C# type per file, Only Dome.Socket.Api uses Docker.DotNet, DOME components: Client, Server, Socket, Test policy: no weakening or skipping

### Community 58 - "Dependabot Config"
Cohesion: 0.17
Nodes (11): default-days, monthly-batch, patterns, interval, updates, cooldown, directory, groups (+3 more)

### Community 59 - "Dependency Review Workflow"
Cohesion: 0.17
Nodes (11): runs-on, steps, jobs, dependency-review, name, permissions, contents, name (+3 more)

### Community 60 - "Dome.Domain/DependencyInjection.cs"
Cohesion: 0.14
Nodes (6): Dome.Domain.Seeding, Dome.Business, Dome.Domain.Tests.Seeding, Dome.Domain.Tests.Sqlite, Dome.Domain.Sqlite, Dome.Socket.Api.Docker

### Community 61 - "ContainerViewModel"
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 62 - "DockerEngineClient.cs"
Cohesion: 0.13
Nodes (5): Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Processes, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker.Compose

### Community 63 - "DiagramStackViewModel"
Cohesion: 0.33
Nodes (5): DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes

### Community 64 - "Commands"
Cohesion: 0.12
Nodes (17): `add` — Add components, `apply` — Apply a preset to an existing project, `build` — Build a custom registry, Commands, Contents, `diff` — Check for updates, `docs` — Get component documentation URLs, Dry-Run Mode (+9 more)

### Community 65 - "DeviceImageVersionCheckerTests.cs"
Cohesion: 0.24
Nodes (5): Dome.Business.Images, Dome.Shared.Images, Dome.Business.Tests.Images, Dome.Domain.Images, ImageVersionKind

### Community 66 - "DomeDbContext"
Cohesion: 0.17
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, DomeDbContextFactory, ImageVersionCacheRepository

### Community 67 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 68 - "Vite and Tailwind Config"
Cohesion: 0.29
Nodes (4): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname

### Community 69 - "Device Diagram Endpoint"
Cohesion: 0.20
Nodes (6): GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, GetDeviceDiagramResponse, Containers, Stacks

### Community 70 - "ContainerResponse"
Cohesion: 0.20
Nodes (9): ContainerResponse, Digest, Id, Image, ImageId, Name, Stack, State (+1 more)

### Community 71 - "Input"
Cohesion: 0.13
Nodes (19): Step 5: Map Component Recipes to Variants, Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs, Forms use FieldGroup + Field, InputGroup requires InputGroupInput/InputGroupTextarea (+11 more)

### Community 73 - "Customization & Theming"
Cohesion: 0.14
Nodes (14): 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors, Border Radius, Changing the Theme, Checking for Updates (+6 more)

### Community 74 - "Step 4: Map Tokens to the Theme"
Cohesion: 0.25
Nodes (8): Dark mode, Elevation, Focus, Fonts, Radius, Role mapping, Step 4: Map Tokens to the Theme, Type scale

### Community 75 - "Device Stack Update Endpoint"
Cohesion: 0.22
Nodes (6): UpdateDeviceStackEndpoint, UpdateDeviceStackRequest, ComposeYaml, ProjectName, SocketName, StackId

### Community 76 - "Socket Status Response"
Cohesion: 0.13
Nodes (7): GetSocketsEndpoint, GetSocketStatusesEndpoint, GetSocketsResponse, Sockets, GetSocketStatusesResponse, Statuses, GetHealthEndpoint

### Community 77 - "Dome.Socket.Api.Docker.Clients"
Cohesion: 0.07
Nodes (16): Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Containers, DockerContainerLifecycleExceptionMapper, SystemDataUsageResponse, SystemDataUsageVolumeResponse (+8 more)

### Community 78 - "Image Version Check Response"
Cohesion: 0.22
Nodes (8): ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest, Tags, Unsupported

### Community 79 - "README Feature Summary"
Cohesion: 0.20
Nodes (10): Compose files persisted under /var/lib/dome/compose, Compose stack create, edit and deploy, Container image update icon, Socket version display and update icon, v0.1.0 first public release, v0.3.1 release, v0.4.0 release, v0.4.1 release (no user-facing changes) (+2 more)

### Community 80 - "Release Notes Highlights"
Cohesion: 0.29
Nodes (8): Start and stop containers from details panel, Images moved to ghcr.io/dome-works (breaking), Project renamed HomelabDocs to DOME (breaking), SQLite settings and paths changed (breaking), v0.3.0 release with breaking changes, EF Core migrations in Dome.Domain, Published images ghcr.io/dome-works, SQLite persistence and backups

### Community 81 - "UpdateSocketEndpoint"
Cohesion: 0.29
Nodes (5): UpdateSocketEndpoint, UpdateSocketRequest, Address, Id, Name

### Community 82 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 83 - "Security Notes"
Cohesion: 0.24
Nodes (7): Current limitations, No remote Docker tcp:// support, No authentication or authorization, Out-of-scope issues, Private vulnerability reporting, In-scope security issues, Only main is supported

### Community 84 - "README.md"
Cohesion: 0.29
Nodes (6): AGENTS.md entry point, Root cause before fix working method, Validation commands (dotnet, npm), Contributing: read AGENTS.md and Instructions.md, Pre-1.0 warning, Troubleshooting: Socket unreachable

### Community 85 - "Docker.DotNet and FastEndpoints Deps"
Cohesion: 0.25
Nodes (6): Docker.DotNet, FastEndpoints, FastEndpoints.Swagger, Microsoft.NET.Sdk.Web, Refit, Microsoft.NET.Sdk

### Community 86 - "Test Tooling Deps"
Cohesion: 0.25
Nodes (7): FsCheck, FsCheck.Xunit.v3, coverlet.collector, Microsoft.NET.Test.Sdk, xunit.runner.visualstudio, xunit.v3, Microsoft.NET.Sdk

### Community 87 - "Config and DI Deps"
Cohesion: 0.25
Nodes (7): Microsoft.Extensions.Configuration, Microsoft.Extensions.DependencyInjection, coverlet.collector, Microsoft.NET.Test.Sdk, xunit.runner.visualstudio, xunit.v3, Microsoft.NET.Sdk

### Community 88 - "Monaco Editor Deps"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 90 - "devDependencies"
Cohesion: 0.15
Nodes (13): devDependencies, oxlint, shadcn, @shadcn/lint, tailwindcss, @tailwindcss/vite, tw-animate-css, @types/node (+5 more)

### Community 91 - "OCI Token and Tag Responses"
Cohesion: 0.29
Nodes (3): OciTokenResponse, AccessToken, Token

### Community 92 - "Socket agents discover containers (breaking)"
Cohesion: 0.67
Nodes (3): Socket agents discover containers (breaking), v0.2.0 Socket agents replace direct Docker access, Architecture: Client to Server to Socket

### Community 93 - "Solution and API Project"
Cohesion: 0.33
Nodes (4): FastEndpoints, FastEndpoints.Swagger, Microsoft.NET.Sdk.Web, Microsoft.NET.Sdk

### Community 94 - "GitHub Release Changelog Config"
Cohesion: 0.29
Nodes (6): labels, title, changelog, categories, exclude, labels

### Community 95 - "Tag and Env Workflow Steps"
Cohesion: 0.29
Nodes (7): TAG, steps, env, id, name, run, uses

### Community 96 - "Docker Build Context Config"
Cohesion: 0.29
Nodes (7): context, file, image, include, strategy, fail-fast, matrix

### Community 97 - "Business Test Deps"
Cohesion: 0.29
Nodes (6): FakeItEasy, coverlet.collector, Microsoft.NET.Test.Sdk, xunit.runner.visualstudio, xunit.v3, Microsoft.NET.Sdk

### Community 98 - "Tools"
Cohesion: 0.17
Nodes (11): Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries`, `shadcn:get_project_registries`, `shadcn:list_items_in_registries`, shadcn MCP Server (+3 more)

### Community 100 - "EF Core Design Deps"
Cohesion: 0.33
Nodes (5): Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.NET.Sdk

### Community 101 - "HTTP and Logging Deps"
Cohesion: 0.33
Nodes (5): Microsoft.Extensions.Http, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.DependencyInjection.Abstractions, Refit, Microsoft.NET.Sdk

### Community 102 - "Client npm Scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 104 - "Component Composition"
Cohesion: 0.17
Nodes (12): Avatar always needs AvatarFallback, Button has no isPending or isLoading prop, Callouts use Alert, Card structure, Choosing between overlay components, Component Composition, Contents, Dialog, Sheet, and Drawer always need a Title (+4 more)

### Community 105 - "ScriptedHttpMessageHandler"
Cohesion: 0.24
Nodes (4): OciAuthenticatedResponse, Response, Token, ScriptedHttpMessageHandler

### Community 106 - "Docker Env Variables"
Cohesion: 0.67
Nodes (3): env, PLATFORMS, REGISTRY

### Community 107 - "Client Dependency Overrides"
Cohesion: 0.67
Nodes (3): dompurify, overrides, monaco-editor

### Community 111 - "Styling & Customization"
Cohesion: 0.17
Nodes (12): Built-in variants first, className for layout only, Contents, No manual dark: color overrides, No raw color values for status/state indicators, No space-x-* / space-y-*, Prefer size-* over w-* h-* when equal, Prefer truncate shorthand (+4 more)

### Community 113 - "Document a DOME release"
Cohesion: 0.18
Nodes (10): CHANGELOG.md, Cutting a version, Do not do, Document a DOME release, GitHub Release notes, Inputs, Inspect before writing, No user-facing changes (+2 more)

### Community 115 - "shadcn/ui"
Cohesion: 0.18
Nodes (11): Component Docs, Examples, and Usage, Current Project Context, Design Systems from DESIGN.md, Detailed References, Key Fields, Key Patterns, Principles, Quick Reference (+3 more)

### Community 117 - "Socket"
Cohesion: 0.13
Nodes (10): Dome.Domain.Stacks, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Socket, Address, CreatedAt, Id, Name (+2 more)

### Community 118 - "Chat & Messaging"
Cohesion: 0.20
Nodes (10): Attachments use Attachment, Chat & Messaging, Code and tool output use CodeBlock, Contents, Escape hatch: the scroller hooks, Message rows use Message, Message surfaces use Bubble, Scrollable threads use MessageScroller (+2 more)

### Community 119 - "Registry Authoring and Addresses"
Cohesion: 0.22
Nodes (9): Address Schemes, Build and Verify, GitHub Registries, Include, Item Definitions, Mental Model, Registry Authoring and Addresses, Registry Dependencies (+1 more)

### Community 120 - "DeviceContainerCommandResult"
Cohesion: 0.11
Nodes (15): DeviceContainerCommandResult, IsContainerNotFound, IsDeviceNotFound, IsSuccess, DeviceContainerCommandService, IDeviceContainerCommandService, DeleteContainerResult, ContainerId (+7 more)

### Community 122 - "Base vs Radix"
Cohesion: 0.25
Nodes (8): Accordion, Base vs Radix, Button / trigger as non-button element (base only), Contents, Select, Select — multiple selection and object values (base only), Slider, ToggleGroup

### Community 123 - "showcase-coverage.mjs"
Cohesion: 0.25
Nodes (6): components, missing, source, [uiArg, ...roots], uiDir, walk()

### Community 124 - "ContainerDto"
Cohesion: 0.15
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 126 - "Icons"
Cohesion: 0.50
Nodes (3): Icons, Icons in Button use data-icon attribute, Pass icons as component objects, not string keys

### Community 127 - "Client index.html"
Cohesion: 0.50
Nodes (3): Run modes: Rider, Docker build, published images, Client entry module src/main.tsx, Client index.html

### Community 132 - "DeleteDeviceContainerEndpoint"
Cohesion: 0.25
Nodes (5): DeleteDeviceContainerEndpoint, DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes

## Knowledge Gaps
- **316 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+311 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 842 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Socket` to `IDomeSocketApi`, `DeviceImageVersionCheckerTests.cs`, `IStackRepository`, `DomeDbContext`, `Task`, `DateTimeOffset`, `Endpoint`, `Stack Service`, `Fact`, `Document a DOME release`, `HTTP Request Primitives`, `ISocketRepository`, `Dome.Business.Stacks`, `DeviceContainerCommandResult`, `Image Version Check Service`, `Dome.Business.Devices`, `DockerEngineClient.cs`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _316 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IDomeSocketApi` be split into smaller, more focused modules?**
  _Cohesion score 0.13135985198889916 - nodes in this community are weakly interconnected._
- **Why does `Stack` connect `Stack Service` to `IDomeSocketApi`, `IStackRepository`, `DomeDbContext`, `DateTimeOffset`, `Endpoint`, `Fact`, `ImageVersionCache`, `Socket`, `Dome.Business.Stacks`, `Dome.Business.Devices`?**
  _High betweenness centrality (0.022) - this node is a cross-community bridge._
- **Should `RecordingProcessRunner` be split into smaller, more focused modules?**
  _Cohesion score 0.050616050616050616 - nodes in this community are weakly interconnected._
- **Why does `DockerEngineClient` connect `Task` to `RecordingProcessRunner`, `InlineData`, `HTTP Request Primitives`, `ISocketRepository`, `DockerEngineClient.cs`?**
  _High betweenness centrality (0.012) - this node is a cross-community bridge._