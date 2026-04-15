# Game Design Document (GDD): Ruin of the Shape

**Document type:** Game Design Document (GDD)  
**Status:** Draft — gameplay rules and level design conventions  
**Related:** [PRD.md](./PRD.md), [TECHNICAL_DESIGN.md](./TECHNICAL_DESIGN.md)  
**Last updated:** 2026-04-15  
**New to game-dev vocabulary?** See [READING_GUIDE.md](./READING_GUIDE.md) for a glossary (MVP, PRD vs GDD, determinism, and more).

---

## 1. Purpose and boundaries

This document specifies **gameplay rules and player experience details** that are too granular for the PRD and not appropriate for the technical design.

- **PRD**: product goals, MVP scope, success criteria, audience.
- **GDD (this doc)**: mechanics, controls, win conditions, level-by-level teaching intent, component behaviors at a design level.
- **Technical design**: architecture and implementation intent in **Godot 4.x + C#** (data model, determinism, routing).

---

## 2. MVP decisions (locked)

### 2.1 MVP Shape

- The MVP Shape is a **two-sided square**: two opposing square faces (front/back) with **no meaningful thickness**.
- Each face is a **grid**. The “two sides” are distinct faces with their own grids and authored content.

### 2.2 MVP content count

- MVP ships with **exactly three** levels (no more, no fewer).

---

## 3. Player controls and camera (MVP)

### 3.1 Face-local control frame

- Player controls are **fixed to the surface** of the currently active face.
- The player is always “on” a face; movement and interaction are constrained to that face’s surface grid.

### 3.2 Portal traversal and rotation-to-destination

- Traversing a portal moves the player to the destination face.
- On portal traversal, the Shape **rotates** so the destination face becomes the **active surface** (the camera/control frame reorients with it).
- Rotation is also used for readability/inspection, but the default expectation is that traversal results in a clear “you are now on this face” orientation.

---

## 4. Core loop (MVP)

- Observe goals → build/edit → simulate → reset → refine.
- Simulation is deterministic (player-facing guarantee).

---

## 5. Win condition and progression framing (MVP)

### 5.1 Win condition

- The objective is to deliver the target product **to the core** through a **final portal**.
- The core is a level-authored endpoint (it may be visually central or conceptually central; design treats it as a special destination).

### 5.2 Failure

- MVP has **no failure mode**. The loop relies on iteration and reset, not punishments.

### 5.3 Saves

- MVP has **no save states** yet.

---

## 6. Automation components (MVP subset)

The exact recipes and throughput are tuning details; this section defines the intended component *roles*.

- **Conveyors**: move items along grid paths.
- **Splitters / mergers**: deterministic fan-out / fan-in routing patterns.
- **Transformers**: convert inputs to outputs (recipes authored per level or global set).
- **Assemblers**: combine multiple items into an output (recipes authored per level or global set).

---

## 7. MVP level design (exactly three levels)

These three levels exist to establish **technical and design conventions** for all future content.

### 7.1 Level 1 — One-sided automation

- Active playfield is **one face only** of the two-sided square.
- **No portals**.
- Teaches: placement, conveyors (and minimal routing/processing), deterministic simulation, reset, and reading goals.

### 7.2 Level 2 — Portals

- Uses the full **two-sided** Shape.
- Introduces **static portals** connecting the faces for player traversal and item routing.
- Teaches: portal literacy, routing across faces, and maintaining readable layouts across rotations.

### 7.3 Level 3 — Spatial instability (traversal-triggered)

- Uses the full **two-sided** Shape.
- Introduces **spatial instability** as an authored, deterministic rule.
- **Trigger**: instability is triggered by **portal traversal / rotation** (i.e., “after traversing, the mapping can change”).
- Teaches: players must reason about connectivity as **stateful** and learn to use the instability as a puzzle mechanic.

---

## 8. Post-MVP expansion targets (design-facing)

- More Shapes (beyond two-sided square), more levels, richer production chains.
- Additional instability archetypes (still deterministic and authorable).

