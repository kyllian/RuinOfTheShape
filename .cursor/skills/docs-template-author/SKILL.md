---
name: docs-template-author
description: Creates new project documentation using the standard section template and layer-aware placement rules. Use when adding new docs in architecture, roadmap, execution, or decision-record workflows.
---

# Docs Template Author

## When To Use
Use this skill when:
- The user asks to create a new document in `docs/`.
- A new stream plan, architecture note, or execution artifact is needed.
- You need consistent section structure and acceptance language.

Primary reference: `docs/ARCHITECTURE/DOCS_SYSTEM.md`.

## Layer Placement Rules
Before drafting content, choose the correct layer:
- `docs/VISION/`: stable product/design intent.
- `docs/ARCHITECTURE/`: canonical technical design and constraints.
- `docs/ROADMAP/`: milestones, sequencing, phase gates, subplans.
- `docs/EXECUTION/`: active checklist, progress, blockers.
- `docs/ADR/`: non-trivial durable decisions.

## Standard Template
Use this exact section structure for new docs:
1. Purpose
2. Scope / non-scope
3. Inputs / assumptions
4. Plan or design
5. Acceptance criteria
6. Risks and fallback
7. Open questions
8. Change log

## Workflow
Copy this checklist and track progress:

```text
Doc Authoring Checklist:
- [ ] Choose correct documentation layer and path
- [ ] Draft all required template sections
- [ ] Tailor plan/design depth to the layer's purpose
- [ ] Add concrete acceptance criteria and fallback notes
- [ ] Add initial change log entry with date and intent
```

### Step 1: Select target file
- Route to a path that matches ownership and volatility of the content.
- Prefer concise durable wording for architecture docs and detailed operational wording for execution docs.

### Step 2: Draft template sections
- Fill every section, even if a section is intentionally brief.
- For non-scope, explicitly state exclusions to reduce ambiguity.

### Step 3: Validate quality
- Acceptance criteria must be testable or reviewable.
- Risks must include fallback behavior or mitigation.
- Open questions should be actionable, not rhetorical.

### Step 4: Add changelog entry
- Add a date-stamped entry describing creation or significant revision.

## Output Template
Use this response structure when proposing or creating docs:

```markdown
## New Documentation Draft

- Target layer: <VISION/ARCHITECTURE/ROADMAP/EXECUTION/ADR>
- Target file: <path>
- Template coverage: <all sections present>
- Key acceptance criteria:
  - <criterion>
  - <criterion>
- Open questions:
  - <question>
```

## Guardrails
- Do not mix stable architecture intent with volatile execution notes.
- Do not omit acceptance criteria or risks/fallback.
- Keep document naming and terminology consistent with existing docs system language.
