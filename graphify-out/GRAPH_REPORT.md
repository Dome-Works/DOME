# Graph Report - DOME  (2026-11-18)

## Corpus Check
- 382 files · ~69,368 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2725 nodes · 6532 edges · 137 communities (118 shown, 19 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 492 edges (avg confidence: 0.87)
- Token cost: 367,444 input · 0 output

## Community Hubs (Navigation)
- shadcn Skill Overview
- Socket Endpoint Plumbing
- Docker Engine Client
- Client Auth and Routing
- Container Diagram Client
- Docker Compose Client
- Socket Property Tests
- Design System Recipes
- Auth and Socket Endpoints
- Stack Service
- Admin Setup and Diagram Endpoints
- Sockets Page and API
- DESIGN.md System Page
- Socket API Factory
- Admin Authentication
- Socket Key Middleware
- shadcn Theming Customization
- Solution and Packages
- Build Workflow
- Container Detail Pane
- Devices and Stacks Namespaces
- Create Stack Endpoint
- Image Tag Versioning
- System Settings Service
- Socket Compose Endpoints
- Socket Container Endpoints
- EF Core Migrations
- Docker Workflow
- Dev Compose Config
- Scorecard Workflow
- Device Query Service
- Settings and Seeding
- Sockets Namespaces
- Device Test Fixtures
- Socket Service Tests
- Socket Pairing Security
- Domain Persistence
- Stack Deploy Service
- Container Command Results
- shadcn Composition Rules
- Design System Verification
- Compose Sample Config
- Delete Stack Endpoint
- Auth Status and Logout
- Image Version Checks
- Zizmor Workflow Config
- OCI Registry Provider
- Base vs Radix Rules
- Telemetry and Version Checker
- TypeScript App Config
- Delete Container Endpoint
- Release Documentation Skill
- shadcn Project Config
- Compose Up/Down Endpoints
- Image Version Cache
- Cache Entity Configuration
- Client Lint and Tooling
- Socket Delete Containers
- Container View Models
- shadcn Registry
- Socket Registry Namespaces
- TypeScript Node Config
- Stack Repository
- GHCR Registry Provider
- shadcn CLI Commands
- shadcn Skill Docs Index
- Docker Build Args
- Domain DI and Data Dir
- Oxlint Config
- Socket Mutation Result
- shadcn MCP and Search
- Socket Status States
- System Settings Endpoint
- Project Name Validation
- Client Dependencies
- Diagram Service Tests
- DOME Architecture Rules
- Client Dev Dependencies
- Chat Component Mapping
- shadcn CLI Reference
- shadcn Component Rules
- Telemetry Build Info
- Image Check Namespaces
- Dependabot Config
- Dependency Review Workflow
- shadcn Skill Principles
- Changelog Releases
- Image Check View Model
- Container View Model
- Get Containers Endpoint
- Container DTO
- Chat Messaging Rules
- Pairing Proof
- Database Context
- Dev Compose Services
- Release Workflow
- Diagram Stack View Model
- Device Diagram DTO
- OCI Token and Tag Responses
- Socket Key Security
- Docker Hub Registry Provider
- Image Check Response
- Release Image Publishing
- Showcase Coverage Script
- Socket App Version
- README Operations
- Publish Workflow Attestations
- Vite Config
- Container Volume Response
- CI Build Pipeline
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Image Matrix
- Socket API Keys
- Database Initializer Tests
- Compose YAML Editor
- Socket Status Service
- Image Reference Parser
- Client npm Scripts
- Create Socket Request
- AGENTS.md Guidelines
- Graphify Rules
- Device Diagram Service
- OCI Auth Response
- Registry Version Info
- shadcn Logo Images
- Supply Chain Scans
- Docker Env Variables
- MCP Server Config
- Monaco Dompurify Override
- TypeScript Project References
- Dependabot Grouping
- License Allowlist
- Graphify Skill
- Release Notes Categories
- Favicon Icon

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
- `Card structure` --references--> `CardContent()`  [INFERRED]
  .agents/skills/shadcn/rules/composition.md → src/Dome.Client/src/components/ui/card.tsx
- `Choosing between overlay components` --references--> `Dialog()`  [INFERRED]
  .agents/skills/shadcn/rules/composition.md → src/Dome.Client/src/components/ui/dialog.tsx
- `Dialog, Sheet, and Drawer always need a Title` --references--> `DialogTitle()`  [INFERRED]
  .agents/skills/shadcn/rules/composition.md → src/Dome.Client/src/components/ui/dialog.tsx
- `Use cn() for conditional classes` --references--> `cn()`  [INFERRED]
  .agents/skills/shadcn/rules/styling.md → src/Dome.Client/src/lib/utils.ts
- `Message surfaces use Bubble` --references--> `Badge()`  [INFERRED]
  .agents/skills/shadcn/rules/chat.md → src/Dome.Client/src/components/ui/badge.tsx

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **shadcn Chat Primitives Composition** — _agents_skills_shadcn_rules_chat_messagescroller, _agents_skills_shadcn_rules_chat_message, _agents_skills_shadcn_rules_chat_bubble, _agents_skills_shadcn_rules_chat_attachment, _agents_skills_shadcn_rules_chat_marker, _agents_skills_shadcn_rules_chat_trace [EXTRACTED 1.00]
- **DESIGN.md to Themed shadcn App Flow** — _agents_skills_shadcn_design_system_design_md, _agents_skills_shadcn_design_system_picking_a_style, _agents_skills_shadcn_design_system_role_mapping, _agents_skills_shadcn_design_system_control_heights, _agents_skills_shadcn_design_system_page, _agents_skills_shadcn_design_system_verify_step [EXTRACTED 1.00]
- **DOME Release Version Sync** — agents_skills_document_release_skill_changelog_md, _agents_skills_document_release_skill_directory_build_props, _agents_skills_document_release_skill_dome_client_package_json, agents_skills_document_release_skill_github_release_notes, agents_skills_document_release_skill_readme_md [EXTRACTED 1.00]
- **CI/CD supply-chain security checks** — _github_workflows_dependency_review_workflow, _github_workflows_scorecard_workflow, _github_workflows_zizmor_workflow, _github_dependabot_config, _github_workflows_release_build_provenance_attestation [INFERRED 0.85]
- **Socket authentication and pairing** — readme_socket_key, readme_certificate_pinning, changelog_unreleased_encrypted_socket_traffic, docker_compose_socket_data_volume [INFERRED 0.85]
- **DOME three-service architecture** — instructions_dome_client, instructions_dome_server, instructions_dome_socket, instructions_communication_flow [EXTRACTED 1.00]

## Communities (137 total, 19 thin omitted)

### Community 0 - "shadcn Skill Overview"
Cohesion: 0.06
Nodes (69): Composition: asChild (radix) vs render (base), Items always inside their Group component, Chat & Messaging → [chat.md](./rules/chat.md), CLI, Component Structure → [composition.md](./rules/composition.md), Critical Rules, Forms & Inputs → [forms.md](./rules/forms.md), Icons → [icons.md](./rules/icons.md) (+61 more)

### Community 1 - "Socket Endpoint Plumbing"
Cohesion: 0.09
Nodes (4): ImageVersionCacheRepository, SocketRepository, DeviceResponse, Name

### Community 2 - "Docker Engine Client"
Cohesion: 0.05
Nodes (25): DockerContainerLifecycleResult, NotFound, Succeeded, DockerEngineClient, IDockerEngineClient, DockerEndpointValidator, ContainerLifecycleRequest, Id (+17 more)

### Community 3 - "Client Auth and Routing"
Cohesion: 0.12
Nodes (40): react-dom, react-router, sonner, fetchAuthStatus(), login(), logout(), postAuth(), setupAdmin() (+32 more)

### Community 4 - "Container Diagram Client"
Cohesion: 0.07
Nodes (42): @xyflow/react, deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), fetchDeviceImageVersionChecks() (+34 more)

### Community 5 - "Docker Compose Client"
Cohesion: 0.07
Nodes (26): DockerComposeClient, DockerComposeResult, Error, IsInvalid, Succeeded, DockerComposeOptions, DockerCliPath, WorkingRoot (+18 more)

### Community 6 - "Socket Property Tests"
Cohesion: 0.08
Nodes (8): Dome.Socket.Tests.Docker.Clients, ContainerMapper, ImageDigestMapper, ContainerMapperProperties, FallbackNameSample, ImageDigestMapperTests, NameResolutionSample, StackSample

### Community 7 - "Design System Recipes"
Cohesion: 0.11
Nodes (44): Coverage check, Known traps, Step 5: Map Component Recipes to Variants, Step 6: Build the Design System Page, Assistant activity uses Trace, Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states (+36 more)

### Community 8 - "Auth and Socket Endpoints"
Cohesion: 0.07
Nodes (24): LoginEndpoint, CreateSocketEndpoint, DeleteSocketEndpoint, GetSocketEndpoint, SetSocketKeyEndpoint, UpdateSocketEndpoint, ISocketService, GetSocketRequest (+16 more)

### Community 9 - "Stack Service"
Cohesion: 0.11
Nodes (16): StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService, IStackRepository (+8 more)

### Community 10 - "Admin Setup and Diagram Endpoints"
Cohesion: 0.07
Nodes (16): SetupAdminEndpoint, GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest, SocketName, AdminAccountService (+8 more)

### Community 11 - "Sockets Page and API"
Cohesion: 0.11
Nodes (32): radix-ui, react, readApiError(), createSocket(), deleteSocket(), fetchSockets(), fetchSocketStatuses(), setSocketKey() (+24 more)

### Community 12 - "DESIGN.md System Page"
Cohesion: 0.06
Nodes (38): DESIGN.md, Design System Page, Color Roles Contrast Pairs, ColorPair, Component Tiers, Do and Don't Pairs, preview-states.css data-preview Variants, previewState (+30 more)

### Community 13 - "Socket API Factory"
Cohesion: 0.10
Nodes (8): ClientKey, DomeSocketApiFactory, ISocketApiKeys, PairRequest, Nonce, DomeSocketApiFactoryTests, FakeSocketHandler, ScriptedHttpMessageHandler

### Community 14 - "Admin Authentication"
Cohesion: 0.07
Nodes (18): Dome.Business.Auth, Dome.Api.Endpoints.Auth, Dome.Shared.Auth, Dome.Business.Tests.Auth, AuthSignIn, PasswordHasher, AuthStatusResponse, Authenticated (+10 more)

### Community 15 - "Socket Key Middleware"
Cohesion: 0.08
Nodes (11): ApiKeyMiddleware, SocketCertificate, SocketKey, IsConfigured, IsPaired, KeyPath, Value, SocketSecurity (+3 more)

### Community 16 - "shadcn Theming Customization"
Cohesion: 0.06
Nodes (38): --radius Border Radius, Semantic CSS Variable Tokens, Adding Custom Colors (Tailwind v3/v4), cva Variants, Dark Mode (.dark class, next-themes), OKLCH Color Format, DESIGN.md Role to shadcn Variable Mapping, Styling & Customization Rules (+30 more)

### Community 17 - "Solution and Packages"
Cohesion: 0.07
Nodes (40): Docker.DotNet.Enhanced, FakeItEasy, FsCheck, FsCheck.Xunit.v3, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Abstractions (+32 more)

### Community 18 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 19 - "Container Detail Pane"
Cohesion: 0.16
Nodes (31): fetchDeviceStack(), updateDeviceStack(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState() (+23 more)

### Community 20 - "Devices and Stacks Namespaces"
Cohesion: 0.10
Nodes (7): Dome.Business.Stacks, Dome.Business.Devices, Dome.Api.Endpoints.Devices, Dome.Shared.Stacks, Dome.Shared.Devices, Dome.Shared.Diagrams, DiagramStackKind

### Community 21 - "Create Stack Endpoint"
Cohesion: 0.07
Nodes (24): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName, GetDeviceStackEndpoint, GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName (+16 more)

### Community 22 - "Image Tag Versioning"
Cohesion: 0.09
Nodes (18): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix, ImageVersionClassification (+10 more)

### Community 23 - "System Settings Service"
Cohesion: 0.13
Nodes (8): ISystemSettingsService, SystemSettingsService, TelemetrySettings, ISystemSettingRepository, SystemSettingRepository, SystemSettingsServiceTests, SocketAddressValidatorTests, ImageReferenceParserTests

### Community 24 - "Socket Compose Endpoints"
Cohesion: 0.09
Nodes (11): Dome.Socket.Contracts.Compose, Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Tests.Docker.Configuration, Dome.Socket.Tests.Docker.Compose, Dome.Socket.Api.Docker.Processes, Dome.Domain.Tests.Seeding, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker.Compose (+3 more)

### Community 25 - "Socket Container Endpoints"
Cohesion: 0.08
Nodes (13): Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Health, Dome.Socket.Contracts.Containers, DockerContainerLifecycleExceptionMapper, SystemDataUsageResponse (+5 more)

### Community 26 - "EF Core Migrations"
Cohesion: 0.11
Nodes (7): InitialCreate, AddSockets, AddStacks, AddImageVersionCache, AddSystemSettings, AddSocketAuth, DomeDbContextModelSnapshot

### Community 27 - "Docker Workflow"
Cohesion: 0.06
Nodes (31): name, permissions, runs-on, steps, strategy, env, PLATFORMS, context (+23 more)

### Community 28 - "Dev Compose Config"
Cohesion: 0.08
Nodes (29): build, environment, image, ports, restart, volumes, context, dockerfile (+21 more)

### Community 29 - "Scorecard Workflow"
Cohesion: 0.07
Nodes (29): name, permissions, runs-on, steps, jobs, analysis, name, permissions (+21 more)

### Community 30 - "Device Query Service"
Cohesion: 0.12
Nodes (11): DeviceQueryService, DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess, DeviceStackDeployService (+3 more)

### Community 31 - "Settings and Seeding"
Cohesion: 0.10
Nodes (11): Dome.Domain.Seeding, Dome.Business.Settings, Dome.Business, Dome.Business.Tests.Settings, Dome.Domain.Settings, Dome.Api.Endpoints.Settings, Dome.Api.Telemetry, DependencyInjection (+3 more)

### Community 32 - "Sockets Namespaces"
Cohesion: 0.14
Nodes (5): Dome.Shared.Sockets, Dome.Business.Sockets, Dome.Business.Tests.Sockets, Dome.Api.Endpoints.Sockets, SocketAddressValidator

### Community 33 - "Device Test Fixtures"
Cohesion: 0.15
Nodes (13): Dome.Socket.Contracts, Dome.Shared.Containers, Dome.Domain.Stacks, Dome.Business.Tests.Devices, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Socket, Address (+5 more)

### Community 34 - "Socket Service Tests"
Cohesion: 0.21
Nodes (4): SocketHealthResponse, DockerReachable, Version, SocketServiceTests

### Community 35 - "Socket Pairing Security"
Cohesion: 0.13
Nodes (4): Dome.Socket.Tests.Security, Dome.Socket.Contracts.Pairing, Dome.Socket.Api.Endpoints.Pairing, Dome.Socket.Api.Security

### Community 38 - "Container Command Results"
Cohesion: 0.19
Nodes (6): DeviceContainerCommandResult, IsContainerNotFound, IsDeviceNotFound, IsSuccess, DeviceContainerCommandService, DeviceContainerCommandServiceTests

### Community 39 - "shadcn Composition Rules"
Cohesion: 0.09
Nodes (20): shadcn openai.yml Agent Interface, Component Composition Rules, Alert for Callouts, Avatar Requires AvatarFallback, Button Loading via Spinner, Full Card Composition, Empty State Component, Overlay Component Selection (+12 more)

### Community 40 - "Design System Verification"
Cohesion: 0.08
Nodes (23): Elevation Shadow Overrides, Focus Treatment Mapping, Fonts via Fontsource Substitutes, Showcase Known Traps, Design System Verification, cn() Utility, Contents, Dark mode (+15 more)

### Community 41 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 42 - "Delete Stack Endpoint"
Cohesion: 0.11
Nodes (15): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId, DeleteDeviceUnmanagedStackEndpoint, DeleteDeviceUnmanagedStackRequest, ProjectName (+7 more)

### Community 43 - "Auth Status and Logout"
Cohesion: 0.09
Nodes (11): GetAuthStatusEndpoint, LogoutEndpoint, GetDevicesEndpoint, GetSystemSettingsEndpoint, GetSocketsEndpoint, GetSocketStatusesEndpoint, GetDevicesResponse, Devices (+3 more)

### Community 44 - "Image Version Checks"
Cohesion: 0.15
Nodes (12): IImageVersionCheckService, ImageVersionCheckService, IRegistryProvider, CheckImageVersionsEndpoint, CheckImageVersionsRequest, Images, CheckImageVersionsResponse, Results (+4 more)

### Community 45 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 46 - "OCI Registry Provider"
Cohesion: 0.18
Nodes (5): DependencyInjection, OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 47 - "Base vs Radix Rules"
Cohesion: 0.10
Nodes (20): Accordion type and defaultValue, asChild (radix) vs render (base), nativeButton={false}, Select Base vs Radix Differences, Slider Scalar vs Array, ToggleGroup type vs multiple, Forms & Inputs Rules, Choosing Form Controls (+12 more)

### Community 48 - "Telemetry and Version Checker"
Cohesion: 0.14
Nodes (9): TelemetryReporter, DeviceImageVersionChecker, ImageVersionCheckDto, CheckedAt, ContainerId, CurrentTag, Image, Kind (+1 more)

### Community 49 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 50 - "Delete Container Endpoint"
Cohesion: 0.13
Nodes (11): DeleteDeviceContainerEndpoint, DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes, DeviceContainerLifecycleRequest, ContainerId, Name (+3 more)

### Community 51 - "Release Documentation Skill"
Cohesion: 0.15
Nodes (15): document-release Skill, Directory.Build.props Version, Dome.Client package.json version, ghcr.io/dome-works server/socket/client images, release.yml Workflow, CHANGELOG.md, Cutting a version, Do not do (+7 more)

### Community 52 - "shadcn Project Config"
Cohesion: 0.10
Nodes (19): info Command, shadcn info Project Context, aliases, components, hooks, lib, ui, utils (+11 more)

### Community 53 - "Compose Up/Down Endpoints"
Cohesion: 0.13
Nodes (11): IDockerComposeClient, DownComposeEndpoint, UpComposeEndpoint, ComposeCommandErrorResponse, Message, ComposeDownRequest, ProjectName, RemoveVolumes (+3 more)

### Community 54 - "Image Version Cache"
Cohesion: 0.16
Nodes (11): Dome.Domain.Images, IImageVersionCacheRepository, ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest (+3 more)

### Community 55 - "Cache Entity Configuration"
Cohesion: 0.15
Nodes (7): ImageVersionCacheConfiguration, SystemSetting, Key, Value, SystemSettingConfiguration, SocketConfiguration, StackConfiguration

### Community 56 - "Client Lint and Tooling"
Cohesion: 0.11
Nodes (18): class-variance-authority, clsx, cn, oxlint, shadcn, @shadcn/lint, tailwind-merge, tailwindcss (+10 more)

### Community 57 - "Socket Delete Containers"
Cohesion: 0.12
Nodes (14): DeleteContainersEndpoint, DeleteContainerResult, ContainerId, Error, Status, DeleteContainersRequest, ContainerIds, RemoveVolumes (+6 more)

### Community 58 - "Container View Models"
Cohesion: 0.12
Nodes (14): ContainerVolumeViewModel, Destination, Name, ReadOnly, SizeBytes, Source, Type, ContainerVolumeDto (+6 more)

### Community 59 - "shadcn Registry"
Cohesion: 0.15
Nodes (16): build Command, Registry Address Schemes, Built Registry, include Field, Registry Item Definitions, registry.json, Source Registry, Address Schemes (+8 more)

### Community 60 - "Socket Registry Namespaces"
Cohesion: 0.22
Nodes (5): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images

### Community 61 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 62 - "Stack Repository"
Cohesion: 0.16
Nodes (9): Stack, ComposeYaml, CreatedAt, Id, ProjectName, Socket, SocketId, UpdatedAt (+1 more)

### Community 63 - "GHCR Registry Provider"
Cohesion: 0.15
Nodes (11): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, ImageReference, Canonical, Digest, Host (+3 more)

### Community 64 - "shadcn CLI Commands"
Cohesion: 0.13
Nodes (14): add Command, apply Command, docs Command, Dry-Run Mode (--dry-run/--diff/--view), init Command, Presets (named, code, URL), Templates (next, vite, start, react-router, astro, laravel), view Command (+6 more)

### Community 65 - "shadcn Skill Docs Index"
Cohesion: 0.20
Nodes (4): Icons, Icons in Button use data-icon attribute, No sizing classes on icons inside components, Pass icons as component objects, not string keys

### Community 66 - "Docker Build Args"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 67 - "Domain DI and Data Dir"
Cohesion: 0.21
Nodes (4): DependencyInjection, DomeDataDirectory, SqlitePath, SqlitePathTests

### Community 68 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 69 - "Socket Mutation Result"
Cohesion: 0.23
Nodes (6): SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Socket

### Community 70 - "shadcn MCP and Search"
Cohesion: 0.13
Nodes (14): search Command, Configuring Registries in components.json, shadcn MCP Tools (search/list/view/examples/add/audit), Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries` (+6 more)

### Community 71 - "Socket Status States"
Cohesion: 0.13
Nodes (13): GetSocketStatusesResponse, Statuses, SocketState, NeedsPairing, Offline, Online, Unauthorized, SocketStatusResponse (+5 more)

### Community 72 - "System Settings Endpoint"
Cohesion: 0.15
Nodes (11): Dome.Shared.Settings, UpdateSystemSettingsEndpoint, SystemSettingsResponse, Environment, InstallationId, OnboardingCompleted, TelemetryEnabled, Version (+3 more)

### Community 74 - "Client Dependencies"
Cohesion: 0.14
Nodes (14): dependencies, class-variance-authority, clsx, cn, lucide-react, monaco-editor, @monaco-editor/react, radix-ui (+6 more)

### Community 76 - "DOME Architecture Rules"
Cohesion: 0.19
Nodes (7): v0.2.0 Socket agents replace direct Docker access, DOME Development Instructions, Dome.Client, Dome.Server, Dome.Socket, Architecture (Client -> Server -> Socket -> Docker), DOME client HTML entry (#root, main.tsx)

### Community 77 - "Client Dev Dependencies"
Cohesion: 0.15
Nodes (13): devDependencies, oxlint, shadcn, @shadcn/lint, tailwindcss, @tailwindcss/vite, tw-animate-css, @types/node (+5 more)

### Community 78 - "Chat Component Mapping"
Cohesion: 0.26
Nodes (12): Chat & Messaging Rules, AI Elements Component Mapping, Attachment, Bubble, CodeBlock, Marker, Message, MessageScroller (+4 more)

### Community 79 - "shadcn CLI Reference"
Cohesion: 0.17
Nodes (12): `add` — Add components, `apply` — Apply a preset to an existing project, `build` — Build a custom registry, Commands, `diff` — Check for updates, `docs` — Get component documentation URLs, Dry-Run Mode, `info` — Project information (+4 more)

### Community 80 - "shadcn Component Rules"
Cohesion: 0.17
Nodes (12): Avatar always needs AvatarFallback, Button has no isPending or isLoading prop, Callouts use Alert, Card structure, Choosing between overlay components, Component Composition, Contents, Dialog, Sheet, and Drawer always need a Title (+4 more)

### Community 81 - "Telemetry Build Info"
Cohesion: 0.17
Nodes (6): TelemetryBuildInfo, Environment, AssemblyApplicationVersion, Current, IApplicationVersion, Current

### Community 82 - "Image Check Namespaces"
Cohesion: 0.20
Nodes (4): Dome.Business.Images, Dome.Shared.Images, Dome.Business.Tests.Images, ImageVersionKind

### Community 83 - "Dependabot Config"
Cohesion: 0.17
Nodes (11): default-days, monthly-batch, patterns, interval, updates, cooldown, directory, groups (+3 more)

### Community 84 - "Dependency Review Workflow"
Cohesion: 0.17
Nodes (11): runs-on, steps, jobs, dependency-review, name, permissions, contents, name (+3 more)

### Community 85 - "shadcn Skill Principles"
Cohesion: 0.18
Nodes (11): Component Docs, Examples, and Usage, Current Project Context, Design Systems from DESIGN.md, Detailed References, Key Fields, Key Patterns, Principles, Quick Reference (+3 more)

### Community 86 - "Changelog Releases"
Cohesion: 0.20
Nodes (11): DOME Changelog, Unreleased: Sign-in and admin account, Unreleased: Encrypted Server-to-Socket HTTPS with certificate pinning, v0.1.0 First public release (HomelabDocs), v0.3.0 Rename HomelabDocs to DOME, v0.3.1 Create/edit/deploy Compose stacks, v0.4.0 Socket version and image update icons, v0.5.0 Delete containers and stacks (+3 more)

### Community 87 - "Image Check View Model"
Cohesion: 0.18
Nodes (9): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag, GetDeviceImageVersionChecksResponse (+1 more)

### Community 88 - "Container View Model"
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 89 - "Get Containers Endpoint"
Cohesion: 0.24
Nodes (6): GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse, Containers, IDeviceQueryService

### Community 90 - "Container DTO"
Cohesion: 0.18
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 91 - "Chat Messaging Rules"
Cohesion: 0.20
Nodes (10): Attachments use Attachment, Chat & Messaging, Code and tool output use CodeBlock, Contents, Escape hatch: the scroller hooks, Message rows use Message, Message surfaces use Bubble, Scrollable threads use MessageScroller (+2 more)

### Community 92 - "Pairing Proof"
Cohesion: 0.36
Nodes (3): Dome.Socket.Tests.Pairing, PairingProof, PairingProofTests

### Community 93 - "Database Context"
Cohesion: 0.22
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 94 - "Dev Compose Services"
Cohesion: 0.27
Nodes (10): api service (local build), client service (local build), dome-data volume, socket-data volume, socket service (local build), dev.docker-compose (build from source), api service (ghcr image), client service (ghcr image) (+2 more)

### Community 95 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 96 - "Diagram Stack View Model"
Cohesion: 0.20
Nodes (8): DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, GetDeviceDiagramResponse, Containers, Stacks

### Community 97 - "Device Diagram DTO"
Cohesion: 0.20
Nodes (8): DeviceDiagramDto, Containers, Stacks, DiagramStackDto, Id, Kind, ProjectName, TotalBytes

### Community 98 - "OCI Token and Tag Responses"
Cohesion: 0.20
Nodes (5): OciTagListResponse, Tags, OciTokenResponse, AccessToken, Token

### Community 99 - "Socket Key Security"
Cohesion: 0.25
Nodes (7): Unreleased: Socket keys, socket-data volume, socket service (ghcr image), Socket key (socket.key / DOME_SOCKET_KEY), Security scope (in/out), Security Policy, Private vulnerability reporting via GitHub Security Advisories

### Community 100 - "Docker Hub Registry Provider"
Cohesion: 0.33
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 101 - "Image Check Response"
Cohesion: 0.22
Nodes (8): ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest, Tags, Unsupported

### Community 102 - "Release Image Publishing"
Cohesion: 0.25
Nodes (8): Build provenance attestation, DOME_ENVIRONMENT=Release build-arg, GHCR registry ghcr.io/dome-works, publish job, Release workflow (publish images), v0.6.0 Onboarding and anonymous usage tracking, Telemetry (PostHog instance_heartbeat), Telemetry environment (Development/Test/Release)

### Community 103 - "Showcase Coverage Script"
Cohesion: 0.25
Nodes (6): components, missing, source, [uiArg, ...roots], uiDir, walk()

### Community 104 - "Socket App Version"
Cohesion: 0.29
Nodes (3): Dome.Socket.Api, ApplicationVersion, Current

### Community 105 - "README Operations"
Cohesion: 0.25
Nodes (8): dome-data volume, Configuration settings, Current limitations, DOME README, Codebase knowledge graph (graphify), Connect multiple Docker hosts, Persistence and backups (dome-data volume), Troubleshooting

### Community 106 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 107 - "Vite Config"
Cohesion: 0.25
Nodes (5): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname, { version }

### Community 108 - "Container Volume Response"
Cohesion: 0.25
Nodes (7): ContainerVolumeResponse, Destination, Name, ReadOnly, SizeBytes, Source, Type

### Community 109 - "CI Build Pipeline"
Cohesion: 0.33
Nodes (7): docker job (reusable call), verify job (Build and test), Build, test, and analyze workflow, Docker image matrix (server, socket, client), Multi-platform build linux/amd64,linux/arm64, Docker reusable workflow, Validation commands

### Community 110 - "GitHub Release Changelog Config"
Cohesion: 0.29
Nodes (6): labels, title, changelog, categories, exclude, labels

### Community 111 - "Tag and Env Workflow Steps"
Cohesion: 0.29
Nodes (7): TAG, steps, env, id, name, run, uses

### Community 112 - "Docker Image Matrix"
Cohesion: 0.29
Nodes (7): context, file, image, include, strategy, fail-fast, matrix

### Community 115 - "Compose YAML Editor"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 118 - "Client npm Scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 119 - "Create Socket Request"
Cohesion: 0.40
Nodes (4): CreateSocketRequest, Address, Key, Name

### Community 123 - "OCI Auth Response"
Cohesion: 0.50
Nodes (3): OciAuthenticatedResponse, Response, Token

### Community 124 - "Registry Version Info"
Cohesion: 0.50
Nodes (3): RegistryVersionInfo, RemoteDigest, Tags

### Community 125 - "shadcn Logo Images"
Cohesion: 0.67
Nodes (3): shadcn Logo Image, shadcn/ui, shadcn Small Logo Icon

### Community 126 - "Supply Chain Scans"
Cohesion: 0.67
Nodes (3): SARIF upload to code-scanning, OpenSSF Scorecard workflow, Zizmor GitHub Actions analysis workflow

### Community 127 - "Docker Env Variables"
Cohesion: 0.67
Nodes (3): env, PLATFORMS, REGISTRY

### Community 129 - "Monaco Dompurify Override"
Cohesion: 0.67
Nodes (3): dompurify, overrides, monaco-editor

## Knowledge Gaps
- **398 isolated node(s):** `/graphify slash command`, `graphify skill (.claude/skills/graphify/SKILL.md)`, `npm`, `cn`, `@shadcn/lint` (+393 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 935 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **19 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Device Test Fixtures` to `Socket Endpoint Plumbing`, `Auth and Socket Endpoints`, `Stack Service`, `Socket API Factory`, `EF Core Migrations`, `Device Query Service`, `Sockets Namespaces`, `Socket Service Tests`, `Socket Pairing Security`, `Stack Deploy Service`, `Container Command Results`, `OCI Registry Provider`, `Telemetry and Version Checker`, `Release Documentation Skill`, `Image Version Cache`, `Cache Entity Configuration`, `Stack Repository`, `Diagram Service Tests`, `Image Check Namespaces`, `Database Context`, `Socket Status Service`?**
  _High betweenness centrality (0.034) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `/graphify slash command`, `graphify skill (.claude/skills/graphify/SKILL.md)`, `npm` to the rest of the system?**
  _398 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `shadcn Skill Overview` be split into smaller, more focused modules?**
  _Cohesion score 0.05894736842105263 - nodes in this community are weakly interconnected._
- **Why does `shadcn Skill` connect `shadcn Composition Rules` to `shadcn CLI Commands`, `shadcn Skill Overview`, `Design System Verification`, `DESIGN.md System Page`, `Chat Component Mapping`, `Base vs Radix Rules`, `shadcn Theming Customization`, `shadcn Project Config`, `shadcn Registry`?**
  _High betweenness centrality (0.023) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `Socket` (e.g. with `CHANGELOG.md` and `.CreateHttpClient()`) actually correct?**
  _`Socket` has 2 INFERRED edges - model-reasoned connections that need verification._
- **Should `Socket Endpoint Plumbing` be split into smaller, more focused modules?**
  _Cohesion score 0.08928571428571429 - nodes in this community are weakly interconnected._