# Visual Style System Plan

## Purpose
Establish a geometry-first visual language that is readable, scalable, and not dependent on bespoke art production.

**Plain language summary:** Decide what the game **looks like at a glance**—simple shapes, shared colors, motion cues—so players can read factory state without custom paintings or textures. “Done” means we have agreed primitives and palette rules plus **objective** readability checks (`VSS-AC##`; see [READING_GUIDE.md](../../READING_GUIDE.md)) evidenced in execution docs.

## Scope
- Shape vocabulary
- Palette system
- Material and line conventions
- Motion cues for gameplay state

## Planning horizon
- Phase B (Visual style baseline): **complete** (objective gate closed; evidence in `docs/EXECUTION/GRAPHICS_PHASE_B_EVIDENCE.md`).
- Phase C (Edge rendering): **complete** (see `docs/EXECUTION/GRAPHICS_PHASE_C_EVIDENCE.md` and roadmap snapshot).
- Later phases: remain high-level in [GRAPHICS_ROADMAP.md](../GRAPHICS_ROADMAP.md) until within one phase of execution.

## Status
- Phase B objective acceptance (`VSS-AC01`–`VSS-AC08`) is **closed** with evidence; post-fix revalidation for `VSS-AC01` / `VSS-AC03` / `VSS-AC04` is **complete** per Phase B execution/evidence and decision log.
- Active graphics checklist cadence follows the **current** roadmap phase execution doc (see [GRAPHICS_ROADMAP.md](../GRAPHICS_ROADMAP.md)), not this subplan’s day-to-day tasks.

## Deliverables
- Baseline style guide for geometry, color, and motion.
- Minimal palette sets for MVP biome/face variation.
- Readability checklist for gameplay-critical entities.
 - Validation scene set and evidence capture format.

## Phase B implementation slices
1. Geometry vocabulary
   - Define approved primitive families and silhouette constraints for gameplay entities.
   - Publish naming and reuse rules for consistency across MVP levels.
2. Palette and contrast baseline
   - Define base/interactive/warning/unstable color roles.
   - Set contrast floor targets for gameplay-significant states.
3. Material and line conventions
   - Define default material behavior and boundary line treatment.
   - Reserve high-emphasis styling for state-significant entities only.
4. State cue mappings
   - Map `active`, `blocked`, `flowing`, `idle`, and `unstable` to deterministic visual cues.
   - Ensure state transitions are legible during orientation changes.
5. Validation and gate evidence
   - Validate in representative MVP contexts.
   - Record pass/fail evidence in execution and decision log docs.

## Phased approach
1. Define primitive and silhouette vocabulary.
2. Define palette and contrast rules.
3. Define state-driven animation cues.
4. Validate in representative MVP scenes.

## Acceptance criteria
- Critical gameplay objects are identifiable at a glance.
- Visual language is consistent across MVP levels.
- No required dependency on custom hand-authored art assets.

## Objective acceptance checklist (Phase B)
- `VSS-AC01`: Core entity classes remain distinguishable at rest in each validation scene.
- `VSS-AC02`: Core entity classes remain distinguishable while moving/animating.
- `VSS-AC03`: `active`, `blocked`, and `unstable` states are distinguishable without UI text.
- `VSS-AC04`: Orientation/rotation moments preserve state readability and spatial context.
- `VSS-AC05`: Palette role usage stays within defined token set (no ad hoc colors for gameplay state).
- `VSS-AC06`: Contrast floors for gameplay-significant foreground/background pairs are met.
- `VSS-AC07`: Style consistency holds across all MVP level contexts.
- `VSS-AC08`: Visual baseline is achieved without bespoke art pipeline dependency.

## Evidence requirements
- Validation scene list with date and evaluator.
- Before/after captures for each failed-then-fixed criterion.
- Final acceptance table keyed by `VSS-AC01` to `VSS-AC08`.

## Phase C prerequisite handoff (confirmed)
- Scene/runtime composition is in place (`Main`, `ShapeView`, `UIRoot`) for edge-pass integration.
- Palette token mapping and state cue surfaces are centralized in `ShapeView`.
- Portal and instability visual anchors are present for contour/depth enhancement in Phase C.
- Baseline fallback remains available through primitive-only rendering without bespoke assets.

## Risks and fallback
- Risk: style inconsistency across levels.
  - Fallback: lock a stricter style token set.
- Risk: low contrast in busy scenes.
  - Fallback: enforce contrast floor and simplify background forms.

## Change log
- 2026-04-15: Initial high-level subplan created.
- 2026-04-15: Added Phase B implementation slices, objective acceptance checklist, and evidence requirements.
- 2026-04-15: Marked Phase B baseline complete and recorded confirmed Phase C prerequisite handoff.
- 2026-04-15: Updated Phase B status after code-review bugfixes to require manual revalidation closeout before Phase C.
- 2026-04-15: Added plain-language summary for newcomers; linked reading guide for `VSS-AC##` decode.
- 2026-04-15: Updated acceptance evidence path to alphabetical phase filename (`GRAPHICS_PHASE_B_EVIDENCE.md`).
- 2026-04-15: Reconciled **Planning horizon** and **Status** with roadmap after Phase B/C closeout (removed stale “hold Phase C” language).
