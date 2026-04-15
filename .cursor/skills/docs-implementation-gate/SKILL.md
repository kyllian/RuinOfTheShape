---
name: docs-implementation-gate
description: Runs a documentation gate for feature builds, enhancements, and bugfixes by chaining source-of-truth resolution, policy checks, phase planning checks, ADR checks, and template-based doc creation when needed. Use when implementing or modifying behavior.
---

# Docs Implementation Gate

## When To Use
Use this skill when:
- The user asks to build a feature, enhance existing behavior, or fix a bug.
- Any code change could affect documented behavior, scope, assumptions, or phase tracking.

This is an orchestration skill that coordinates:
- `docs-source-truth-resolver`
- `docs-change-policy-enforcer`
- `docs-phase-planning-maintainer`
- `docs-adr-decision-recorder`
- `docs-template-author`
- `godot-mcp-playtest` (when the change is not docs-only and affects runtime play)

Primary references:
- `docs/ARCHITECTURE/DOCS_SYSTEM.md`
- `.cursor/rules/docs-source-of-truth.mdc`
- `.cursor/rules/planning-granularity.mdc`
- `.cursor/rules/graphics-principles.mdc`

## Gate Workflow
Copy this checklist and track progress:

```text
Docs Implementation Gate Checklist:
- [ ] Run source-of-truth pass before implementation
- [ ] Identify docs/rules that constrain the requested change
- [ ] Implement and verify code changes
- [ ] Run Godot MCP playtest when gameplay/engine-facing assets changed (see `godot-mcp-playtest`)
- [ ] Run documentation change-policy pass after implementation
- [ ] Run phase-planning pass when roadmap/execution state is impacted
- [ ] Run ADR pass when durable assumptions or decisions changed
- [ ] Create missing docs with standard template if needed
- [ ] Report consulted docs, updated docs, and reconciliation status
```

### Step 1: Pre-implementation authority pass
Invoke `docs-source-truth-resolver` to determine which docs are authoritative for the task.

### Step 2: Implement change
Perform requested coding work using constraints from Step 1.

### Step 2b: Runtime verification (Godot MCP)
When the change affects gameplay or engine-facing assets, invoke `godot-mcp-playtest` after local verification (build/tests as applicable) and **before** treating the implementation as complete. Hooks cannot call MCP; the agent must call `run_project` / `get_debug_output` (and `stop_project` if needed) so the user can manually smoke-test.

### Step 3: Post-implementation policy pass
Invoke `docs-change-policy-enforcer` to decide required doc updates in the same task.

### Step 4: Conditional passes
- If phases, milestones, subplans, or checklists changed, invoke `docs-phase-planning-maintainer`.
- If a durable architecture decision changed, invoke `docs-adr-decision-recorder`.
- If a new doc is needed, invoke `docs-template-author`.

### Step 5: Reconciliation closeout
Ensure lower-precedence docs are reconciled with authoritative sources after updates.

## Subagent Guidance
When codebase discovery is broad, use subagents:
- Prefer `explore` subagent for fast read-only discovery of relevant docs and constraints.
- Use `generalPurpose` subagent for larger reconciliation tasks spanning multiple doc layers.
- Run parallel `explore` subagents by layer (architecture, roadmap, execution) when speed matters.

## Output Template
Use this response structure:

```markdown
## Documentation Gate Result

- Change type: <feature/enhancement/bugfix>
- Authoritative docs consulted:
  - <file/path>
  - <file/path>
- Skills applied:
  - <skill-name>: <result>
  - <skill-name>: <result>
- Godot MCP playtest: <run_project + get_debug_output summary, or skipped reason>
- Documentation updates made:
  - <file/path>: <why>
  - <file/path>: <why>
- ADR updated/created: <yes/no> (details)
- Reconciliation status: <complete/incomplete>
- Residual follow-ups: <none or list>
```

## Guardrails
- Do not start implementation without identifying authoritative docs.
- Do not leave required documentation updates for a later task.
- Do not let execution-layer docs override architecture or ADR decisions.
