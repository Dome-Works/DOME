# Graph Report - DOME  (2026-11-19)

## Corpus Check
- 371 files · ~69,938 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2756 nodes · 6623 edges · 146 communities (123 shown, 23 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 513 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `d46cb349`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- shadcn Skill Overview
- ImageReference
- IReadOnlyCollection
- Client Auth and Routing
- Container Diagram Client
- RecordingProcessRunner
- Socket Property Tests
- Design System Recipes
- SocketService
- Guid
- CancellationToken
- SocketsPage.tsx
- Design System Page
- IStackRepository
- DeviceContainerLifecycleRequest
- ApiKeyMiddleware
- Styling & Customization Rules
- Solution and Packages
- Build Workflow
- SocketsPage.tsx
- .CreateService
- .AddDomeDomain
- Image Tag Versioning
- DeleteDeviceStackRequest
- Docker/DependencyInjection.cs
- Socket Container Endpoints
- EF Core Migrations
- Docker Workflow
- Dev Compose Config
- Scorecard Workflow
- .AddDomeBusiness
- color-pair.tsx
- Dome.Business.Stacks
- Socket
- Socket Service Tests
- DomeSocketApiFactory.cs
- Domain Persistence
- Stack Deploy Service
- Container Command Results
- shadcn Composition Rules
- Design System Verification
- Compose Sample Config
- GetDeviceContainersEndpoint
- .GetDevicesAsync
- fastendpoints
- Zizmor Workflow Config
- OCI Registry Provider
- Base vs Radix Rules
- GetDeviceStacksEndpoint
- TypeScript App Config
- Dome.Api.Endpoints.Devices
- Release Documentation Skill
- Tools
- InlineData
- ImageVersionCache
- Cache Entity Configuration
- Client Lint and Tooling
- DeleteContainerResult
- Container View Models
- shadcn Registry
- Dome.Socket.Contracts.Images
- TypeScript Node Config
- microsoft_extensions_configuration
- CheckImageVersionsRequest
- shadcn CLI Commands
- shadcn Skill Docs Index
- Docker Build Args
- DeviceImageVersionChecker
- Oxlint Config
- GetDeviceStackRequest
- TelemetryReporter
- Customization & Theming
- ISystemSettingsService
- Regex
- Client Dependencies
- Diagram Service Tests
- DOME Architecture Rules
- Client Dev Dependencies
- Chat Component Mapping
- shadcn CLI Reference
- shadcn Component Rules
- RecordingProcessRunner
- ImageVersionCheckDto
- Dependabot Config
- Dependency Review Workflow
- shadcn Skill Principles
- Changelog Releases
- Image Check View Model
- Container View Model
- RegistryVersionInfo
- Container DTO
- DeleteContainerStatus
- Database Context
- Dev Compose Services
- Release Workflow
- Fact
- GetDeviceDiagramResponse
- components.json
- Socket Key Security
- shadcn Skill
- ProcessRunResult
- Release Image Publishing
- Showcase Coverage Script
- AssemblyApplicationVersion
- README Operations
- Publish Workflow Attestations
- Vite Config
- DomeSocketApiFactory
- CI Build Pipeline
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Image Matrix
- Chat & Messaging
- Compose YAML Editor
- Client npm Scripts
- .AddDomeBusiness
- AGENTS.md Guidelines
- Graphify Rules
- .DownAsync
- IDockerComposeClient
- Dome.Api/Program.cs
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
- Endpoint
- DockerHubRegistryProvider
- Foundations
- DeviceDiagramDto
- IAdminAccountService
- Step 4: Map Tokens to the Theme
- ImageVersionCheckResponse
- CreateSocketRequest
- tailwind
- DeleteDeviceContainerEndpoint
- DatabaseInitializerTests
- DockerEndpointOptions
- AuthStatusResponse

## God Nodes (most connected - your core abstractions)
1. `cn()` - 92 edges
2. `Socket` - 64 edges
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
- `Icons → [icons.md](./rules/icons.md)` --references--> `Button()`  [INFERRED]
  .agents/skills/shadcn/SKILL.md → src/Dome.Client/src/components/ui/button.tsx
- `Card structure` --references--> `CardContent()`  [INFERRED]
  .agents/skills/shadcn/rules/composition.md → src/Dome.Client/src/components/ui/card.tsx

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **DESIGN.md to Themed shadcn App Flow** — _agents_skills_shadcn_design_system_design_md, _agents_skills_shadcn_design_system_picking_a_style, _agents_skills_shadcn_design_system_role_mapping, _agents_skills_shadcn_design_system_control_heights, _agents_skills_shadcn_design_system_page, _agents_skills_shadcn_design_system_verify_step [EXTRACTED 1.00]
- **DOME Release Version Sync** — agents_skills_document_release_skill_changelog_md, _agents_skills_document_release_skill_directory_build_props, _agents_skills_document_release_skill_dome_client_package_json, agents_skills_document_release_skill_github_release_notes, agents_skills_document_release_skill_readme_md [EXTRACTED 1.00]
- **DOME three-service architecture** — instructions_dome_client, instructions_dome_server, instructions_dome_socket, instructions_communication_flow [EXTRACTED 1.00]
- **shadcn Chat Primitives Composition** — _agents_skills_shadcn_rules_chat_messagescroller, _agents_skills_shadcn_rules_chat_message, _agents_skills_shadcn_rules_chat_bubble, _agents_skills_shadcn_rules_chat_attachment, _agents_skills_shadcn_rules_chat_marker, _agents_skills_shadcn_rules_chat_trace [EXTRACTED 1.00]
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **CI/CD supply-chain security checks** — _github_workflows_dependency_review_workflow, _github_workflows_scorecard_workflow, _github_workflows_zizmor_workflow, _github_dependabot_config, _github_workflows_release_build_provenance_attestation [INFERRED 0.85]
- **Socket authentication and pairing** — readme_socket_key, readme_certificate_pinning, changelog_unreleased_encrypted_socket_traffic, docker_compose_socket_data_volume [INFERRED 0.85]

## Communities (146 total, 23 thin omitted)

### Community 0 - "shadcn Skill Overview"
Cohesion: 0.06
Nodes (69): Composition: asChild (radix) vs render (base), System notes and dividers use Marker, Items always inside their Group component, No sizing classes on icons inside components, Chat & Messaging → [chat.md](./rules/chat.md), CLI, Critical Rules, Icons → [icons.md](./rules/icons.md) (+61 more)

### Community 1 - "ImageReference"
Cohesion: 0.12
Nodes (12): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, IRegistryProvider, ImageReference, Canonical, Digest (+4 more)

### Community 2 - "IReadOnlyCollection"
Cohesion: 0.07
Nodes (16): DockerContainerLifecycleExceptionMapper, DockerContainerLifecycleResult, NotFound, Succeeded, DockerEngineClient, IDockerEngineClient, ContainerLifecycleRequest, Id (+8 more)

### Community 3 - "Client Auth and Routing"
Cohesion: 0.11
Nodes (42): Component Structure → [composition.md](./rules/composition.md), react-dom, react-router, sonner, fetchAuthStatus(), login(), logout(), postAuth() (+34 more)

### Community 4 - "Container Diagram Client"
Cohesion: 0.07
Nodes (42): @xyflow/react, deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), fetchDeviceImageVersionChecks() (+34 more)

### Community 6 - "Socket Property Tests"
Cohesion: 0.05
Nodes (24): Dome.Socket.Tests.Docker.Clients, ContainerMapper, ImageDigestMapper, ContainerResponse, Digest, Id, Image, ImageId (+16 more)

### Community 7 - "Design System Recipes"
Cohesion: 0.12
Nodes (42): Known traps, Step 5: Map Component Recipes to Variants, Assistant activity uses Trace, Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs (+34 more)

### Community 8 - "SocketService"
Cohesion: 0.10
Nodes (16): GetSocketStatusesEndpoint, SocketMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Socket, SocketService (+8 more)

### Community 9 - "Guid"
Cohesion: 0.05
Nodes (34): CreateDeviceStackEndpoint, CreateDeviceStackRequest, ProjectName, SocketName, GetDeviceStackEndpoint, StackViewModel, ComposeYaml, CreatedAt (+26 more)

### Community 11 - "SocketsPage.tsx"
Cohesion: 0.16
Nodes (31): fetchDeviceStack(), updateDeviceStack(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState() (+23 more)

### Community 12 - "Design System Page"
Cohesion: 0.12
Nodes (20): DESIGN.md, Design System Page, Color Roles Contrast Pairs, ColorPair, Component Tiers, Do and Don't Pairs, preview-states.css data-preview Variants, previewState (+12 more)

### Community 14 - "DeviceContainerLifecycleRequest"
Cohesion: 0.24
Nodes (5): DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint

### Community 15 - "ApiKeyMiddleware"
Cohesion: 0.17
Nodes (7): AuthSignIn, ApiKeyMiddleware, SocketKey, IsConfigured, IsPaired, KeyPath, Value

### Community 16 - "Styling & Customization Rules"
Cohesion: 0.12
Nodes (19): Semantic CSS Variable Tokens, Dark Mode (.dark class, next-themes), DESIGN.md Role to shadcn Variable Mapping, Styling & Customization Rules, No Manual z-index on Overlays, gap-* not space-x/y, size-* over w-*/h-*, truncate Shorthand (+11 more)

### Community 17 - "Solution and Packages"
Cohesion: 0.05
Nodes (38): Docker.DotNet.Enhanced, FakeItEasy, FsCheck, FsCheck.Xunit.v3, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Abstractions (+30 more)

### Community 18 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 19 - "SocketsPage.tsx"
Cohesion: 0.11
Nodes (32): radix-ui, react, readApiError(), createSocket(), deleteSocket(), fetchSockets(), fetchSocketStatuses(), setSocketKey() (+24 more)

### Community 21 - ".AddDomeDomain"
Cohesion: 0.16
Nodes (6): DependencyInjection, SqlitePath, DockerComposeOptions, DockerCliPath, WorkingRoot, SqlitePathTests

### Community 22 - "Image Tag Versioning"
Cohesion: 0.09
Nodes (18): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix, ImageVersionClassification (+10 more)

### Community 23 - "DeleteDeviceStackRequest"
Cohesion: 0.25
Nodes (5): DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId

### Community 24 - "Docker/DependencyInjection.cs"
Cohesion: 0.20
Nodes (5): Dome.Socket.Contracts.Compose, Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Tests.Docker.Configuration, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker.Compose

### Community 25 - "Socket Container Endpoints"
Cohesion: 0.09
Nodes (12): Dome.Socket.Contracts, Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Health, Dome.Socket.Contracts.Containers, SystemDataUsageResponse, SystemDataUsageVolumeResponse (+4 more)

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

### Community 30 - ".AddDomeBusiness"
Cohesion: 0.12
Nodes (13): DeleteDeviceUnmanagedStackEndpoint, DeleteDeviceUnmanagedStackRequest, ProjectName, RemoveVolumes, SocketName, DeviceStackDeployResult, Error, IsDeviceNotFound (+5 more)

### Community 31 - "color-pair.tsx"
Cohesion: 0.23
Nodes (10): ColorPair(), contrast(), luminance(), toHex(), toRgb(), defaultStates, previewState(), StateMatrix() (+2 more)

### Community 33 - "Socket"
Cohesion: 0.10
Nodes (20): Dome.Domain.Stacks, Dome.Business.Tests.Devices, Dome.Business.Tests.Settings, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Socket, Address, CertificateFingerprint (+12 more)

### Community 34 - "Socket Service Tests"
Cohesion: 0.10
Nodes (8): ISocketRepository, SocketRepository, GetHealthEndpoint, SocketHealthResponse, DockerReachable, Version, SocketServiceTests, SocketRepositoryTests

### Community 35 - "DomeSocketApiFactory.cs"
Cohesion: 0.15
Nodes (4): Dome.Socket.Tests.Security, Dome.Socket.Api.Security, Dome.Socket.Api.Docker, SocketCertificate

### Community 38 - "Container Command Results"
Cohesion: 0.16
Nodes (7): DeviceContainerCommandResult, IsContainerNotFound, IsDeviceNotFound, IsSuccess, DeviceContainerCommandService, IDeviceContainerCommandService, DeviceContainerCommandServiceTests

### Community 39 - "shadcn Composition Rules"
Cohesion: 0.15
Nodes (10): Component Composition Rules, Alert for Callouts, Avatar Requires AvatarFallback, Button Loading via Spinner, Full Card Composition, Empty State Component, Overlay Component Selection, TabsTrigger Inside TabsList (+2 more)

### Community 40 - "Design System Verification"
Cohesion: 0.10
Nodes (18): --radius Border Radius, Elevation Shadow Overrides, Focus Treatment Mapping, Fonts via Fontsource Substitutes, Showcase Known Traps, Design System Verification, cn() Utility, Contents (+10 more)

### Community 41 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 42 - "GetDeviceContainersEndpoint"
Cohesion: 0.28
Nodes (5): GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse, Containers

### Community 43 - ".GetDevicesAsync"
Cohesion: 0.24
Nodes (5): GetDevicesEndpoint, DeviceResponse, Name, GetDevicesResponse, Devices

### Community 44 - "fastendpoints"
Cohesion: 0.20
Nodes (4): Dome.Shared.Sockets, Dome.Business.Sockets, Dome.Business.Tests.Sockets, Dome.Api.Endpoints.Sockets

### Community 45 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 46 - "OCI Registry Provider"
Cohesion: 0.17
Nodes (6): OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, OciTagListResponse, Tags

### Community 47 - "Base vs Radix Rules"
Cohesion: 0.10
Nodes (20): Accordion type and defaultValue, asChild (radix) vs render (base), nativeButton={false}, Select Base vs Radix Differences, Slider Scalar vs Array, ToggleGroup type vs multiple, Forms & Inputs Rules, Choosing Form Controls (+12 more)

### Community 48 - "GetDeviceStacksEndpoint"
Cohesion: 0.28
Nodes (5): GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName, GetDeviceStacksResponse, Stacks

### Community 49 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 50 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.09
Nodes (14): Dome.Business.Images, Dome.Shared.Images, Dome.Business.Devices, Dome.Shared.Containers, Dome.Business.Tests.Images, Dome.Api.Endpoints.Devices, Dome.Shared.Devices, GetDeviceImageVersionChecksEndpoint (+6 more)

### Community 51 - "Release Documentation Skill"
Cohesion: 0.15
Nodes (15): document-release Skill, Directory.Build.props Version, Dome.Client package.json version, ghcr.io/dome-works server/socket/client images, release.yml Workflow, CHANGELOG.md, Cutting a version, Do not do (+7 more)

### Community 52 - "Tools"
Cohesion: 0.13
Nodes (14): search Command, Configuring Registries in components.json, shadcn MCP Tools (search/list/view/examples/add/audit), Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries` (+6 more)

### Community 53 - "InlineData"
Cohesion: 0.06
Nodes (17): AdminAccountService, AdminSetupResult, Error, IsConflict, IsSuccess, PasswordHasher, SystemSettingsService, SocketAddressValidator (+9 more)

### Community 54 - "ImageVersionCache"
Cohesion: 0.22
Nodes (8): ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest, RemoteTags, Unsupported

### Community 55 - "Cache Entity Configuration"
Cohesion: 0.15
Nodes (7): ImageVersionCacheConfiguration, SystemSetting, Key, Value, SystemSettingConfiguration, SocketConfiguration, StackConfiguration

### Community 56 - "Client Lint and Tooling"
Cohesion: 0.11
Nodes (18): class-variance-authority, clsx, cn, oxlint, shadcn, @shadcn/lint, tailwind-merge, tailwindcss (+10 more)

### Community 57 - "DeleteContainerResult"
Cohesion: 0.17
Nodes (10): DeleteContainerResult, ContainerId, Error, Status, DeleteContainersResponse, Results, DeleteContainerStatus, Deleted (+2 more)

### Community 58 - "Container View Models"
Cohesion: 0.12
Nodes (14): ContainerVolumeViewModel, Destination, Name, ReadOnly, SizeBytes, Source, Type, ContainerVolumeDto (+6 more)

### Community 59 - "shadcn Registry"
Cohesion: 0.15
Nodes (16): build Command, Registry Address Schemes, Built Registry, include Field, Registry Item Definitions, registry.json, Source Registry, Address Schemes (+8 more)

### Community 60 - "Dome.Socket.Contracts.Images"
Cohesion: 0.12
Nodes (13): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images, DependencyInjection, IImageVersionCheckService (+5 more)

### Community 61 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 62 - "microsoft_extensions_configuration"
Cohesion: 0.10
Nodes (12): Dome.Domain.Seeding, Dome.Domain.Tests.Sockets, Dome.Domain.Tests.Settings, Dome.Domain.Tests.Seeding, Dome.Domain.Tests.Sqlite, Dome.Domain.Settings, Dome.Domain.Sqlite, DatabaseInitializer (+4 more)

### Community 63 - "CheckImageVersionsRequest"
Cohesion: 0.29
Nodes (6): ImageVersionCheckService, CheckImageVersionsRequest, Images, ImageVersionCheckRequestItem, Image, ImageId

### Community 64 - "shadcn CLI Commands"
Cohesion: 0.13
Nodes (14): add Command, apply Command, docs Command, Dry-Run Mode (--dry-run/--diff/--view), init Command, Presets (named, code, URL), Templates (next, vite, start, react-router, astro, laravel), view Command (+6 more)

### Community 65 - "shadcn Skill Docs Index"
Cohesion: 0.22
Nodes (3): Icons, Icons in Button use data-icon attribute, Pass icons as component objects, not string keys

### Community 66 - "Docker Build Args"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 68 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 69 - "GetDeviceStackRequest"
Cohesion: 0.29
Nodes (4): DeployDeviceStackEndpoint, GetDeviceStackRequest, SocketName, StackId

### Community 71 - "Customization & Theming"
Cohesion: 0.11
Nodes (18): Adding Custom Colors (Tailwind v3/v4), cva Variants, OKLCH Color Format, 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors (+10 more)

### Community 72 - "ISystemSettingsService"
Cohesion: 0.08
Nodes (17): GetAuthStatusEndpoint, LogoutEndpoint, GetSystemSettingsEndpoint, UpdateSystemSettingsEndpoint, IApplicationVersion, Current, ISystemSettingsService, TelemetrySettings (+9 more)

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

### Community 81 - "RecordingProcessRunner"
Cohesion: 0.13
Nodes (14): IProcessRunner, ProcessRunner, ProcessRunResult, ExitCode, StandardError, StandardOutput, RecordingProcessRunner, Arguments (+6 more)

### Community 82 - "ImageVersionCheckDto"
Cohesion: 0.25
Nodes (7): ImageVersionCheckDto, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag

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
Cohesion: 0.25
Nodes (7): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag

### Community 88 - "Container View Model"
Cohesion: 0.18
Nodes (10): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 89 - "RegistryVersionInfo"
Cohesion: 0.50
Nodes (3): RegistryVersionInfo, RemoteDigest, Tags

### Community 90 - "Container DTO"
Cohesion: 0.15
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 91 - "DeleteContainerStatus"
Cohesion: 0.15
Nodes (8): SocketState, NeedsPairing, Offline, Online, Unauthorized, OciTokenResponse, AccessToken, Token

### Community 93 - "Database Context"
Cohesion: 0.18
Nodes (7): Dome.Domain.Images, DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 94 - "Dev Compose Services"
Cohesion: 0.27
Nodes (10): api service (local build), client service (local build), dome-data volume, socket-data volume, socket service (local build), dev.docker-compose (build from source), api service (ghcr image), client service (ghcr image) (+2 more)

### Community 95 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 96 - "Fact"
Cohesion: 0.11
Nodes (4): DockerContainerLifecycleExceptionMapperTests, ApiKeyMiddlewareTests, SocketCertificateTests, SocketKeyTests

### Community 97 - "GetDeviceDiagramResponse"
Cohesion: 0.19
Nodes (7): GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, GetDeviceDiagramResponse, Containers, Stacks, IDeviceDiagramService

### Community 98 - "components.json"
Cohesion: 0.14
Nodes (13): info Command, shadcn info Project Context, aliases, components, hooks, lib, ui, utils (+5 more)

### Community 99 - "Socket Key Security"
Cohesion: 0.25
Nodes (7): Unreleased: Socket keys, socket-data volume, socket service (ghcr image), Socket key (socket.key / DOME_SOCKET_KEY), Security scope (in/out), Security Policy, Private vulnerability reporting via GitHub Security Advisories

### Community 100 - "shadcn Skill"
Cohesion: 0.18
Nodes (10): shadcn openai.yml Agent Interface, Icons Rules, Project iconLibrary, Pass Icons as Component Objects, shadcn Skill, Component Selection Table, Project Context Key Fields, shadcn CLI (npx shadcn@latest) (+2 more)

### Community 102 - "Release Image Publishing"
Cohesion: 0.25
Nodes (8): Build provenance attestation, DOME_ENVIRONMENT=Release build-arg, GHCR registry ghcr.io/dome-works, publish job, Release workflow (publish images), v0.6.0 Onboarding and anonymous usage tracking, Telemetry (PostHog instance_heartbeat), Telemetry environment (Development/Test/Release)

### Community 103 - "Showcase Coverage Script"
Cohesion: 0.25
Nodes (6): components, missing, source, [uiArg, ...roots], uiDir, walk()

### Community 104 - "AssemblyApplicationVersion"
Cohesion: 0.13
Nodes (7): Dome.Socket.Api, TelemetryBuildInfo, Environment, AssemblyApplicationVersion, Current, ApplicationVersion, Current

### Community 105 - "README Operations"
Cohesion: 0.25
Nodes (8): dome-data volume, Configuration settings, Current limitations, DOME README, Codebase knowledge graph (graphify), Connect multiple Docker hosts, Persistence and backups (dome-data volume), Troubleshooting

### Community 106 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 107 - "Vite Config"
Cohesion: 0.25
Nodes (5): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname, { version }

### Community 108 - "DomeSocketApiFactory"
Cohesion: 0.06
Nodes (19): Dome.Socket.Contracts.Pairing, Dome.Socket.Api.Endpoints.Pairing, Dome.Socket.Tests.Pairing, ClientKey, DomeSocketApiFactory, ISocketApiKeys, SocketApiKeys, PairEndpoint (+11 more)

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

### Community 114 - "Chat & Messaging"
Cohesion: 0.22
Nodes (9): Attachments use Attachment, Chat & Messaging, Code and tool output use CodeBlock, Contents, Escape hatch: the scroller hooks, Message rows use Message, Message surfaces use Bubble, Scrollable threads use MessageScroller (+1 more)

### Community 115 - "Compose YAML Editor"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 118 - "Client npm Scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 119 - ".AddDomeBusiness"
Cohesion: 0.18
Nodes (8): DependencyInjection, DeviceDiagramService, DeviceQueryService, IDeviceQueryService, IDomeSocketApiFactory, GetContainersResponse, Containers, DeviceQueryServiceTests

### Community 122 - ".DownAsync"
Cohesion: 0.26
Nodes (5): DockerComposeClient, DockerComposeResult, Error, IsInvalid, Succeeded

### Community 123 - "IDockerComposeClient"
Cohesion: 0.13
Nodes (11): IDockerComposeClient, DownComposeEndpoint, UpComposeEndpoint, ComposeCommandErrorResponse, Message, ComposeDownRequest, ProjectName, RemoveVolumes (+3 more)

### Community 124 - "Dome.Api/Program.cs"
Cohesion: 0.10
Nodes (9): Dome.Business.Settings, Dome.Business, Dome.Business.Auth, Dome.Api.Endpoints.Auth, Dome.Shared.Settings, Dome.Shared.Auth, Dome.Api.Endpoints.Settings, Dome.Api.Telemetry (+1 more)

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

### Community 138 - "Endpoint"
Cohesion: 0.06
Nodes (25): LoginEndpoint, DeleteDeviceContainerEndpoint, CreateSocketEndpoint, DeleteSocketEndpoint, GetSocketEndpoint, GetSocketsEndpoint, SetSocketKeyEndpoint, UpdateSocketEndpoint (+17 more)

### Community 139 - "DockerHubRegistryProvider"
Cohesion: 0.29
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 140 - "Foundations"
Cohesion: 0.25
Nodes (8): Color roles, Elevation, Foundations, Motion and icons, Palette, Radius, Spacing, Typography

### Community 141 - "DeviceDiagramDto"
Cohesion: 0.11
Nodes (15): Dome.Shared.Diagrams, DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, DeviceDiagramDto, Containers (+7 more)

### Community 143 - "IAdminAccountService"
Cohesion: 0.12
Nodes (8): SetupAdminEndpoint, IAdminAccountService, LoginRequest, Password, Username, SetupAdminRequest, Password, Username

### Community 145 - "Step 4: Map Tokens to the Theme"
Cohesion: 0.25
Nodes (8): Dark mode, Elevation, Focus, Fonts, Radius, Role mapping, Step 4: Map Tokens to the Theme, Type scale

### Community 147 - "ImageVersionCheckResponse"
Cohesion: 0.17
Nodes (10): CheckImageVersionsResponse, Results, ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest (+2 more)

### Community 149 - "CreateSocketRequest"
Cohesion: 0.40
Nodes (4): CreateSocketRequest, Address, Key, Name

### Community 150 - "tailwind"
Cohesion: 0.33
Nodes (6): tailwind, baseColor, config, css, cssVariables, prefix

### Community 151 - "DeleteDeviceContainerEndpoint"
Cohesion: 0.40
Nodes (4): DeleteDeviceContainerRequest, ContainerId, Name, RemoveVolumes

### Community 156 - "DockerEndpointOptions"
Cohesion: 0.33
Nodes (3): DockerEndpointOptions, Endpoint, DockerEndpointOptionsTests

### Community 157 - "AuthStatusResponse"
Cohesion: 0.33
Nodes (5): AuthStatusResponse, Authenticated, OnboardingCompleted, SetupRequired, Username

## Knowledge Gaps
- **407 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+402 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 950 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **23 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Socket` to `SocketService`, `Guid`, `Endpoint`, `IStackRepository`, `.CreateService`, `Docker/DependencyInjection.cs`, `Socket Container Endpoints`, `EF Core Migrations`, `Dome.Business.Stacks`, `Socket Service Tests`, `DomeSocketApiFactory.cs`, `Stack Deploy Service`, `Container Command Results`, `fastendpoints`, `OCI Registry Provider`, `Dome.Api.Endpoints.Devices`, `Release Documentation Skill`, `Cache Entity Configuration`, `DeviceImageVersionChecker`, `Diagram Service Tests`, `Database Context`, `DomeSocketApiFactory`, `.AddDomeBusiness`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _407 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `shadcn Skill Overview` be split into smaller, more focused modules?**
  _Cohesion score 0.05824561403508772 - nodes in this community are weakly interconnected._
- **Why does `shadcn Skill` connect `shadcn Skill` to `shadcn CLI Commands`, `shadcn Skill Overview`, `components.json`, `shadcn Composition Rules`, `Customization & Theming`, `Design System Verification`, `Design System Page`, `Chat Component Mapping`, `Base vs Radix Rules`, `Styling & Customization Rules`, `shadcn Registry`?**
  _High betweenness centrality (0.022) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `Socket` (e.g. with `CHANGELOG.md` and `.CreateHttpClient()`) actually correct?**
  _`Socket` has 4 INFERRED edges - model-reasoned connections that need verification._
- **Should `ImageReference` be split into smaller, more focused modules?**
  _Cohesion score 0.12105263157894737 - nodes in this community are weakly interconnected._