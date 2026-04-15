---
name: docs-change-policy-enforcer
description: Enforces documentation update obligations after implementation or behavior changes, including graphics-specific policy requirements and same-task reconciliation. Use when code changes alter behavior, assumptions, scope, or documented constraints.
---

# Docs Change Policy Enforcer

## When To Use
Use this skill when:
- The user requests implementation changes that may affect documented behavior.
- A change touches rendering, graphics state visibility, or graphics pipeline constraints.
- Locked assumptions or durable decisions are modified.

Primary references:
- `docs/ARCHITECTURE/DOCS_SYSTEM.md`
- `.cursor/rules/docs-source-of-truth.mdc`
- `.cursor/rules/graphics-principles.mdc`

## Workflow
Copy this checklist and track progress:

```text
Docs Change Policy Checklist:
- [ ] Classify change type (behavior/scope/assumption-only/refactor-only)
- [ ] Determine required doc updates by policy
- [ ] Identify graphics-specific obligations if graphics behavior changed
- [ ] Determine whether ADR creation/update is required
- [ ] Confirm same-task documentation reconciliation is complete
```

### Step 1: Classify change impact
- `No-doc-change`: purely internal refactor with no externally relevant behavior or assumption changes.
- `Doc-change-required`: behavior, scope, constraints, acceptance criteria, or assumptions changed.

### Step 2: Apply update policy
For `Doc-change-required`, update appropriate docs in the same task.

Minimum required targets depend on impact:
- Architecture-level behavior/constraints: `docs/ARCHITECTURE/`.
- Milestone/phase sequencing shifts: `docs/ROADMAP/` and `docs/ROADMAP/subplans/`.
- Day-to-day progress/checklist updates: `docs/EXECUTION/`.

### Step 3: Graphics-specific policy
If graphics behavior/scope changed, update at least one of:
- `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md`
- `docs/ROADMAP/GRAPHICS_ROADMAP.md`
- Relevant `docs/ROADMAP/subplans/*.md`

Preserve readability and deterministic communication of item state, routing state, portal state, and topology state.

### Step 4: ADR decision check
- If the change modifies a locked assumption or introduces/reverses a durable architectural decision, create/update an ADR under `docs/ADR/`.

### Step 5: Reconcile lower-precedence docs
- Ensure lower-precedence docs do not conflict with higher-precedence sources after updates.

## Output Template
Use this response structure:

```markdown
## Documentation Policy Impact

- Change classification: <No-doc-change | Doc-change-required>
- Required updates:
  - <file/path>: <why>
  - <file/path>: <why>
- ADR required: <yes/no> (reason)
- Graphics policy triggered: <yes/no> (details)
- Reconciliation status: <complete/incomplete>
```

## Guardrails
- Do not defer required documentation updates to a future task.
- Do not change graphics behavior without matching docs updates.
- Keep architecture docs durable and concise; keep execution docs operational and current.
