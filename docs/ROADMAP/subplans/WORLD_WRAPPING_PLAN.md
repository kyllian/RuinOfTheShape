# World Wrapping Plan

## Purpose
Plan visual and technical strategy for repeated/wrapped spatial presentation while preserving player orientation.

**Plain language summary:** Some levels may **repeat space** visually (the world seems to wrap). This plan keeps players from getting lost—**which way is forward**, where they came from—when that happens. “Done” means a prototype approach, a small set of **orientation cues**, and checks that wrapping still matches the **true topology** rules and plays nicely with future portal visuals.

## Scope
- Wrapping presentation model
- Orientation cues during wrapped traversal
- Performance implications of repeated geometry presentation

## Deliverables
- Wrapping prototype approach document. (delivered via Phase D execution model notes)
- Orientation and continuity cue set. (delivered baseline: ghost repeats, geometric directional/return chevrons, continuity separators)
- Validation checklist for traversal comprehension. (delivered via `WSC-AC01` to `WSC-AC06` in Phase D execution/evidence docs)

## Phased approach
1. Prototype simple wrapping presentation.
2. Add orientation cues and continuity markers.
3. Stress-test readability with representative level layouts.
4. Prepare optimization options for later phases.

## Acceptance criteria
- Wrapped traversal does not disorient players beyond intended puzzle challenge.
- Visual cues align with logical topology rules.
- Approach remains compatible with planned portal rendering.

## Risks and fallback
- Risk: player loses directional context.
  - Fallback: strengthen framing cues and transitions.
- Risk: rendering overhead grows too fast.
  - Fallback: constrain visible repeat depth for MVP.

## Change log
- 2026-04-15: Initial high-level subplan created.
- 2026-04-15: Added plain-language summary for newcomers.
- 2026-04-15: Reconciled subplan with Phase D baseline delivery artifacts and evidence IDs (`WSC-AC01` to `WSC-AC06`).
- 2026-04-15: Updated Phase D baseline wording to require non-text orientation continuity cues (geometric chevrons/separators) and align with live profile iteration.
