---
name: godot-mcp-playtest
description: Runs the Godot project via MCP after implementation so the user can manually smoke-test. Use after feature/enhancement/bugfix work that affects gameplay or engine-facing assets.
---

# Godot MCP playtest

## When to use

Use after you implement or materially change:

- C# / GDScript / shaders
- Scenes (`.tscn`), resources that affect play (`.tres` under `levels/` or similar), `project.godot`
- Any change where functional verification requires launching the game

Skip when the change is **docs-only**, **tooling-only**, or **strictly non-runtime** (e.g. comments with no behavior change) and you are confident no playtest adds value.

## Preconditions

- Godot MCP is configured in `.cursor/mcp.json` under the `godot` server entry (`GODOT_PATH`, etc.).
- Resolve the MCP **server identifier** Cursor uses for `call_mcp_tool` (commonly `project-0-<WorkspaceFolderName>-godot`). If unsure, use the Godot server shown in the IDE MCP list for this workspace.

## Steps

1. **`run_project`**
   - `projectPath`: absolute path to this repository root (the folder that contains `project.godot`).
   - `scene`: omit unless the user asked to boot a specific scene.

2. **`get_debug_output`**
   - Arguments: `{}`.
   - Summarize recent lines and any captured errors for the user.

3. **Already running / restart**
   - If the MCP indicates the project is already running and you need a clean boot, call **`stop_project`** when that tool exists on the same server, then `run_project` again.

4. **Tell the user**
   - State that the game was launched via MCP and the window should be available for manual testing.

## Integration with the docs implementation gate

When `.cursor/skills/docs-implementation-gate/SKILL.md` applies, treat this skill as part of **implementation verification**: run the Godot MCP playtest before you close out the task, alongside documentation obligations from that gate.
