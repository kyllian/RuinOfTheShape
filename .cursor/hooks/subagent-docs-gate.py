#!/usr/bin/env python3
import json
import re
import sys


DOC_TERMS = (
    "docs-gate-orchestrator",
    "docs-implementation-gate",
    "docs-discovery",
    "docs-policy-auditor",
    "docs-phase-planner",
    "docs-adr-maintainer",
    "godot-playtest",
    "godot-mcp-playtest",
)

IMPL_HINT = re.compile(
    r"\b(feature|enhancement|bugfix|bug fix|implement|build|create|add|fix)\b",
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

    text = " ".join(
        str(payload.get(k, "")) for k in ("prompt", "description", "message", "input")
    )
    lower = text.lower()

    has_impl_intent = bool(IMPL_HINT.search(text))
    has_docs_gate = any(term in lower for term in DOC_TERMS)

    if has_impl_intent and not has_docs_gate:
        print(
            json.dumps(
                {
                    "permission": "ask",
                    "user_message": (
                        "This subagent task looks like implementation work. "
                        "Use `docs-gate-orchestrator` or reference "
                        "`.cursor/skills/docs-implementation-gate/SKILL.md` first."
                    ),
                    "agent_message": (
                        "Subagent start blocked pending docs governance path."
                    ),
                }
            )
        )
        return 0

    print(json.dumps({"permission": "allow"}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
