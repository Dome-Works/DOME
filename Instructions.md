# Agent Instructions

- Keep one type per file. Every `class`, `interface`, `record`, `enum`, and similar type must live in its own file.
- Do not use nested classes or nested records.
- Endpoints must use ViewModels to communicate outward.
- Services must communicate using DTOs.
- Prefer clear, explicit naming for transport shapes and service shapes so the layer boundary stays obvious.
