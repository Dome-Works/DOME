---
name: document-release
description: >-
  Prepares DOME release documentation by moving CHANGELOG.md Unreleased
  notes into a versioned section, syncing README.md with shipped user-facing
  behavior, and drafting GitHub Release notes. Use when preparing a release,
  cutting a version, updating the changelog for a version bump, writing release
  notes, or aligning README features with a new DOME version.
---

# Document a DOME release

Edit **only** release documentation and the assembly version unless the user
also asks to tag, commit, or publish. Default files:

- `CHANGELOG.md`
- `README.md`
- `Directory.Build.props` (`Version` only)
- `src/Dome.Client/package.json` and `src/Dome.Client/package-lock.json` (`version` only)

Do not invent versions, dates, features, or bug fixes.

## Inputs

1. Confirm the version string (example: `v0.3.0`). If omitted, propose the next
   semantic version from existing git tags and GitHub releases, then wait for
   confirmation before writing.
2. Confirm whether it is a stable release or a pre-release.
3. Use today's date (`YYYY-MM-DD`) unless the user supplies another date.

Version headings in `CHANGELOG.md` use a **v-prefix**: `[v0.3.0] - YYYY-MM-DD`.

## Inspect before writing

Read, in this order:

1. `CHANGELOG.md` (especially `[Unreleased]`)
2. `README.md` Features, Architecture, Configuration, Current limitations
3. Git tags and GitHub releases (`git tag -l`, `gh release list`)
4. Commits and merged PRs since the previous tag

Treat the changelog as user documentation, not a git log. Include a change only
if a DOME operator would notice it.

**Usually omit:** Dependabot bumps, refactors, cleanup, tests-only, CI,
developer tooling, formatting, contributor-only docs.

**Include:** user-facing features, noticeable fixes, install/upgrade/platform
changes, and breaking changes.

## CHANGELOG.md

Keep the existing structure and voice. Write for operators, in plain English,
using README terms (`Socket`, `Server`, `Client`, Diagrams, Compose stack).

Describe user impact, not implementation.

Allowed subsection order under a version:

```markdown
### Added
### Improved
### Fixed
### Breaking Changes
```

**Omit any subsection that has no items.** Never write `None.` or leave an empty
heading.

### No user-facing changes

When a release contains only dependency updates, vulnerability fixes, and other
changes an operator would not notice, do not add subsections and do not name
packages or advisories. Write this sentence under the version heading:

```markdown
## [vX.Y.Z] - YYYY-MM-DD

This release contains no user-facing changes and includes only dependency updates and vulnerability fixes.
```

Use that same sentence in the GitHub Release notes.

### Cutting a version

1. Review `[Unreleased]` against git history. Add missing user-facing items;
   remove developer-only items; de-duplicate.
2. Move the remaining Unreleased items into a new section **immediately below**
   the Unreleased heading:

   ```markdown
   ## [vX.Y.Z] - YYYY-MM-DD
   ```

3. Replace `[Unreleased]` with an empty placeholder (heading only, no empty
   subsections):

   ```markdown
   ## [Unreleased]

   ## [vX.Y.Z] - YYYY-MM-DD
   ```

4. Keep older version sections unchanged.
5. In **Release process**, keep the example heading in v-prefix form
   (`[v0.3.0] - YYYY-MM-DD`).
6. Set `<Version>X.Y.Z</Version>` in `Directory.Build.props` to the confirmed
   tag with the `v` prefix stripped (`v0.3.0` → `0.3.0`). Do not change other
   properties in that file.
7. Set the same `X.Y.Z` as `version` in `src/Dome.Client/package.json` and in
   the root entries of `src/Dome.Client/package-lock.json` (top-level
   `version` and `packages[""].version`). Change nothing else in those files.

Do not create a version section for a tag/release that does not exist unless
the user is actively cutting that release.

## README.md

After the changelog section is final, sync the README with **shipped** behavior
for this version. Do not advertise Unreleased work.

Check at least:

| Section | Update when |
| --- | --- |
| Opening summary | The product description no longer matches what users get |
| Features | A newly released capability is missing, or a listed feature was removed |
| Architecture / Configuration / Troubleshooting | Install, Socket registration, or connectivity guidance changed |
| Current limitations | A limitation no longer applies, or a new user-visible limitation shipped |
| Docker image / Compose docs | Platforms, service names, or upgrade steps changed |

Rules:

- Match changelog terminology; do not paste changelog bullets verbatim if the
  Features list already covers the idea.
- Keep the pre-1.0 warning until a real `v1.0.0` (or the user asks to change it).
- Do not pin `docker-compose.yml` image tags from `latest` to a version unless
  the user asks.
- Do not add an unrelated version badge or rewrite the README.

## GitHub Release notes

Draft notes from the new changelog section (not GitHub's autogenerated PR list).
They should be paste-ready:

```markdown
## [vX.Y.Z] - YYYY-MM-DD

### Added
- ...
```

Include only subsections that have items. For a release with no user-facing
changes, copy the standard sentence from **No user-facing changes** and omit
the subsections. Mention image tags if useful:
`ghcr.io/dome-works/{server,socket,client}:X.Y.Z`.

Create the GitHub Release with `gh` **only if the user asks**. Publishing a
GitHub Release triggers `.github/workflows/release.yml` and pushes images.

## Do not do

- Commit, tag, or push unless asked
- Edit application code, tests, or CI as part of this skill
- Change anything in `Directory.Build.props` except `Version`, or anything in
  the client `package.json`/`package-lock.json` except `version`
- Summarize every commit
- Use unprefixed headings like `[0.3.0]`

## Quick check

- [ ] Version matches the confirmed tag (`vX.Y.Z`)
- [ ] `Directory.Build.props` `Version` is the confirmed tag without the `v`
- [ ] Client `package.json` and `package-lock.json` `version` match it
- [ ] Date is real
- [ ] Unreleased is empty of shipped items
- [ ] Empty changelog subsections are omitted
- [ ] A release with no user-facing changes uses the standard sentence
- [ ] Wording is operator-facing
- [ ] README Features and limitations match this release
- [ ] GitHub notes can be copied from the new changelog section
