# ADR-003: AI-Assisted Implementation Policy

## Summary for readers
AI tools are allowed as **accelerators**, not as unreviewed truth—especially for shaders and graphics. Any change that affects behavior must update the same documentation task, and big shifts need an ADR. The goal is speed **without** silent drift from the agreed design.

## Status
Accepted

## Date
2026-04-15

## Context
AI assistance is a core workflow accelerator for this project. Without constraints, it can introduce architectural drift and unvalidated rendering behavior.

## Decision
Adopt a strict AI-assisted workflow policy:
- AI proposals must include constraints and acceptance criteria.
- No blind copy/paste of generated rendering/shader code.
- Behavior-changing graphics work must update source-of-truth docs in the same task.
- Durable deviations from documented intent require ADR updates.

## Consequences
- Slightly higher documentation overhead per change.
- Better long-term consistency and reproducibility.
- Lower risk of hidden coupling and unreadable visual outcomes.

## Related docs
- `docs/VISION/AI_ASSISTED_GRAPHICS_PRINCIPLES.md`
- `docs/ARCHITECTURE/DOCS_SYSTEM.md`
- `.cursor/rules/*.mdc`

## Notes
This policy applies to all future graphics implementation phases unless superseded by ADR.

## Change log
- 2026-04-15: Added non-normative “Summary for readers” for documentation onboarding.
