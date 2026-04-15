#!/usr/bin/env python3
"""
After game-facing file edits, inject agent context to run Godot via MCP before
claiming an implementation task is done. Hooks cannot invoke MCP themselves.
"""
from __future__ import annotations

import json
import re
import sys
import time
from pathlib import Path

THROTTLE_SECONDS = 90.0
STATE_PATH = Path(__file__).resolve().parent / ".godot_playtest_nudge_ts"

# Paths/extensions that typically require a playable smoke check
GAME_EDIT_HINT = re.compile(
    r"(^|[\\/])(src|scenes|assets|levels)([\\/]|$)|"
    r"([\\/]|^)project\.godot$|"
    r"\.(cs|gd|gdshader|tscn|tres)$",
    re.IGNORECASE,
)


def _tool_paths(payload: dict) -> str:
    bits: list[str] = []
    inp = payload.get("tool_input")
    if isinstance(inp, dict):
        for key in ("path", "file_path", "target_file", "old_string", "new_string"):
            val = inp.get(key)
            if isinstance(val, str):
                bits.append(val)
    out = payload.get("tool_output")
    if isinstance(out, str):
        bits.append(out)
    return "\n".join(bits)


def _should_throttle() -> bool:
    try:
        if not STATE_PATH.is_file():
            return False
        last = float(STATE_PATH.read_text(encoding="utf-8").strip())
    except (OSError, ValueError):
        return False
    return (time.monotonic() - last) < THROTTLE_SECONDS


def _mark_throttle() -> None:
    try:
        STATE_PATH.write_text(str(time.monotonic()), encoding="utf-8")
    except OSError:
        pass


def main() -> int:
    raw = sys.stdin.read().strip()
    if not raw:
        print(json.dumps({}))
        return 0
    try:
        payload = json.loads(raw)
    except json.JSONDecodeError:
        print(json.dumps({}))
        return 0

    tool_name = str(payload.get("tool_name", ""))
    if tool_name.startswith("MCP:"):
        print(json.dumps({}))
        return 0

    if tool_name not in {"Write", "StrReplace"}:
        print(json.dumps({}))
        return 0

    haystack = _tool_paths(payload)
    if not haystack or not GAME_EDIT_HINT.search(haystack):
        print(json.dumps({}))
        return 0

    if _should_throttle():
        print(json.dumps({}))
        return 0

    _mark_throttle()
    ctx = (
        "Godot MCP playtest gate: this edit touches game-facing files. Before you finish the "
        "implementation task, call the Godot MCP `run_project` tool with `projectPath` set to the "
        "repo root, then `get_debug_output` with `{}`, and give the user a brief log summary so they "
        "can manually test. Follow `.cursor/skills/godot-mcp-playtest/SKILL.md`."
    )
    print(json.dumps({"additional_context": ctx}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
