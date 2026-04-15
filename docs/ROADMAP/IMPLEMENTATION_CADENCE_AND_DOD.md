# Implementation Cadence and Definition of Done

## Purpose
Define when each documentation layer is updated and what must be true before a phase or work item is considered complete.

**Plain language:** This doc answers “**how often** do we update which write-ups?” and “**when can we honestly call a chunk of work finished?**” **Definition of Done** (DoD) is simply the agreed checklist for completion—documentation folders in place, links sane, phase evidence captured—not a mysterious gatekeeping term. **Cadence** means rhythm: per task, weekly-ish for active checklists, and extra updates when a phase closes or architecture shifts.

## Update cadence

### Per task
- Update impacted docs in the same task when behavior or constraints change.
- Record notable decisions in `docs/EXECUTION/DECISION_LOG.md`.

### Weekly (or equivalent sprint rhythm)
- Refresh the **active** graphics phase execution checklist for the current roadmap phase (filename pattern `docs/EXECUTION/GRAPHICS_PHASE_<LETTER>_EXECUTION.md`; phase letter is authoritative in [GRAPHICS_ROADMAP.md](GRAPHICS_ROADMAP.md)—e.g. Phase D while wrapping/spatial cues own the gate).
- Reconcile completed work against that phase’s acceptance checks.

### Per phase boundary
- Update `docs/ROADMAP/GRAPHICS_ROADMAP.md` phase status and gate results.
- Promote durable decisions to ADRs if they change long-term direction.

### On architecture changes
- Update `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md`.
- Re-link affected subplans and execution docs.

## Definition of done (documentation system)
Documentation setup work is done when all are true:
1. Layered directories and baseline docs exist.
2. Vision and architecture docs are linked and internally consistent.
3. Roadmap and high-level subplans exist for all major graphics streams.
4. Execution and decision-log docs exist for immediate phase work.
5. Initial ADR baseline exists for principles, scope, and AI policy.
6. Cursor rules exist and map to documented constraints.

## Definition of done (per graphics phase)
A graphics phase is done when all are true:
1. Phase deliverables are completed and validated.
2. Phase gate criteria in roadmap are satisfied.
3. Fallback behavior for introduced effects is documented.
4. Docs impacted by the phase are updated.
5. Decisions are logged; ADRs updated if durable assumptions changed.

## Escalation protocol
- If implementation and docs diverge, pause new feature work in that stream.
- Reconcile via architecture update or ADR before proceeding.

## Change log
- 2026-04-15: Initial cadence and DoD baseline created.
- 2026-04-15: Added plain-language explanation of cadence and Definition of Done for newcomers.
- 2026-04-15: Updated weekly cadence reference to alphabetical phase execution filename (`GRAPHICS_PHASE_B_EXECUTION.md`).
- 2026-04-15: Generalized weekly cadence to the **active** `GRAPHICS_PHASE_*_EXECUTION.md` per roadmap (avoids stale Phase B-only guidance after later phases close).
