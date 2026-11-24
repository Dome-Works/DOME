# Graph Report - DOME  (2026-11-24)

## Corpus Check
- 374 files · ~71,106 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 21 file(s) not represented in the graph (top: (none) 8, .xml 6, .css 3)

## Summary
- 2785 nodes · 6699 edges · 154 communities (134 shown, 20 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 521 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a0a093f3`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- shadcn Skill Overview
- ImageReference
- CancellationToken
- Client Auth and Routing
- Container Diagram Client
- Input
- ContainerResponse
- Design System Recipes
- SystemSettingsResponse
- Guid
- ComposeUpRequest
- SocketsPage.tsx
- Design System Page
- Socket
- IReadOnlyList
- SocketState
- Regex
- Solution and Packages
- Build Workflow
- SocketsPage.tsx
- Dome.Socket.Contracts.Compose
- IConfiguration
- .Classify
- .DeleteUnmanagedAsync
- DeleteContainerStatus
- Dome.Socket.Api.Docker.Clients
- EF Core Migrations
- Docker Workflow
- Dev Compose Config
- Scorecard Workflow
- AuthStatusResponse
- Docker/DependencyInjection.cs
- StackViewModel
- DeviceStackDeployServiceTests.cs
- .CreateService
- DomeSocketApiFactory.cs
- Domain Persistence
- Stack Deploy Service
- Container Command Results
- shadcn Composition Rules
- Design System Verification
- Compose Sample Config
- AssemblyApplicationVersion
- .InvokeAsync
- fastendpoints
- Zizmor Workflow Config
- .SendAuthenticatedAsync
- Base vs Radix Rules
- IReadOnlyCollection
- TypeScript App Config
- Dome.Domain.Sockets
- Release Documentation Skill
- Tools
- InlineData
- .Compute
- Cache Entity Configuration
- package.json
- GetDeviceDiagramResponse
- Container View Models
- shadcn Registry
- Dome.Socket.Contracts.Images
- TypeScript Node Config
- Dome.Business.Settings
- .AddDomeSocket
- shadcn CLI Commands
- shadcn Skill Docs Index
- Docker Build Args
- DeviceImageVersionChecker
- Oxlint Config
- ImageTagVersion
- .InvokeAsync
- Styling & Customization Rules
- ContainerVolumeResponse
- .TryMap
- Client Dependencies
- Diagram Service Tests
- DOME Architecture Rules
- Client Dev Dependencies
- Chat Component Mapping
- shadcn CLI Reference
- shadcn Component Rules
- RecordingProcessRunner
- .CreateService
- Dependabot Config
- Dependency Review Workflow
- shadcn Skill Principles
- Changelog Releases
- DockerComposeResult
- microsoft_data_sqlite
- ImageVersionCheckDto
- ContainerDto
- ImageVersionCache
- .AddDomeBusiness
- DomeDbContext
- Dev Compose Services
- Release Workflow
- .LoadOrCreate
- GetDeviceContainersEndpoint
- components.json
- Socket Key Security
- microsoft_entityframeworkcore
- Customization & Theming
- Release Image Publishing
- showcase-coverage.mjs
- IReadOnlyCollection
- README Operations
- Publish Workflow Attestations
- The Design System Page
- DomeSocketApiFactory
- CI Build Pipeline
- GitHub Release Changelog Config
- Tag and Env Workflow Steps
- Docker Image Matrix
- Endpoint
- Chat & Messaging
- shadcn Skill
- DeleteContainerResult
- Dome.Api.Endpoints.Devices
- GhcrRegistryProvider
- Dome.Socket.Api.Docker.Processes
- AGENTS.md Guidelines
- Graphify Rules
- DeviceImageVersionCheckerTests.cs
- Auth/DependencyInjection.cs
- Dome.Business.Auth
- shadcn Logo Images
- Supply Chain Scans
- Docker Env Variables
- MCP Server Config
- UpdateDeviceStackRequest
- TypeScript Project References
- Dependabot Grouping
- License Allowlist
- Graphify Skill
- Release Notes Categories
- Favicon Icon
- SqlitePathTests
- SocketService
- DockerHubRegistryProvider
- Step 4: Map Tokens to the Theme
- ImageVersionClassification
- Foundations
- vite.config.ts
- CreateSocketRequest
- .AddDomeSocket
- ComposeYamlEditor.tsx
- ImageVersionCheckResponse
- tailwind
- ComposeDownRequest
- SetupAdminRequest
- .RunAsync
- scripts
- overrides

## God Nodes (most connected - your core abstractions)
1. `cn()` - 92 edges
2. `Socket` - 64 edges
3. `IDomeSocketApi` - 54 edges
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
- **DESIGN.md to Themed shadcn App Flow** — _agents_skills_shadcn_design_system_design_md, _agents_skills_shadcn_design_system_picking_a_style, _agents_skills_shadcn_design_system_role_mapping, _agents_skills_shadcn_design_system_control_heights, _agents_skills_shadcn_design_system_page, _agents_skills_shadcn_design_system_verify_step [EXTRACTED 1.00]
- **DOME Release Version Sync** — agents_skills_document_release_skill_changelog_md, _agents_skills_document_release_skill_directory_build_props, _agents_skills_document_release_skill_dome_client_package_json, agents_skills_document_release_skill_github_release_notes, agents_skills_document_release_skill_readme_md [EXTRACTED 1.00]
- **DOME three-service architecture** — instructions_dome_client, instructions_dome_server, instructions_dome_socket, instructions_communication_flow [EXTRACTED 1.00]
- **shadcn Chat Primitives Composition** — _agents_skills_shadcn_rules_chat_messagescroller, _agents_skills_shadcn_rules_chat_message, _agents_skills_shadcn_rules_chat_bubble, _agents_skills_shadcn_rules_chat_attachment, _agents_skills_shadcn_rules_chat_marker, _agents_skills_shadcn_rules_chat_trace [EXTRACTED 1.00]
- **Graphify usage rules** — claude_graphify_rules, claude_graphify_query_rule, claude_graphify_update_rule, claude_graphify_hook_churn_rule [INFERRED 0.75]
- **CI/CD supply-chain security checks** — _github_workflows_dependency_review_workflow, _github_workflows_scorecard_workflow, _github_workflows_zizmor_workflow, _github_dependabot_config, _github_workflows_release_build_provenance_attestation [INFERRED 0.85]
- **Socket authentication and pairing** — readme_socket_key, readme_certificate_pinning, changelog_unreleased_encrypted_socket_traffic, docker_compose_socket_data_volume [INFERRED 0.85]

## Communities (154 total, 20 thin omitted)

### Community 0 - "shadcn Skill Overview"
Cohesion: 0.07
Nodes (62): Composition: asChild (radix) vs render (base), Items always inside their Group component, Component Structure → [composition.md](./rules/composition.md), lucide-react, radix-ui, react, AppHeader(), pageTitles (+54 more)

### Community 1 - "ImageReference"
Cohesion: 0.15
Nodes (10): IRegistryProvider, RegistryVersionInfo, RemoteDigest, Tags, ImageReference, Canonical, Digest, Host (+2 more)

### Community 2 - "CancellationToken"
Cohesion: 0.06
Nodes (10): GetAuthStatusEndpoint, LogoutEndpoint, GetSystemSettingsEndpoint, UpdateSystemSettingsEndpoint, ISystemSettingsService, ImageVersionCacheRepository, DatabaseInitializer, IDatabaseInitializer (+2 more)

### Community 3 - "Client Auth and Routing"
Cohesion: 0.12
Nodes (40): react-dom, react-router, sonner, fetchAuthStatus(), login(), logout(), postAuth(), setupAdmin() (+32 more)

### Community 4 - "Container Diagram Client"
Cohesion: 0.07
Nodes (42): @xyflow/react, deleteDeviceContainer(), fetchDeviceDiagram(), postContainerLifecycle(), startDeviceContainer(), stopDeviceContainer(), fetchDevices(), fetchDeviceImageVersionChecks() (+34 more)

### Community 5 - "Input"
Cohesion: 0.14
Nodes (20): Buttons inside inputs use InputGroup + InputGroupAddon, Contents, Field validation and disabled states, FieldSet + FieldLegend for grouping related fields, Forms & Inputs, Forms use FieldGroup + Field, InputGroup requires InputGroupInput/InputGroupTextarea, Option sets (2–7 choices) use ToggleGroup (+12 more)

### Community 6 - "ContainerResponse"
Cohesion: 0.08
Nodes (8): Dome.Socket.Tests.Docker.Clients, ContainerMapper, ImageDigestMapper, ContainerMapperProperties, FallbackNameSample, ImageDigestMapperTests, NameResolutionSample, StackSample

### Community 7 - "Design System Recipes"
Cohesion: 0.20
Nodes (26): Known traps, ComposeEditorDialog(), ComposeEditorDialogProps, ComposeYamlEditor, ConfirmDeleteDialog(), ConfirmDeleteDialogProps, CreateStackDialog(), CreateStackDialogProps (+18 more)

### Community 8 - "SystemSettingsResponse"
Cohesion: 0.17
Nodes (10): Dome.Shared.Settings, SystemSettingsResponse, Environment, InstallationId, OnboardingCompleted, TelemetryEnabled, Version, UpdateSystemSettingsRequest (+2 more)

### Community 9 - "Guid"
Cohesion: 0.08
Nodes (25): StackMutationResult, Error, IsConflict, IsNotFound, IsSuccess, Stack, StackService, IStackRepository (+17 more)

### Community 10 - "ComposeUpRequest"
Cohesion: 0.29
Nodes (4): UpComposeEndpoint, ComposeUpRequest, ComposeYaml, ProjectName

### Community 11 - "SocketsPage.tsx"
Cohesion: 0.16
Nodes (30): updateDeviceStack(), AccessIcon(), canStart(), canStop(), ContainerDetailPane(), ContainerDetailPaneProps, normalizedState(), stateBadgeClassName() (+22 more)

### Community 12 - "Design System Page"
Cohesion: 0.23
Nodes (11): DESIGN.md, Design System Page, Color Roles Contrast Pairs, ColorPair, Component Tiers, Do and Don't Pairs, preview-states.css data-preview Variants, previewState (+3 more)

### Community 13 - "Socket"
Cohesion: 0.14
Nodes (9): Socket, Address, CertificateFingerprint, CreatedAt, Id, Name, ProtectedApiKey, SocketRepository (+1 more)

### Community 14 - "IReadOnlyList"
Cohesion: 0.12
Nodes (9): GetSocketsEndpoint, GetSocketsResponse, Sockets, DeleteContainersEndpoint, DeleteContainersRequest, ContainerIds, RemoveVolumes, DeleteContainersResponse (+1 more)

### Community 15 - "SocketState"
Cohesion: 0.12
Nodes (14): GetSocketStatusesResponse, Statuses, SocketState, CertificateChanged, NeedsPairing, Offline, Online, Unauthorized (+6 more)

### Community 16 - "Regex"
Cohesion: 0.18
Nodes (6): StackProjectName, ComposeProjectName, OciRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost

### Community 17 - "Solution and Packages"
Cohesion: 0.05
Nodes (38): Docker.DotNet.Enhanced, FakeItEasy, FsCheck, FsCheck.Xunit.v3, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Abstractions (+30 more)

### Community 18 - "Build Workflow"
Cohesion: 0.05
Nodes (39): concurrency, cancel-in-progress, group, name, needs, permissions, uses, jobs (+31 more)

### Community 19 - "SocketsPage.tsx"
Cohesion: 0.11
Nodes (35): Assistant activity uses Trace, No manual z-index on overlay components, readApiError(), createSocket(), deleteSocket(), fetchSockets(), fetchSocketStatuses(), setSocketKey() (+27 more)

### Community 20 - "Dome.Socket.Contracts.Compose"
Cohesion: 0.15
Nodes (5): Dome.Socket.Contracts.Compose, Dome.Socket.Api.Endpoints.Compose, Dome.Socket.Api.Endpoints.Health, Dome.Socket.Api.Docker.Compose, Dome.Socket.Contracts.Health

### Community 22 - ".Classify"
Cohesion: 0.20
Nodes (7): ImageVersionClassifier, ImageVersionSnapshot, Error, RemoteDigest, Tags, Unsupported, ImageVersionClassifierTests

### Community 23 - ".DeleteUnmanagedAsync"
Cohesion: 0.21
Nodes (7): DeviceStackDeployResult, Error, IsDeviceNotFound, IsFailed, IsStackNotFound, IsSuccess, DeviceStackDeployService

### Community 24 - "DeleteContainerStatus"
Cohesion: 0.14
Nodes (9): OciTagListResponse, Tags, OciTokenResponse, AccessToken, Token, DeleteContainerStatus, Deleted, Failed (+1 more)

### Community 25 - "Dome.Socket.Api.Docker.Clients"
Cohesion: 0.09
Nodes (10): Dome.Socket.Api.Endpoints.Containers, Dome.Socket.Tests.Docker.Images, Dome.Socket.Api.Docker.Clients, Dome.Socket.Contracts.Containers, SystemDataUsageResponse, SystemDataUsageVolumeResponse, Name, UsageData (+2 more)

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

### Community 30 - "AuthStatusResponse"
Cohesion: 0.17
Nodes (9): Dome.Shared.Auth, AuthStatusResponse, Authenticated, OnboardingCompleted, SetupRequired, Username, LoginRequest, Password (+1 more)

### Community 31 - "Docker/DependencyInjection.cs"
Cohesion: 0.27
Nodes (4): Dome.Socket.Tests.Docker.Configuration, Dome.Domain.Tests.Seeding, Dome.Socket.Api.Docker.Configuration, Dome.Socket.Api.Docker

### Community 32 - "StackViewModel"
Cohesion: 0.09
Nodes (16): CreateDeviceStackEndpoint, GetDeviceStackEndpoint, GetDeviceStacksEndpoint, GetDeviceStacksRequest, SocketName, GetDeviceStacksResponse, Stacks, StackViewModel (+8 more)

### Community 33 - "DeviceStackDeployServiceTests.cs"
Cohesion: 0.18
Nodes (4): Dome.Socket.Contracts, Dome.Business.Tests.Devices, Dome.Business.Tests.Settings, DeviceQueryServiceTests

### Community 34 - ".CreateService"
Cohesion: 0.16
Nodes (9): DeviceQueryService, IDomeSocketApiFactory, ISocketRepository, GetHealthEndpoint, SocketHealthResponse, DockerReachable, Version, IDomeSocketApi (+1 more)

### Community 35 - "DomeSocketApiFactory.cs"
Cohesion: 0.12
Nodes (6): Dome.Socket.Tests.Security, Dome.Socket.Contracts.Pairing, Dome.Socket.Api.Endpoints.Pairing, Dome.Socket.Api.Security, DependencyInjection, SocketCertificate

### Community 37 - "Stack Deploy Service"
Cohesion: 0.22
Nodes (3): GetContainersResponse, Containers, DeviceStackDeployServiceTests

### Community 38 - "Container Command Results"
Cohesion: 0.10
Nodes (13): DeleteDeviceContainerEndpoint, DeviceContainerLifecycleRequest, ContainerId, Name, StartDeviceContainerEndpoint, StopDeviceContainerEndpoint, DeviceContainerCommandResult, IsContainerNotFound (+5 more)

### Community 39 - "shadcn Composition Rules"
Cohesion: 0.15
Nodes (10): Component Composition Rules, Alert for Callouts, Avatar Requires AvatarFallback, Button Loading via Spinner, Full Card Composition, Empty State Component, Overlay Component Selection, TabsTrigger Inside TabsList (+2 more)

### Community 40 - "Design System Verification"
Cohesion: 0.11
Nodes (18): --radius Border Radius, Elevation Shadow Overrides, Focus Treatment Mapping, Fonts via Fontsource Substitutes, Showcase Known Traps, Design System Verification, Contents, Coverage check (+10 more)

### Community 41 - "Compose Sample Config"
Cohesion: 0.09
Nodes (23): environment, image, ports, restart, volumes, depends_on, image, ports (+15 more)

### Community 42 - "AssemblyApplicationVersion"
Cohesion: 0.14
Nodes (5): Dome.Socket.Api, TelemetryBuildInfo, Environment, ApplicationVersion, Current

### Community 43 - ".InvokeAsync"
Cohesion: 0.13
Nodes (8): AuthSignIn, ApiKeyMiddleware, SocketKey, IsConfigured, IsPaired, KeyPath, Value, SocketSecurity

### Community 44 - "fastendpoints"
Cohesion: 0.21
Nodes (4): Dome.Shared.Sockets, Dome.Business.Sockets, Dome.Business.Tests.Sockets, Dome.Api.Endpoints.Sockets

### Community 45 - "Zizmor Workflow Config"
Cohesion: 0.09
Nodes (21): jobs, zizmor, name, permissions, actions, contents, security-events, paths (+13 more)

### Community 46 - ".SendAuthenticatedAsync"
Cohesion: 0.21
Nodes (3): OciAuthenticatedResponse, Response, Token

### Community 47 - "Base vs Radix Rules"
Cohesion: 0.10
Nodes (20): Accordion type and defaultValue, asChild (radix) vs render (base), nativeButton={false}, Select Base vs Radix Differences, Slider Scalar vs Array, ToggleGroup type vs multiple, Forms & Inputs Rules, Choosing Form Controls (+12 more)

### Community 48 - "IReadOnlyCollection"
Cohesion: 0.16
Nodes (9): DockerContainerLifecycleResult, NotFound, Succeeded, IDockerEngineClient, ContainerLifecycleRequest, Id, StartContainerEndpoint, StopContainerEndpoint (+1 more)

### Community 49 - "TypeScript App Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+12 more)

### Community 50 - "Dome.Domain.Sockets"
Cohesion: 0.10
Nodes (7): Dome.Business.Stacks, Dome.Domain.Stacks, Dome.Business.Tests.Stacks, Dome.Domain.Sockets, Dome.Shared.Stacks, Dome.Shared.Diagrams, DiagramStackKind

### Community 51 - "Release Documentation Skill"
Cohesion: 0.15
Nodes (15): document-release Skill, Directory.Build.props Version, Dome.Client package.json version, ghcr.io/dome-works server/socket/client images, release.yml Workflow, CHANGELOG.md, Cutting a version, Do not do (+7 more)

### Community 52 - "Tools"
Cohesion: 0.13
Nodes (14): search Command, Configuring Registries in components.json, shadcn MCP Tools (search/list/view/examples/add/audit), Configuring Registries, Setup, `shadcn:get_add_command_for_items`, `shadcn:get_audit_checklist`, `shadcn:get_item_examples_from_registries` (+6 more)

### Community 53 - "InlineData"
Cohesion: 0.05
Nodes (19): AdminAccountService, AdminSetupResult, Error, IsConflict, IsSuccess, PasswordHasher, SystemSettingsService, TelemetrySettings (+11 more)

### Community 54 - ".Compute"
Cohesion: 0.36
Nodes (3): Dome.Socket.Tests.Pairing, PairingProof, PairingProofTests

### Community 55 - "Cache Entity Configuration"
Cohesion: 0.15
Nodes (7): ImageVersionCacheConfiguration, SystemSetting, Key, Value, SystemSettingConfiguration, SocketConfiguration, StackConfiguration

### Community 56 - "package.json"
Cohesion: 0.11
Nodes (18): class-variance-authority, clsx, cn, oxlint, shadcn, @shadcn/lint, tailwind-merge, tailwindcss (+10 more)

### Community 57 - "GetDeviceDiagramResponse"
Cohesion: 0.11
Nodes (16): DiagramStackViewModel, Id, Kind, ProjectName, TotalBytes, GetDeviceDiagramResponse, Containers, Stacks (+8 more)

### Community 58 - "Container View Models"
Cohesion: 0.07
Nodes (24): ContainerViewModel, Digest, Id, Image, ImageId, Name, Stack, State (+16 more)

### Community 59 - "shadcn Registry"
Cohesion: 0.15
Nodes (16): build Command, Registry Address Schemes, Built Registry, include Field, Registry Item Definitions, registry.json, Source Registry, Address Schemes (+8 more)

### Community 60 - "Dome.Socket.Contracts.Images"
Cohesion: 0.22
Nodes (5): Dome.Socket.Tests.Docker.Registries, Dome.Socket.Api.Docker.Images, Dome.Socket.Api.Endpoints.Images, Dome.Socket.Api.Docker.Registries, Dome.Socket.Contracts.Images

### Community 61 - "TypeScript Node Config"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 62 - "Dome.Business.Settings"
Cohesion: 0.19
Nodes (5): Dome.Business.Settings, Dome.Business, Dome.Api.Endpoints.Settings, Dome.Api.Telemetry, DependencyInjection

### Community 63 - ".AddDomeSocket"
Cohesion: 0.17
Nodes (11): IImageVersionCheckService, ImageVersionCheckService, CheckImageVersionsEndpoint, CheckImageVersionsRequest, Images, CheckImageVersionsResponse, Results, ImageVersionCheckRequestItem (+3 more)

### Community 64 - "shadcn CLI Commands"
Cohesion: 0.13
Nodes (14): add Command, apply Command, docs Command, Dry-Run Mode (--dry-run/--diff/--view), init Command, Presets (named, code, URL), Templates (next, vite, start, react-router, astro, laravel), view Command (+6 more)

### Community 65 - "shadcn Skill Docs Index"
Cohesion: 0.20
Nodes (4): Icons, Icons in Button use data-icon attribute, No sizing classes on icons inside components, Pass icons as component objects, not string keys

### Community 66 - "Docker Build Args"
Cohesion: 0.12
Nodes (16): with, build-args, context, file, images, labels, password, persist-credentials (+8 more)

### Community 68 - "Oxlint Config"
Cohesion: 0.12
Nodes (15): categories, correctness, jsPlugins, overrides, plugins, rules, react/only-export-components, react/rules-of-hooks (+7 more)

### Community 69 - "ImageTagVersion"
Cohesion: 0.22
Nodes (7): ImageTagParser, ImageTagVersion, Major, Minor, Original, Patch, Suffix

### Community 71 - "Styling & Customization Rules"
Cohesion: 0.10
Nodes (20): Semantic CSS Variable Tokens, Dark Mode (.dark class, next-themes), DESIGN.md Role to shadcn Variable Mapping, Styling & Customization Rules, cn() Utility, No Manual z-index on Overlays, gap-* not space-x/y, size-* over w-*/h-* (+12 more)

### Community 72 - "ContainerVolumeResponse"
Cohesion: 0.22
Nodes (7): ContainerVolumeResponse, Destination, Name, ReadOnly, SizeBytes, Source, Type

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
Cohesion: 0.19
Nodes (9): DockerComposeClientTests, RecordingProcessRunner, Arguments, CallCount, Environment, Exception, FileName, Result (+1 more)

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

### Community 87 - "DockerComposeResult"
Cohesion: 0.32
Nodes (4): DockerComposeResult, Error, IsInvalid, Succeeded

### Community 88 - "microsoft_data_sqlite"
Cohesion: 0.32
Nodes (3): Dome.Domain.Tests.Sockets, Dome.Domain.Tests.Sqlite, Dome.Domain.Sqlite

### Community 89 - "ImageVersionCheckDto"
Cohesion: 0.11
Nodes (16): ContainerImageVersionCheckViewModel, CheckedAt, ContainerId, CurrentTag, Image, Kind, NewestTag, GetDeviceImageVersionChecksResponse (+8 more)

### Community 90 - "ContainerDto"
Cohesion: 0.18
Nodes (10): ContainerDto, Digest, Id, Image, ImageId, Name, Stack, State (+2 more)

### Community 91 - "ImageVersionCache"
Cohesion: 0.25
Nodes (8): ImageVersionCache, CheckedAt, Id, ImageReference, LastError, RemoteDigest, RemoteTags, Unsupported

### Community 92 - ".AddDomeBusiness"
Cohesion: 0.09
Nodes (15): GetDeviceDiagramEndpoint, GetDeviceDiagramRequest, SocketName, GetDeviceImageVersionChecksEndpoint, GetDeviceImageVersionChecksRequest, SocketName, AssemblyApplicationVersion, Current (+7 more)

### Community 93 - "DomeDbContext"
Cohesion: 0.18
Nodes (6): DomeDbContext, ImageVersionCaches, Sockets, Stacks, SystemSettings, DomeDbContextFactory

### Community 94 - "Dev Compose Services"
Cohesion: 0.27
Nodes (10): api service (local build), client service (local build), dome-data volume, socket-data volume, socket service (local build), dev.docker-compose (build from source), api service (ghcr image), client service (ghcr image) (+2 more)

### Community 95 - "Release Workflow"
Cohesion: 0.20
Nodes (9): concurrency, cancel-in-progress, group, jobs, name, permissions, types, True (+1 more)

### Community 96 - ".LoadOrCreate"
Cohesion: 0.13
Nodes (3): PrivateFile, SocketCertificateTests, SocketKeyTests

### Community 97 - "GetDeviceContainersEndpoint"
Cohesion: 0.24
Nodes (6): GetDeviceContainersEndpoint, GetDeviceContainersRequest, Name, GetDeviceContainersResponse, Containers, IDeviceQueryService

### Community 98 - "components.json"
Cohesion: 0.14
Nodes (13): info Command, shadcn info Project Context, aliases, components, hooks, lib, ui, utils (+5 more)

### Community 99 - "Socket Key Security"
Cohesion: 0.25
Nodes (7): Unreleased: Socket keys, socket-data volume, socket service (ghcr image), Socket key (socket.key / DOME_SOCKET_KEY), Security scope (in/out), Security Policy, Private vulnerability reporting via GitHub Security Advisories

### Community 100 - "microsoft_entityframeworkcore"
Cohesion: 0.13
Nodes (7): Dome.Domain.Seeding, Dome.Domain.Tests.Settings, Dome.Domain.Settings, Dome.Domain.Images, DependencyInjection, SystemSettingKeys, SystemSettingRepositoryTests

### Community 101 - "Customization & Theming"
Cohesion: 0.11
Nodes (18): Adding Custom Colors (Tailwind v3/v4), cva Variants, OKLCH Color Format, 1. Built-in variants, 2. Tailwind classes via `className`, 3. Add a new variant, 4. Wrapper components, Adding Custom Colors (+10 more)

### Community 102 - "Release Image Publishing"
Cohesion: 0.25
Nodes (8): Build provenance attestation, DOME_ENVIRONMENT=Release build-arg, GHCR registry ghcr.io/dome-works, publish job, Release workflow (publish images), v0.6.0 Onboarding and anonymous usage tracking, Telemetry (PostHog instance_heartbeat), Telemetry environment (Development/Test/Release)

### Community 103 - "showcase-coverage.mjs"
Cohesion: 0.25
Nodes (6): components, missing, source, [uiArg, ...roots], uiDir, walk()

### Community 104 - "IReadOnlyCollection"
Cohesion: 0.12
Nodes (11): DockerEngineClient, DependencyInjection, ContainerResponse, Digest, Id, Image, ImageId, Name (+3 more)

### Community 105 - "README Operations"
Cohesion: 0.25
Nodes (8): dome-data volume, Configuration settings, Current limitations, DOME README, Codebase knowledge graph (graphify), Connect multiple Docker hosts, Persistence and backups (dome-data volume), Troubleshooting

### Community 106 - "Publish Workflow Attestations"
Cohesion: 0.25
Nodes (8): publish, attestations, contents, id-token, packages, name, permissions, runs-on

### Community 107 - "The Design System Page"
Cohesion: 0.14
Nodes (14): defaultStates, previewState(), StateMatrix(), Block anatomy, Checklist, Components: Tiers and Block Anatomy, Contents, Do and Don't (+6 more)

### Community 108 - "DomeSocketApiFactory"
Cohesion: 0.09
Nodes (10): ClientKey, DomeSocketApiFactory, PairEndpoint, PairRequest, Nonce, PairResponse, Proof, DomeSocketApiFactoryTests (+2 more)

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

### Community 113 - "Endpoint"
Cohesion: 0.08
Nodes (14): LoginEndpoint, SetupAdminEndpoint, DeleteDeviceStackEndpoint, DeleteDeviceStackRequest, RemoveVolumes, SocketName, StackId, DeleteDeviceUnmanagedStackEndpoint (+6 more)

### Community 114 - "Chat & Messaging"
Cohesion: 0.20
Nodes (10): Attachments use Attachment, Chat & Messaging, Code and tool output use CodeBlock, Contents, Escape hatch: the scroller hooks, Message rows use Message, Message surfaces use Bubble, Scrollable threads use MessageScroller (+2 more)

### Community 115 - "shadcn Skill"
Cohesion: 0.18
Nodes (10): shadcn openai.yml Agent Interface, Icons Rules, Project iconLibrary, Pass Icons as Component Objects, shadcn Skill, Component Selection Table, Project Context Key Fields, shadcn CLI (npx shadcn@latest) (+2 more)

### Community 116 - "DeleteContainerResult"
Cohesion: 0.40
Nodes (4): DeleteContainerResult, ContainerId, Error, Status

### Community 117 - "Dome.Api.Endpoints.Devices"
Cohesion: 0.07
Nodes (19): Dome.Business.Devices, Dome.Api.Endpoints.Devices, Dome.Shared.Devices, CreateDeviceStackRequest, ProjectName, SocketName, DeleteDeviceContainerRequest, ContainerId (+11 more)

### Community 118 - "GhcrRegistryProvider"
Cohesion: 0.32
Nodes (5): GhcrRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, GhcrRegistryProviderTests

### Community 122 - "DeviceImageVersionCheckerTests.cs"
Cohesion: 0.20
Nodes (5): Dome.Business.Images, Dome.Shared.Images, Dome.Shared.Containers, Dome.Business.Tests.Images, ImageVersionKind

### Community 124 - "Dome.Business.Auth"
Cohesion: 0.20
Nodes (3): Dome.Business.Auth, Dome.Api.Endpoints.Auth, Dome.Business.Tests.Auth

### Community 125 - "shadcn Logo Images"
Cohesion: 0.67
Nodes (3): shadcn Logo Image, shadcn/ui, shadcn Small Logo Icon

### Community 126 - "Supply Chain Scans"
Cohesion: 0.67
Nodes (3): SARIF upload to code-scanning, OpenSSF Scorecard workflow, Zizmor GitHub Actions analysis workflow

### Community 127 - "Docker Env Variables"
Cohesion: 0.67
Nodes (3): env, PLATFORMS, REGISTRY

### Community 129 - "UpdateDeviceStackRequest"
Cohesion: 0.33
Nodes (5): UpdateDeviceStackRequest, ComposeYaml, ProjectName, SocketName, StackId

### Community 138 - "SocketService"
Cohesion: 0.05
Nodes (28): CreateSocketEndpoint, DeleteSocketEndpoint, GetSocketEndpoint, GetSocketStatusesEndpoint, SetSocketKeyEndpoint, UpdateSocketEndpoint, ISocketService, SocketMutationResult (+20 more)

### Community 139 - "DockerHubRegistryProvider"
Cohesion: 0.33
Nodes (5): DockerHubRegistryProvider, DefaultTokenRealm, DefaultTokenService, RegistryApiHost, DockerHubRegistryProviderTests

### Community 140 - "Step 4: Map Tokens to the Theme"
Cohesion: 0.25
Nodes (8): Dark mode, Elevation, Focus, Fonts, Radius, Role mapping, Step 4: Map Tokens to the Theme, Type scale

### Community 141 - "ImageVersionClassification"
Cohesion: 0.40
Nodes (4): ImageVersionClassification, CurrentTag, Kind, NewestTag

### Community 142 - "Foundations"
Cohesion: 0.19
Nodes (13): ColorPair(), contrast(), luminance(), toHex(), toRgb(), Color roles, Elevation, Foundations (+5 more)

### Community 143 - "vite.config.ts"
Cohesion: 0.25
Nodes (5): @tailwindcss/vite, vite, @vitejs/plugin-react, __dirname, { version }

### Community 144 - "CreateSocketRequest"
Cohesion: 0.40
Nodes (4): CreateSocketRequest, Address, Key, Name

### Community 145 - ".AddDomeSocket"
Cohesion: 0.15
Nodes (10): DockerComposeClient, IDockerComposeClient, DockerComposeOptions, DockerCliPath, WorkingRoot, DockerEndpointOptions, Endpoint, IProcessRunner (+2 more)

### Community 146 - "ComposeYamlEditor.tsx"
Cohesion: 0.40
Nodes (3): monaco-editor, @monaco-editor/react, ComposeYamlEditorProps

### Community 147 - "ImageVersionCheckResponse"
Cohesion: 0.22
Nodes (8): ImageVersionCheckResponse, Error, Image, ImageId, LocalDigest, RemoteDigest, Tags, Unsupported

### Community 148 - "tailwind"
Cohesion: 0.33
Nodes (6): tailwind, baseColor, config, css, cssVariables, prefix

### Community 149 - "ComposeDownRequest"
Cohesion: 0.20
Nodes (6): DownComposeEndpoint, ComposeCommandErrorResponse, Message, ComposeDownRequest, ProjectName, RemoveVolumes

### Community 150 - "SetupAdminRequest"
Cohesion: 0.50
Nodes (3): SetupAdminRequest, Password, Username

### Community 152 - ".RunAsync"
Cohesion: 0.25
Nodes (4): ProcessRunResult, ExitCode, StandardError, StandardOutput

### Community 153 - "scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 157 - "overrides"
Cohesion: 0.67
Nodes (3): dompurify, overrides, monaco-editor

## Knowledge Gaps
- **408 isolated node(s):** `npm`, `cn`, `@shadcn/lint`, `@types/node`, `@types/react` (+403 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 956 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Socket` connect `Socket` to `Guid`, `SocketService`, `IReadOnlyList`, `EF Core Migrations`, `DeviceStackDeployServiceTests.cs`, `.CreateService`, `DomeSocketApiFactory.cs`, `Stack Deploy Service`, `Container Command Results`, `fastendpoints`, `Dome.Domain.Sockets`, `Release Documentation Skill`, `Cache Entity Configuration`, `DeviceImageVersionChecker`, `Diagram Service Tests`, `.CreateService`, `DomeDbContext`, `IReadOnlyCollection`, `DomeSocketApiFactory`, `DeviceImageVersionCheckerTests.cs`?**
  _High betweenness centrality (0.031) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `cn()` (e.g. with `Helpers` and `Type scale`) actually correct?**
  _`cn()` has 4 INFERRED edges - model-reasoned connections that need verification._
- **What connects `npm`, `cn`, `@shadcn/lint` to the rest of the system?**
  _408 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `shadcn Skill Overview` be split into smaller, more focused modules?**
  _Cohesion score 0.0687719298245614 - nodes in this community are weakly interconnected._
- **Why does `shadcn Skill` connect `shadcn Skill` to `shadcn CLI Commands`, `components.json`, `Customization & Theming`, `Input`, `Styling & Customization Rules`, `shadcn Composition Rules`, `Design System Verification`, `Design System Page`, `Chat Component Mapping`, `Base vs Radix Rules`, `shadcn Registry`?**
  _High betweenness centrality (0.025) - this node is a cross-community bridge._
- **Are the 4 inferred relationships involving `Socket` (e.g. with `CHANGELOG.md` and `.CreateHttpClient()`) actually correct?**
  _`Socket` has 4 INFERRED edges - model-reasoned connections that need verification._
- **Should `ImageReference` be split into smaller, more focused modules?**
  _Cohesion score 0.14705882352941177 - nodes in this community are weakly interconnected._