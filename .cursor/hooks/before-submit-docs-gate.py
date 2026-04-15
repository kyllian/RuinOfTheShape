#!/usr/bin/env python3
import json
import re
import sys


def main() -> int:
    raw = sys.stdin.read().strip()
    payload = {}
    if raw:
        try:
            payload = json.loads(raw)
        except json.JSONDecodeError:
            print(json.dumps({}))
            return 0

    text = " ".join(
        str(payload.get(k, "")) for k in ("prompt", "message", "input", "text")
    ).lower()
    if not text:
        print(json.dumps({}))
        return 0

    trigger = re.search(
        r"\b(build|implement|create|add|enhance|improve|bugfix|bug fix|fix)\b", text
    )
    if not trigger:
        print(json.dumps({}))
        return 0

    msg = (
        "Documentation gate reminder: run `.cursor/skills/docs-implementation-gate/"
        "SKILL.md` or use `docs-gate-orchestrator` before/around implementation "
        "to resolve authority and same-task documentation updates."
    )
    print(json.dumps({"additional_context": msg}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
