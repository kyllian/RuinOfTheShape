---
name: docs-adr-decision-recorder
description: Detects when architecture decisions require ADR creation or updates and records durable decision context with downstream document reconciliation. Use when assumptions change, trade-offs are accepted, or prior decisions are reversed.
---

# Docs ADR Decision Recorder

## When To Use
Use this skill when:
- A non-trivial, durable architecture decision is made.
- An existing architectural assumption is changed.
- A previous decision is reversed or superseded.

Primary references:
- `docs/ARCHITECTURE/DOCS_SYSTEM.md`
- `.cursor/rules/docs-source-of-truth.mdc`

## ADR Trigger Criteria
Create or update an ADR when at least one applies:
- Decision has long-term impact across systems or phases.
- Trade-offs are accepted between viable alternatives.
- Existing durable assumption is modified.
- Future contributors need rationale to avoid churn.

## Workflow
Copy this checklist and track progress:

```text
ADR Recording Checklist:
- [ ] Confirm decision durability and scope
- [ ] Determine create-new vs update-existing ADR
- [ ] Capture context, options, and rationale
- [ ] Record decision, consequences, and status
- [ ] Reconcile impacted architecture/roadmap/execution docs
```

### Step 1: Frame decision
- Summarize problem and constraints.
- Identify alternatives considered and rejected.

### Step 2: Choose ADR action
- `Create`: no existing ADR covers this decision.
- `Update`: existing ADR exists but needs status change, supersession, or revision.

### Step 3: Capture durable content
Include at minimum:
1. Context
2. Decision
3. Alternatives considered
4. Consequences
5. Status / supersession notes

### Step 4: Reconcile other docs
- Update architecture docs to align with ADR decision.
- Update roadmap/execution docs where decision affects sequencing or current tasks.
- Ensure no lower-precedence docs conflict with the ADR.

## Output Template
Use this response structure:

```markdown
## ADR Decision Record

- ADR action: <create/update>
- Decision scope: <system/stream/component>
- Decision summary: <final choice>
- Alternatives considered: <list>
- Consequences: <key impacts>
- Required reconciliations:
  - <file>: <update needed>
  - <file>: <update needed>
```

## Guardrails
- Do not create ADRs for trivial or temporary execution details.
- Do not leave an ADR decision undocumented in architecture docs when behavior is affected.
- Keep ADR language stable and rationale-focused.
