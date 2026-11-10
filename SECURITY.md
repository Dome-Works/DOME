# Security Policy

## Supported versions

Security fixes are applied to the latest commit on `main`. Older releases or forks are not maintained with backports unless noted otherwise.

| Version | Supported |
| --- | --- |
| `main` (latest) | Yes |
| Older tags / forks | No |

## Reporting a vulnerability

Please **do not** open a public GitHub issue for security vulnerabilities.

Report them privately through GitHub Security Advisories:

1. Open [Security Advisories](https://github.com/Dome-Works/DOME/security/advisories/new) for this repository.
2. Include a clear description, impact, affected versions/commits, and steps to reproduce.
3. If you have a patch or suggested fix, include it.

We aim to acknowledge reports within **7 days** and to share an initial assessment or next steps within **14 days**. Timelines may vary for complex issues.

Please give us a reasonable window to investigate and release a fix before any public disclosure.

## Scope

In scope:

- Remote code execution, privilege escalation, or unintended Docker Engine control through Dome.Socket, Dome.Server, or the client
- Authentication or authorization bypass, including Socket key or certificate pinning bypass
- Path traversal, SSRF, injection, or similar flaws in the APIs that expose host or Docker data beyond intended listing
- Supply-chain issues in first-party build or release artifacts published from this repository

Out of scope (unless they cause an unexpected security impact beyond the documented threat model):

- Running DOME on an untrusted network without network controls
- Claiming a fresh installation before its admin account is created (finish onboarding right after installing)
- Sniffing Client-to-Server traffic when DOME is served over plain HTTP without a TLS reverse proxy
- Exposing Dome.Socket (full Docker Engine access) or the Docker socket to untrusted parties by operator misconfiguration
- Denial of service from resource exhaustion in a local/homelab deployment
- Issues only present in third-party dependencies that are not exploitable through DOME (report those upstream when appropriate)
- Social engineering, physical access, or compromised host/Docker environments

## Deployment guidance

DOME is intended for trusted homelab or private networks.

- Do not expose the Server, Socket, or UI to the public internet without additional access controls (reverse proxy auth, VPN, firewall rules, and so on).
- Treat Dome.Socket as highly privileged: it mounts the local Docker socket. Anyone who has its `DOME_SOCKET_KEY` controls that Engine. Keep the key out of public repositories and issue reports.
- The Server and Socket communicate over HTTPS with a pinned certificate. The Server stores Socket keys encrypted with ASP.NET Core Data Protection; the keys for that live next to the database, so back up and protect the whole data volume.
- Put the Client behind a TLS reverse proxy when it is reachable from other machines, so passwords and session cookies are not sent in plain text.
- Keep `.env`, `appsettings*.json` overrides, and Compose secrets out of public repositories and issue reports.

## Prefer private disclosure

Thank you for helping keep DOME and its users safe. Responsible private reporting is preferred over public issues or social media posts for security findings.
