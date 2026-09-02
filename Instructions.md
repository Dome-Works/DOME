# HomelabDocs Development Instructions

These instructions apply to every human or automated contributor working in
this repository. Follow existing patterns unless this document explicitly says
otherwise.

## Project purpose

HomelabDocs visualizes Docker environments:

- `HomelabDocs.Client` is the React user interface.
- `HomelabDocs.Server` stores configuration and coordinates Docker hosts.
- `HomelabDocs.Socket` runs on each Docker host and is the only component that
  communicates with the local Docker Engine.

The intended communication flow is:

`Client -> Server API -> Business -> Domain and/or Socket API`

The Server must never connect directly to a Docker Engine. The Client must only
communicate with the Server API. The Socket must use its local Unix Docker
socket; do not introduce remote Docker TCP access unless explicitly requested.

## Repository structure

- `src/HomelabDocs.Client`: React 19, TypeScript, Vite, React Flow and Radix UI.
- `src/HomelabDocs.Server/HomelabDocs.Api`: FastEndpoints HTTP API.
- `src/HomelabDocs.Server/HomelabDocs.Business`: application and orchestration logic.
- `src/HomelabDocs.Server/HomelabDocs.Domain`: EF Core, SQLite, entities,
  repositories and migrations.
- `src/HomelabDocs.Server/HomelabDocs.Shared`: Server API transport contracts.
- `src/HomelabDocs.Socket/HomelabDocs.Socket.Api`: Docker integration and
  FastEndpoints socket API.
- `src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts`: Refit interface and
  Socket API contracts.
- `*.Tests`: xUnit test projects. Existing tests also use FakeItEasy and FsCheck.

The solution targets .NET 10 with nullable reference types and implicit usings
enabled. NuGet versions are managed centrally in `Directory.Packages.props`.

## Required working method

Before changing code:

1. Read the relevant implementation, interfaces, callers, contracts and tests.
2. Search for an existing implementation of the same pattern elsewhere in the repository.
3. Reproduce the failure when practical, or establish the root cause from concrete evidence.
4. State or record the root cause before selecting a fix.

While changing code:

1. Make the smallest coherent change that fixes the root cause.
2. Preserve public contracts unless the task requires a contract change.
3. Update every affected layer when a contract intentionally changes.
4. Do not perform unrelated refactoring, dependency upgrades or formatting.
5. Do not invent methods, properties, package APIs or framework behavior. Inspect
   their definitions or official documentation first.

After changing code:

1. Build the affected code.
2. Run the relevant tests and frontend linting when applicable.
3. Investigate and correct failures caused by the change.
4. Review the final diff for accidental or unrelated edits.
5. Report the commands run and any validation that could not be performed.

Do not claim that a task is complete while relevant build, lint or test failures remain.

## C# conventions and boundaries

- Keep one type per file. Every `class`, `interface`, `record`, `enum` and
  similar type must live in its own file.
- Do not use nested classes or nested records.
- Use file-scoped namespaces, matching the surrounding code.
- Keep nullable reference type warnings meaningful; do not suppress them to
  avoid fixing a real nullability problem.
- Use asynchronous APIs for I/O. Async methods should use the `Async` suffix and
  accept/propagate a `CancellationToken` where the surrounding API supports it.
- Do not block asynchronous work with `.Result`, `.Wait()` or
  `.GetAwaiter().GetResult()`.
- Prefer dependency injection and existing abstractions over constructing
  infrastructure dependencies inside consumers.
- Avoid catching `Exception` unless the boundary intentionally converts a
  dependency failure into an application result and cancellation is preserved.

Layer responsibilities:

- FastEndpoints endpoints handle HTTP concerns, validation, status codes and
  mapping. Keep business logic out of endpoints.
- Endpoints must use ViewModels for outward communication. Never expose Domain
  entities through the API.
- Services must communicate using DTOs. Prefer explicit names that keep
  transport, service and persistence shapes distinguishable.
- Business contains application logic and orchestration. It may use Domain
  abstractions and Socket contracts.
- Domain owns EF Core entities, the `DbContext`, repositories and migrations. It
  must not depend on API or frontend concerns.
- Socket contracts define the Refit boundary shared between Server and Socket.
  Do not leak Docker.DotNet types across this boundary.
- Only `HomelabDocs.Socket.Api` may reference or invoke Docker.DotNet.

Follow existing FastEndpoints, Refit, repository and dependency-injection
patterns before introducing a new abstraction.

## Database changes

- SQLite persistence and all EF Core migrations belong in
  `src/HomelabDocs.Server/HomelabDocs.Domain`.
- Do not replace migrations with startup-time schema mutations.
- Preserve existing data unless destructive behavior is explicitly requested.
- When changing persisted models, add or update tests and create an EF migration
  when appropriate.
- Generate migrations from the repository root with:

```bash
dotnet ef migrations add <Name> \
  --project src/HomelabDocs.Server/HomelabDocs.Domain \
  --startup-project src/HomelabDocs.Server/HomelabDocs.Domain
```

Review generated migrations before considering a database change complete.

## Frontend conventions

- Use TypeScript and preserve strict typing. Do not introduce `any` merely to
  bypass a type error.
- Keep network calls in `src/HomelabDocs.Client/src/api` and reusable API shapes
  in `src/HomelabDocs.Client/src/types`.
- Keep components focused. Extract reusable behavior when it genuinely reduces
  duplication, not pre-emptively.
- Treat component props as read-only.
- Preserve the existing visual language and reuse current UI components before
  adding another component library.
- Use semantic HTML and accessible names. Preserve keyboard interaction, focus
  behavior and meaningful loading/error states.
- Do not edit generated or library-style files under `components/ui` unless the
  task specifically requires changing the shared primitive.

## Validation commands

Use the pinned SDK from `global.json` and the lockfile in the Client project.

Full .NET validation from the repository root:

```bash
dotnet restore HomelabDocs.slnx
dotnet build HomelabDocs.slnx --configuration Release --no-restore
dotnet test HomelabDocs.slnx --configuration Release --no-build
```

Frontend validation:

```bash
cd src/HomelabDocs.Client
npm ci --ignore-scripts
npm run lint
npm run build
```

For a small change, targeted builds and tests may be used during development,
but run the broadest practical validation before finishing. If Dockerfiles or
Compose files change, also validate the relevant image build or Compose
configuration when Docker is available.

## Tests

- Add or update tests for behavior changes and bug fixes.
- Test externally observable behavior and important edge cases rather than
  private implementation details.
- Place tests in the corresponding `*.Tests` project and follow nearby naming
  and fixture patterns.
- Do not weaken, delete or skip a failing test solely to make validation pass.

## Security and operational constraints

- Treat access to `/var/run/docker.sock` as privileged.
- Do not expand network exposure, add unauthenticated privileged operations or
  log secrets without explicit requirements and a clear security review.
- Do not commit credentials, tokens, local database files or machine-specific paths.
- Preserve cancellation, timeouts and useful error reporting around HTTP and
  Docker operations.
- Keep production Docker images and Compose behavior compatible with the
  documented `api`, `socket` and `client` services.

## Completion report

Summarize:

- the root cause or requested behavior;
- the files and behavior changed;
- the build, lint and test commands executed and their outcomes;
- any remaining limitation or validation that was not possible.
