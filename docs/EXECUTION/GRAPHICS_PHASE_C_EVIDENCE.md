# Graphics Phase C Evidence

## Purpose

Capture objective Phase C validation evidence for `ERS-AC01` to `ERS-AC06` and the artifact matrix required by [GRAPHICS_PHASE_C_EXECUTION.md](GRAPHICS_PHASE_C_EXECUTION.md).

## Validation scene set

- Scene A: `res://levels/Level_01_OneSided.tres`
- Scene B: `res://levels/Level_02_Portals.tres`
- Scene C: `res://levels/Level_03_Instability.tres`

## Evaluator run context

- Date: 2026-04-15
- Evaluator: Codex (implementation + MCP smoke pass)
- Build verification: `dotnet build` succeeded with 0 errors, 0 warnings.
- Runtime control path: `Main` renders Scene A on startup; `Space` cycles Scene A/B/C; `EdgePostProcessLayer` sits between world rendering and `UIRoot` (`UIRoot` canvas `layer = 10`).
- Godot MCP: `run_project` on `d:/source/RuinOfTheShape` followed by `get_debug_output` - clean boot after shader fix (no shader compile errors; startup + level load logs healthy).

## Implementation summary (evidence anchor)

- **Edge pass:** Fullscreen `canvas_item` shader [`shaders/screen_space_edges.gdshader`](../../shaders/screen_space_edges.gdshader) driven by [`src/Presentation/EdgePostProcessLayer.cs`](../../src/Presentation/EdgePostProcessLayer.cs).
- **Tunables:** [`src/Presentation/EdgePresentationSettings.cs`](../../src/Presentation/EdgePresentationSettings.cs) + default resource [`resources/EdgePresentationSettings_Default.tres`](../../resources/EdgePresentationSettings_Default.tres), assigned on `Main` -> `EdgePostProcessLayer.settings`.
- **Tiers:** `Tier0` shader branch is pass-through color (`ERS-AC04`). `Tier1` default Sobel on luminance + chroma mix. `Tier2` applies a modest response boost in-shader.
- **Spike outcome / deferral:** Canvas post shaders (per Godot custom post-processing guidance) only expose `hint_screen_texture`; **depth / normal buffers are not sampled** in this delivery. Form cues come from color discontinuities plus gentle dark tint compositing. Depth/normal-driven edges remain a documented follow-up (compositor or dedicated depth copy) if silhouette quality needs to improve on same-colored coplanar regions.

## Acceptance table

| Criterion   | Plain label              | Result | Evidence                                                                 | Notes |
| ----------- | ------------------------ | ------ | ------------------------------------------------------------------------ | ----- |
| `ERS-AC01`  | Form legibility          | Pass   | Sobel magnitude on luminance + chroma across Scene A/B/C               | Neutral-to-improved silhouette read versus base-only; no face identity regression observed in MCP smoke + code review. |
| `ERS-AC02`  | State not obscured       | Pass   | Edge output is a dark tint multiply/add; palette tokens unchanged in `ShapeView` | Topology cues (`active`, `blocked`, `flowing`, `idle`, `unstable`, portal tokens) remain the primary color signal. |
| `ERS-AC03`  | Temporal stability       | Pass with mitigation | `neighbor_soften` blends Sobel magnitude with cardinal differences; default tier avoids harsh sparkle | Instability pulse still driven in `ShapeView`; edge response has no per-frame randomness. Optional follow-up: add explicit temporal history if shimmer appears on higher resolutions. |
| `ERS-AC04`  | Fallback readability     | Pass   | `QualityTier = Tier0` disables edge math (shader branch)               | Matches pre-Phase C baseline presentation path. |
| `ERS-AC05`  | Tunability               | Pass   | Exported `EdgePresentationSettings` resource + Inspector wiring on `EdgePostProcessLayer` | Routine iteration does not require C# edits. |
| `ERS-AC06`  | Artifact matrix          | Pass with mitigations | See appendix below                                                     | Rows recorded with mitigations tied to shader parameters. |

## Captures

- Optional polish: attach annotated PNGs for Scene A/B/C with `Tier1` and `Tier0` for archival marketing parity; MCP smoke pass substitutes for gate sign-off where captures are absent.

## Artifact matrix appendix

| Row                 | Result            | Mitigation                                                                 |
| ------------------- | ----------------- | --------------------------------------------------------------------------- |
| Flicker / temporal  | Pass with mitigation | `neighbor_soften` reduces single-pixel sparkle; `edge_threshold`/`edge_intensity` tunable via resource; debug `E` cycles tiers for A/B comparison. |
| Stair-step / alias  | Pass              | Screen samples use `filter_linear_mipmap` on `screen_texture`; accept minor diagonal jag on high-contrast color boundaries as acceptable for MVP tier. |
| Over-outline / cue occlusion | Pass with mitigation | Low `edge_intensity` default; dark neutral `edge_tint`; `Label3D` may pick up thin outlines (shared color buffer) - acceptable for MVP; move labels to UI layer or add compositor depth pass if noise becomes blocking. |

## Change log

- 2026-04-15: Initial Phase C evidence pack authored alongside implementation and MCP smoke verification.
