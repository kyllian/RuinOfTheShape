# Documentation System

## Purpose
This project uses a layered documentation system so future implementation work, including AI-assisted work, can follow one source of truth without mixing stable intent and volatile execution details.

## If you are new to game development
Engineering and design docs use **specialized vocabulary** (phases, gates, topology, render passes, and more). That is normal: precise words prevent ambiguity. For a plain-language map of the doc folders, suggested reading orders, and a **glossary**, start with [READING_GUIDE.md](../READING_GUIDE.md). Use it as the “Rosetta stone,” then return here for precedence rules and update policy.

## Layers and ownership

### `docs/VISION/` (why)
- Stable product/design intent and principles.
- Updated when vision changes.
- Owner: product/design direction.

### `docs/ARCHITECTURE/` (what)
- Canonical system-level design, constraints, and interfaces.
- Updated when technical intent changes.
- Owner: engineering architecture.

### `docs/ROADMAP/` (when)
- Milestones, phase gates, dependency sequencing, high-level stream plans.
- Updated at milestone boundaries or major reprioritization.
- Owner: planning and delivery.

### `docs/EXECUTION/` (how now)
- Current phase checklists, acceptance checks, progress and blockers.
- Updated continuously during active implementation.
- Owner: active implementer.

### `docs/ADR/` (decision record)
- Architecture decision records for non-trivial, durable decisions.
- Updated when key decisions are made or reversed.
- Owner: decision maker plus reviewer.

### `.cursor/rules/` (AI guardrails)
- Concise rules that guide Cursor agents on constraints and workflow.
- Updated when project process or standards change.
- Owner: maintainers of project conventions.

## Source-of-truth precedence
When documents disagree, use this order:
1. ADR (if decision exists and is active)
2. Architecture
3. Vision
4. Roadmap
5. Execution

If a lower-precedence document needs to diverge, create or update an ADR and reconcile affected docs in the same task.

## Update policy
- Any change to graphics behavior or scope must update at least one of:
  - `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md`
  - `docs/ROADMAP/GRAPHICS_ROADMAP.md`
  - relevant `docs/ROADMAP/subplans/*.md`
- If implementation changes a locked assumption, add or update an ADR.
- Execution docs may be detailed and temporary; architecture docs should stay concise and durable.

## Planning granularity policy
- Detailed steps are required for the current phase and the next phase.
- Later phases remain high-level until they move within one phase of execution.
- Subplans define stream-level intent and phase gates, not day-to-day task logs.

## Standard document template
Use this minimal structure for new docs:
1. Purpose
2. Scope / non-scope
3. Inputs / assumptions
4. Plan or design
5. Acceptance criteria
6. Risks and fallback
7. Open questions
8. Change log

## Change log
- 2026-04-15: Initial scaffold created.
- 2026-04-15: Linked reading guide and added newcomer-oriented onboarding pointer.
