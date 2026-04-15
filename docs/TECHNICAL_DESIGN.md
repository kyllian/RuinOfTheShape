# Technical Design: Ruin of the Shape — Implementation Intent

**Document type:** Technical Design / Implementation Intent (engineering counterpart to the PRD)  
**Status:** Draft — foundations and decisions-in-progress  
**Related:** [PRD.md](./PRD.md), [GDD.md](./GDD.md)  
**Last updated:** 2026-04-15

---

## 1. Purpose of this document

This document translates product requirements into **engineering intent**: what we will build, which constraints are non-negotiable, and where the PRD leaves room for iteration. It is **not** a full API spec or task list; those emerge during implementation.

**How to use it (if you are new to games):**

- The **PRD** answers *what the player experiences* and *why*.
- The **GDD** answers *what the game rules are* (controls, win condition, level-by-level teaching intent).
- This document answers *how we structure software* so that experience is achievable, testable, and shippable in **Godot**.
- Game projects still benefit from the same habits as other software: single sources of truth, deterministic core logic where promised, separation of data from presentation, and thin glue at the engine boundary.
- For shared term definitions (MVP, topology, portal graph, Godot **Scene** / **Resource**, documentation layers), see [READING_GUIDE.md](./READING_GUIDE.md).

---

## 2. Engine and toolchain

| Decision | Choice | Rationale |
|----------|--------|-----------|
| **Engine** | **Godot 4.x** (LTS or current stable — pin exact version at project bootstrap) | Strong 3D tooling for a small team, open source, excellent iteration speed for gameplay prototypes. |
| **Primary scripting** | **C#** | Strong typing and tooling; good fit for deterministic simulation and topology code; keeps non-visual logic explicit and testable. |
| **Version control** | Git; ignore Godot import/cache artifacts via `.gitignore` | Standard practice; keeps diffs readable. |
| **Build targets** | **MVP:** desktop first (Windows primary); others as needed | Reduces platform-specific risk until core loop is proven. |

*Action:* When you create the Godot project, record the **exact engine version** and **renderer** (Forward+ vs Compatibility) here and lock them for the MVP unless a concrete need forces a change.

**Starter project status (bootstrap):**

- **Pinned engine build:** `4.6.2.stable.mono` (C# / Mono).
- **Hybrid level schema decision:** Canonical runtime schema is C# `Resource` classes; JSON import/export can be layered later without changing runtime APIs.

---

## 3. Architectural principles (from PRD → code)

These are implementation-level commitments that mirror PRD §9.

| Principle | Meaning for implementation |
|-----------|----------------------------|
| **Deterministic simulation** | Item flow and processing outcomes must not depend on frame timing, floating-point order, or unordered iteration where order affects results. Use a fixed **simulation tick** (or explicit ordering rules) for factory logic. |
| **Logical topology ≠ mesh adjacency** | **Routing and traversal** use an **authorable graph** (faces, edges/portals, rules). The 3D mesh exists for presentation and input; it does not silently define “what connects to what.” MVP **Level 1** has no portal edges; **Level 2** uses a **static** graph; **Level 3** adds **authored instability** triggered at **portal traversal / rotation** (graph or rules change deterministically). |
| **Single routing truth at tick boundaries** | At each simulation step, “where can this item go next?” must resolve from one well-defined model (graph + rules), not multiple competing heuristics. |
| **Data-driven levels (stretch for MVP)** | Prefer level definitions (grids, portals, goals, inventories) as **data** loaded at runtime. If MVP ships with hard-coded levels, document that gap and keep code structured so data-driven loading is a natural next step. |

---

## 4. Conceptual module map (intended responsibilities)

Rough boundaries — names will map to folders/autoloads as the repo grows:

| Module / layer | Responsibility |
|----------------|------------------|
| **Level / Shape definition** | Describes faces, grids, biome tags, portal graph, win rules (MVP has **no failure mode**), starting inventories. |
| **Topology / routing model** | Graph operations: valid edges, portal state (**static** in Level 2; **rule-driven / stateful** in Level 3, updated deterministically on traversal/rotation events). Consumed only by simulation and high-level traversal checks — not by raw mesh queries. |
| **Grid & placement** | Maps logical cell IDs ↔ stable coordinates on each face; validates build/remove rules. |
| **Factory simulation** | Deterministic tick: conveyors, splitters, mergers, transformers, assemblers; item entities or aggregate flows (decision TBD in vertical slice). |
| **Presentation (3D)** | Shape mesh, camera, face highlighting, item visuals, VFX — **driven by** simulation/state, not authoritative over rules. |
| **Player interaction** | Input, rotate Shape, build mode, run/pause/step, reset — translates UI/input into commands validated by placement/simulation. |
| **UI** | HUD, goals, simulation speed, reset affordances; minimal narrative UI per PRD. |

*Game-dev note:* Keep **simulation** able to run without rendering (or behind a “headless” test hook later). That pattern makes bugs reproducible and aligns with determinism goals.

---

## 5. Godot-oriented mapping (high level)

Not prescriptive until prototyped, but useful as a mental model:

- **Scenes:** Separate **Level** (loads data), **ShapeView** (3D + camera), **SimulationController** (tick + state), **UI** layers.
- **Autoloads (singletons):** Candidates for **event bus**, **game settings**, or a thin **simulation facade** — avoid turning singletons into a “god object”; prefer explicit dependencies where it stays readable.
- **Physics:** PRD non-goals exclude heavy rigid-body item physics for MVP. Prefer **logical movement on the grid graph** with visuals that **follow** simulated positions.

**Controls (MVP intent):** Implement the face-local control scheme specified in [GDD.md](./GDD.md) (controls fixed to active face; traversal rotates to destination). Engineering intent is to keep this separate from simulation: rotation is a presentation/input alignment step, not a source of simulation truth.

---

## 6. MVP technical scope (engineering checklist)

Aligned with PRD §6 and §10:

| Area | Implementation intent |
|------|----------------------|
| Shape | One authored **two-sided square**: **two** opposing square faces, **zero / nominal thickness** in the mesh (same “square” extruded to no meaningful width). Grids authored per face; shared edge topology matches the PRD. |
| Levels | **Three** levels total—each must exercise the **same level-load path** and establish **conventions** (IDs, schema, **win / goal** fields—MVP has **no failure mode** per [PRD.md](./PRD.md) §12.3, per-face grid size). |
| Level 1 | **Single active face** (one-sided); **no** portal edges in the routing graph. |
| Level 2 | **Both** faces active; **static** portal graph between them. |
| Level 3 | **Both** faces; **instability** is triggered by **portal traversal / rotation** and implemented as deterministic, authorable changes to logical connectivity (or equivalent rules) without breaking single routing truth at tick boundaries. |
| Simulation | Deterministic tick; core components: conveyors, split/merge, transform, assemble — exact recipes **TBD** with design. |
| Progress | **Win / goal** evaluation per level data (no MVP lose branch); **reset** restores initial state reliably. |
| Content pipeline | **Three** handcrafted levels; ideal path is **data files** + editor tooling; minimum path is level scenes or resources with a schema that can grow post-MVP. |

**Routing code shape:** Implement the portal graph and “active edge set” so Level 1 is simply **no edges** / single face, Level 2 is **fixed** edges, and Level 3 reuses the same APIs with **dynamic** (but deterministic) edge sets or rule hooks—avoid a one-off for instability.

---

## 7. Primary technical risk (PRD §9.2) — engineering response

**Risk:** Players or code infer wrong adjacency when the world “looks” connected in 3D but the **logical** graph says otherwise.

**Mitigations to bake in early:**

- **Debug visualization** of the portal graph (edges, labels, active/inactive if/when dynamics exist).
- **No silent fallbacks** — if a connection is invalid, fail visibly in debug builds.
- **Tests** on the routing model (even simple unit tests on graph + tick rules) before complex visuals.

---

## 8. Performance posture (PRD §9.3)

- MVP prioritizes **correctness** and **debuggability** over entity count.
- Set **budgets** after vertical slice: max items, tick rate, target frame time. Until then, avoid premature optimization; do avoid accidental O(n²) in routing on every frame.

---

## 9. Open technical questions (to close during prototype)

1. **Simulation representation:** Discrete item entities per cell vs. flow volumes — affects memory, UX clarity, and save/replay.
2. **Camera / controls tuning:** Controls are fixed to the active face surface; remaining question is tuning: camera distance, FOV, and readability affordances during the rotate-to-destination transition.
3. **Level format:** Godot **Resource** (.tres) + custom Resource classes vs. JSON — trade editor ergonomics vs. diff-friendliness.

---

## 10. Document ownership

- Update this doc when architecture **intent** changes (e.g., determinism strategy, level data schema).
- Keep detailed schemas, recipes, and tuning in focused addenda or inline code docs as those stabilize.

---

## 11. Next steps (suggested order for a first-time game implementer)

1. **Bootstrap** Godot project; pin version; use **C#**.
2. **Prototype** the **two-sided square** in 3D (two grids) but ship **Level 1** with **one face** wired in data (second face dormant or omitted from play).
3. **Implement** the **routing graph** as pure data + tests: empty / single-face (Level 1), then **static** two-face graph (Level 2), then **stateful** edge sets (Level 3).
4. **Add** deterministic **simulation tick** with one moving “item” and one conveyor rule; expand to MVP component subset.
5. **Layer** placement, reset, win conditions for **three** levels, then UI.

This sequence validates the PRD commitments (topology stages + determinism) in the same order players will see them.
