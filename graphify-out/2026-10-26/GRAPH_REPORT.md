# Graph Report - DOME  (2026-10-25)

## Corpus Check
- Corpus is ~37,135 words - fits in a single context window. You may not need a graph.

## Summary
- 1960 nodes · 4510 edges · 111 communities (103 shown, 8 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 244 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- HTTP Client and Refit API
- Docker Compose Processes
- Image Version Classification
- Client Layout and Routing
- HTTP Endpoint Handlers
- EF Core Domain and Migrations
- Docker Configuration Tests
- Socket and Compose Editor UI
- Build Workflow
- Container API Client
- CodeQL Workflow
- Socket Service Handlers
- Stack Service
- Docker Workflow
- Container Detail Pane
- Dev Compose Config
- Scorecard Workflow
- Socket Endpoints
- HTTP Request Primitives
- Container Node Diagram UI
- Docker Client Tests and Models
- Device Container API and Errors
- Compose Sample Config
- Device Diagram Service
- Client Lint and Tooling Config
- Zizmor Workflow Config
- TypeScript App Config
- Image Version Check Service
- Domain Seeding and Stacks
- Socket Contracts and Health
- Docker Client Wrappers
- Device Containers Endpoint
- Device Query Service
- Socket Repository CRUD
- Stack Deletion Logic
- shadcn UI Config
- TypeScript Node Config
- Assembly Versioning
- Docker Registry Tests and Image Endpoints
- Name Shortening Helpers
- Docker Build Args Config
- Device Service Handlers
- Oxlint Config
- Compose Lifecycle Endpoints
- Device and Container Shared Types
- Health Endpoint and FastEndpoints
- Devtools Package
- Image Version Cache
- Docker Hub Registry Provider
- Create Device Stack Endpoint
- Device Endpoint Wiring
- Registry Provider Abstraction
- Container List Model
- Client UI Utilities
- GHCR Registry Provider
- Business Layer Tests
- Diagram Stack View Model
- AGENTS.md Guidelines
- Dependabot Config
- Dependency Review Workflow
- Domain SQLite Tests
- Container View Model
- Stack Project Name Parsing
- Image Tag Parsing
- Container DTO
- Image Business Layer
- Container Lifecycle Endpoints
- Release Workflow
- Vite and Tailwind Config
- Device Diagram Endpoint
- Container Response Mapping
- Docker Client Fallback Tests
- Container Volume DTO
- Container Volume View Model
- Device Stacks Endpoint
- Device Stack Update Endpoint
- Socket Status Response
- System Data Usage Model
- Image Version Check Response
- README Feature Summary
- Release Notes Highlights
- Changelog and Release Process
- Publish Workflow Attestations
- Security Notes
- Dev Validation and Run Modes
- Docker.DotNet and FastEndpoints Deps
- Test Tooling Deps
- Config and DI Deps
- Monaco Editor Deps
- Socket Update Endpoint
- Container Volume Response Mapping
- OCI Token and Tag Responses
- Changelog Version History
- Solution and API Project
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Build Context Config
- Business Test Deps
- Toast Notifications UI
- Device Image Version Check Tests
- EF Core Design Deps
- HTTP and Logging Deps
- Client npm Scripts
- Graphify Rules and Workflow
- Socket Docker Limitations
- OCI Auth Response
- Docker Env Variables
- Client Dependency Overrides
- TypeScript Project References
- Graphify Skill Docs
- Favicon Icon

## God Nodes (most connected - your core abstractions)
1. `cn()` - 82 edges
2. `Socket` - 51 edges
3. `Stack` - 35 edges
4. `SocketsPage()` - 34 edges
5. `IDomeSocketApi` - 33 edges
6. `ContainerDetailPane()` - 29 edges
7. `Dome.Api.Endpoints.Devices` - 29 edges
8. `IStackRepository` - 29 edges
9. `OciRegistryProvider` - 28 edges
10. `ContainerDto` - 26 edges

## Surprising Connections (you probably didn't know these)
- `Changelog release process` --semantically_similar_to--> `Cutting a version procedure`  [INFERRED] [semantically similar]
  CHANGELOG.md → .cursor/skills/document-release/SKILL.md
- `GitHub Release notes drafting` --shares_data_with--> `Published images ghcr.io/dome-works`  [INFERRED]
  .cursor/skills/document-release/SKILL.md → README.md
- `AGENTS.md entry point` --references--> `document-release skill`  [EXTRACTED]
  AGENTS.md → .cursor/skills/document-release/SKILL.md
- `Run modes: Rider, Docker build, published images` --conceptually_related_to--> `Client index.html`  [INFERRED]
  README.md → src/Dome.Client/index.html
- `StubDockerEngineClient` --implements--> `IDockerEngineClient`  [EXTRACTED]
  tests/Dome.Socket.Tests/Docker/Images/StubDockerEngineClient.cs → src/Dome.Socket/Dome.Socket.Api/Docker/Clients/IDockerEngineClient.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **Release documentation workflow** — cursor_document_release_skill, document_release_cut_version, document_release_github_release_notes, changelog_document, readme_document, document_release_directory_build_props [INFERRED 0.75]
- **Security and no-authentication model** — readme_no_auth, security_scope, security_out_of_scope, security_deployment_guidance, instructions_security_constraints [INFERRED 0.75]

## Communities (111 total, 8 thin omitted)

### Community 0 - "HTTP Client and Refit API"
Cohesion: 0.06
Nodes (26): DeviceContainerCommandResult, IsContainerNotFound, IsDeviceNotFound, IsSuccess, DeviceContainerCommandService, DeviceStackDeployResult, Error, IsDeviceNotFound (+18 more)

### Community 1 - "Docker Compose Processes"
Cohesion: 0.05
Nodes (33): Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Processes, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker.Compose, Dome.Socket.Api.Docker, DockerComposeClient, DockerComposeResult, Error (+25 more)

### Community 2 - "Image Version Classification"
Cohesion: 0.08
Nodes (16): ImageVersionClassification, CurrentTag, Kind, NewestTag, ImageVersionClassifier, ImageVersionSnapshot, Error, RemoteDigest (+8 more)

### Community 3 - "Client Layout and Routing"
Cohesion: 0.07
Nodes (56): lucide-react, react-router, AppHeader(), pageTitles, AppLayout(), AppSidebar(), navigation, BreadcrumbEllipsis() (+48 more)

### Community 4 - "HTTP Endpoint Handlers"
Cohesion: 0.09
Nodes (5): IDeviceContainerCommandService, IDeviceStackDeployService, DockerContainerLifecycleResult, NotFound, Succeeded

### Community 5 - "EF Core Domain and Migrations"
Cohesion: 0.07
Nodes (21): Dome.Domain, Dome.Domain.Migrations, ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind (+13 more)

### Community 6 - "Docker Configuration Tests"
Cohesion: 0.06
Nodes (16): Dome.Socket.Tests.Docker.Configuration, SocketAddressValidator, DependencyInjection, DomeDbContext, ImageVersionCaches, Sockets, Stacks, DomeDbContextFactory (+8 more)

### Community 7 - "Socket and Compose Editor UI"
Cohesion: 0.14
Nodes (31): radix-ui, react, createSocket(), deleteSocket(), ComposeEditorDialog(), ComposeEditorDialogProps, ComposeYamlEditor, CreateStackDialog() (+23 more)

### Community 8 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 9 - "Container API Client"
Cohesion: 0.10
Nodes (30): fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), fetchDeviceImageVersionChecks(), ContainerDiagram(), ContainerDiagramProps (+22 more)

### Community 10 - "CodeQL Workflow"
Cohesion: 0.05
Nodes (36): name, permissions, runs-on, steps, strategy, concurrency, cancel-in-progress, group (+28 more)

### Community 11 - "Socket Service Handlers"
Cohesion: 0.11
Nodes (13): ISocketService, SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Socket, SocketService (+5 more)

### Community 12 - "Stack Service"
Cohesion: 0.10
Nodes (15): IStackService, StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService (+7 more)

### Community 13 - "Docker Workflow"
Cohesion: 0.06
Nodes (31): name, permissions, runs-on, steps, strategy, env, PLATFORMS, context (+23 more)

### Community 14 - "Container Detail Pane"
Cohesion: 0.19
Nodes (27): AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState(), stateBadgeClassName(), volumeName() (+19 more)

### Community 15 - "Dev Compose Config"
Cohesion: 0.08
Nodes (29): build, environment, image, ports, restart, volumes, context, dockerfile (+21 more)

### Community 16 - "Scorecard Workflow"
Cohesion: 0.07
Nodes (29): name, permissions, runs-on, steps, jobs, analysis, name, permissions (+21 more)

### Community 17 - "Socket Endpoints"
Cohesion: 0.10
Nodes (13): Dome.Shared.Sockets, Dome.Api.Endpoints.Sockets, CreateSocketEndpoint, DeleteSocketEndpoint, GetSocketEndpoint, GetSocketsEndpoint, CreateSocketRequest, Address (+5 more)

### Community 18 - "HTTP Request Primitives"
Cohesion: 0.16
Nodes (4): OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 19 - "Container Node Diagram UI"
Cohesion: 0.15
Nodes (20): class-variance-authority, @xyflow/react, ContainerNode(), ContainerNodeData, stateClassName(), ImageVersionCheckIcon(), ImageVersionCheckIconProps, StackNode() (+12 more)

### Community 20 - "Docker Client Tests and Models"
Cohesion: 0.10
Nodes (5): Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Containers, DockerContainerLifecycleExceptionMapper, GetContainersEndpoint

### Community 21 - "Device Container API and Errors"
Cohesion: 0.15
Nodes (19): fetchDeviceContainers(), readApiError(), fetchSockets(), fetchSocketStatuses(), updateSocket(), createDeviceStack(), deployDeviceStack(), fetchDeviceStack() (+11 more)

### Community 22 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 23 - "Device Diagram Service"
Cohesion: 0.22
Nodes (6): DeviceDiagramService, IDeviceDiagramService, DeviceDiagramDto, Containers, Stacks, DeviceDiagramServiceTests

### Community 24 - "Client Lint and Tooling Config"
Cohesion: 0.12
Nodes (22): oxlint, @shadcn/lint, tailwindcss, tw-animate-css, @types/node, @types/react, @types/react-dom, typescript (+14 more)

### Community 25 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 26 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 27 - "Image Version Check Service"
Cohesion: 0.16
Nodes (11): IImageVersionCheckService, ImageVersionCheckService, CheckImageVersionsEndpoint, CheckImageVersionsRequest, Images, CheckImageVersionsResponse, Results, ImageVersionCheckRequestItem (+3 more)

### Community 28 - "Domain Seeding and Stacks"
Cohesion: 0.15
Nodes (4): Dome.Domain.Seeding, Dome.Domain.Stacks, Dome.Domain.Images, StackConfiguration

### Community 29 - "Socket Contracts and Health"
Cohesion: 0.21
Nodes (5): Dome.Socket.Contracts.Compose, Dome.Socket.Contracts, Dome.Business.Sockets, Dome.Socket.Contracts.Health, Dome.Business.Tests.Sockets

### Community 30 - "Docker Client Wrappers"
Cohesion: 0.16
Nodes (5): DockerEngineClient, IDockerEngineClient, GetContainersResponse, Containers, StubDockerEngineClient

### Community 31 - "Device Containers Endpoint"
Cohesion: 0.13
Nodes (11): Dome.Api.Endpoints.Devices, GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse, Containers, GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest (+3 more)

### Community 32 - "Device Query Service"
Cohesion: 0.19
Nodes (8): DeviceQueryService, ISocketRepository, Socket, Address, CreatedAt, Id, Name, SocketRepository

### Community 33 - "Socket Repository CRUD"
Cohesion: 0.16
Nodes (9): Stack, ComposeYaml, CreatedAt, Id, ProjectName, Socket, SocketId, UpdatedAt (+1 more)

### Community 34 - "Stack Deletion Logic"
Cohesion: 0.13
Nodes (8): Dome.Business.Stacks, Dome.Shared.Stacks, DeleteDeviceStackEndpoint, DeployDeviceStackEndpoint, GetDeviceStackEndpoint, GetDeviceStackRequest, SocketName, StackId

### Community 35 - "shadcn UI Config"
Cohesion: 0.11
Nodes (17): aliases, components, hooks, lib, ui, utils, iconLibrary, rsc (+9 more)

### Community 36 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 37 - "Assembly Versioning"
Cohesion: 0.15
Nodes (8): Dome.Business, Dome.Socket.Api, AssemblyApplicationVersion, Current, IApplicationVersion, Current, ApplicationVersion, Current

### Community 38 - "Docker Registry Tests and Image Endpoints"
Cohesion: 0.23
Nodes (5): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images

### Community 40 - "Docker Build Args Config"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 41 - "Device Service Handlers"
Cohesion: 0.20
Nodes (4): DeviceResponse, Name, GetDevicesResponse, Devices

### Community 42 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 43 - "Compose Lifecycle Endpoints"
Cohesion: 0.14
Nodes (7): Dome.Socket.Api.Endpoints.Compose, DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint, UpComposeEndpoint

### Community 44 - "Device and Container Shared Types"
Cohesion: 0.18
Nodes (5): Dome.Business.Devices, Dome.Shared.Containers, Dome.Shared.Devices, Dome.Shared.Diagrams, DiagramStackKind

### Community 45 - "Health Endpoint and FastEndpoints"
Cohesion: 0.16
Nodes (4): Dome.Socket.Api.Endpoints.Health, GetDevicesEndpoint, GetSocketStatusesEndpoint, GetHealthEndpoint

### Community 46 - "Devtools Package"
Cohesion: 0.13
Nodes (14): author, description, keywords, license, main, name, scripts, graphify (+6 more)

### Community 47 - "Image Version Cache"
Cohesion: 0.15
Nodes (10): ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest, RemoteTags, Unsupported (+2 more)

### Community 48 - "Docker Hub Registry Provider"
Cohesion: 0.19
Nodes (6): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests, ScriptedHttpMessageHandler

### Community 49 - "Create Device Stack Endpoint"
Cohesion: 0.13
Nodes (11): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName, StackViewModel, ComposeYaml, CreatedAt, Id (+3 more)

### Community 50 - "Device Endpoint Wiring"
Cohesion: 0.22
Nodes (5): IDeviceQueryService, DeviceImageVersionChecker, IDeviceImageVersionChecker, IImageVersionCacheRepository, ImageVersionCacheRepository

### Community 51 - "Registry Provider Abstraction"
Cohesion: 0.15
Nodes (10): IRegistryProvider, RegistryVersionInfo, RemoteDigest, Tags, ImageReference, Canonical, Digest, Host (+2 more)

### Community 53 - "Client UI Utilities"
Cohesion: 0.14
Nodes (14): clsx, react-dom, tailwind-merge, dependencies, class-variance-authority@^0.7.1, clsx@^2.1.1, lucide-react@^1.48.0, radix-ui@^1.6.7 (+6 more)

### Community 54 - "GHCR Registry Provider"
Cohesion: 0.19
Nodes (6): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, ImageReferenceParser, GhcrRegistryProviderTests

### Community 55 - "Business Layer Tests"
Cohesion: 0.23
Nodes (4): Dome.Business.Tests.Devices, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, DeviceQueryServiceTests

### Community 56 - "Diagram Stack View Model"
Cohesion: 0.15
Nodes (10): DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, DiagramStackDto, Id, Kind (+2 more)

### Community 57 - "AGENTS.md Guidelines"
Cohesion: 0.21
Nodes (10): AGENTS.md entry point, Completion report requirements, DOME Development Instructions, Frontend conventions, Layer responsibilities and boundaries, One C# type per file, DOME components: Client, Server, Socket, Root cause before fix working method (+2 more)

### Community 58 - "Dependabot Config"
Cohesion: 0.17
Nodes (11): default-days, monthly-batch, patterns, interval, updates, cooldown, directory, groups (+3 more)

### Community 59 - "Dependency Review Workflow"
Cohesion: 0.17
Nodes (11): runs-on, steps, jobs, dependency-review, name, permissions, contents, name (+3 more)

### Community 60 - "Domain SQLite Tests"
Cohesion: 0.24
Nodes (4): Dome.Domain.Tests.Seeding, Dome.Domain.Tests.Sqlite, Dome.Domain.Sqlite, DependencyInjection

### Community 61 - "Container View Model"
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 63 - "Image Tag Parsing"
Cohesion: 0.22
Nodes (7): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix

### Community 64 - "Container DTO"
Cohesion: 0.18
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 65 - "Image Business Layer"
Cohesion: 0.27
Nodes (4): Dome.Business.Images, Dome.Shared.Images, Dome.Business.Tests.Images, ImageVersionKind

### Community 66 - "Container Lifecycle Endpoints"
Cohesion: 0.24
Nodes (5): Dome.Socket.Api.Endpoints.Containers, ContainerLifecycleRequest, Id, StartContainerEndpoint, StopContainerEndpoint

### Community 67 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 68 - "Vite and Tailwind Config"
Cohesion: 0.20
Nodes (7): @tailwindcss/vite, vite, @vitejs/plugin-react, @tailwindcss/vite@^4.3.3, vite@^8.3.5, @vitejs/plugin-react@^6.1.1, __dirname

### Community 69 - "Device Diagram Endpoint"
Cohesion: 0.20
Nodes (6): GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, GetDeviceDiagramResponse, Containers, Stacks

### Community 70 - "Container Response Mapping"
Cohesion: 0.20
Nodes (9): ContainerResponse, Digest, Id, Image, ImageId, Name, Stack, State (+1 more)

### Community 71 - "Docker Client Fallback Tests"
Cohesion: 0.28
Nodes (3): Dome.Socket.Tests.Docker.Clients, FallbackNameSample, NameResolutionSample

### Community 72 - "Container Volume DTO"
Cohesion: 0.22
Nodes (7): ContainerVolumeDto, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 73 - "Container Volume View Model"
Cohesion: 0.22
Nodes (7): ContainerVolumeViewModel, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 74 - "Device Stacks Endpoint"
Cohesion: 0.22
Nodes (5): GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName, GetDeviceStacksResponse, Stacks

### Community 75 - "Device Stack Update Endpoint"
Cohesion: 0.22
Nodes (6): UpdateDeviceStackEndpoint, UpdateDeviceStackRequest, ComposeYaml, ProjectName, SocketName, StackId

### Community 76 - "Socket Status Response"
Cohesion: 0.22
Nodes (7): GetSocketStatusesResponse, Statuses, SocketStatusResponse, Id, IsReachable, NeedsUpdate, Version

### Community 77 - "System Data Usage Model"
Cohesion: 0.22
Nodes (6): SystemDataUsageResponse, SystemDataUsageVolumeResponse, Name, UsageData, SystemDataUsageVolumeUsageDataResponse, Size

### Community 78 - "Image Version Check Response"
Cohesion: 0.22
Nodes (8): ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest, Tags, Unsupported

### Community 79 - "README Feature Summary"
Cohesion: 0.29
Nodes (8): Compose files persisted under /var/lib/dome/compose, Compose stack create, edit and deploy, Container image update icon, Socket version display and update icon, v0.3.1 release, v0.4.0 release, Configuration table, Features list

### Community 80 - "Release Notes Highlights"
Cohesion: 0.29
Nodes (8): Start and stop containers from details panel, Images moved to ghcr.io/dome-works (breaking), Project renamed HomelabDocs to DOME (breaking), SQLite settings and paths changed (breaking), v0.3.0 release with breaking changes, EF Core migrations in Dome.Domain, Published images ghcr.io/dome-works, SQLite persistence and backups

### Community 81 - "Changelog and Release Process"
Cohesion: 0.36
Nodes (6): document-release skill, Cutting a version procedure, Directory.Build.props Version field, GitHub Release notes drafting, No user-facing changes sentence, README sync with shipped behavior

### Community 82 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 83 - "Security Notes"
Cohesion: 0.32
Nodes (5): No authentication or authorization, Out-of-scope issues, Private vulnerability reporting, In-scope security issues, Only main is supported

### Community 84 - "Dev Validation and Run Modes"
Cohesion: 0.25
Nodes (6): Validation commands (dotnet, npm), Pre-1.0 warning, Run modes: Rider, Docker build, published images, Troubleshooting: Socket unreachable, Client entry module src/main.tsx, Client index.html

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
Cohesion: 0.29
Nodes (5): monaco-editor, @monaco-editor/react, monaco-editor@^0.56.0, @monaco-editor/react@^4.7.0, ComposeYamlEditorProps

### Community 89 - "Socket Update Endpoint"
Cohesion: 0.25
Nodes (5): UpdateSocketEndpoint, UpdateSocketRequest, Address, Id, Name

### Community 90 - "Container Volume Response Mapping"
Cohesion: 0.25
Nodes (7): ContainerVolumeResponse, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 91 - "OCI Token and Tag Responses"
Cohesion: 0.25
Nodes (5): OciTagListResponse, Tags, OciTokenResponse, AccessToken, Token

### Community 92 - "Changelog Version History"
Cohesion: 0.29
Nodes (6): Socket agents discover containers (breaking), v0.1.0 first public release, v0.2.0 Socket agents replace direct Docker access, v0.4.1 release (no user-facing changes), Release docs edit scope, Architecture: Client to Server to Socket

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

### Community 98 - "Toast Notifications UI"
Cohesion: 0.43
Nodes (3): sonner, App(), Toaster()

### Community 100 - "EF Core Design Deps"
Cohesion: 0.33
Nodes (5): Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration.Abstractions, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.NET.Sdk

### Community 101 - "HTTP and Logging Deps"
Cohesion: 0.33
Nodes (5): Microsoft.Extensions.Http, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.DependencyInjection.Abstractions, Refit, Microsoft.NET.Sdk

### Community 102 - "Client npm Scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 104 - "Socket Docker Limitations"
Cohesion: 0.50
Nodes (3): Only Dome.Socket.Api uses Docker.DotNet, Current limitations, No remote Docker tcp:// support

### Community 105 - "OCI Auth Response"
Cohesion: 0.50
Nodes (3): OciAuthenticatedResponse, Response, Token

### Community 106 - "Docker Env Variables"
Cohesion: 0.67
Nodes (3): env, PLATFORMS, REGISTRY

### Community 107 - "Client Dependency Overrides"
Cohesion: 0.67
Nodes (3): dompurify, overrides, monaco-editor

## Knowledge Gaps
- **285 isolated node(s):** `FastEndpoints`, `FastEndpoints.Swagger`, `Microsoft.NET.Sdk.Web`, `ContainerId`, `Image` (+280 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 667 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Device Query Service` to `HTTP Client and Refit API`, `Socket Repository CRUD`, `Stack Deletion Logic`, `Image Business Layer`, `HTTP Endpoint Handlers`, `EF Core Domain and Migrations`, `Docker Configuration Tests`, `Device Image Version Check Tests`, `Image Version Classification`, `Device Service Handlers`, `Socket Service Handlers`, `Stack Service`, `Image Version Cache`, `Device Endpoint Wiring`, `Device Diagram Service`, `Business Layer Tests`, `Socket Contracts and Health`, `Docker Client Wrappers`?**
  _High betweenness centrality (0.022) - this node is a cross-community bridge._
- **What connects `FastEndpoints`, `FastEndpoints.Swagger`, `Microsoft.NET.Sdk.Web` to the rest of the system?**
  _285 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `HTTP Client and Refit API` be split into smaller, more focused modules?**
  _Cohesion score 0.061088485746019994 - nodes in this community are weakly interconnected._
- **Why does `OciRegistryProvider` connect `HTTP Request Primitives` to `Docker Hub Registry Provider`, `Registry Provider Abstraction`, `GHCR Registry Provider`, `Socket Contracts and Health`, `Docker Client Wrappers`?**
  _High betweenness centrality (0.014) - this node is a cross-community bridge._
- **Should `Docker Compose Processes` be split into smaller, more focused modules?**
  _Cohesion score 0.050078247261345854 - nodes in this community are weakly interconnected._
- **Why does `ContainerResponse` connect `Container Response Mapping` to `Device and Container Shared Types`, `Docker Client Tests and Models`, `Container List Model`, `Business Layer Tests`, `Container Volume Response Mapping`, `Docker Client Wrappers`?**
  _High betweenness centrality (0.012) - this node is a cross-community bridge._
- **Should `Image Version Classification` be split into smaller, more focused modules?**
  _Cohesion score 0.07587382779198636 - nodes in this community are weakly interconnected._