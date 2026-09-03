const BYTE_UNITS = ['B', 'KB', 'MB', 'GB', 'TB', 'PB'] as const

export function formatBytes(bytes: number | null | undefined): string {
  if (bytes === null || bytes === undefined || !Number.isFinite(bytes) || bytes < 0) {
    return '—'
  }

  let size = bytes
  let unitIndex = 0

  while (size >= 1024 && unitIndex < BYTE_UNITS.length - 1) {
    size /= 1024
    unitIndex += 1
  }

  const fractionDigits = size >= 10 || unitIndex === 0 ? 0 : 1
  return `${size.toFixed(fractionDigits)} ${BYTE_UNITS[unitIndex]}`
}
