# AI-Assisted Graphics Principles

## Purpose
Define how AI is used in graphics implementation so speed gains do not introduce quality drift, unreadable visuals, or architectural inconsistency.

## Summary for readers
Use AI to draft scaffolding, shaders, tests, and docs—then **you** verify against acceptance criteria, determinism, and fallbacks. Never merge generated rendering code on trust alone. If behavior or constraints change, update architecture/roadmap docs in the **same** task; durable assumption changes get an ADR. For general doc and game-dev terms, see [READING_GUIDE.md](../READING_GUIDE.md).

## Scope
Applies to graphics-related planning, implementation, shader work, tuning, and documentation updates.

## AI usage model
Use AI as an accelerator for:
- Pattern proposals and implementation scaffolding
- Shader prototyping
- Test scene and debug overlay setup
- Refactoring into cleaner module boundaries
- Documentation and acceptance criteria maintenance

Do not use AI as an unreviewed source of final truth.

## Required workflow

### 1) Constrain the request
Every AI request for implementation should include:
- Engine/version context
- Target subsystem
- Performance/readability constraints
- Inputs/outputs
- Acceptance criteria

### 2) Validate output before merge
For AI-proposed graphics changes:
- Verify visual behavior against documented acceptance criteria.
- Check determinism and readability implications.
- Confirm fallback behavior for non-critical effects.

### 3) Keep documentation in sync
If behavior or constraints change:
- Update relevant architecture and roadmap docs in the same task.
- Add/update ADR when decisions alter durable assumptions.

## Quality gates for AI-generated graphics work
- No blind copy/paste from generated code.
- Must include a test plan or reproducible verification steps.
- Must include instrumentation hooks for debugging when effect complexity is moderate or high.
- Must note known artifacts and mitigation options.

## Prompting standards
Prompts should request:
- Small, composable changes
- Explicit tradeoffs
- Clear failure modes and fallback options
- Integration notes for existing architecture

## Risk controls
- Avoid introducing hidden coupling between visuals and simulation truth.
- Avoid complex effects before baseline readability is stable.
- Prefer reversible changes for experimentation.

## Acceptance criteria
- AI output quality is measurable and repeatable.
- Team can explain why each accepted graphics change exists.
- Documentation remains authoritative after each accepted change.

## Change log
- 2026-04-15: Initial AI-assisted graphics policy created.
- 2026-04-15: Added “Summary for readers” and link to reading guide.
