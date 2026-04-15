# Product Requirements Document: Ruin of the Shape

**Document type:** Product Requirements Document (PRD)  
**Status:** Draft — vision and MVP scope  
**Last updated:** 2026-04-15

**Related:** [GDD.md](./GDD.md), [TECHNICAL_DESIGN.md](./TECHNICAL_DESIGN.md)  
**New to game-dev vocabulary?** See [READING_GUIDE.md](./READING_GUIDE.md) for a glossary (MVP, topology, determinism, doc layers, and more).

---

## 1. Executive summary

**Ruin of the Shape** is a systems-driven 3D puzzle game played on the surface of a mutable geometric structure (the **Shape**). Players design automated logistics—conveyors, splitters, mergers, transformers, assemblers—to route and process items across a multi-face world linked by **portals**. The core tension is **factory optimization** combined with **spatial reasoning** under **controlled non-Euclidean behavior**: portal connectivity can shift with **rotation state** and/or **level-specific rules**, so adjacency is not fully stable. The **MVP Level 3** teaching slice locks *when* that shifts for players to **portal traversal / rotation** (see [GDD.md](./GDD.md) §7.3); post-MVP can add richer instability archetypes. The **MVP** locks scope to a **two-sided square** (no thickness) and **three levels**: one-sided automation, then portals, then instability; post-MVP progression scales puzzle density through more faces, item types, routing constraints, and unstable spatial rules.

**North-star experience:** A compact, high-density puzzle loop where mastery emerges through iteration, optimization, and understanding an increasingly complex spatial-logical system.

---

## 2. Product vision

| Dimension | Statement |
|-----------|-----------|
| **Genre** | 3D puzzle / automation / logistics |
| **Pillar 1** | Readable factory systems on a grid, with deterministic item flow |
| **Pillar 2** | Multi-face navigation and portal networks as the spatial backbone |
| **Pillar 3** | Deliberate topological inconsistency as a puzzle mechanic—not bugs |
| **Tone** | Minimal narrative framing; thematic escalation over explicit storytelling |

---

## 3. Goals and non-goals

### 3.1 Goals

- Deliver **short iterative puzzle loops**: observe goals → build/edit → simulate → reset → refine.
- Teach **automation first** (one face), then **portal literacy** across the two-sided Shape, then **spatial instability**—in three fixed MVP levels.
- Keep **rules deterministic** so players can reason about cause and effect.
- Ship an **MVP** that proves: grid-on-a-two-sided Shape, a **three-level** progression (automation → portals → spatial instability), core components, and **level data conventions** reusable for future content.

### 3.2 Non-goals (MVP)

- Procedural shapes or infinite content.
- Fully simulated rigid-body or fluid physics for items.
- Cinematic narrative, voice acting, or heavy lore delivery.
- High-fidelity art production; visual clarity beats spectacle.

---

## 4. Target player and use cases

**Primary audience:** Players who enjoy optimization puzzles, factory games (e.g. conveyor routing, split/merge logic), and spatial “aha” moments—especially those tolerant of iteration-driven learning.

**Typical session:** 10–30 minutes per level attempt; frequent resets; optional longer sessions for optimization-minded players.

---

## 5. Core gameplay

The game is a deterministic automation puzzle played on the **surface** of a multi-face Shape. Players build small factories on face-grids and route items through a portal-linked world.

- Gameplay rules, controls, component behaviors, and the three MVP levels’ teaching intent live in [GDD.md](./GDD.md).
- Implementation intent (Godot 4.x + **C#**, determinism, routing model) lives in [TECHNICAL_DESIGN.md](./TECHNICAL_DESIGN.md).

### 5.5 Instability and non-Euclidean behavior (product framing)

- **Portal relationships may change** based on **rotation state** and/or **level-specific rules** (the general product space for authored topology shifts).
- **MVP Level 3** uses the **traversal/rotation–triggered** instability contract in [GDD.md](./GDD.md) §7.3; engineering follows [TECHNICAL_DESIGN.md](./TECHNICAL_DESIGN.md) §6 (same trigger). Post-MVP levels may introduce additional trigger patterns while keeping determinism and authorability.
- Instability is **controlled** and **authorable**: level design defines when and how topology shifts.
- Design intent: **spatial inconsistency as puzzle fuel**, not confusion from broken simulation.

**Mitigation principle:** Treat **topology as an explicit mapping system** (graph / ruleset), not as implied consistency from raw 3D mesh adjacency alone.

---

## 6. Progression and difficulty

- **MVP (exactly three levels):** A fixed sequence that **defines technical and design conventions** for all future levels (level IDs; **win / goal** data—with **no failure mode** in MVP (§12.3); grid sizing per face; portal graph schema; how instability is authored).
  - **Level 1 — One-sided:** Single face of the two-sided square; **no portals**. Teaches core **automation**: grid placement, conveyors (and any MVP subset of splitters/mergers/processors), goals, deterministic simulation, reset.
  - **Level 2 — Portals:** Full **two-sided** Shape; introduces **static** portal connections between faces (and traversal/routing across the portal network).
  - **Level 3 — Spatial instability:** Same two-sided framework; introduces **controlled spatial instability** with the **MVP** trigger and framing in [GDD.md](./GDD.md) §7.3 (portal traversal / rotation); §5.5 describes the broader product space for future archetypes.
- **Post-MVP:** More levels, more faces and Shapes, richer item chains, and deeper instability—without changing the conventions proven in the three-level MVP.

---

## 7. Narrative and theme

- **Premise:** The player is an **immortal entity** pursuing **destruction of worlds** through engineered systems.
- **Level framing:** Each level represents a **world or structure** to be broken down.
- **End-state fantasy:** Deliver **destructive artifacts** (e.g. a **black hole–like payload**) to a **central core** through a **final portal**.
- **Delivery style:** **Minimal and thematic**—supports systemic escalation without dominating play.

---

## 8. Visual and UX direction

- **Low-production, high-clarity:** Modular geometry and asset packs acceptable; avoid art scope creep.
- **Readability first:** Items are **abstract**; silhouettes and motion communicate state.
- **Biomes:** Simple **color and texture** variation per face.
- **UI/UX priorities:** Clear grid feedback, portal labels or affordances as needed, readable simulation speed controls, forgiving reset flow.

---

## 9. Technical requirements

### 9.1 Core systems

| System | Requirement |
|--------|-------------|
| **Placement** | Grid-based placement mapped onto a multi-face 3D object |
| **Simulation** | **Deterministic** item flow through components and edges |
| **Routing graph** | Graph-based representation for paths across **dynamic** portal connections |
| **Topology** | Authorable mapping from logical connectivity to presentation (rotation, rules) |

### 9.2 Primary technical risk

**Risk:** Logic and UX drift when spatial relationships change—players or systems inferring wrong adjacency.

**Mitigation:**

- Separate **logical topology** (graph, rules) from **pure mesh adjacency**.
- Single source of truth for “where can an item go next?” at simulation tick boundaries.
- Extensive tooling/visualization for designers (portal state, graph preview) as soon as instability appears in builds.

### 9.3 Performance and scope (initial assumptions)

- MVP levels are **small**; prioritize correctness and debuggability over maximum entity count.
- Scale targets to be set after vertical slice (item counts, face counts, simulation tick rate).

---

## 10. MVP scope (must ship)

| Area | MVP |
|------|-----|
| **Shape** | One authored **two-sided square** (two opposing square faces, **no width** / zero-thickness presentation): the canonical MVP “polyhedron” for **face** and **grid** conventions |
| **Levels** | **Exactly three** handcrafted levels—no more, no fewer for MVP **ship scope**—each implementing one “pillar” of the teaching arc |
| **Level 1** | **One-sided** play: a **single** face only; **no portals**. Establishes automation and level-data conventions |
| **Level 2** | **Two-sided** play; **portals** between faces (**static** logical graph for that level) |
| **Level 3** | **Two-sided** play; **spatial instability** (authored, deterministic reconfiguration of logical connectivity; **MVP** trigger per [GDD.md](./GDD.md) §7.3 and §5.5) |
| **Items** | **Limited** item types |
| **Automation** | **Limited** component set (conveyors + key processing/routing building blocks) |
| **Loop** | Build → simulate → reset → **win / goal** evaluation per level (**no** MVP failure mode per §12.3) |

**MVP success criteria (suggested):**

- New players can complete all **three** MVP levels without undocumented tricks.
- Deterministic replays: same inputs → same outcomes.
- Designers can author a new level by configuring grids, portals, goals, and inventories without code changes (stretch if needed; document gap if not).
- The **three MVP levels** collectively demonstrate reusable **level format**, **portal authoring**, and **instability authoring** as specified in §6.

---

## 11. Post-MVP / expansion (not required for first ship)

- Procedural or parameterized **Shapes** beyond the two-sided square
- **Additional** levels and **richer** instability patterns (MVP already ships a controlled instability slice in **Level 3**)
- Deeper **production chains** and more item/component variety
- Richer **narrative framing** (still optional relative to systems)
- Additional win conditions and “anomaly” level archetypes

---

## 12. Open questions

1. **Control scheme:** Closed for MVP — controls are **fixed to the active face surface**, and portal traversal **rotates the Shape** to the destination face.
2. **Win conditions per level:** Closed for MVP — deliver the product **to the core** through a **final portal**.
3. **Failure modes:** Closed for MVP — **no failure mode** (levels are pure optimization/iteration).
4. **Save model:** Closed for MVP — **no save states** yet.
5. **Multiplayer:** Closed for MVP — none planned.

---

## 13. Document ownership

- This PRD is a living document for **product** decisions (audience, scope, success criteria).
- **Gameplay rules and level design** belong in [GDD.md](./GDD.md).
- **Engineering intent and architecture** belong in [TECHNICAL_DESIGN.md](./TECHNICAL_DESIGN.md).

---

## 14. Glossary

| Term | Definition |
|------|------------|
| **Ruin of the Shape** | The game (product title) |
| **Shape** | The mutable geometric structure whose surface is the playfield |
| **Face** | One planar (or projectable) grid region on the Shape with a biome theme |
| **Portal / door** | Authored connection between faces (and possibly extra-graph semantics) |
| **Portal network** | The graph of traversable / routable connections between faces |
| **Topology / mapping system** | Logical rules defining connectivity; may diverge from naive 3D adjacency |
| **Instability** | Designed, deterministic changes to portal connectivity—often described with **rotation state** and/or **level rules**; **MVP Level 3** uses the **portal traversal / rotation** trigger in [GDD.md](./GDD.md) §7.3 |
| **Two-sided square (MVP Shape)** | The MVP geometry: two opposing square faces with no meaningful thickness between them; the smallest multi-face Shape for establishing face + grid conventions |
| **One-sided (Level 1)** | MVP level configuration using only one face of the two-sided square, deferring portals |
