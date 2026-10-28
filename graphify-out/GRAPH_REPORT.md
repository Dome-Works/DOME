# Graph Report - DOME  (2026-10-28)

## Corpus Check
- 318 files · ~60,292 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2308 nodes · 5301 edges · 140 communities (125 shown, 15 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 379 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `742a040a`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Fact
- RecordingProcessRunner
- IStackRepository
- Client Layout and Routing
- IReadOnlyCollection
- DateTimeOffset
- StackDto
- Socket and Compose Editor UI
- Build Workflow
- Container API Client
- CodeQL Workflow
- SocketService
- Guid
- Docker Workflow
- SocketsPage.tsx
- Dev Compose Config
- Scorecard Workflow
- StackDetailPane.tsx
- OciRegistryProvider
- ContainerDetailPane.tsx
- IReadOnlyCollection
- Task
- Compose Sample Config
- StackViewModel
- Client Lint and Tooling Config
- Zizmor Workflow Config
- TypeScript App Config
- CheckImageVersionsRequest
- .GetDiagramAsync
- Socket
- .Map
- Step 4: Map Tokens to the Theme
- microsoft_entityframeworkcore
- The Design System Page
- Dome.Shared.Sockets
- shadcn UI Config
- TypeScript Node Config
- GetDeviceImageVersionChecksEndpoint
- Docker Registry Tests and Image Endpoints
- ContainerResponse
- Docker Build Args Config
- DeleteDeviceStackRequest
- Oxlint Config
- ContainerMapperProperties
- Dome.Api.Endpoints.Devices
- ContainerMapperProperties.cs
- .Classify
- ImageVersionCheckDto
- Docker Hub Registry Provider
- Endpoint
- .CreateService
- SocketStatusResponse
- Design Systems from DESIGN.md
- Client UI Utilities
- ImageReference
- .DeleteUnmanagedAsync
- ContainerVolumeDto
- AGENTS.md Guidelines
- Dependabot Config
- Dependency Review Workflow
- Dome.Domain/DependencyInjection.cs
- .Resolve
- Dome.Business.Stacks
- DockerEngineClient.cs
- Commands
- DeviceImageVersionCheckerTests.cs
- DomeDbContext
- Release Workflow
- Vite and Tailwind Config
- GetDeviceDiagramResponse
- DeviceContainerLifecycleRequest
- Input
- ImageVersionCache
- Customization & Theming
- Dome.Business.Sockets
- DeleteContainerResult
- ComposeDownRequest
- Dome.Socket.Api.Docker.Clients
- Image Version Check Response
- README Feature Summary
- Release Notes Highlights
- .TryParse
- Publish Workflow Attestations
- Security Notes
- README.md
- Docker.DotNet and FastEndpoints Deps
- Test Tooling Deps
- Config and DI Deps
- GetDevicesEndpoint
- .AddDomeBusiness
- devDependencies
- OCI Token and Tag Responses
- Socket agents discover containers (breaking)
- Solution and API Project
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Build Context Config
- Business Test Deps
- Tools
- .GetStatusesAsync
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
- ImageTagVersion
- Document a DOME release
- shadcn/ui
- AssemblyApplicationVersion.cs
- SystemSetting
- Chat & Messaging
- Registry Authoring and Addresses
- ContainerMapperProperties.cs
- ImageTagParser.cs
- Base vs Radix
- showcase-coverage.mjs
- ContainerViewModel
- ContainerDto
- ImageVersionClassification
- Client index.html
- shadcn
- Dome.Domain.Settings
- ComposeYamlEditor.tsx
- UpdateSocketRequest
- DeviceDiagramDto
- GetDeviceStacksEndpoint
- CreateSocketEndpoint
- DeleteDeviceContainerEndpoint
- GetSocketsEndpoint
- GetSocketStatusesEndpoint
- Docker/DependencyInjection.cs
- DeleteDeviceUnmanagedStackRequest

## God Nodes (most connected - your core abstractions)
1. `cn()` - 88 edges
2. `Socket` - 51 edges
3. `IDomeSocketApi` - 47 edges
4. `Stack` - 37 edges
5. `Button()` - 35 edges
6. `SocketsPage()` - 34 edges
7. `Dome.Api.Endpoints.Devices` - 34 edges
8. `ContainerDetailPane()` - 30 edges
9. `IStackRepository` - 29 edges
10. `react` - 28 edges

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

## Communities (140 total, 15 thin omitted)

### Community 0 - "Fact"
Cohesion: 0.23
Nodes (5): GetContainersEndpoint, GetContainersResponse, Containers, IDomeSocketApi, DeviceStackDeployServiceTests

### Community 1 - "RecordingProcessRunner"
Cohesion: 0.07
Nodes (28): Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Processes, DockerComposeClient, DockerComposeResult, Error, IsInvalid, Succeeded, DockerComposeOptions (+20 more)

### Community 2 - "IStackRepository"
Cohesion: 0.14
Nodes (10): StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService, ISocketRepository (+2 more)

### Community 3 - "Client Layout and Routing"
Cohesion: 0.07
Nodes (54): Known traps, Step 2: Install Everything, Composition: asChild (radix) vs render (base), Chat & Messaging → [chat.md](./rules/chat.md), Use Components, Not Custom Markup → [composition.md](./rules/composition.md), lucide-react, react-router, sonner (+46 more)

### Community 4 - "IReadOnlyCollection"
Cohesion: 0.12
Nodes (10): DockerContainerLifecycleResult, NotFound, Succeeded, IDockerEngineClient, ContainerLifecycleRequest, Id, StartContainerEndpoint, StopContainerEndpoint (+2 more)

### Community 5 - "DateTimeOffset"
Cohesion: 0.11
Nodes (6): InitialCreate, AddSockets, AddStacks, AddImageVersionCache, AddSystemSettings, DomeDbContextModelSnapshot

### Community 6 - "StackDto"
Cohesion: 0.13
Nodes (9): GetDeviceStackEndpoint, IStackService, StackDto, ComposeYaml, CreatedAt, Id, ProjectName, SocketId (+1 more)

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

### Community 11 - "SocketService"
Cohesion: 0.08
Nodes (17): DeleteSocketEndpoint, GetSocketEndpoint, ISocketService, SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess (+9 more)

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

### Community 17 - "StackDetailPane.tsx"
Cohesion: 0.26
Nodes (16): Component Structure → [composition.md](./rules/composition.md), fetchDeviceContainers(), fetchSockets(), StackDetailPane(), StackDetailPaneProps, StackLoadState, Card(), CardAction() (+8 more)

### Community 18 - "OciRegistryProvider"
Cohesion: 0.18
Nodes (4): OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 19 - "ContainerDetailPane.tsx"
Cohesion: 0.12
Nodes (36): @xyflow/react, fetchDeviceImageVersionChecks(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState() (+28 more)

### Community 21 - "Task"
Cohesion: 0.09
Nodes (4): ISystemSettingRepository, SystemSettingRepository, SocketRepository, DatabaseInitializerTests

### Community 22 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 23 - "StackViewModel"
Cohesion: 0.09
Nodes (17): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName, StackViewModel, ComposeYaml, CreatedAt, Id (+9 more)

### Community 24 - "Client Lint and Tooling Config"
Cohesion: 0.11
Nodes (18): clsx, cn, oxlint, react-dom, shadcn, @shadcn/lint, tailwind-merge, tailwindcss (+10 more)

### Community 25 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 26 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 27 - "CheckImageVersionsRequest"
Cohesion: 0.29
Nodes (6): ImageVersionCheckService, CheckImageVersionsRequest, Images, ImageVersionCheckRequestItem, Image, ImageId

### Community 29 - "Socket"
Cohesion: 0.08
Nodes (22): Dome.Domain.Stacks, Dome.Business.Tests.Devices, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Dome.Shared.Diagrams, Socket, Address, CreatedAt (+14 more)

### Community 31 - "Step 4: Map Tokens to the Theme"
Cohesion: 0.25
Nodes (8): Dark mode, Elevation, Focus, Fonts, Radius, Role mapping, Step 4: Map Tokens to the Theme, Type scale

### Community 32 - "microsoft_entityframeworkcore"
Cohesion: 0.20
Nodes (3): Dome.Domain, Dome.Domain.Migrations, Dome.Domain.Images

### Community 33 - "The Design System Page"
Cohesion: 0.08
Nodes (29): ColorPair(), contrast(), luminance(), toHex(), toRgb(), defaultStates, previewState(), StateMatrix() (+21 more)

### Community 35 - "shadcn UI Config"
Cohesion: 0.11
Nodes (17): aliases, components, hooks, lib, ui, utils, iconLibrary, rsc (+9 more)

### Community 36 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 37 - "GetDeviceImageVersionChecksEndpoint"
Cohesion: 0.25
Nodes (5): GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest, SocketName, GetDeviceImageVersionChecksResponse, Checks

### Community 38 - "Docker Registry Tests and Image Endpoints"
Cohesion: 0.12
Nodes (12): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images, IImageVersionCheckService, RegistryVersionInfo (+4 more)

### Community 39 - "ContainerResponse"
Cohesion: 0.11
Nodes (16): ContainerResponse, Digest, Id, Image, ImageId, Name, Stack, State (+8 more)

### Community 40 - "Docker Build Args Config"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 41 - "DeleteDeviceStackRequest"
Cohesion: 0.33
Nodes (5): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId

### Community 42 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 44 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.13
Nodes (6): Dome.Business.Devices, Dome.Api.Endpoints.Devices, GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, IDeviceDiagramService

### Community 45 - "ContainerMapperProperties.cs"
Cohesion: 0.15
Nodes (4): Dome.Socket.Tests.Docker.Clients, FallbackNameSample, NameResolutionSample, StackSample

### Community 46 - ".Classify"
Cohesion: 0.12
Nodes (12): ImageVersionClassification, CurrentTag, Kind, NewestTag, ImageVersionClassifier, ImageVersionSnapshot, Error, RemoteDigest (+4 more)

### Community 47 - "ImageVersionCheckDto"
Cohesion: 0.13
Nodes (14): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag, ImageVersionCheckDto (+6 more)

### Community 48 - "Docker Hub Registry Provider"
Cohesion: 0.33
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 49 - "Endpoint"
Cohesion: 0.11
Nodes (11): DeleteDeviceUnmanagedStackEndpoint, DeployDeviceStackEndpoint, GetDeviceStackRequest, SocketName, StackId, IDeviceStackDeployService, IDockerComposeClient, DownComposeEndpoint (+3 more)

### Community 50 - ".CreateService"
Cohesion: 0.22
Nodes (5): GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, IDeviceQueryService, DeviceImageVersionCheckerTests

### Community 51 - "SocketStatusResponse"
Cohesion: 0.33
Nodes (5): SocketStatusResponse, Id, IsReachable, NeedsUpdate, Version

### Community 52 - "Design Systems from DESIGN.md"
Cohesion: 0.20
Nodes (10): Contents, Coverage check, Design Systems from DESIGN.md, Inputs to Confirm, Picking a style, Report Back, Step 1: Scaffold, Step 3: Read the DESIGN.md (+2 more)

### Community 53 - "Client UI Utilities"
Cohesion: 0.14
Nodes (14): dependencies, class-variance-authority, clsx, cn, lucide-react, monaco-editor, @monaco-editor/react, radix-ui (+6 more)

### Community 54 - "ImageReference"
Cohesion: 0.14
Nodes (12): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, IRegistryProvider, ImageReference, Canonical, Digest (+4 more)

### Community 55 - ".DeleteUnmanagedAsync"
Cohesion: 0.14
Nodes (10): DeviceQueryService, DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess, DeviceStackDeployService (+2 more)

### Community 56 - "ContainerVolumeDto"
Cohesion: 0.12
Nodes (14): ContainerVolumeViewModel, Destination, Name, ReadOnly, SizeBytes, Source, Type, ContainerVolumeDto (+6 more)

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
Cohesion: 0.21
Nodes (5): Dome.Domain.Seeding, Dome.Domain.Tests.Seeding, Dome.Domain.Tests.Sqlite, Dome.Domain.Sqlite, DependencyInjection

### Community 62 - "Dome.Business.Stacks"
Cohesion: 0.16
Nodes (4): Dome.Business, Dome.Business.Stacks, Dome.Shared.Stacks, Dome.Socket.Api.Docker

### Community 63 - "DockerEngineClient.cs"
Cohesion: 0.16
Nodes (5): Dome.Socket.Contracts.Compose, Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Tests.Docker.Configuration, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker.Compose

### Community 64 - "Commands"
Cohesion: 0.12
Nodes (17): `add` — Add components, `apply` — Apply a preset to an existing project, `build` — Build a custom registry, Commands, Contents, `diff` — Check for updates, `docs` — Get component documentation URLs, Dry-Run Mode (+9 more)

### Community 65 - "DeviceImageVersionCheckerTests.cs"
Cohesion: 0.17
Nodes (7): Dome.Business.Images, Dome.Shared.Images, Dome.Shared.Containers, Dome.Business.Tests.Images, Dome.Shared.Devices, IDeviceImageVersionChecker, ImageVersionKind

### Community 66 - "DomeDbContext"
Cohesion: 0.22
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 67 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 68 - "Vite and Tailwind Config"
Cohesion: 0.29
Nodes (4): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname

### Community 69 - "GetDeviceDiagramResponse"
Cohesion: 0.20
Nodes (8): DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, GetDeviceDiagramResponse, Containers, Stacks

### Community 70 - "DeviceContainerLifecycleRequest"
Cohesion: 0.25
Nodes (5): DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint

### Community 71 - "Input"
Cohesion: 0.13
Nodes (19): Step 5: Map Component Recipes to Variants, Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs, Forms use FieldGroup + Field, InputGroup requires InputGroupInput/InputGroupTextarea (+11 more)

### Community 72 - "ImageVersionCache"
Cohesion: 0.14
Nodes (10): TelemetryReporter, DeviceImageVersionChecker, ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest (+2 more)

### Community 73 - "Customization & Theming"
Cohesion: 0.14
Nodes (14): 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors, Border Radius, Changing the Theme, Checking for Updates (+6 more)

### Community 74 - "Dome.Business.Sockets"
Cohesion: 0.25
Nodes (4): Dome.Socket.Contracts, Dome.Business.Sockets, Dome.Socket.Contracts.Health, Dome.Business.Tests.Sockets

### Community 75 - "DeleteContainerResult"
Cohesion: 0.14
Nodes (14): DeleteContainersEndpoint, DeleteContainerResult, ContainerId, Error, Status, DeleteContainersRequest, ContainerIds, RemoveVolumes (+6 more)

### Community 76 - "ComposeDownRequest"
Cohesion: 0.14
Nodes (6): ComposeDownRequest, ProjectName, RemoveVolumes, ComposeUpRequest, ComposeYaml, ProjectName

### Community 77 - "Dome.Socket.Api.Docker.Clients"
Cohesion: 0.09
Nodes (11): Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Containers, DockerContainerLifecycleExceptionMapper, SystemDataUsageResponse, SystemDataUsageVolumeResponse, Name (+3 more)

### Community 78 - "Image Version Check Response"
Cohesion: 0.17
Nodes (10): CheckImageVersionsResponse, Results, ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest (+2 more)

### Community 79 - "README Feature Summary"
Cohesion: 0.20
Nodes (10): Compose files persisted under /var/lib/dome/compose, Compose stack create, edit and deploy, Container image update icon, Socket version display and update icon, v0.1.0 first public release, v0.3.1 release, v0.4.0 release, v0.4.1 release (no user-facing changes) (+2 more)

### Community 80 - "Release Notes Highlights"
Cohesion: 0.29
Nodes (8): Start and stop containers from details panel, Images moved to ghcr.io/dome-works (breaking), Project renamed HomelabDocs to DOME (breaking), SQLite settings and paths changed (breaking), v0.3.0 release with breaking changes, EF Core migrations in Dome.Domain, Published images ghcr.io/dome-works, SQLite persistence and backups

### Community 81 - ".TryParse"
Cohesion: 0.09
Nodes (8): SocketAddressValidator, SqlitePath, DockerEndpointValidator, ImageReferenceParser, SocketAddressValidatorTests, SqlitePathTests, DockerEndpointValidatorTests, ImageReferenceParserTests

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

### Community 88 - "GetDevicesEndpoint"
Cohesion: 0.22
Nodes (5): GetDevicesEndpoint, DeviceResponse, Name, GetDevicesResponse, Devices

### Community 89 - ".AddDomeBusiness"
Cohesion: 0.17
Nodes (8): DependencyInjection, ISystemSettingsService, SystemSettingsService, TelemetrySettings, IImageVersionCacheRepository, ImageVersionCacheRepository, DatabaseInitializer, IDatabaseInitializer

### Community 90 - "devDependencies"
Cohesion: 0.15
Nodes (13): devDependencies, oxlint, shadcn, @shadcn/lint, tailwindcss, @tailwindcss/vite, tw-animate-css, @types/node (+5 more)

### Community 91 - "OCI Token and Tag Responses"
Cohesion: 0.22
Nodes (5): OciTagListResponse, Tags, OciTokenResponse, AccessToken, Token

### Community 92 - "Socket agents discover containers (breaking)"
Cohesion: 0.67
Nodes (3): Socket agents discover containers (breaking), v0.2.0 Socket agents replace direct Docker access, Architecture: Client to Server to Socket

### Community 93 - "Solution and API Project"
Cohesion: 0.29
Nodes (5): PostHog.AspNetCore, FastEndpoints, FastEndpoints.Swagger, Microsoft.NET.Sdk.Web, Microsoft.NET.Sdk

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

### Community 99 - ".GetStatusesAsync"
Cohesion: 0.25
Nodes (4): SocketHealthResponse, DockerReachable, Version, SocketServiceTests

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
Cohesion: 0.19
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

### Community 112 - "ImageTagVersion"
Cohesion: 0.22
Nodes (7): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix

### Community 113 - "Document a DOME release"
Cohesion: 0.18
Nodes (10): CHANGELOG.md, Cutting a version, Do not do, Document a DOME release, GitHub Release notes, Inputs, Inspect before writing, No user-facing changes (+2 more)

### Community 115 - "shadcn/ui"
Cohesion: 0.18
Nodes (11): Component Docs, Examples, and Usage, Current Project Context, Design Systems from DESIGN.md, Detailed References, Key Fields, Key Patterns, Principles, Quick Reference (+3 more)

### Community 116 - "AssemblyApplicationVersion.cs"
Cohesion: 0.10
Nodes (10): Dome.Socket.Api, Dome.Api.Telemetry, TelemetryBuildInfo, Environment, AssemblyApplicationVersion, Current, IApplicationVersion, Current (+2 more)

### Community 117 - "SystemSetting"
Cohesion: 0.20
Nodes (5): ImageVersionCacheConfiguration, SystemSetting, Key, Value, SystemSettingConfiguration

### Community 118 - "Chat & Messaging"
Cohesion: 0.20
Nodes (10): Attachments use Attachment, Chat & Messaging, Code and tool output use CodeBlock, Contents, Escape hatch: the scroller hooks, Message rows use Message, Message surfaces use Bubble, Scrollable threads use MessageScroller (+2 more)

### Community 119 - "Registry Authoring and Addresses"
Cohesion: 0.22
Nodes (9): Address Schemes, Build and Verify, GitHub Registries, Include, Item Definitions, Mental Model, Registry Authoring and Addresses, Registry Dependencies (+1 more)

### Community 120 - "ContainerMapperProperties.cs"
Cohesion: 0.16
Nodes (7): DeviceContainerCommandResult, IsContainerNotFound, IsDeviceNotFound, IsSuccess, DeviceContainerCommandService, IDeviceContainerCommandService, DeviceContainerCommandServiceTests

### Community 122 - "Base vs Radix"
Cohesion: 0.25
Nodes (8): Accordion, Base vs Radix, Button / trigger as non-button element (base only), Contents, Select, Select — multiple selection and object values (base only), Slider, ToggleGroup

### Community 123 - "showcase-coverage.mjs"
Cohesion: 0.25
Nodes (6): components, missing, source, [uiArg, ...roots], uiDir, walk()

### Community 124 - "ContainerViewModel"
Cohesion: 0.14
Nodes (12): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+4 more)

### Community 125 - "ContainerDto"
Cohesion: 0.18
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 126 - "ImageVersionClassification"
Cohesion: 0.50
Nodes (3): Icons, Icons in Button use data-icon attribute, Pass icons as component objects, not string keys

### Community 127 - "Client index.html"
Cohesion: 0.50
Nodes (3): Run modes: Rider, Docker build, published images, Client entry module src/main.tsx, Client index.html

### Community 129 - "Dome.Domain.Settings"
Cohesion: 0.18
Nodes (5): Dome.Business.Settings, Dome.Business.Tests.Settings, Dome.Domain.Settings, SystemSettingKeys, SystemSettingsServiceTests

### Community 130 - "ComposeYamlEditor.tsx"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 131 - "UpdateSocketRequest"
Cohesion: 0.25
Nodes (5): UpdateSocketEndpoint, UpdateSocketRequest, Address, Id, Name

### Community 132 - "DeviceDiagramDto"
Cohesion: 0.20
Nodes (8): DeviceDiagramDto, Containers, Stacks, DiagramStackDto, Id, Kind, ProjectName, TotalBytes

### Community 133 - "GetDeviceStacksEndpoint"
Cohesion: 0.25
Nodes (5): GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName, GetDeviceStacksResponse, Stacks

### Community 134 - "CreateSocketEndpoint"
Cohesion: 0.29
Nodes (4): CreateSocketEndpoint, CreateSocketRequest, Address, Name

### Community 135 - "DeleteDeviceContainerEndpoint"
Cohesion: 0.33
Nodes (5): DeleteDeviceContainerEndpoint, DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes

### Community 136 - "GetSocketsEndpoint"
Cohesion: 0.33
Nodes (3): GetSocketsEndpoint, GetSocketsResponse, Sockets

### Community 137 - "GetSocketStatusesEndpoint"
Cohesion: 0.33
Nodes (3): GetSocketStatusesEndpoint, GetSocketStatusesResponse, Statuses

### Community 139 - "DeleteDeviceUnmanagedStackRequest"
Cohesion: 0.50
Nodes (4): DeleteDeviceUnmanagedStackRequest, ProjectName, RemoveVolumes, SocketName

## Knowledge Gaps
- **321 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+316 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 852 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **15 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Socket` to `Fact`, `DeviceImageVersionCheckerTests.cs`, `IStackRepository`, `.GetStatusesAsync`, `DomeDbContext`, `DateTimeOffset`, `ImageVersionCache`, `Docker/DependencyInjection.cs`, `SocketService`, `Guid`, `Dome.Business.Sockets`, `Document a DOME release`, `.CreateService`, `Task`, `ContainerMapperProperties.cs`, `.GetDiagramAsync`, `Dome.Business.Stacks`?**
  _High betweenness centrality (0.032) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _321 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `RecordingProcessRunner` be split into smaller, more focused modules?**
  _Cohesion score 0.06557377049180328 - nodes in this community are weakly interconnected._
- **Why does `ImageVersionCache` connect `ImageVersionCache` to `microsoft_entityframeworkcore`, `DomeDbContext`, `DateTimeOffset`, `Guid`, `.CreateService`, `IReadOnlyCollection`, `SystemSetting`, `.AddDomeBusiness`?**
  _High betweenness centrality (0.014) - this node is a cross-community bridge._
- **Should `IStackRepository` be split into smaller, more focused modules?**
  _Cohesion score 0.1423076923076923 - nodes in this community are weakly interconnected._
- **Why does `DeviceStackDeployResult` connect `.DeleteUnmanagedAsync` to `Fact`, `Endpoint`, `Dome.Api.Endpoints.Devices`, `Task`?**
  _High betweenness centrality (0.013) - this node is a cross-community bridge._