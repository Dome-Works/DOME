# Graph Report - DOME  (2026-11-10)

## Corpus Check
- 367 files · ~68,662 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2613 nodes · 6314 edges · 126 communities (110 shown, 16 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 471 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `2edb4d08`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Fact
- RecordingProcessRunner
- Guid
- Client Layout and Routing
- Task
- DateTimeOffset
- SystemSettingsResponse
- Socket and Compose Editor UI
- Build Workflow
- Container API Client
- CodeQL Workflow
- SocketResponse
- IDomeSocketApi
- Docker Workflow
- SocketsPage.tsx
- Dev Compose Config
- Scorecard Workflow
- StackDetailPane.tsx
- OciRegistryProvider
- fastendpoints
- Dome.Business.Auth
- SystemSettingsService
- Compose Sample Config
- Dome.Api.Endpoints.Devices
- Client Lint and Tooling Config
- Zizmor Workflow Config
- TypeScript App Config
- CheckImageVersionsRequest
- .GetDiagramAsync
- Dome.Business.Devices
- ContainerMapperProperties
- .InvokeAsync
- microsoft_entityframeworkcore
- The Design System Page
- .AddDomeDomain
- shadcn UI Config
- TypeScript Node Config
- InlineData
- Docker Registry Tests and Image Endpoints
- DeleteContainerResult
- Docker Build Args Config
- .CreateService
- Oxlint Config
- DatabaseInitializerTests
- Component Selection
- GhcrRegistryProvider
- .Classify
- ImageVersionCheckDto
- Docker Hub Registry Provider
- Dome.Socket.Api.Docker.Compose
- SocketCreatedResponse
- IReadOnlyList
- Design Systems from DESIGN.md
- Client UI Utilities
- ImageReference
- .DeleteUnmanagedAsync
- ContainerVolumeDto
- AGENTS.md Guidelines
- Dependabot Config
- Dependency Review Workflow
- Dome.Domain.Settings
- DomeSocketApiFactory
- Dome.Domain.Settings
- Docker/DependencyInjection.cs
- Commands
- DomeSocketApiFactory.cs
- DomeDbContext
- Release Workflow
- Vite and Tailwind Config
- GetDeviceDiagramResponse
- DeviceDiagramService
- .Validate
- ImageVersionCache
- Customization & Theming
- .Hash
- .TryMap
- DeleteDeviceStackRequest
- Dome.Socket.Api.Docker.Clients
- Icons
- README Feature Summary
- Release Notes Highlights
- .SetupAsync
- Publish Workflow Attestations
- Security Notes
- README.md
- Dome.slnx
- DeleteDeviceUnmanagedStackEndpoint
- Step 4: Map Tokens to the Theme
- Dome.Business.Devices
- devDependencies
- OCI Token and Tag Responses
- Socket agents discover containers (breaking)
- SetupAdminEndpoint
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Build Context Config
- GetContainersResponse
- Tools
- SocketService
- OciAuthenticatedResponse
- Client npm Scripts
- Graphify Rules and Workflow
- Component Composition
- Docker Env Variables
- Client Dependency Overrides
- TypeScript Project References
- Graphify Skill Docs
- Favicon Icon
- Styling & Customization
- Document a DOME release
- shadcn/ui
- .AddDomeBusiness
- Chat & Messaging
- Registry Authoring and Addresses
- ContainerMapperProperties.cs
- .Pattern
- Base vs Radix
- showcase-coverage.mjs
- ContainerViewModel
- ContainerDto
- Client index.html
- shadcn
- ComposeYamlEditor.tsx
- Endpoint

## God Nodes (most connected - your core abstractions)
1. `cn()` - 92 edges
2. `Socket` - 62 edges
3. `IDomeSocketApi` - 53 edges
4. `Button()` - 43 edges
5. `SocketsPage()` - 38 edges
6. `Stack` - 37 edges
7. `Dome.Api.Endpoints.Devices` - 34 edges
8. `react` - 33 edges
9. `ContainerDetailPane()` - 30 edges
10. `IStackRepository` - 29 edges

## Surprising Connections (you probably didn't know these)
- `Message surfaces use Bubble` --references--> `Badge()`  [INFERRED]
  .agents/skills/shadcn/rules/chat.md → src/Dome.Client/src/components/ui/badge.tsx
- `Component Selection` --references--> `Breadcrumb()`  [INFERRED]
  .agents/skills/shadcn/SKILL.md → src/Dome.Client/src/components/ui/breadcrumb.tsx
- `Button / trigger as non-button element (base only)` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/rules/base-vs-radix.md → src/Dome.Client/src/components/ui/button.tsx
- `Option sets (2–7 choices) use ToggleGroup` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/rules/forms.md → src/Dome.Client/src/components/ui/button.tsx
- `Icons → [icons.md](./rules/icons.md)` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/SKILL.md → src/Dome.Client/src/components/ui/button.tsx

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **Security and no-authentication model** — readme_no_auth, security_scope, security_out_of_scope, security_deployment_guidance, instructions_security_constraints [INFERRED 0.75]

## Communities (126 total, 16 thin omitted)

### Community 0 - "Fact"
Cohesion: 0.17
Nodes (6): SocketApiKeys, SocketHealthResponse, DockerReachable, Version, SocketApiKeysTests, SocketServiceTests

### Community 1 - "RecordingProcessRunner"
Cohesion: 0.07
Nodes (26): DockerComposeClient, DockerComposeResult, Error, IsInvalid, Succeeded, DockerComposeOptions, DockerCliPath, WorkingRoot (+18 more)

### Community 2 - "Guid"
Cohesion: 0.07
Nodes (26): IStackService, StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService (+18 more)

### Community 3 - "Client Layout and Routing"
Cohesion: 0.07
Nodes (59): Composition: asChild (radix) vs render (base), Items always inside their Group component, No sizing classes on icons inside components, lucide-react, AppHeader(), pageTitles, AppLayout(), AppSidebar() (+51 more)

### Community 4 - "Task"
Cohesion: 0.07
Nodes (11): IDeviceDiagramService, IDeviceStackDeployService, DockerContainerLifecycleResult, NotFound, Succeeded, DockerEngineClient, IDockerEngineClient, GetHealthEndpoint (+3 more)

### Community 5 - "DateTimeOffset"
Cohesion: 0.11
Nodes (7): InitialCreate, AddSockets, AddStacks, AddImageVersionCache, AddSystemSettings, AddSocketAuth, DomeDbContextModelSnapshot

### Community 6 - "SystemSettingsResponse"
Cohesion: 0.06
Nodes (21): Dome.Business.Settings, Dome.Socket.Api, Dome.Shared.Settings, Dome.Api.Endpoints.Settings, Dome.Api.Telemetry, UpdateSystemSettingsEndpoint, TelemetryBuildInfo, Environment (+13 more)

### Community 7 - "Socket and Compose Editor UI"
Cohesion: 0.18
Nodes (28): Fonts, radix-ui, react, ComposeEditorDialog(), ComposeEditorDialogProps, ComposeYamlEditor, ConfirmDeleteDialog(), ConfirmDeleteDialogProps (+20 more)

### Community 8 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 9 - "Container API Client"
Cohesion: 0.08
Nodes (42): deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), readApiError(), fetchDeviceImageVersionChecks() (+34 more)

### Community 10 - "CodeQL Workflow"
Cohesion: 0.05
Nodes (36): name, permissions, runs-on, steps, strategy, concurrency, cancel-in-progress, group (+28 more)

### Community 11 - "SocketResponse"
Cohesion: 0.09
Nodes (16): DeleteSocketEndpoint, GetSocketEndpoint, RotateSocketKeyEndpoint, UpdateSocketEndpoint, ISocketService, GetSocketRequest, Id, SocketResponse (+8 more)

### Community 13 - "Docker Workflow"
Cohesion: 0.06
Nodes (31): name, permissions, runs-on, steps, strategy, env, PLATFORMS, context (+23 more)

### Community 14 - "SocketsPage.tsx"
Cohesion: 0.09
Nodes (53): Step 2: Install Everything, Assistant activity uses Trace, No manual z-index on overlay components, createSocket(), deleteSocket(), fetchSockets(), fetchSocketStatuses(), rotateSocketKey() (+45 more)

### Community 15 - "Dev Compose Config"
Cohesion: 0.08
Nodes (29): build, environment, image, ports, restart, volumes, context, dockerfile (+21 more)

### Community 16 - "Scorecard Workflow"
Cohesion: 0.07
Nodes (29): name, permissions, runs-on, steps, jobs, analysis, name, permissions (+21 more)

### Community 17 - "StackDetailPane.tsx"
Cohesion: 0.12
Nodes (41): Component Structure → [composition.md](./rules/composition.md), react-dom, react-router, sonner, fetchAuthStatus(), login(), logout(), postAuth() (+33 more)

### Community 18 - "OciRegistryProvider"
Cohesion: 0.15
Nodes (5): ComposeProjectName, OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 19 - "fastendpoints"
Cohesion: 0.15
Nodes (8): Dome.Shared.Sockets, Dome.Business.Sockets, Dome.Api.Endpoints.Sockets, DeleteDeviceContainerEndpoint, DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes

### Community 20 - "Dome.Business.Auth"
Cohesion: 0.11
Nodes (13): Dome.Business.Auth, Dome.Api.Endpoints.Auth, Dome.Shared.Auth, Dome.Business.Tests.Auth, GetAuthStatusEndpoint, AuthStatusResponse, Authenticated, OnboardingCompleted (+5 more)

### Community 21 - "SystemSettingsService"
Cohesion: 0.18
Nodes (4): GetSystemSettingsEndpoint, ISystemSettingsService, SystemSettingsService, TelemetrySettings

### Community 22 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 23 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.05
Nodes (30): Dome.Api.Endpoints.Devices, CreateDeviceStackRequest, ProjectName, SocketName, DeviceContainerLifecycleRequest, ContainerId, Name, GetDeviceContainersEndpoint (+22 more)

### Community 24 - "Client Lint and Tooling Config"
Cohesion: 0.11
Nodes (17): clsx, cn, oxlint, shadcn, @shadcn/lint, tailwind-merge, tailwindcss, tw-animate-css (+9 more)

### Community 25 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 26 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 27 - "CheckImageVersionsRequest"
Cohesion: 0.10
Nodes (19): IImageVersionCheckService, ImageVersionCheckService, CheckImageVersionsEndpoint, CheckImageVersionsRequest, Images, CheckImageVersionsResponse, Results, ImageVersionCheckRequestItem (+11 more)

### Community 29 - "Dome.Business.Devices"
Cohesion: 0.13
Nodes (15): Dome.Socket.Contracts.Compose, Dome.Socket.Contracts, Dome.Domain.Stacks, Dome.Business.Tests.Devices, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Dome.Socket.Contracts.Health, Dome.Socket.Contracts.Containers (+7 more)

### Community 30 - "ContainerMapperProperties"
Cohesion: 0.05
Nodes (24): Dome.Socket.Tests.Docker.Clients, ContainerMapper, ImageDigestMapper, ContainerResponse, Digest, Id, Image, ImageId (+16 more)

### Community 31 - ".InvokeAsync"
Cohesion: 0.13
Nodes (4): AuthSignIn, ApiKeyMiddleware, SocketSecurity, ApiKeyMiddlewareTests

### Community 33 - "The Design System Page"
Cohesion: 0.08
Nodes (29): ColorPair(), contrast(), luminance(), toHex(), toRgb(), defaultStates, previewState(), StateMatrix() (+21 more)

### Community 34 - ".AddDomeDomain"
Cohesion: 0.21
Nodes (4): DependencyInjection, DomeDataDirectory, SqlitePath, SqlitePathTests

### Community 35 - "shadcn UI Config"
Cohesion: 0.11
Nodes (17): aliases, components, hooks, lib, ui, utils, iconLibrary, rsc (+9 more)

### Community 36 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 37 - "InlineData"
Cohesion: 0.20
Nodes (5): SocketAddressValidator, ISystemSettingRepository, SystemSettingsServiceTests, SocketAddressValidatorTests, ImageReferenceParserTests

### Community 38 - "Docker Registry Tests and Image Endpoints"
Cohesion: 0.25
Nodes (5): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images

### Community 39 - "DeleteContainerResult"
Cohesion: 0.16
Nodes (10): DeleteContainersEndpoint, DeleteContainerResult, ContainerId, Error, Status, DeleteContainersRequest, ContainerIds, RemoveVolumes (+2 more)

### Community 40 - "Docker Build Args Config"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 42 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 43 - "DatabaseInitializerTests"
Cohesion: 0.26
Nodes (4): Dome.Domain.Seeding, DatabaseInitializer, IDatabaseInitializer, DatabaseInitializerTests

### Community 44 - "Component Selection"
Cohesion: 0.08
Nodes (35): Step 5: Map Component Recipes to Variants, Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs, Forms use FieldGroup + Field, InputGroup requires InputGroupInput/InputGroupTextarea (+27 more)

### Community 45 - "GhcrRegistryProvider"
Cohesion: 0.19
Nodes (6): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, ImageReferenceParser, GhcrRegistryProviderTests

### Community 46 - ".Classify"
Cohesion: 0.08
Nodes (18): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix, ImageVersionClassification (+10 more)

### Community 47 - "ImageVersionCheckDto"
Cohesion: 0.13
Nodes (14): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag, ImageVersionCheckDto (+6 more)

### Community 48 - "Docker Hub Registry Provider"
Cohesion: 0.19
Nodes (6): DependencyInjection, DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 49 - "Dome.Socket.Api.Docker.Compose"
Cohesion: 0.10
Nodes (11): Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Api.Docker.Compose, IDockerComposeClient, DownComposeEndpoint, UpComposeEndpoint, ComposeDownRequest, ProjectName, RemoveVolumes (+3 more)

### Community 50 - "SocketCreatedResponse"
Cohesion: 0.15
Nodes (10): CreateSocketEndpoint, CreateSocketRequest, Address, Name, SocketCreatedResponse, Address, ApiKey, CreatedAt (+2 more)

### Community 51 - "IReadOnlyList"
Cohesion: 0.09
Nodes (15): LogoutEndpoint, GetSocketsEndpoint, GetSocketStatusesEndpoint, DeviceResponse, Name, GetSocketsResponse, Sockets, GetSocketStatusesResponse (+7 more)

### Community 52 - "Design Systems from DESIGN.md"
Cohesion: 0.18
Nodes (11): Contents, Coverage check, Design Systems from DESIGN.md, Inputs to Confirm, Known traps, Picking a style, Report Back, Step 1: Scaffold (+3 more)

### Community 53 - "Client UI Utilities"
Cohesion: 0.14
Nodes (14): dependencies, class-variance-authority, clsx, cn, lucide-react, monaco-editor, @monaco-editor/react, radix-ui (+6 more)

### Community 54 - "ImageReference"
Cohesion: 0.15
Nodes (10): IRegistryProvider, RegistryVersionInfo, RemoteDigest, Tags, ImageReference, Canonical, Digest, Host (+2 more)

### Community 55 - ".DeleteUnmanagedAsync"
Cohesion: 0.19
Nodes (8): DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess, DeviceStackDeployService, IDomeSocketApiFactory

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

### Community 60 - "Dome.Domain.Settings"
Cohesion: 0.15
Nodes (6): SystemSetting, Key, Value, SystemSettingConfiguration, SocketConfiguration, StackConfiguration

### Community 61 - "DomeSocketApiFactory"
Cohesion: 0.06
Nodes (15): Dome.Socket.Tests.Pairing, ClientKey, DomeSocketApiFactory, PairEndpoint, SocketCertificate, PairingProof, PairRequest, Nonce (+7 more)

### Community 62 - "Dome.Domain.Settings"
Cohesion: 0.17
Nodes (4): Dome.Business.Tests.Settings, Dome.Domain.Settings, SystemSettingKeys, SystemSettingRepository

### Community 63 - "Docker/DependencyInjection.cs"
Cohesion: 0.13
Nodes (8): Dome.Socket.Tests.Docker.Configuration, Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Processes, Dome.Domain.Tests.Seeding, Dome.Socket.Api.Docker.Configuration, Dome.Domain.Tests.Sqlite, Dome.Domain.Sqlite, Dome.Socket.Api.Docker

### Community 64 - "Commands"
Cohesion: 0.12
Nodes (17): `add` — Add components, `apply` — Apply a preset to an existing project, `build` — Build a custom registry, Commands, Contents, `diff` — Check for updates, `docs` — Get component documentation URLs, Dry-Run Mode (+9 more)

### Community 65 - "DomeSocketApiFactory.cs"
Cohesion: 0.08
Nodes (6): Dome.Business, Dome.Socket.Tests.Security, Dome.Socket.Contracts.Pairing, Dome.Socket.Api.Endpoints.Pairing, Dome.Business.Tests.Sockets, Dome.Socket.Api.Security

### Community 66 - "DomeDbContext"
Cohesion: 0.18
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 67 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 68 - "Vite and Tailwind Config"
Cohesion: 0.25
Nodes (5): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname, { version }

### Community 69 - "GetDeviceDiagramResponse"
Cohesion: 0.10
Nodes (15): Dome.Shared.Diagrams, DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, DeviceDiagramDto, Containers (+7 more)

### Community 72 - "ImageVersionCache"
Cohesion: 0.20
Nodes (9): ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest, RemoteTags, Unsupported (+1 more)

### Community 73 - "Customization & Theming"
Cohesion: 0.14
Nodes (14): 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors, Border Radius, Changing the Theme, Checking for Updates (+6 more)

### Community 76 - "DeleteDeviceStackRequest"
Cohesion: 0.25
Nodes (5): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId

### Community 77 - "Dome.Socket.Api.Docker.Clients"
Cohesion: 0.07
Nodes (15): Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Docker.Clients, SystemDataUsageResponse, SystemDataUsageVolumeResponse, Name, UsageData (+7 more)

### Community 78 - "Icons"
Cohesion: 0.50
Nodes (3): Icons, Icons in Button use data-icon attribute, Pass icons as component objects, not string keys

### Community 79 - "README Feature Summary"
Cohesion: 0.20
Nodes (10): Compose files persisted under /var/lib/dome/compose, Compose stack create, edit and deploy, Container image update icon, Socket version display and update icon, v0.1.0 first public release, v0.3.1 release, v0.4.0 release, v0.4.1 release (no user-facing changes) (+2 more)

### Community 80 - "Release Notes Highlights"
Cohesion: 0.29
Nodes (8): Start and stop containers from details panel, Images moved to ghcr.io/dome-works (breaking), Project renamed HomelabDocs to DOME (breaking), SQLite settings and paths changed (breaking), v0.3.0 release with breaking changes, EF Core migrations in Dome.Domain, Published images ghcr.io/dome-works, SQLite persistence and backups

### Community 81 - ".SetupAsync"
Cohesion: 0.15
Nodes (7): AdminAccountService, AdminSetupResult, Error, IsConflict, IsSuccess, IAdminAccountService, AdminAccountServiceTests

### Community 82 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 83 - "Security Notes"
Cohesion: 0.24
Nodes (7): Current limitations, No remote Docker tcp:// support, No authentication or authorization, Out-of-scope issues, Private vulnerability reporting, In-scope security issues, Only main is supported

### Community 84 - "README.md"
Cohesion: 0.29
Nodes (6): AGENTS.md entry point, Root cause before fix working method, Validation commands (dotnet, npm), Contributing: read AGENTS.md and Instructions.md, Pre-1.0 warning, Troubleshooting: Socket unreachable

### Community 85 - "Dome.slnx"
Cohesion: 0.05
Nodes (38): Docker.DotNet.Enhanced, FakeItEasy, FsCheck, FsCheck.Xunit.v3, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Abstractions (+30 more)

### Community 86 - "DeleteDeviceUnmanagedStackEndpoint"
Cohesion: 0.25
Nodes (5): DeleteDeviceUnmanagedStackEndpoint, DeleteDeviceUnmanagedStackRequest, ProjectName, RemoveVolumes, SocketName

### Community 87 - "Step 4: Map Tokens to the Theme"
Cohesion: 0.29
Nodes (7): Dark mode, Elevation, Focus, Radius, Role mapping, Step 4: Map Tokens to the Theme, Type scale

### Community 88 - "Dome.Business.Devices"
Cohesion: 0.13
Nodes (9): Dome.Business.Images, Dome.Shared.Images, Dome.Business.Devices, Dome.Shared.Containers, Dome.Business.Tests.Images, Dome.Domain.Images, Dome.Shared.Devices, DependencyInjection (+1 more)

### Community 90 - "devDependencies"
Cohesion: 0.15
Nodes (13): devDependencies, oxlint, shadcn, @shadcn/lint, tailwindcss, @tailwindcss/vite, tw-animate-css, @types/node (+5 more)

### Community 91 - "OCI Token and Tag Responses"
Cohesion: 0.11
Nodes (14): SocketState, NeedsPairing, Offline, Online, Unauthorized, OciTagListResponse, Tags, OciTokenResponse (+6 more)

### Community 92 - "Socket agents discover containers (breaking)"
Cohesion: 0.67
Nodes (3): Socket agents discover containers (breaking), v0.2.0 Socket agents replace direct Docker access, Architecture: Client to Server to Socket

### Community 93 - "SetupAdminEndpoint"
Cohesion: 0.33
Nodes (4): SetupAdminEndpoint, SetupAdminRequest, Password, Username

### Community 94 - "GitHub Release Changelog Config"
Cohesion: 0.29
Nodes (6): labels, title, changelog, categories, exclude, labels

### Community 95 - "Tag and Env Workflow Steps"
Cohesion: 0.29
Nodes (7): TAG, steps, env, id, name, run, uses

### Community 96 - "Docker Build Context Config"
Cohesion: 0.29
Nodes (7): context, file, image, include, strategy, fail-fast, matrix

### Community 97 - "GetContainersResponse"
Cohesion: 0.40
Nodes (3): GetContainersResponse, Containers, DeviceQueryServiceTests

### Community 98 - "Tools"
Cohesion: 0.17
Nodes (11): Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries`, `shadcn:get_project_registries`, `shadcn:list_items_in_registries`, shadcn MCP Server (+3 more)

### Community 99 - "SocketService"
Cohesion: 0.10
Nodes (11): ISocketApiKeys, SocketMutationResult, ApiKey, Error, IsConflict, IsNotFound, IsSuccess, Socket (+3 more)

### Community 100 - "OciAuthenticatedResponse"
Cohesion: 0.50
Nodes (3): OciAuthenticatedResponse, Response, Token

### Community 102 - "Client npm Scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 104 - "Component Composition"
Cohesion: 0.17
Nodes (12): Avatar always needs AvatarFallback, Button has no isPending or isLoading prop, Callouts use Alert, Card structure, Choosing between overlay components, Component Composition, Contents, Dialog, Sheet, and Drawer always need a Title (+4 more)

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

### Community 116 - ".AddDomeBusiness"
Cohesion: 0.08
Nodes (12): GetDevicesEndpoint, TelemetryReporter, DeviceQueryService, IDeviceQueryService, IApplicationVersion, Current, DeviceImageVersionChecker, IDeviceImageVersionChecker (+4 more)

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
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 125 - "ContainerDto"
Cohesion: 0.18
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 127 - "Client index.html"
Cohesion: 0.50
Nodes (3): Run modes: Rider, Docker build, published images, Client entry module src/main.tsx, Client index.html

### Community 130 - "ComposeYamlEditor.tsx"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 132 - "Endpoint"
Cohesion: 0.06
Nodes (22): Dome.Business.Stacks, Dome.Shared.Stacks, LoginEndpoint, CreateDeviceStackEndpoint, DeployDeviceStackEndpoint, GetDeviceStackEndpoint, GetDeviceStackRequest, SocketName (+14 more)

## Knowledge Gaps
- **351 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+346 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 911 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **16 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Dome.Business.Devices` to `Fact`, `Guid`, `DateTimeOffset`, `SocketResponse`, `IDomeSocketApi`, `.GetDiagramAsync`, `.CreateService`, `Docker Hub Registry Provider`, `IReadOnlyList`, `.DeleteUnmanagedAsync`, `Dome.Domain.Settings`, `DomeSocketApiFactory`, `DomeSocketApiFactory.cs`, `DomeDbContext`, `Dome.Business.Devices`, `SocketService`, `Document a DOME release`, `.AddDomeBusiness`, `ContainerMapperProperties.cs`?**
  _High betweenness centrality (0.034) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _351 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `RecordingProcessRunner` be split into smaller, more focused modules?**
  _Cohesion score 0.07071887784921099 - nodes in this community are weakly interconnected._
- **Why does `Stack` connect `Guid` to `DomeDbContext`, `DateTimeOffset`, `IDomeSocketApi`, `.GetDiagramAsync`, `.DeleteUnmanagedAsync`, `Dome.Business.Devices`, `Dome.Domain.Settings`, `Dome.Business.Devices`?**
  _High betweenness centrality (0.015) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `Socket` (e.g. with `CHANGELOG.md` and `.CreateHttpClient()`) actually correct?**
  _`Socket` has 2 INFERRED edges - model-reasoned connections that need verification._
- **Should `Guid` be split into smaller, more focused modules?**
  _Cohesion score 0.0683526999316473 - nodes in this community are weakly interconnected._