# Performance Scaling Plan

## Purpose
Define graphics quality scaling strategy that preserves readability while meeting target hardware constraints.

**Plain language summary:** Define **graphics presets** (low/medium/high-style tiers) so the game stays playable on target machines. Fancy effects can step down, but **puzzle-critical clarity** must survive every tier. “Done” means written tier rules, ordered **fallbacks**, a simple way to measure performance, and locked defaults for MVP with rationale written down.

## Scope
- Quality profile model
- Fallback behavior
- Measurement and verification protocol

## Deliverables
- Quality tier definitions and default profile policy.
- Fallback ordering for optional graphics features.
- Performance verification checklist and reporting format.

## Phased approach
1. Define baseline quality tiers.
2. Add instrumentation and repeatable capture methodology.
3. Profile representative scenes and adjust defaults.
4. Lock MVP profile settings and document rationale.

## Acceptance criteria
- MVP target hardware achieves required performance budget.
- Readability-critical cues survive all fallback tiers.
- Performance profile behavior is documented and reproducible.

## Risks and fallback
- Risk: too many configuration knobs create maintenance burden.
  - Fallback: keep a small, curated profile set.
- Risk: visual regressions in low-tier profiles.
  - Fallback: re-prioritize readable cues above cosmetic effects.

## Change log
- 2026-04-15: Initial high-level subplan created.
- 2026-04-15: Added plain-language summary for newcomers.
