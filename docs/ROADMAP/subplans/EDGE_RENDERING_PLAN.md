# Edge Rendering Plan

## Purpose
Introduce edge/line rendering that improves depth and form readability while preserving visual stability.

**Plain language summary:** Add crisp **outlines or edge lines** so 3D forms read clearly (depth and corners) without shimmer or clutter that hides items or portal state. “Done” means an edge drawing step exists, can be tuned or turned down (**fallback**) on weak PCs, and was checked in both normal and portal-heavy scenes.

## Scope
- Screen-space edge rendering strategy
- Quality tiers and fallback
- Artifact detection and mitigation

## Deliverables
- Initial edge pass integrated with baseline pipeline. **Status:** delivered via `EdgePostProcessLayer` + `screen_space_edges.gdshader` + `EdgePresentationSettings` (see `docs/EXECUTION/GRAPHICS_PHASE_C_EVIDENCE.md`).
- Tunable parameters for edge threshold and intensity. **Status:** delivered (`edge_threshold`, `edge_intensity`, `neighbor_soften`, `edge_tint`, `quality_tier` on the settings resource).
- Artifact matrix (flicker, stair-stepping, over-outlining) and mitigations. **Status:** documented in Phase C evidence appendix.

## Reconciliation notes (2026-04-15)

- **Screen-space strategy:** Implemented as **color-buffer Sobel** (luminance + chroma) on `hint_screen_texture`, not depth/normal discontinuity sampling. Godot’s canvas post-processing path does not expose depth/normal samplers; upgrading to compositor-driven buffers remains a follow-up if silhouettes on same-colored coplanar regions become blocking.
- **Label3D interaction:** Text quads participate in the color buffer; expect mild outline/hash at label boundaries. Mitigation options: lower `edge_intensity`, future UI-layer labels, or depth-aware compositor pass (deferred).
- **Quality tiers:** `Tier0` pass-through, `Tier1` default, `Tier2` stronger response — aligns with pipeline Tier0/Tier1 posture; Tier2 here is a presentation-only boost rather than a distinct engine budget tier.

## Phased approach
1. Implement baseline edge pass.
2. Add parameter controls and debug views.
3. Add quality tiers and fallback path.
4. Validate in portal and non-portal scenes.

## Acceptance criteria
- Edges increase form legibility without obscuring gameplay state.
- Visual stability is acceptable during camera and traversal transitions.
- Fallback mode retains readability if advanced edge mode is disabled.

## Risks and fallback
- Risk: line shimmer and temporal instability.
  - Fallback: reduce edge sensitivity and simplify pass.
- Risk: overdraw/performance cost.
  - Fallback: switch to lower quality mode by default.

## Change log
- 2026-04-15: Initial high-level subplan created.
- 2026-04-15: Added plain-language summary for newcomers.
- 2026-04-15: Reconciled deliverables with Phase C implementation (canvas Sobel + deferrals).
- 2026-04-15: Updated evidence references to alphabetical phase filename (`GRAPHICS_PHASE_C_EVIDENCE.md`).
