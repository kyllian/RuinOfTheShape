# Graphics Phase C Execution

## Purpose
Track immediate implementation work for **Phase C — edge rendering** with concrete checks, artifact visibility, and blocker notes.

## Phase alignment
- Current roadmap phase: **Phase C — Edge rendering** (complete as of 2026-04-15; evidence in [GRAPHICS_PHASE_C_EVIDENCE.md](GRAPHICS_PHASE_C_EVIDENCE.md)).
- Prior phase dependency: **Phase B — Visual style baseline** (`VSS-AC01`-`VSS-AC08` gate per [GRAPHICS_PHASE_B_EXECUTION.md](GRAPHICS_PHASE_B_EXECUTION.md)).
- Next roadmap phase: **Phase D — Wrapping and spatial cues** (prerequisites confirmed at exit; planning detail in [WORLD_WRAPPING_PLAN.md](../ROADMAP/subplans/WORLD_WRAPPING_PLAN.md)).

## Entry criteria (before treating Phase C as "in flight")
- Phase B objective gate is closed with evidence in [GRAPHICS_PHASE_B_EVIDENCE.md](GRAPHICS_PHASE_B_EVIDENCE.md) and any open remediation items are either resolved or explicitly waived with owner + date.
- Baseline pipeline wiring from Phase B remains valid: base geometry pass, palette token surface, portal/instability cue anchors available for contour augmentation (see Phase C prerequisites in Phase B execution doc).

## Phase scope (execution)
- Implement the **edge and line pass** per [ARCHITECTURE/GRAPHICS_PIPELINE.md](../ARCHITECTURE/GRAPHICS_PIPELINE.md) (downstream of simulation/topology; readability-first; tiered fallback).
- Integrate tuning, debug visibility, and an **artifact test matrix** aligned with [ROADMAP/subplans/EDGE_RENDERING_PLAN.md](../ROADMAP/subplans/EDGE_RENDERING_PLAN.md).
- Validate in the same representative MVP contexts used for Phase B (single-face, portals, instability).
- Capture only the prerequisites needed to start **Phase D** without redoing edge/composite contracts.

## Active checklist
- [x] Integrate initial edge pass after base geometry (pipeline order: base -> edge -> composite path as architecture allows).
- [x] Centralize edge parameters (threshold, intensity, width or equivalent) for presentation-layer tuning.
- [x] Add developer-facing controls or debug views for edge parameters (minimum: documented inspector/project settings path if no runtime HUD).
- [x] Define **Tier 0** (maximum readability / fallback) vs enhanced edge mode; verify clean degradation path.
- [x] Execute artifact matrix: flicker/temporal noise, stair-stepping/aliasing, over-outlining/occluding state cues; record mitigations.
- [x] Validate Scene A/B/C (same level resources as Phase B evidence doc) for legibility and stability during level swap and motion.
- [x] Document Phase D prerequisites and link forward to roadmap/subplan when confirmed.

## Acceptance checklist (objective)

**`ERS`** = **edge rendering system**; **`AC`** = **acceptance check**. IDs are local to this execution doc until promoted to the reading guide with a stable table.

- [x] `ERS-AC01` (form legibility): Silhouette and corner/depth reading are **improved or neutral** versus base-only in Scene A/B/C (no regression on "which face is which" at rest).
- [x] `ERS-AC02` (state not obscured): `active`, `blocked`, `flowing`, `idle`, `unstable`, and portal-related cues remain readable; edges do not replace or contradict topology state.
- [x] `ERS-AC03` (temporal stability): No unacceptable shimmer/flicker during motion, instability pulses, or `Space` scene transitions at default quality settings.
- [x] `ERS-AC04` (fallback readability): With enhanced edges disabled (Tier 0 / fallback), gameplay readability matches or exceeds pre-Phase C baseline; no dependency on bespoke art.
- [x] `ERS-AC05` (tunability): Edge behavior can be adjusted without code changes for routine iteration (parameters exposed per centralized presentation convention).
- [x] `ERS-AC06` (artifact matrix): Documented pass/fail (or "pass with mitigation") for flicker, stair-stepping, and over-outlining rows, tied to captures or measurable notes.

## Required acceptance conditions
- Checklist `ERS-AC01` through `ERS-AC06` is fully evidenced.
- Any failed criterion has an explicit remediation item, owner, and re-test note.
- Quality tier and fallback behavior are described beside performance expectations (initial posture: correctness/readability over polish; see pipeline doc).

## Validation scenes (required)
- Scene A: single-face baseline (`res://levels/Level_01_OneSided.tres`).
- Scene B: static portals (`res://levels/Level_02_Portals.tres`).
- Scene C: instability/state transitions (`res://levels/Level_03_Instability.tres`).
- Runtime control path should match Phase B: startup scene + `Space` cycle unless superseded by a documented change in this doc's change log.

## Evidence pack (required at gate)
- Manual validation record for Scene A/B/C with default quality and Tier 0 fallback comparisons (date + evaluator + runtime path used).
- Pass/fail table keyed by `ERS-AC01` to `ERS-AC06`.
- Artifact matrix appendix (rows: flicker, stair-stepping, over-outlining) with mitigation notes.
- Decision log entry for sign-off or deferred items.
- Phase C evidence document: `docs/EXECUTION/GRAPHICS_PHASE_C_EVIDENCE.md` (create at gate; keep in sync with this execution doc).
- Optional supporting artifacts: annotated captures for Scene A/B/C (default and Tier 0) when needed for archive, onboarding, or external review.

## Implementation boundary notes
- Graphics consumes immutable presentation snapshots at frame boundaries; **no pass mutates gameplay or topology** ([GRAPHICS_PIPELINE.md](../ARCHITECTURE/GRAPHICS_PIPELINE.md)).
- Edge pass must preserve gameplay comprehension: avoid effects that imply false adjacency or hide routing/portal/instability state ([GRAPHICS_ROADMAP.md](../ROADMAP/GRAPHICS_ROADMAP.md) risk register).
- Prefer geometry/shader-driven edges over bespoke mesh authoring; keep debug toggles available in development builds (pipeline debug posture).

## Phase D prerequisites (to confirm at exit)
- Edge pass output composes predictably with base and later passes (no ad hoc ordering hacks).
- Fallback path is verified and documented alongside default quality.
- Orientation and face-readability remain stable enough to layer **wrapping/spatial cues** without fighting edge noise (high-level intent only; detail stays in [WORLD_WRAPPING_PLAN.md](../ROADMAP/subplans/WORLD_WRAPPING_PLAN.md)).

**Exit confirmation (2026-04-15):** Edge overlay is a dedicated `CanvasLayer` with predictable ordering (`ShapeView` world -> `EdgePostProcessLayer` layer 0 -> `UIRoot` layer 10). Tier0 fallback and default tunables are documented in [GRAPHICS_PHASE_C_EVIDENCE.md](GRAPHICS_PHASE_C_EVIDENCE.md). Phase D planning continues in [WORLD_WRAPPING_PLAN.md](../ROADMAP/subplans/WORLD_WRAPPING_PLAN.md).

## Logging format
Use entries in this format:
- Date:
- Work item:
- What changed:
- Validation evidence:
- Follow-up action:

## Blocker template
- Blocker:
- Impact:
- Attempted mitigation:
- Decision needed:
- Owner:

## Exit criteria
- Phase C acceptance gate passes with complete evidence file.
- Roadmap phase status snapshot updated for Phase C completion and Phase D readiness.
- Subplan [EDGE_RENDERING_PLAN.md](../ROADMAP/subplans/EDGE_RENDERING_PLAN.md) reconciled with delivered behavior (deliverables vs deferrals).
- Any durable rendering-order or fallback policy changes recorded in ADR or decision log if assumptions shifted.

## Change log
- 2026-04-15: Initial Phase C execution scaffold created.
- 2026-04-15: Phase C delivery recorded — fullscreen canvas edge pass (`EdgePostProcessLayer` + `EdgePresentationSettings`), tiered fallback, debug tier cycling (`E` in debug builds), evidence in [GRAPHICS_PHASE_C_EVIDENCE.md](GRAPHICS_PHASE_C_EVIDENCE.md).
- 2026-04-15: Reconciled gate evidence language with accepted manual validation workflow; captures now explicitly optional supporting artifacts.

## Developer controls (Phase C)
- **Inspector:** Select `Main` -> child `EdgePostProcessLayer` -> `Settings` resource (`res://resources/EdgePresentationSettings_Default.tres` by default). Adjust `quality_tier`, thresholds, intensity, tint, and neighbor soften without recompiling.
- **Debug runtime:** Press **`E`** (debug builds only) to cycle `Tier0` -> `Tier1` -> `Tier2` for quick comparisons against `ERS-AC04`.
