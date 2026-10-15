import type { ImageVersionKind } from '@/types/imageVersionChecks'

const RANK: Record<ImageVersionKind, number> = {
  none: 0,
  unknown: 0,
  digest: 1,
  patch: 2,
  minor: 3,
  major: 4,
}

export function isVisibleImageVersionCheck(
  kind: ImageVersionKind | undefined,
): kind is 'digest' | 'patch' | 'minor' | 'major' {
  return kind === 'digest' || kind === 'patch' || kind === 'minor' || kind === 'major'
}

export function worseImageVersionKind(
  current: ImageVersionKind | undefined,
  next: ImageVersionKind | undefined,
): ImageVersionKind | undefined {
  if (!isVisibleImageVersionCheck(next)) {
    return current
  }

  if (!isVisibleImageVersionCheck(current) || RANK[next] > RANK[current]) {
    return next
  }

  return current
}

export function imageVersionCheckLabel(kind: ImageVersionKind): string {
  switch (kind) {
    case 'major':
      return 'Major update'
    case 'minor':
      return 'Minor update'
    case 'patch':
      return 'Patch update'
    case 'digest':
      return 'Image update'
    default:
      return 'Image status'
  }
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
