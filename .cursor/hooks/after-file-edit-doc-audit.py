#!/usr/bin/env python3
import json
import re
import sys


CODE_PATH_HINT = re.compile(r"(src/|scenes/|assets/|\.gd$|\.cs$|\.gdshader$)", re.IGNORECASE)
DOC_PATH_HINT = re.compile(r"(^|/)docs/|\.cursor/rules/|\.cursor/skills/", re.IGNORECASE)


def extract_paths(payload: dict) -> str:
    bits = []
    for key in ("path", "file_path", "target_file", "content", "diff", "output"):
        val = payload.get(key, "")
        if isinstance(val, str):
            bits.append(val)
    return "\n".join(bits)


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

    haystack = extract_paths(payload)
    code_touched = bool(CODE_PATH_HINT.search(haystack))
    docs_touched = bool(DOC_PATH_HINT.search(haystack))

    if code_touched and not docs_touched:
        ctx = (
            "Doc audit: implementation-related file edits detected. Confirm whether same-task "
            "documentation updates are required in `docs/ARCHITECTURE/`, `docs/ROADMAP/`, "
            "`docs/EXECUTION/`, or `docs/ADR/`. Apply `.cursor/skills/docs-change-policy-enforcer/SKILL.md`."
        )
        print(json.dumps({"additional_context": ctx}))
        return 0

    print(json.dumps({}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
