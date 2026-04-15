#!/usr/bin/env python3
import json
import re
import sys


RISKY_CMD = re.compile(
    r"\b(dotnet|godot|python|git|msbuild|cmake|ninja|make)\b", re.IGNORECASE
)
IMPL_HINT = re.compile(
    r"\b(feature|enhancement|bugfix|bug fix|implement|build|create|add|fix)\b",
    re.IGNORECASE,
)
DOC_HINT = re.compile(
    r"(docs-gate-orchestrator|docs-implementation-gate|docs-source-truth-resolver)",
    re.IGNORECASE,
)


def main() -> int:
    raw = sys.stdin.read().strip()
    if not raw:
        print(json.dumps({"permission": "allow"}))
        return 0

    try:
        payload = json.loads(raw)
    except json.JSONDecodeError:
        print(json.dumps({"permission": "allow"}))
        return 0

    cmd = str(payload.get("command", ""))
    context = " ".join(
        str(payload.get(k, "")) for k in ("prompt", "message", "description", "input")
    )

    if not cmd or not RISKY_CMD.search(cmd):
        print(json.dumps({"permission": "allow"}))
        return 0

    impl_like = bool(IMPL_HINT.search(context))
    has_docs_ref = bool(DOC_HINT.search(context))

    if impl_like and not has_docs_ref:
        print(
            json.dumps(
                {
                    "permission": "ask",
                    "user_message": (
                        "Run docs governance first: use `docs-gate-orchestrator` "
                        "or `.cursor/skills/docs-implementation-gate/SKILL.md` "
                        "before risky implementation shell commands."
                    ),
                    "agent_message": "Shell command gated pending docs workflow.",
                }
            )
        )
        return 0

    print(json.dumps({"permission": "allow"}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
