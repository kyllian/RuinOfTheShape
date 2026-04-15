# ADR-001: Graphics Visual Principles

## Summary for readers
This decision record locks **how bold the game is allowed to look** versus **how readable the puzzle must stay**. We chose readability, simple geometry, and code-driven visuals over custom art and flashy effects. Anything that makes state harder to read needs review. The formal decision and consequences are below; this summary is for navigation only.

## Status
Accepted

## Date
2026-04-15

## Context
The project requires a graphics direction that supports readability and deterministic puzzle play while minimizing dependence on bespoke art production.

## Decision
Adopt the following visual principles as durable project guidance:
- Readability over spectacle
- Geometry-first style
- Minimal custom art dependency
- Deterministic visual semantics
- Explicit topology clarity cues

## Consequences
- Graphics work prioritizes programmatic geometry/palette/shader solutions.
- Any feature that lowers readability requires explicit review and mitigation.
- Documentation and rules must reinforce these principles.

## Related docs
- `docs/VISION/GRAPHICS_VISION.md`
- `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md`

## Notes
This ADR may be superseded only by a new ADR that explains tradeoffs and updated constraints.

## Change log
- 2026-04-15: Added non-normative “Summary for readers” for documentation onboarding.
