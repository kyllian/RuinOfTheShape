# Docs Implementation Gate Examples

## Example 1: New Feature

### Request
Add a new portal-link preview mode in the map view.

### Gate execution
1. Run `docs-source-truth-resolver`:
   - Confirm authority from `docs/ARCHITECTURE/GRAPHICS_PIPELINE.md` and any active ADRs.
2. Implement feature in code.
3. Run `godot-mcp-playtest` (MCP `run_project` + `get_debug_output`) so the user can manually verify.
4. Run `docs-change-policy-enforcer`:
   - Graphics behavior changed, so update at least one required graphics doc target.
5. Run `docs-phase-planning-maintainer`:
   - Update relevant `docs/ROADMAP/subplans/*.md` and active `docs/EXECUTION/` checklist entries.
6. Run `docs-adr-decision-recorder` only if the feature changes a durable rendering assumption.
7. Reconcile lower-precedence docs and report closeout.

### Expected report shape
- Change type: `feature`
- Authoritative docs consulted: ADR + architecture files
- Documentation updates made: graphics pipeline + roadmap/execution entries
- ADR updated/created: conditional

## Example 2: Enhancement

### Request
Improve existing route readability with deterministic color palette rules.

### Gate execution
1. Run `docs-source-truth-resolver`:
   - Confirm graphics readability constraints and precedence.
2. Implement enhancement in code.
3. Run `godot-mcp-playtest` when the change affects in-game visuals or simulation.
4. Run `docs-change-policy-enforcer`:
   - Update docs if behavior/comprehension guidance changed.
5. Run `docs-phase-planning-maintainer` if milestone sequencing or current checklists changed.
6. Run `docs-adr-decision-recorder` only if this introduces a durable architecture decision.
7. Reconcile and report.

### Expected report shape
- Change type: `enhancement`
- Skills applied: source-truth + change-policy (phase/ADR conditional)
- Reconciliation status: `complete`

## Example 3: Bugfix

### Request
Fix incorrect portal state rendering during rapid topology updates.

### Gate execution
1. Run `docs-source-truth-resolver`:
   - Identify authoritative portal/topology state behavior definitions.
2. Implement bugfix and verify behavior.
3. Run `godot-mcp-playtest` to confirm the fix in a running build.
4. Run `docs-change-policy-enforcer`:
   - If fix aligns code to existing docs, no doc content change may be required.
   - If fix changes expected behavior, update docs in the same task.
5. Run `docs-adr-decision-recorder` only if root-cause fix alters durable assumptions.
6. Reconcile and report.

### Expected report shape
- Change type: `bugfix`
- Documentation updates made: none or explicit list with reasons
- ADR updated/created: usually `no`, unless durable decision changed
