# Graphics Vision

## Purpose
Define a clear visual north star for `Ruin of the Shape` that minimizes custom art dependency while preserving readability, puzzle clarity, and systemic identity.

**Plain language — topology, routing, adjacency:** Imagine two cells on the Shape look “next to each other” in 3D, but a portal or instability rule says items may not move between them right now. **Routing** is the allowed item paths; **topology** is the authoritative map of connections and rules; **adjacency** in docs usually means “neighbors in that logical map,” not just eyeballing the mesh. **Portals** can link distant-looking spots. If the picture and the rules ever disagree, the **visuals must advertise** the true rules so players are not fooled.

## Source basis
This document is derived from:
- `docs/PRD.md`
- `docs/GDD.md`
- `docs/TECHNICAL_DESIGN.md`

## Core vision statement
The game should look distinct through geometry, composition, motion, and color systems rather than handcrafted asset volume. Visual communication must serve deterministic puzzle reasoning first.

## Principles

### 1) Readability over spectacle
- Prioritize player comprehension of state, routing, and topology.
- Keep silhouettes and motion cues clear at all times.
- Avoid visual effects that obscure adjacency or flow states.

### 2) Geometry-first style
- Prefer primitive forms, modular structures, and consistent line language.
- Build aesthetic coherence from repeated forms and stable composition rules.
- Treat textures as optional accents, not primary readability channels.

### 3) Minimal custom art dependency
- Use programmatic or parameterized visuals where possible.
- Favor palette, shader, and geometry variation over bespoke asset production.
- Keep MVP viable with near-zero custom texture pipeline requirements.

### 4) Deterministic visual semantics
- The same game state should produce the same visual cues.
- Portal and instability states must be legible and unambiguous.
- Visual transitions may be stylized, but must not alter simulation truth.

### 5) Topology clarity
- Visual framing should reinforce that logical topology is authoritative.
- If presentation and logical adjacency diverge, visuals should signal this explicitly.
- Debug and designer-facing overlays are part of visual quality, not optional extras.

## MVP graphics intent
- Focus on clear grids, portal affordances, item readability, and state feedback.
- Keep content scope aligned with three MVP levels.
- Defer high-complexity effects if they threaten determinism, clarity, or ship scope.

## Non-goals (MVP)
- High-fidelity bespoke art pipeline.
- Large custom texture authoring effort.
- Cinematic VFX-heavy presentation that reduces puzzle readability.
- Any visual solution that couples gameplay logic to mesh adjacency assumptions.

## Style system constraints (initial)
- Limited palette sets per biome/face.
- Limited visual primitives and shape vocabulary.
- Consistent line/edge behavior for object boundaries and depth cues.
- Animation patterns that convey state (flow, active, blocked, unstable).

## Acceptance criteria
- Players can identify actionable game state from visuals without hidden rules.
- Portal and instability transitions remain understandable during iteration.
- MVP visual quality can be produced and iterated without a custom art pipeline bottleneck.

## Change log
- 2026-04-15: Initial graphics vision scaffold derived from PRD/GDD/Technical Design.
- 2026-04-15: Added plain-language topology/routing/adjacency orientation for newcomers.
