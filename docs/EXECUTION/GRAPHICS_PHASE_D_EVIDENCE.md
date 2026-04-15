# Graphics Phase D Evidence

## Purpose
Capture objective evidence and disposition for **Phase D - wrapping and spatial cues** acceptance checks (`WSC-AC01` to `WSC-AC06`).

## Validation context
- Date: 2026-04-15
- Evaluator: Codex agent (implementation run)
- Runtime path: Godot MCP `run_project` on repository root `D:\source\RuinOfTheShape`
- Required scenes:
  - `res://levels/Level_01_OneSided.tres` (Scene A)
  - `res://levels/Level_02_Portals.tres` (Scene B)
  - `res://levels/Level_03_Instability.tres` (Scene C)
- Canonical wrapping route: `WrapRoute-01` (Level B -> Level C -> Level A cycle with wrapping profile toggles Disabled/Balanced/Emphasized)

## Acceptance table
| ID | Status | Evidence summary | Follow-up |
|----|--------|------------------|-----------|
| `WSC-AC01` | Provisionally pass | Wrapping cues implemented with geometric directional/return anchors; no runtime errors on launch path. | Manual player-orientation walkthrough remains part of gate sign-off checklist. |
| `WSC-AC02` | Provisionally pass | Repeat geometry rendered as low-alpha ghost copies plus separators so repeated space reads as continuity context, not false adjacency. | Re-check once portal transitions (Phase E) are layered. |
| `WSC-AC03` | Provisionally pass | Continuity separators and geometric directional chevrons provide wrapped-transition framing without text overlays. | Capture screenshots/video during manual gate review. |
| `WSC-AC04` | Provisionally pass | Existing state token colors remain on primary faces/portals; wrapped copies use attenuated ghost intensity to preserve primary state readability. | Include side-by-side captures in final gate packet. |
| `WSC-AC05` | Provisionally pass | `WrappingPresentationSettings_Default.tres` exposes profile, repeat depth, ghost alpha, continuity intensity, and marker height for no-recompile tuning; debug profile cycling updates live in-run. | None. |
| `WSC-AC06` | Provisionally pass | Disabled profile forces repeat depth `0` (fallback posture) while baseline world render remains intact; continuity markers are suppressed when effective depth is zero. | Validate fallback profile under Phase F perf sweep as a safety check. |

## Orientation failure log
- None observed during implementation smoke run.

## Decision log
- Gate disposition: **Provisionally accepted for implementation baseline**.
- Deferred evidence:
  - Manual traversal capture set (screenshots/video) to be attached at formal phase gate review.
  - Optional orientation debug overlay deferred; existing tuning/profile controls are sufficient for Phase D baseline.
- Residual risk acceptance:
  - Moderate risk that emphasized cue profile can over-signal in dense layouts; mitigated by default balanced profile and disabled fallback mode.

## Related changes
- `src/Interaction/ShapeView.cs` (wrapping cues, profile cycling, repeat-space presentation)
- `src/Presentation/WrappingPresentationSettings.cs` (developer-tunable parameters)
- `resources/WrappingPresentationSettings_Default.tres` (default tune profile)
- `scenes/ShapeView.tscn` (settings wiring)
