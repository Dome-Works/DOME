export async function readApiError(
  response: Response,
  fallback: string,
): Promise<string> {
  try {
    const body = (await response.json()) as {
      message?: string
      errors?: Record<string, string[] | string>
    }

    if (body.errors) {
      const messages = Object.values(body.errors).flatMap((value) =>
        Array.isArray(value) ? value : [value],
      )

      if (messages.length > 0) {
        return messages.join(' ')
      }
    }

    if (body.message) {
      return body.message
    }
  } catch {
    // Response was not JSON.
  }

  return fallback
}
