# Reading guide: documentation for newcomers

## Purpose

Help readers who are new to **game development** (or to this repository) find the right documents first, understand how the docs are organized, and decode common terms and shorthand used across the project.

## Scope / non-scope

**In scope:** What each documentation layer is for, suggested reading orders, a glossary of recurring terms, and decoding of **`VSS-AC##`** checklist identifiers used in graphics execution docs.

**Non-scope:** Step-by-step Godot tutorials, C# language teaching, or replacing the technical detail in architecture documents and ADRs (those stay authoritative; this guide explains how to read them).

## Inputs / assumptions

- You can open Markdown files in the repo and follow links.
- Product and engineering terms in this project follow the definitions in the glossary below; if a document uses a term differently, treat **ADR** and **Architecture** layers as the tie-breaker (see [ARCHITECTURE/DOCS_SYSTEM.md](ARCHITECTURE/DOCS_SYSTEM.md)).

## Plan or design

### If you are new to game development

Game projects use the same ideas as other software (requirements, design, testing), but you will see extra vocabulary for **player experience**, **levels**, **rendering**, and **engines**. This repo keeps “what we promise the player” separate from “how we structure code,” and keeps long-lived decisions separate from this week’s checklist. Jargon is normal in engineering docs; use this guide and the glossary when a word is unclear.

### How documentation is layered (mini map)

| Folder | Plain-language role | Typical update frequency |
|--------|---------------------|---------------------------|
| [VISION/](VISION/) | **Why** the game should feel and look a certain way; principles and goals. | When design direction changes |
| [ARCHITECTURE/](ARCHITECTURE/) | **What** the systems are supposed to do and what is ruled out; the technical contract. | When technical intent or constraints change |
| [ROADMAP/](ROADMAP/) | **When** work is sequenced; phases, milestones, and stream-level subplans. | At phase boundaries or reprioritization |
| [EXECUTION/](EXECUTION/) | **What we are doing right now**: checklists, evidence, blockers. | Often during active work |
| [ADR/](ADR/) | **Durable decisions** and their tradeoffs (“we chose A over B because…”). | When a decision is made or reversed |

If two documents disagree, follow the **source-of-truth order** in [ARCHITECTURE/DOCS_SYSTEM.md](ARCHITECTURE/DOCS_SYSTEM.md) (ADR first when a decision exists, then Architecture, Vision, Roadmap, Execution).

### Suggested reading orders

**A) I want to understand the game (player-facing intent)**  
1. [PRD.md](PRD.md) — product goals, MVP scope, audience  
2. [GDD.md](GDD.md) — rules, controls, win condition, level teaching order  
3. [TECHNICAL_DESIGN.md](TECHNICAL_DESIGN.md) — how engineering intends to support that in Godot + C# (skim if you are not implementing)

**B) I want to understand how we build and ship features**  
1. [ARCHITECTURE/DOCS_SYSTEM.md](ARCHITECTURE/DOCS_SYSTEM.md)  
2. [ROADMAP/IMPLEMENTATION_CADENCE_AND_DOD.md](ROADMAP/IMPLEMENTATION_CADENCE_AND_DOD.md)  
3. Current execution doc for the active work (for graphics style baseline: [EXECUTION/GRAPHICS_PHASE_B_EXECUTION.md](EXECUTION/GRAPHICS_PHASE_B_EXECUTION.md))

**C) I want to understand graphics and rendering work**  
1. [VISION/GRAPHICS_VISION.md](VISION/GRAPHICS_VISION.md)  
2. [ARCHITECTURE/GRAPHICS_PIPELINE.md](ARCHITECTURE/GRAPHICS_PIPELINE.md)  
3. [ROADMAP/GRAPHICS_ROADMAP.md](ROADMAP/GRAPHICS_ROADMAP.md)  
4. Relevant [ROADMAP/subplans/](ROADMAP/subplans/) file for the stream you care about  
5. [VISION/AI_ASSISTED_GRAPHICS_PRINCIPLES.md](VISION/AI_ASSISTED_GRAPHICS_PRINCIPLES.md) if using AI tools on graphics tasks

### Glossary

| Term | Plain-language meaning |
|------|-------------------------|
| **MVP** | Minimum viable product: the smallest version we plan to ship first (here: three levels, two-sided square Shape, scoped features). |
| **PRD** | Product requirements document: goals, scope, audience, success criteria. |
| **GDD** | Game design document: concrete rules, controls, progression, level intent. |
| **ADR** | Architecture decision record: a short, durable log of an important technical or process decision and its consequences. |
| **Phase** | A chunk of roadmap work with a defined outcome (for example “visual style baseline” before “edge rendering”). |
| **Phase gate** | A checkpoint: we do not treat the phase as complete until listed criteria and evidence are satisfied. |
| **Acceptance criteria** | Testable or reviewable conditions that must be true for work to count as done. |
| **Subplan** | A roadmap-level plan for one stream of work (edges, portals, performance, etc.); less day-to-day than execution docs. |
| **Execution doc** | Day-to-day checklist, evidence links, and blockers for the current implementation push. |
| **Source of truth** | The document (or layer) that wins when descriptions conflict; see precedence in DOCS_SYSTEM. |
| **Deterministic** | The same inputs produce the same results; simulation outcomes do not depend on frame rate luck or random ordering tricks. |
| **Simulation** | The logical game step that moves items, applies machines, and resolves rules—often separate from drawing frames. |
| **Topology** | The **logical** map of what connects to what (faces, portals, rules)—not “whatever the 3D model looks like it touches.” |
| **Portal graph** | The data structure of which face locations connect through portals under current rules. |
| **Instability** | Authored puzzle behavior where connectivity or rules can change in a controlled way (not random bugs). **MVP Level 3** uses the traversal/rotation trigger in [GDD.md](GDD.md) §7.3. |
| **Routing** | Where items are allowed to travel next on the grid/graph. |
| **Adjacency** | “Next to” in a gameplay sense; may follow topology rules, not only raw 3D nearness. |
| **Render pass** | One drawing step in the pipeline (for example: draw solid shapes, then outlines, then combine). |
| **Composite / composite pass** | A step that **combines** images from earlier passes into one picture before UI. |
| **Buffer** | GPU memory holding intermediate images or data used between passes. |
| **Quality tier** | A preset graphics quality level (higher = more effects; lower = faster or safer on weak hardware). |
| **Fallback** | A simpler or cheaper behavior when performance or artifacts fail checks—readability usually wins over fancy effects. |
| **Godot Scene** | A tree of nodes (objects) saved as a scene file; levels and UI are often separate scenes. |
| **Autoload** | A Godot singleton script available everywhere; useful sparingly for global services. |
| **Resource** | In Godot, a data object (often saved as a `.tres` file) that configures gameplay or presentation. |

### Visual style acceptance checks (VSS-AC codes)

**`VSS`** = visual style system. **`AC`** = acceptance check. The numbers match the objective checklist in [EXECUTION/GRAPHICS_PHASE_B_EXECUTION.md](EXECUTION/GRAPHICS_PHASE_B_EXECUTION.md) and the evidence table in [EXECUTION/GRAPHICS_PHASE_B_EVIDENCE.md](EXECUTION/GRAPHICS_PHASE_B_EVIDENCE.md).

| ID | Short label | What it means in plain language |
|----|-------------|----------------------------------|
| `VSS-AC01` | Still, distinct types | Players can tell important object **kinds** apart when nothing is moving. |
| `VSS-AC02` | Motion, distinct types | Players can still tell those kinds apart **while** things move. |
| `VSS-AC03` | States without labels | **Active**, **blocked**, and **unstable** states read clearly **without** reading text on screen. |
| `VSS-AC04` | Orientation survives rotation | After the Shape/camera orientation changes, layout and important cues still make sense. |
| `VSS-AC05` | Palette discipline | Colors used for gameplay come from the **approved palette tokens**, not one-off picks. |
| `VSS-AC06` | Contrast | Pairs of colors that must not be confused meet a **contrast floor**. |
| `VSS-AC07` | Consistency across levels | The same visual rules apply across the MVP validation scenes/levels. |
| `VSS-AC08` | No bespoke art pipeline | Baseline look ships **without** a custom art production pipeline dependency. |

## Acceptance criteria

- A newcomer can pick a reading path (game vs build vs graphics) without reading every file first.
- Terms in the glossary cover the majority of unexplained shorthand in PRD, GDD, roadmap, execution, and pipeline docs.
- `VSS-AC##` identifiers are defined once here and linked from execution docs.

## Risks and fallback

- **Risk:** The glossary oversimplifies a technical term. **Fallback:** Follow the link to Architecture or the relevant ADR for the precise meaning.
- **Risk:** This guide drifts out of date. **Fallback:** Update the change log when phases or checklist IDs change.

## Open questions

- None tracked here; file-level open questions stay in their owning documents.

## Change log

- 2026-04-15: Initial reading guide and glossary added for newcomer onboarding.
- 2026-04-15: Updated graphics execution/evidence links to alphabetical phase filenames (`GRAPHICS_PHASE_B_*`).
- 2026-04-15: Glossary **Instability** row aligned with PRD/GDD (MVP Level 3 trigger pointer).
