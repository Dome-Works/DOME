import type { ImageVersionKind } from '@/types/imageVersionChecks'

export function isVisibleImageVersionCheck(
  kind: ImageVersionKind | undefined,
): kind is 'digest' | 'patch' | 'minor' | 'major' {
  return kind === 'digest' || kind === 'patch' || kind === 'minor' || kind === 'major'
}

export function imageVersionCheckLabel(kind: ImageVersionKind): string {
  return isVisibleImageVersionCheck(kind) ? 'Image update' : 'Image status'
}

export function imageVersionCheckDescription(check: {
  kind: ImageVersionKind
  currentTag: string | null
  newestTag: string | null
}): string {
  const label = imageVersionCheckLabel(check.kind)
  if (check.kind === 'digest') {
    return `${label} available for ${check.currentTag ?? 'this tag'}.`
  }

  if (check.newestTag && check.currentTag && check.newestTag !== check.currentTag) {
    return `${label} available: ${check.newestTag} (running ${check.currentTag}).`
  }

  if (check.newestTag) {
    return `${label} available: ${check.newestTag}.`
  }

  return `${label} available.`
}
