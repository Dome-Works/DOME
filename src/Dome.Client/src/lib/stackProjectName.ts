export const STACK_PROJECT_NAME_MAX_LENGTH = 128

const PROJECT_NAME_PATTERN = /^[a-z0-9][a-z0-9_-]*$/

export function validateStackProjectName(projectName: string): string | null {
  const trimmed = projectName.trim()
  if (trimmed.length === 0) {
    return 'Project name is required.'
  }

  if (trimmed.length > STACK_PROJECT_NAME_MAX_LENGTH) {
    return 'Project name must be 128 characters or fewer.'
  }

  if (!PROJECT_NAME_PATTERN.test(trimmed)) {
    return 'Use a Compose project name: lowercase letters, digits, hyphens, and underscores, starting with a letter or digit.'
  }

  return null
}
