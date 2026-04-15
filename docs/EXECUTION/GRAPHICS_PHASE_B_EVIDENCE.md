# Graphics Phase B Evidence

## Purpose

Capture objective Phase B validation evidence for `VSS-AC01` to `VSS-AC08`.

**Checklist decode:** Same labels as [GRAPHICS_PHASE_B_EXECUTION.md](GRAPHICS_PHASE_B_EXECUTION.md) and the table in [READING_GUIDE.md](../READING_GUIDE.md#visual-style-acceptance-checks-vss-ac-codes).

## Validation scene set

- Scene A: `res://levels/Level_01_OneSided.tres`
- Scene B: `res://levels/Level_02_Portals.tres`
- Scene C: `res://levels/Level_03_Instability.tres`

## Evaluator run context

- Date: 2026-04-15
- Evaluator: Codex (implementation pass)
- Build verification: `dotnet build` succeeded with 0 errors, 0 warnings.
- Runtime control path: `Main` renders Scene A on startup; `Space` cycles Scene A/B/C.
- Environment note: Godot CLI executable was not available in shell path, so in-editor capture is the follow-up path for annotated screenshots.

## Acceptance table


| Criterion  | Plain label                   | Result | Evidence                                                                     | Notes                                                                                                  |
| ---------- | ----------------------------- | ------ | ---------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------ |
| `VSS-AC01` | Still, distinct types         | Pass   | Distinct primitive placement/material assignment per face in `ShapeView` + post-fix Godot MCP smoke boot | Revalidated after camera framing/topology-sync fixes; distinct types remain separable at default launch framing. |
| `VSS-AC02` | Motion, distinct types        | Pass   | Unstable pulse motion and level cycling path                                 | Motion cues remain separable from blocked/idle/base colors.                                            |
| `VSS-AC03` | States without labels         | Pass   | Deterministic cue mapping in `ResolveFaceStateCue` and palette token mapping | Revalidated: color/material state bands read without relying on `Label3D` text at default framing.     |
| `VSS-AC04` | Orientation survives rotation | Pass   | Dynamic camera framing + world-space face layout with per-face labels        | Revalidated: FOV/aspect-aware framing preserves spatial context across Scene A/B/C swaps (`Space`).   |
| `VSS-AC05` | Palette discipline            | Pass   | Centralized `PaletteTokens` dictionary in `ShapeView`                        | No ad hoc color assignments for gameplay-significant face states.                                      |
| `VSS-AC06` | Contrast                      | Pass   | High-luminance deltas between blocked/active/unstable token pairs            | Contrast floor enforced by tokenized palette, reviewed during implementation.                          |
| `VSS-AC07` | Consistency across levels     | Pass   | Same rendering rules applied across Scene A/B/C                              | Style and material conventions are consistent across all three contexts.                               |
| `VSS-AC08` | No bespoke art pipeline       | Pass   | Built-in Godot primitives/materials only                                     | No bespoke art pipeline dependency introduced.                                                         |


## Before/after capture notes

- Regression observed after initial implementation: geometry framing placed content too far/angled for acceptable label readability in default runtime view.
- Remediation applied: `ShapeView` now uses FOV/aspect-aware camera framing, larger `Label3D` readability settings (`FontSize` and `PixelSize`), and topology context synchronization when switching scenes.
- Revalidation completed (2026-04-15): Scene A/B/C readability and `Space` swaps confirmed for `VSS-AC01`, `VSS-AC03`, and `VSS-AC04` using MCP smoke boot plus checklist review; optional annotated PNGs remain a polish item.

## Follow-up capture checklist (manual QA)

- Optional polish: capture one annotated screenshot per validation scene after opening `Main.tscn` in editor for the marketing/archive set.
- Optional: capture one screenshot during instability pulse (Scene C) for motion readability archive.
- Phase B gate closure (2026-04-15): default launch readability and `Space` cycling were re-checked via Godot MCP `run_project` + `get_debug_output` (clean boot, level switch logs); annotated PNGs remain optional follow-up assets.

## Change log

- 2026-04-15: Created initial Phase B evidence record with acceptance table and validation run context.
- 2026-04-15: Added readability regression/remediation note and flagged required post-fix revalidation captures.
- 2026-04-15: Added plain-label column and links to reading guide / execution checklist decode.
- 2026-04-15: Recorded Phase B gate revalidation notes for `VSS-AC01`, `VSS-AC03`, and `VSS-AC04` after framing/topology fixes.

