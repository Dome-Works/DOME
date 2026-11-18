# Graph Report - DOME  (2026-11-18)

## Corpus Check
- 369 files · ~69,451 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2629 nodes · 6359 edges · 143 communities (122 shown, 21 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 471 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `53e6a9f0`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Fact
- RecordingProcessRunner
- Guid
- Client Layout and Routing
- Endpoint
- DateTimeOffset
- ISystemSettingsService
- Socket and Compose Editor UI
- Build Workflow
- Container API Client
- CodeQL Workflow
- SocketResponse
- .AddDomeBusiness
- Docker Workflow
- ContainerDetailPane.tsx
- Dev Compose Config
- Scorecard Workflow
- StackDetailPane.tsx
- OciRegistryProvider
- fastendpoints
- Dome.Business.Auth
- SocketsPage.tsx
- Compose Sample Config
- DeviceContainerLifecycleRequest
- Client Lint and Tooling Config
- Zizmor Workflow Config
- TypeScript App Config
- IReadOnlyCollection
- .GetDiagramAsync
- DeviceStackDeployService.cs
- ContainerResponse
- Fact
- microsoft_entityframeworkcore
- The Design System Page
- .AddDomeDomain
- shadcn UI Config
- TypeScript Node Config
- SystemSettingsService
- Docker Registry Tests and Image Endpoints
- Endpoint
- Docker Build Args Config
- .CreateService
- Oxlint Config
- ContainerViewModel
- Input
- .TryParse
- .Classify
- ImageVersionCheckDto
- Docker Hub Registry Provider
- Docker/DependencyInjection.cs
- CreateSocketRequest
- SocketService
- Design Systems from DESIGN.md
- Client UI Utilities
- ImageReference
- .DeleteUnmanagedAsync
- ContainerVolumeDto
- AGENTS.md Guidelines
- Dependabot Config
- Dependency Review Workflow
- Dome.Domain.Settings
- DomeSocketApiFactoryTests
- Dome.Api/Program.cs
- Dome.Domain.Settings
- Commands
- DomeSocketApiFactory.cs
- DomeDbContext
- Release Workflow
- showcase-coverage.mjs
- GetDeviceDiagramResponse
- IStackService
- Dome.Socket.Api.Docker.Configuration
- Socket
- Customization & Theming
- .Hash
- DeleteContainerResult
- DatabaseInitializerTests
- Dome.Socket.Api.Docker.Clients
- SystemSetting
- README Feature Summary
- Release Notes Highlights
- .SetupAsync
- Publish Workflow Attestations
- Security Notes
- README.md
- Dome.slnx
- DeleteDeviceUnmanagedStackEndpoint
- PairRequest
- GetDeviceDiagramEndpoint
- devDependencies
- OCI Token and Tag Responses
- Socket agents discover containers (breaking)
- DockerComposeResult
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Build Context Config
- ContainerMapperProperties.cs
- Tools
- SocketMutationResult
- OciAuthenticatedResponse
- .Map
- Client npm Scripts
- Graphify Rules and Workflow
- Component Composition
- ContainerMapperProperties
- Docker Env Variables
- DomeSocketApiFactory
- TypeScript Project References
- Graphify Skill Docs
- Favicon Icon
- Styling & Customization
- IDockerComposeClient
- Document a DOME release
- shadcn/ui
- ImageVersionCache
- AssemblyApplicationVersion
- Chat & Messaging
- Registry Authoring and Addresses
- ContainerMapperProperties.cs
- Regex
- Base vs Radix
- ImageTagVersion
- Dome.Shared.Devices
- ContainerDto
- .Resolve
- Client index.html
- shadcn
- .Compute
- SocketApiKeys
- DeviceDiagramDto
- Dome.Api.Endpoints.Devices
- GetDeviceImageVersionChecksEndpoint
- Dome.Socket.Api.Docker.Processes
- .GetDevicesAsync
- DeleteContainerResult
- ImageVersionCheckResponse
- ScriptedHttpMessageHandler
- DeleteDeviceContainerEndpoint
- DockerComposeClient
- TelemetryBuildInfo
- RegistryVersionInfo

## God Nodes (most connected - your core abstractions)
1. `cn()` - 92 edges
2. `Socket` - 62 edges
3. `IDomeSocketApi` - 53 edges
4. `Button()` - 44 edges
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

## Communities (143 total, 21 thin omitted)

### Community 0 - "Fact"
Cohesion: 0.20
Nodes (4): SocketHealthResponse, DockerReachable, Version, SocketServiceTests

### Community 1 - "RecordingProcessRunner"
Cohesion: 0.19
Nodes (9): DockerComposeClientTests, RecordingProcessRunner, Arguments, CallCount, Environment, Exception, FileName, Result (+1 more)

### Community 2 - "Guid"
Cohesion: 0.07
Nodes (25): StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService, IStackRepository (+17 more)

### Community 3 - "Client Layout and Routing"
Cohesion: 0.07
Nodes (61): Items always inside their Group component, Chat & Messaging → [chat.md](./rules/chat.md), CLI, Component Structure → [composition.md](./rules/composition.md), Critical Rules, Forms & Inputs → [forms.md](./rules/forms.md), Icons → [icons.md](./rules/icons.md), Styling & Tailwind → [styling.md](./rules/styling.md) (+53 more)

### Community 4 - "Endpoint"
Cohesion: 0.08
Nodes (5): DockerContainerLifecycleResult, NotFound, Succeeded, GetHealthEndpoint, StubDockerEngineClient

### Community 5 - "DateTimeOffset"
Cohesion: 0.12
Nodes (7): InitialCreate, AddSockets, AddStacks, AddImageVersionCache, AddSystemSettings, AddSocketAuth, DomeDbContextModelSnapshot

### Community 6 - "ISystemSettingsService"
Cohesion: 0.09
Nodes (15): GetAuthStatusEndpoint, LogoutEndpoint, GetSystemSettingsEndpoint, UpdateSystemSettingsEndpoint, ISystemSettingsService, TelemetrySettings, SystemSettingsResponse, Environment (+7 more)

### Community 7 - "Socket and Compose Editor UI"
Cohesion: 0.19
Nodes (26): Known traps, radix-ui, react, ComposeEditorDialog(), ComposeEditorDialogProps, ComposeYamlEditor, ConfirmDeleteDialog(), ConfirmDeleteDialogProps (+18 more)

### Community 8 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 9 - "Container API Client"
Cohesion: 0.07
Nodes (52): @xyflow/react, deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), fetchDeviceImageVersionChecks() (+44 more)

### Community 10 - "CodeQL Workflow"
Cohesion: 0.05
Nodes (36): name, permissions, runs-on, steps, strategy, concurrency, cancel-in-progress, group (+28 more)

### Community 11 - "SocketResponse"
Cohesion: 0.06
Nodes (26): CreateSocketEndpoint, DeleteSocketEndpoint, GetSocketEndpoint, GetSocketsEndpoint, GetSocketStatusesEndpoint, SetSocketKeyEndpoint, UpdateSocketEndpoint, ISocketService (+18 more)

### Community 12 - ".AddDomeBusiness"
Cohesion: 0.26
Nodes (4): GetContainersResponse, Containers, IDomeSocketApi, DeviceStackDeployServiceTests

### Community 13 - "Docker Workflow"
Cohesion: 0.06
Nodes (31): name, permissions, runs-on, steps, strategy, env, PLATFORMS, context (+23 more)

### Community 14 - "ContainerDetailPane.tsx"
Cohesion: 0.12
Nodes (36): Composition: asChild (radix) vs render (base), updateDeviceStack(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState() (+28 more)

### Community 15 - "Dev Compose Config"
Cohesion: 0.08
Nodes (29): build, environment, image, ports, restart, volumes, context, dockerfile (+21 more)

### Community 16 - "Scorecard Workflow"
Cohesion: 0.07
Nodes (29): name, permissions, runs-on, steps, jobs, analysis, name, permissions (+21 more)

### Community 17 - "StackDetailPane.tsx"
Cohesion: 0.14
Nodes (37): react-router, sonner, fetchAuthStatus(), login(), logout(), postAuth(), setupAdmin(), fetchDeviceContainers() (+29 more)

### Community 18 - "OciRegistryProvider"
Cohesion: 0.18
Nodes (4): OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 19 - "fastendpoints"
Cohesion: 0.26
Nodes (3): Dome.Shared.Sockets, Dome.Business.Sockets, Dome.Api.Endpoints.Sockets

### Community 20 - "Dome.Business.Auth"
Cohesion: 0.06
Nodes (22): Dome.Business.Auth, Dome.Api.Endpoints.Auth, Dome.Shared.Auth, Dome.Business.Tests.Auth, AuthSignIn, AuthStatusResponse, Authenticated, OnboardingCompleted (+14 more)

### Community 21 - "SocketsPage.tsx"
Cohesion: 0.13
Nodes (28): Assistant activity uses Trace, readApiError(), createSocket(), deleteSocket(), fetchSockets(), fetchSocketStatuses(), setSocketKey(), updateSocket() (+20 more)

### Community 22 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 23 - "DeviceContainerLifecycleRequest"
Cohesion: 0.20
Nodes (5): DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint

### Community 24 - "Client Lint and Tooling Config"
Cohesion: 0.07
Nodes (26): class-variance-authority, clsx, cn, monaco-editor, @monaco-editor/react, oxlint, react-dom, shadcn (+18 more)

### Community 25 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 26 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 27 - "IReadOnlyCollection"
Cohesion: 0.08
Nodes (15): ImageVersionCacheRepository, DockerEngineClient, IDockerEngineClient, IImageVersionCheckService, ImageVersionCheckService, IRegistryProvider, GetContainersEndpoint, CheckImageVersionsEndpoint (+7 more)

### Community 28 - ".GetDiagramAsync"
Cohesion: 0.38
Nodes (3): DeviceDiagramService, IDeviceQueryService, DeviceDiagramServiceTests

### Community 29 - "DeviceStackDeployService.cs"
Cohesion: 0.19
Nodes (4): Dome.Socket.Contracts.Compose, Dome.Socket.Contracts, Dome.Socket.Contracts.Health, Dome.Business.Tests.Sockets

### Community 30 - "ContainerResponse"
Cohesion: 0.11
Nodes (16): ContainerResponse, Digest, Id, Image, ImageId, Name, Stack, State (+8 more)

### Community 31 - "Fact"
Cohesion: 0.14
Nodes (3): ApiKeyMiddlewareTests, SocketCertificateTests, SocketKeyTests

### Community 33 - "The Design System Page"
Cohesion: 0.08
Nodes (29): ColorPair(), contrast(), luminance(), toHex(), toRgb(), defaultStates, previewState(), StateMatrix() (+21 more)

### Community 34 - ".AddDomeDomain"
Cohesion: 0.22
Nodes (4): DatabaseInitializer, IDatabaseInitializer, SqlitePath, SqlitePathTests

### Community 35 - "shadcn UI Config"
Cohesion: 0.11
Nodes (17): aliases, components, hooks, lib, ui, utils, iconLibrary, rsc (+9 more)

### Community 36 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 37 - "SystemSettingsService"
Cohesion: 0.18
Nodes (4): SocketAddressValidator, SocketAddressValidatorTests, DockerEndpointValidatorTests, ImageReferenceParserTests

### Community 38 - "Docker Registry Tests and Image Endpoints"
Cohesion: 0.16
Nodes (7): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images, ImageVersionCheckServiceTests

### Community 39 - "Endpoint"
Cohesion: 0.12
Nodes (11): LoginEndpoint, ContainerLifecycleRequest, Id, DeleteContainersEndpoint, StartContainerEndpoint, StopContainerEndpoint, DeleteContainersRequest, ContainerIds (+3 more)

### Community 40 - "Docker Build Args Config"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 42 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 44 - "Input"
Cohesion: 0.27
Nodes (12): Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs, Forms use FieldGroup + Field, InputGroup requires InputGroupInput/InputGroupTextarea, Option sets (2–7 choices) use ToggleGroup (+4 more)

### Community 46 - ".Classify"
Cohesion: 0.14
Nodes (11): ImageVersionClassification, CurrentTag, Kind, NewestTag, ImageVersionClassifier, ImageVersionSnapshot, Error, RemoteDigest (+3 more)

### Community 47 - "ImageVersionCheckDto"
Cohesion: 0.13
Nodes (14): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag, ImageVersionCheckDto (+6 more)

### Community 48 - "Docker Hub Registry Provider"
Cohesion: 0.33
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 49 - "Docker/DependencyInjection.cs"
Cohesion: 0.15
Nodes (7): Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Api.Docker.Compose, DependencyInjection, UpComposeEndpoint, ComposeUpRequest, ComposeYaml, ProjectName

### Community 50 - "CreateSocketRequest"
Cohesion: 0.40
Nodes (4): CreateSocketRequest, Address, Key, Name

### Community 51 - "SocketService"
Cohesion: 0.14
Nodes (14): IApplicationVersion, Current, SocketService, SocketState, NeedsPairing, Offline, Online, Unauthorized (+6 more)

### Community 52 - "Design Systems from DESIGN.md"
Cohesion: 0.10
Nodes (20): Contents, Coverage check, Dark mode, Design Systems from DESIGN.md, Elevation, Focus, Fonts, Inputs to Confirm (+12 more)

### Community 53 - "Client UI Utilities"
Cohesion: 0.14
Nodes (14): dependencies, class-variance-authority, clsx, cn, lucide-react, monaco-editor, @monaco-editor/react, radix-ui (+6 more)

### Community 54 - "ImageReference"
Cohesion: 0.13
Nodes (11): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, ImageReference, Canonical, Digest, Host (+3 more)

### Community 55 - ".DeleteUnmanagedAsync"
Cohesion: 0.12
Nodes (12): DependencyInjection, DeviceQueryService, DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess (+4 more)

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
Nodes (7): ImageVersionCacheConfiguration, SystemSetting, Key, Value, SystemSettingConfiguration, SocketConfiguration, StackConfiguration

### Community 62 - "Dome.Api/Program.cs"
Cohesion: 0.15
Nodes (5): Dome.Business.Settings, Dome.Business.Tests.Settings, Dome.Shared.Settings, Dome.Api.Endpoints.Settings, Dome.Api.Telemetry

### Community 63 - "Dome.Domain.Settings"
Cohesion: 0.11
Nodes (9): Dome.Domain.Seeding, Dome.Domain.Tests.Seeding, Dome.Domain.Tests.Sqlite, Dome.Domain.Settings, Dome.Domain.Images, Dome.Domain.Sqlite, DependencyInjection, SystemSettingKeys (+1 more)

### Community 64 - "Commands"
Cohesion: 0.12
Nodes (17): `add` — Add components, `apply` — Apply a preset to an existing project, `build` — Build a custom registry, Commands, Contents, `diff` — Check for updates, `docs` — Get component documentation URLs, Dry-Run Mode (+9 more)

### Community 65 - "DomeSocketApiFactory.cs"
Cohesion: 0.14
Nodes (4): Dome.Socket.Tests.Security, Dome.Socket.Api.Security, Dome.Socket.Api.Docker, SocketCertificate

### Community 66 - "DomeDbContext"
Cohesion: 0.20
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 67 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 68 - "showcase-coverage.mjs"
Cohesion: 0.13
Nodes (11): components, missing, source, [uiArg, ...roots], uiDir, walk(), @tailwindcss/vite, vite (+3 more)

### Community 69 - "GetDeviceDiagramResponse"
Cohesion: 0.10
Nodes (14): Dome.Shared.Diagrams, DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, GetDeviceDiagramEndpoint, GetDeviceDiagramRequest (+6 more)

### Community 70 - "IStackService"
Cohesion: 0.14
Nodes (9): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName, GetDeviceStackEndpoint, GetDeviceStackRequest, SocketName, StackId (+1 more)

### Community 71 - "Dome.Socket.Api.Docker.Configuration"
Cohesion: 0.15
Nodes (7): Dome.Socket.Tests.Docker.Configuration, Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Configuration, DockerEndpointOptions, Endpoint, DockerEndpointValidator, DockerEndpointOptionsTests

### Community 72 - "Socket"
Cohesion: 0.18
Nodes (9): ISocketRepository, Socket, Address, CertificateFingerprint, CreatedAt, Id, Name, ProtectedApiKey (+1 more)

### Community 73 - "Customization & Theming"
Cohesion: 0.14
Nodes (14): 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors, Border Radius, Changing the Theme, Checking for Updates (+6 more)

### Community 76 - "DatabaseInitializerTests"
Cohesion: 0.25
Nodes (5): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId

### Community 77 - "Dome.Socket.Api.Docker.Clients"
Cohesion: 0.08
Nodes (11): Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Containers, DockerContainerLifecycleExceptionMapper, SystemDataUsageResponse, SystemDataUsageVolumeResponse, Name (+3 more)

### Community 78 - "SystemSetting"
Cohesion: 0.40
Nodes (4): Icons, Icons in Button use data-icon attribute, No sizing classes on icons inside components, Pass icons as component objects, not string keys

### Community 79 - "README Feature Summary"
Cohesion: 0.20
Nodes (10): Compose files persisted under /var/lib/dome/compose, Compose stack create, edit and deploy, Container image update icon, Socket version display and update icon, v0.1.0 first public release, v0.3.1 release, v0.4.0 release, v0.4.1 release (no user-facing changes) (+2 more)

### Community 80 - "Release Notes Highlights"
Cohesion: 0.29
Nodes (8): Start and stop containers from details panel, Images moved to ghcr.io/dome-works (breaking), Project renamed HomelabDocs to DOME (breaking), SQLite settings and paths changed (breaking), v0.3.0 release with breaking changes, EF Core migrations in Dome.Domain, Published images ghcr.io/dome-works, SQLite persistence and backups

### Community 81 - ".SetupAsync"
Cohesion: 0.11
Nodes (12): SetupAdminEndpoint, AdminAccountService, AdminSetupResult, Error, IsConflict, IsSuccess, IAdminAccountService, SystemSettingsService (+4 more)

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

### Community 87 - "PairRequest"
Cohesion: 0.17
Nodes (8): Dome.Socket.Contracts.Pairing, Dome.Socket.Api.Endpoints.Pairing, PairEndpoint, SocketSecurity, PairRequest, Nonce, PairResponse, Proof

### Community 88 - "GetDeviceDiagramEndpoint"
Cohesion: 0.10
Nodes (13): Dome.Business.Images, Dome.Business, Dome.Shared.Images, Dome.Business.Devices, Dome.Shared.Containers, Dome.Domain.Stacks, Dome.Business.Tests.Devices, Dome.Business.Tests.Stacks (+5 more)

### Community 90 - "devDependencies"
Cohesion: 0.15
Nodes (13): devDependencies, oxlint, shadcn, @shadcn/lint, tailwindcss, @tailwindcss/vite, tw-animate-css, @types/node (+5 more)

### Community 91 - "OCI Token and Tag Responses"
Cohesion: 0.20
Nodes (5): OciTagListResponse, Tags, OciTokenResponse, AccessToken, Token

### Community 92 - "Socket agents discover containers (breaking)"
Cohesion: 0.67
Nodes (3): Socket agents discover containers (breaking), v0.2.0 Socket agents replace direct Docker access, Architecture: Client to Server to Socket

### Community 93 - "DockerComposeResult"
Cohesion: 0.18
Nodes (8): DockerComposeResult, Error, IsInvalid, Succeeded, ProcessRunResult, ExitCode, StandardError, StandardOutput

### Community 94 - "GitHub Release Changelog Config"
Cohesion: 0.29
Nodes (6): labels, title, changelog, categories, exclude, labels

### Community 95 - "Tag and Env Workflow Steps"
Cohesion: 0.29
Nodes (7): TAG, steps, env, id, name, run, uses

### Community 96 - "Docker Build Context Config"
Cohesion: 0.29
Nodes (7): context, file, image, include, strategy, fail-fast, matrix

### Community 97 - "ContainerMapperProperties.cs"
Cohesion: 0.15
Nodes (4): Dome.Socket.Tests.Docker.Clients, FallbackNameSample, NameResolutionSample, StackSample

### Community 98 - "Tools"
Cohesion: 0.17
Nodes (11): Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries`, `shadcn:get_project_registries`, `shadcn:list_items_in_registries`, shadcn MCP Server (+3 more)

### Community 99 - "SocketMutationResult"
Cohesion: 0.25
Nodes (6): SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Socket

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

### Community 111 - "Styling & Customization"
Cohesion: 0.15
Nodes (13): Built-in variants first, className for layout only, Contents, No manual dark: color overrides, No manual z-index on overlay components, No raw color values for status/state indicators, No space-x-* / space-y-*, Prefer size-* over w-* h-* when equal (+5 more)

### Community 112 - "IDockerComposeClient"
Cohesion: 0.18
Nodes (7): IDockerComposeClient, DownComposeEndpoint, ComposeCommandErrorResponse, Message, ComposeDownRequest, ProjectName, RemoveVolumes

### Community 113 - "Document a DOME release"
Cohesion: 0.18
Nodes (10): CHANGELOG.md, Cutting a version, Do not do, Document a DOME release, GitHub Release notes, Inputs, Inspect before writing, No user-facing changes (+2 more)

### Community 115 - "shadcn/ui"
Cohesion: 0.18
Nodes (11): Component Docs, Examples, and Usage, Current Project Context, Design Systems from DESIGN.md, Detailed References, Key Fields, Key Patterns, Principles, Quick Reference (+3 more)

### Community 116 - "ImageVersionCache"
Cohesion: 0.11
Nodes (10): TelemetryReporter, DeviceImageVersionChecker, ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest (+2 more)

### Community 117 - "AssemblyApplicationVersion"
Cohesion: 0.20
Nodes (5): Dome.Socket.Api, AssemblyApplicationVersion, Current, ApplicationVersion, Current

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

### Community 123 - "ImageTagVersion"
Cohesion: 0.22
Nodes (7): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix

### Community 124 - "Dome.Shared.Devices"
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 125 - "ContainerDto"
Cohesion: 0.15
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 127 - "Client index.html"
Cohesion: 0.50
Nodes (3): Run modes: Rider, Docker build, published images, Client entry module src/main.tsx, Client index.html

### Community 129 - ".Compute"
Cohesion: 0.36
Nodes (3): Dome.Socket.Tests.Pairing, PairingProof, PairingProofTests

### Community 130 - "SocketApiKeys"
Cohesion: 0.31
Nodes (3): ISocketApiKeys, SocketApiKeys, SocketApiKeysTests

### Community 131 - "DeviceDiagramDto"
Cohesion: 0.20
Nodes (8): DeviceDiagramDto, Containers, Stacks, DiagramStackDto, Id, Kind, ProjectName, TotalBytes

### Community 132 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.05
Nodes (27): Dome.Business.Stacks, Dome.Api.Endpoints.Devices, Dome.Shared.Stacks, DeployDeviceStackEndpoint, GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse (+19 more)

### Community 133 - "GetDeviceImageVersionChecksEndpoint"
Cohesion: 0.24
Nodes (5): GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest, SocketName, GetDeviceImageVersionChecksResponse, Checks

### Community 134 - "Dome.Socket.Api.Docker.Processes"
Cohesion: 0.25
Nodes (3): Dome.Socket.Api.Docker.Processes, IProcessRunner, ProcessRunner

### Community 135 - ".GetDevicesAsync"
Cohesion: 0.31
Nodes (5): GetDevicesEndpoint, DeviceResponse, Name, GetDevicesResponse, Devices

### Community 136 - "DeleteContainerResult"
Cohesion: 0.22
Nodes (8): DeleteContainerResult, ContainerId, Error, Status, DeleteContainerStatus, Deleted, Failed, NotFound

### Community 137 - "ImageVersionCheckResponse"
Cohesion: 0.22
Nodes (8): ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest, Tags, Unsupported

### Community 139 - "DeleteDeviceContainerEndpoint"
Cohesion: 0.25
Nodes (5): DeleteDeviceContainerEndpoint, DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes

### Community 140 - "DockerComposeClient"
Cohesion: 0.33
Nodes (4): DockerComposeClient, DockerComposeOptions, DockerCliPath, WorkingRoot

### Community 142 - "RegistryVersionInfo"
Cohesion: 0.50
Nodes (3): RegistryVersionInfo, RemoteDigest, Tags

## Knowledge Gaps
- **352 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+347 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 914 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **21 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Socket` to `Fact`, `Guid`, `Dome.Api.Endpoints.Devices`, `DateTimeOffset`, `Endpoint`, `.AddDomeBusiness`, `OciRegistryProvider`, `IReadOnlyCollection`, `.GetDiagramAsync`, `DeviceStackDeployService.cs`, `.CreateService`, `SocketService`, `.DeleteUnmanagedAsync`, `Dome.Domain.Settings`, `DomeSocketApiFactoryTests`, `DomeSocketApiFactory.cs`, `DomeDbContext`, `GetDeviceDiagramEndpoint`, `DomeSocketApiFactory`, `Document a DOME release`, `ContainerMapperProperties.cs`?**
  _High betweenness centrality (0.031) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _352 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Guid` be split into smaller, more focused modules?**
  _Cohesion score 0.07494824016563147 - nodes in this community are weakly interconnected._
- **Why does `Stack` connect `Guid` to `DomeDbContext`, `Dome.Api.Endpoints.Devices`, `DateTimeOffset`, `Socket`, `.AddDomeBusiness`, `.GetDiagramAsync`, `.DeleteUnmanagedAsync`, `GetDeviceDiagramEndpoint`, `Dome.Domain.Settings`, `DeviceStackDeployService.cs`?**
  _High betweenness centrality (0.015) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `Socket` (e.g. with `CHANGELOG.md` and `.CreateHttpClient()`) actually correct?**
  _`Socket` has 2 INFERRED edges - model-reasoned connections that need verification._
- **Should `Client Layout and Routing` be split into smaller, more focused modules?**
  _Cohesion score 0.06582952815829528 - nodes in this community are weakly interconnected._