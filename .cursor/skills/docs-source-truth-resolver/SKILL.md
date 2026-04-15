---
name: docs-source-truth-resolver
description: Resolves documentation conflicts by applying source-of-truth precedence across ADR, Architecture, Vision, Roadmap, and Execution documents. Use when docs disagree, when choosing where to read first, or when reconciling conflicting implementation guidance.
---

# Docs Source Truth Resolver

## When To Use
Use this skill when:
- The user asks which document is authoritative.
- Two or more docs provide conflicting direction.
- You need to decide which doc layer to update first.

## Authority Order
Apply this precedence exactly:
1. ADR (if an active decision exists)
2. Architecture
3. Vision
4. Roadmap
5. Execution

Primary reference: `docs/ARCHITECTURE/DOCS_SYSTEM.md`.

## Workflow
Copy this checklist and track progress:

```text
Source Truth Resolution Checklist:
- [ ] Identify all relevant docs by layer (ADR/Architecture/Vision/Roadmap/Execution)
- [ ] Extract the specific conflicting or overlapping statements
- [ ] Apply precedence and choose the authoritative statement
- [ ] Determine whether reconciliation edits are required in lower-precedence docs
- [ ] Report chosen authority and required follow-up updates
```

### Step 1: Gather relevant documents
- Include relevant files from `docs/ADR/`, `docs/ARCHITECTURE/`, `docs/VISION/`, `docs/ROADMAP/`, and `docs/EXECUTION/`.
- Prefer narrow file reads over broad assumptions.

### Step 2: Compare intent and constraints
- Focus on behavior-defining statements, acceptance criteria, assumptions, and phase commitments.
- Ignore wording differences that do not change intent.

### Step 3: Resolve by precedence
- If an ADR exists and is active, treat it as final authority.
- Otherwise choose the highest-precedence layer with explicit guidance.

### Step 4: Reconciliation policy
- If lower-precedence docs diverge from the selected authority, mark them for update in the same task.
- If divergence cannot be resolved without a new durable decision, escalate to ADR creation/update.

## Output Template
Use this response structure:

```markdown
## Source-of-Truth Resolution

- Conflict area: <topic>
- Authoritative source: <file/path and layer>
- Applied rule: ADR > Architecture > Vision > Roadmap > Execution
- Decision: <what to follow now>
- Required reconciliations:
  - <file>: <update needed>
  - <file>: <update needed>
```

## Guardrails
- Do not treat execution notes as overriding architecture or ADR decisions.
- Do not skip reconciliation when a conflict is confirmed.
- Keep terminology consistent with `Vision`, `Architecture`, `Roadmap`, `Execution`, `ADR`, and `subplans`.
