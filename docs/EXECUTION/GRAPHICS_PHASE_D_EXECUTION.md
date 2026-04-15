# Graphics Phase D Execution

## Purpose
Track immediate implementation work for **Phase D - wrapping and spatial cues** with concrete checks, blocker visibility, and evidence expectations.

## Scope / non-scope
### Scope
- Implement presentation-layer world wrapping cues that preserve orientation during repeated traversal.
- Define and validate continuity markers that help players map repeated space without contradicting true topology.
- Establish objective acceptance checks and an evidence pack for the Phase D gate.

### Non-scope
- Full portal rendering transitions and effects (Phase E).
- Broad performance tier finalization beyond immediate safety checks (Phase F).
- Changes to gameplay topology rules or simulation authority.

## Inputs / assumptions
- Roadmap phase status and gate intent from [GRAPHICS_ROADMAP.md](../ROADMAP/GRAPHICS_ROADMAP.md).
- Stream-level Phase D intent from [WORLD_WRAPPING_PLAN.md](../ROADMAP/subplans/WORLD_WRAPPING_PLAN.md).
- Phase C closeout and entry prerequisites from [GRAPHICS_PHASE_C_EXECUTION.md](GRAPHICS_PHASE_C_EXECUTION.md) and [GRAPHICS_PHASE_C_EVIDENCE.md](GRAPHICS_PHASE_C_EVIDENCE.md).
- Graphics pipeline constraints remain active: readability first, deterministic communication, and no presentation pass mutates gameplay/topology state.

## Plan or design
## Phase alignment
- Current roadmap phase: **Phase D - Wrapping and spatial cues** (execution scaffold created; objective gate pending).
- Prior phase dependency: **Phase C - Edge rendering** (gate closed with `ERS-AC01` to `ERS-AC06` evidence).
- Next roadmap phase: **Phase E - Portal rendering** (prerequisites only; no Phase E implementation in this doc).

## Entry criteria (before treating Phase D as "in flight")
- Phase C objective gate is closed with evidence and no unresolved blocker that invalidates edge/composite ordering assumptions.
- Validation runtime path for Scene A/B/C remains stable and documented (or superseding path recorded in this doc's change log).
- World wrapping constraints in subplan remain architecture-aligned (orientation cues augment readability, not gameplay rules).

## Active checklist
- [x] Select and document the initial wrapping presentation model (repeat depth, anchor behavior, and continuity cue placement).
- [x] Implement orientation cues for repeated traversal (minimum: directional continuity markers and return-path readability cue).
- [x] Validate cues against topology truth (no false adjacency implication; no contradiction of portal/instability state).
- [x] Add developer-facing tuning controls for wrapping cue intensity/density or equivalent parameters.
- [x] Execute traversal comprehension walkthroughs on representative layouts (single-face, portal-bearing, instability-bearing contexts).
- [x] Record required mitigations and residual risks for disorientation cases.
- [x] Document Phase E prerequisites and handoff notes once wrapping behavior is stable.

## Initial wrapping presentation model (implemented)
- Repeat depth: default depth `1` in balanced mode; emphasized mode promotes to depth `2`; disabled mode forces depth `0`.
- Anchor behavior: directional markers at left/right bounds (`<- return`, `forward ->`) establish stable traversal orientation.
- Continuity cue placement: low-alpha repeated face clones are offset by one wrap stride with geometric chevron anchors and continuity separators marking wrap boundaries.
- Topology truth guardrail: repeated geometry uses ghost treatment and explicit labels so cues read as continuity context rather than new adjacency.
- Tunability path: `res://resources/WrappingPresentationSettings_Default.tres` exposes profile, repeat depth, ghost alpha, continuity intensity, and marker height without recompiles.

## Acceptance checklist (objective)
`WSC` = **wrapping and spatial cues**; `AC` = **acceptance check**. IDs are local to this execution doc until promoted to the reading guide.

- [ ] `WSC-AC01` (orientation retention): Traversal through repeated/wrapped presentation preserves player orientation in required validation scenes.
- [ ] `WSC-AC02` (topology truth): Wrapping cues do not imply false adjacency or contradict active topology/portal state.
- [ ] `WSC-AC03` (continuity readability): Transition between repeated segments is visually legible using non-text geometric/palette cues only.
- [ ] `WSC-AC04` (state compatibility): Existing state cues (`active`, `blocked`, `flowing`, `idle`, `unstable`) remain readable under wrapping presentation.
- [ ] `WSC-AC05` (tunability): Core wrapping cue parameters are adjustable without code changes for normal iteration.
- [ ] `WSC-AC06` (fallback clarity): Reduced or disabled wrapping cue mode retains baseline gameplay readability and orientation.

## Required acceptance conditions
- Checklist `WSC-AC01` through `WSC-AC06` is fully evidenced.
- Any failed criterion has explicit remediation, owner, and re-test note.
- Decision log captures sign-off, deferrals, and rationale for any accepted residual risk.
- Manual capture set (screenshots/video) is attached before promoting provisional implementation pass to full gate closure.

## Validation scenes (required)
- Scene A: single-face baseline (`res://levels/Level_01_OneSided.tres`).
- Scene B: static portals (`res://levels/Level_02_Portals.tres`).
- Scene C: instability/state transitions (`res://levels/Level_03_Instability.tres`).
- Additional wrapping-focused traversal route(s) must be documented once selected.
- Canonical wrap route for Phase D evidence: `WrapRoute-01` (Level B -> Level C -> Level A cycle with wrapping profile toggles Disabled/Balanced/Emphasized).

## Evidence pack (required at gate)
- Manual validation record for Scene A/B/C plus wrapping-focused traversal route(s) (date, evaluator, runtime path).
- Pass/fail table keyed by `WSC-AC01` to `WSC-AC06`.
- Orientation failure log (if any) and mitigation outcomes.
- Decision log entry for final gate sign-off or deferred work.
- Phase D evidence document placeholder: `docs/EXECUTION/GRAPHICS_PHASE_D_EVIDENCE.md` (create at gate).

## Implementation boundary notes
- Presentation consumes immutable snapshots at frame boundaries and does not mutate gameplay/topology state.
- Wrapping cues must improve orientation without obscuring state/routing/portal cues.
- Prefer geometry/palette/shader-driven communication over bespoke art asset dependencies.

## Phase E prerequisites (to confirm at exit)
- Wrapping cues are stable enough that portal visuals can layer on top without ambiguity.
- Continuity markers and orientation anchors are documented for portal-transition integration planning.
- Fallback posture for wrapping cues is verified and recorded with evidence.

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

## Acceptance criteria
- Phase D objective gate passes with complete evidence file.
- Roadmap snapshot is updated to reflect Phase D state and Phase E readiness.
- World wrapping subplan is reconciled with delivered behavior (deliverables vs deferrals).
- Durable behavior or policy changes are recorded in ADR/decision log as needed.

## Risks and fallback
- Risk: wrapping cues overload the frame and reduce state readability.
  - Fallback: reduce cue density/intensity and prioritize directional anchors only.
- Risk: repeated-space presentation causes false adjacency interpretation.
  - Fallback: add continuity separators and stronger orientation framing cues.
- Risk: portability/performance issues emerge on lower-tier targets.
  - Fallback: constrain repeat depth and use low-cost cue profile as default.

## Open questions
- Should reading-guide promotion for `WSC-AC##` happen at Phase D gate close or at Phase E entry? (deferred to gate close)
- Do we need a dedicated debug overlay for orientation vectors, or are existing controls sufficient? (current answer: existing controls are sufficient for Phase D)

## Change log
- 2026-04-15: Initial Phase D execution scaffold created from roadmap/subplan intent and Phase C exit prerequisites.
- 2026-04-15: Implemented Phase D baseline wrapping model (ghost repeats + directional/return markers + separators), selected `WrapRoute-01`, and closed active checklist with evidence tracked in `GRAPHICS_PHASE_D_EVIDENCE.md`.
- 2026-04-15: Refined baseline cues to non-text geometric markers, added live in-run profile switching expectation, and aligned gate language with provisional evidence status pending manual capture.
- 2026-04-15: **Documentation amendment (no runtime change):** PRD §5.5–§10, TECH §6, `READING_GUIDE.md`, `IMPLEMENTATION_CADENCE_AND_DOD.md`, `VISUAL_STYLE_SYSTEM_PLAN.md`, `GRAPHICS_PHASE_B_EXECUTION.md` phase header, `DECISION_LOG.md`, and this roadmap’s change log were reconciled for MVP **no failure mode**, **win/goal** wording, **MVP Level 3** = GDD §7.3 traversal/rotation trigger, and **active phase** execution cadence. **Implementation:** none required—clarification only; level data already uses `InstabilityTrigger.PortalTraversalRotation` for Level 3 (`Enums.cs`).
