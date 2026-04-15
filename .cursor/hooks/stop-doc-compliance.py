#!/usr/bin/env python3
import json
import re
import sys


REQUIRED_SIGNALS = [
    re.compile(r"authoritative docs consulted", re.IGNORECASE),
    re.compile(r"documentation updates made|docs updated", re.IGNORECASE),
    re.compile(r"adr (updated|created|status)", re.IGNORECASE),
    re.compile(r"reconciliation status", re.IGNORECASE),
]


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

    text = " ".join(
        str(payload.get(k, "")) for k in ("response", "message", "output", "content")
    )
    if not text:
        print(json.dumps({}))
        return 0

    missing = [i for i, rx in enumerate(REQUIRED_SIGNALS, start=1) if not rx.search(text)]
    if missing:
        followup = (
            "Before finalizing, include docs compliance summary: authoritative docs consulted, "
            "documentation updates made, ADR updated/created status, and reconciliation status."
        )
        print(json.dumps({"followup_message": followup}))
        return 0

    print(json.dumps({}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
