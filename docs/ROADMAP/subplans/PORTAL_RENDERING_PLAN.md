# Portal Rendering Plan

## Purpose
Define how portal visuals and transitions are introduced while keeping topology and state changes legible.

**Plain language summary:** Show **portals** and **walking through them** so players understand where they went and whether links are stable or **unstable**, without VFX that lie about valid paths. “Done” means baseline portal look, traversal transitions that preserve sense of direction, debug views teams can use to verify the **portal graph**, and validation on the portal and instability levels.

## Scope
- Portal visual framing
- Traversal transition behavior
- Instability-state visual signaling

## Deliverables
- Portal visual baseline and transition pattern.
- State indicators for static vs unstable connectivity contexts.
- Debug views for portal graph and active mapping.

## Phased approach
1. Implement baseline portal presentation.
2. Add traversal transitions and orientation aids.
3. Integrate instability-state visual signaling.
4. Validate across Level 2 and Level 3 scenarios.

## Acceptance criteria
- Players can infer portal destination/state with minimal ambiguity.
- Transitions preserve user orientation and puzzle readability.
- Visual behavior never contradicts logical topology state.

## Risks and fallback
- Risk: portal effects imply false connectivity.
  - Fallback: stronger explicit state indicators and reduced effects.
- Risk: transition complexity harms clarity.
  - Fallback: simplify transition stack to readability-first mode.

## Change log
- 2026-04-15: Initial high-level subplan created.
- 2026-04-15: Added plain-language summary for newcomers.
