# ADR-002: Graphics Pipeline Scope for MVP

## Summary for readers
Graphics ships in **layers** (style baseline, then edges, wrapping cues, portals, then performance tuning) instead of jumping straight to advanced rendering. Each layer is supposed to finish with checks before the next starts. That keeps MVP scope under control. Details and consequences are in the sections below.

## Status
Accepted

## Date
2026-04-15

## Context
MVP scope is constrained to three levels and must ship with clear gameplay communication and stable performance on initial target platforms.

## Decision
Use a phased graphics pipeline rollout for MVP:
1. Visual style baseline
2. Edge rendering
3. Wrapping/spatial cues
4. Portal rendering
5. Performance scaling and profile lock

Advanced or high-risk polish remains optional unless it passes readability, determinism, and performance gates.

## Consequences
- Limits early over-investment in advanced rendering before core readability is proven.
- Requires phase-gated acceptance criteria and fallback paths.
- Encourages stable documentation updates as each phase lands.

## Related docs
- `docs/ROADMAP/GRAPHICS_ROADMAP.md`
- `docs/ROADMAP/subplans/*`
- `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md`

## Notes
If pipeline sequencing changes materially, create a superseding ADR.

## Change log
- 2026-04-15: Added non-normative “Summary for readers” for documentation onboarding.
