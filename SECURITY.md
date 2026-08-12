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

1. Open [Security Advisories](https://github.com/HomelabDocs/HomelabDocs/security/advisories/new) for this repository.
2. Include a clear description, impact, affected versions/commits, and steps to reproduce.
3. If you have a patch or suggested fix, include it.

We aim to acknowledge reports within **7 days** and to share an initial assessment or next steps within **14 days**. Timelines may vary for complex issues.

Please give us a reasonable window to investigate and release a fix before any public disclosure.

## Scope

In scope:

- Remote code execution, privilege escalation, or unintended Docker Engine control through the HomelabDocs API or client
- Authentication or authorization bypass once those features exist
- Path traversal, SSRF, injection, or similar flaws in the API that expose host or Docker data beyond intended read-only listing
- Supply-chain issues in first-party build or release artifacts published from this repository

Out of scope (unless they cause an unexpected security impact beyond the documented threat model):

- Running HomelabDocs on an untrusted network without network controls — the API currently has **no authentication**
- Use of unencrypted `tcp://` Docker Engine endpoints — TLS for remote Docker is not implemented yet; see the README
- Exposing the Docker socket or Engine API to untrusted parties by operator misconfiguration
- Denial of service from resource exhaustion in a local/homelab deployment
- Issues only present in third-party dependencies that are not exploitable through HomelabDocs (report those upstream when appropriate)
- Social engineering, physical access, or compromised host/Docker environments

## Deployment guidance

HomelabDocs is intended for trusted homelab or private networks.

- Do not expose the API or UI to the public internet without additional access controls (reverse proxy auth, VPN, firewall rules, and so on).
- Prefer Unix socket access for local Docker Engines over plain `tcp://` where possible.
- Treat configured Docker endpoints as highly privileged: anyone who can reach the API can query container metadata from those Engines today.
- Keep `.env`, `appsettings*.json` overrides, and Compose secrets out of public repositories and issue reports.

## Prefer private disclosure

Thank you for helping keep HomelabDocs and its users safe. Responsible private reporting is preferred over public issues or social media posts for security findings.
