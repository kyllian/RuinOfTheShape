---
name: docs-phase-planning-maintainer
description: Maintains planning documents with correct granularity between roadmap intent and execution detail. Use when updating phases, milestones, subplans, or active implementation checklists.
---

# Docs Phase Planning Maintainer

## When To Use
Use this skill when:
- The user asks to update phase plans, milestone sequencing, or checklists.
- Work transitions between phases.
- You need to decide whether content belongs in `docs/ROADMAP/` versus `docs/EXECUTION/`.

Primary references:
- `docs/ARCHITECTURE/DOCS_SYSTEM.md`
- `.cursor/rules/planning-granularity.mdc`

## Granularity Rules
- Keep high-level stream intent in `docs/ROADMAP/subplans/`.
- Keep volatile day-to-day details in `docs/EXECUTION/`.
- Keep detailed plans for current and next phase.
- Keep later phases concise until they are within one phase of execution.
- At phase transition, update roadmap status, execution checklist, and resulting decisions.

## Workflow
Copy this checklist and track progress:

```text
Phase Planning Checklist:
- [ ] Identify the current phase and next phase
- [ ] Place strategic intent in roadmap/subplans
- [ ] Place operational tasks and blockers in execution docs
- [ ] Ensure detailed coverage only for current + next phase
- [ ] Apply transition updates consistently across docs
```

### Step 1: Determine planning horizon
- Map requested changes to `current`, `next`, or `later` phases.
- If horizon is unclear, infer from existing roadmap phase markers.

### Step 2: Route content by layer
- `docs/ROADMAP/` and `docs/ROADMAP/subplans/`: milestones, gates, dependency ordering, stream intent.
- `docs/EXECUTION/`: in-flight tasks, acceptance checks, blockers, immediate sequencing.

### Step 3: Enforce depth policy
- Current + next phase: include actionable detail.
- Later phases: keep concise and goal-oriented.

### Step 4: Handle phase transition
When entering a new phase:
- Update roadmap phase status.
- Move detail focus in execution docs to the new current phase.
- Record resulting durable decisions via ADR if needed.

## Output Template
Use this response structure:

```markdown
## Phase Documentation Update

- Current phase: <phase>
- Next phase: <phase>
- Updated roadmap/subplans:
  - <file>: <change>
- Updated execution docs:
  - <file>: <change>
- Granularity check: <pass/fail + reason>
```

## Guardrails
- Do not put daily task logs in roadmap/subplans.
- Do not over-detail late phases.
- Keep phase transition updates synchronized across roadmap and execution.
