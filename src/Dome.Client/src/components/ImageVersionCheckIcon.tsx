import { ArrowUpCircle } from 'lucide-react'

import {
  imageVersionCheckDescription,
  imageVersionCheckLabel,
} from '@/lib/imageVersionChecks'
import type { ImageVersionKind } from '@/types/imageVersionChecks'
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from './ui/tooltip'

type ImageVersionCheckIconProps = {
  kind: ImageVersionKind
  currentTag: string | null
  newestTag: string | null
}

export function ImageVersionCheckIcon({
  kind,
  currentTag,
  newestTag,
}: Readonly<ImageVersionCheckIconProps>) {
  const label = imageVersionCheckLabel(kind)
  const description = imageVersionCheckDescription({ kind, currentTag, newestTag })

  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger asChild>
          <span
            className={`image-version-check-icon image-version-check-icon-${kind}`}
            aria-label={description}
          >
            <ArrowUpCircle className="size-3.5" aria-hidden="true" />
            <span className="sr-only">{label}</span>
          </span>
        </TooltipTrigger>
        <TooltipContent side="top">{description}</TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
