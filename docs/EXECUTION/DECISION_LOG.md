# Decision Log

## Purpose
Maintain a lightweight chronological record of notable implementation decisions and link them to ADRs when decisions become durable.

## Entry format
- Date:
- Context:
- Decision:
- Rationale:
- Impact:
- Follow-up:
- ADR link (if created):

## Entries

### 2026-04-15
- Context: Documentation audit found PRD/TECH wording drift (“win/lose”, “win/fail”) vs locked MVP **no failure mode**; PRD instability framing broader than GDD **Level 3** traversal/rotation trigger; graphics cadence and subplan status stale vs roadmap.
- Decision: Apply wording reconciliation in PRD, TECH, cadence, visual-style subplan, and Phase B execution header; record amendment in Phase D execution + graphics roadmap change logs.
- Rationale: Lower-precedence docs must match authoritative product/design intent (GDD Level 3, PRD §12.3) without changing runtime contracts.
- Impact: Readers see consistent MVP loop and Level 3 trigger; weekly cadence points at the active phase execution doc.
- Follow-up: None for code; if future levels add failure modes, update PRD §12 and TECH §6 together with an ADR if assumptions lock.
- ADR link: none (clarification only).

### 2026-04-15
- Context: Phase C edge rendering gate required a screen-space pass with tiered fallback without mutating gameplay/topology state.
- Decision: Ship a canvas-layer fullscreen Sobel post-process (`EdgePostProcessLayer` + `EdgePresentationSettings`) with `Tier0` pass-through fallback and debug-only tier cycling on `E`.
- Rationale: Godot canvas post shaders expose `hint_screen_texture` reliably across Forward+ targets; depth/normal compositor work is deferred to avoid blocking MVP readability gains.
- Impact: Main scene composes world → edge overlay → UI (`UIRoot` elevated to canvas layer 10); parameters are data-driven via `res://resources/EdgePresentationSettings_Default.tres`.
- Follow-up: If same-color coplanar silhouettes or label outline noise block acceptance in future levels, prototype compositor/depth sampling or move labels off the post-processed color buffer.
- ADR link: none (non-durable pipeline hook choice documented in architecture + subplan reconciliation).

### 2026-04-15
- Context: Phase B objective gate required `VSS-AC01`, `VSS-AC03`, and `VSS-AC04` revalidation after camera framing and topology-sync fixes.
- Decision: Close Phase B and authorize Phase C edge rendering work to proceed under the Phase C execution checklist.
- Rationale: Framing and topology drift risks were remediated; remaining evidence gaps are optional screenshot polish rather than blocking readability unknowns.
- Impact: Roadmap snapshot now treats Phase B as complete and Phase C as in flight; Phase C entry criteria in `GRAPHICS_PHASE_C_EXECUTION.md` are unblocked.
- Follow-up: Capture optional annotated screenshots for archive/marketing and keep them linked from `GRAPHICS_PHASE_B_EVIDENCE.md` if produced.
- ADR link: none.

### 2026-04-15
- Context: Phase B graphics baseline implementation for geometry-first readability and state cues.
- Decision: Implement a presentation-only `ShapeView` that renders level snapshots (`ActiveFaceIds`, `Portals`, `InstabilityTrigger`) with tokenized state materials and no gameplay-state mutation.
- Rationale: Preserve deterministic topology/simulation authority while delivering immediate readability validation for Phase B.
- Impact: Main scene now composes `ShapeView` and `UIRoot`; validation levels can be cycled in runtime for Scene A/B/C checks.
- Follow-up: Add in-editor annotated captures to `docs/EXECUTION/GRAPHICS_PHASE_B_EVIDENCE.md` during manual QA pass.
- ADR link: none (no durable architecture change beyond existing graphics pipeline constraints).

### 2026-04-15
- Context: Functional test found default camera framing too distant/angled, making label readability insufficient for acceptance walkthroughs.
- Decision: Add automatic camera framing to rendered face bounds and increase label readability settings in `ShapeView`.
- Rationale: Keep default validation run aligned with Phase B readability intent without requiring manual camera edits.
- Impact: Scene A/B/C should now launch with legible geometry/labels for baseline checks.
- Follow-up: Re-run manual acceptance capture pass and confirm `VSS-AC01`, `VSS-AC03`, and `VSS-AC04` remain passable.
- ADR link: none (implementation tuning under existing architecture constraints).

### 2026-04-15
- Context: Code review found validation harness drift risk where visual scene cycling did not update topology resolver state.
- Decision: Re-initialize topology resolver on each level switch before `ShapeView` render and upgrade camera framing to FOV/aspect-aware fit logic.
- Rationale: Keep validation visuals aligned with authoritative runtime context and reduce camera-dependent readability failures.
- Impact: Validation scene switch path now keeps topology and presentation context synchronized; framing behavior scales more reliably across viewport sizes.
- Follow-up: Complete manual capture revalidation and update acceptance statuses in evidence/execution docs.
- ADR link: none (bugfix within existing architecture constraints).

### 2026-04-15
- Context: Documentation system initialization for graphics initiative.
- Decision: Adopt layered documentation architecture (`VISION`, `ARCHITECTURE`, `ROADMAP`, `EXECUTION`, `ADR`) plus Cursor rules.
- Rationale: Separate stable intent from volatile execution detail and reduce AI/implementation drift.
- Impact: New canonical docs and rule scaffolding are required before feature implementation.
- Follow-up: Keep phase details in execution docs; keep architecture concise.
- ADR link: `docs/ADR/ADR-002-graphics-pipeline-scope-mvp.md` (planned baseline scope alignment)

## Change log
- 2026-04-15: Decision log initialized.
- 2026-04-15: Added Phase B implementation decision entry for presentation boundary and validation flow.
- 2026-04-15: Added readability remediation decision entry for default validation camera framing.
- 2026-04-15: Added topology sync + framing robustness bugfix decision entry from code review remediation.
- 2026-04-15: Updated execution/evidence document references to alphabetical phase filenames (Phase B/Phase C).
