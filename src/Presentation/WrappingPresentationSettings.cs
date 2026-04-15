using Godot;

namespace RuinOfTheShape.Presentation;

/// <summary>
/// Tunable wrapping and continuity cue settings for Phase D iteration without recompiles.
/// </summary>
[GlobalClass]
public partial class WrappingPresentationSettings : Resource
{
    public enum CueProfileKind
    {
        Disabled = 0,
        Balanced = 1,
        Emphasized = 2
    }

    [Export] public CueProfileKind CueProfile { get; set; } = CueProfileKind.Balanced;

    [Export(PropertyHint.Range, "0,2,1")]
    public int RepeatDepth { get; set; } = 1;

    [Export(PropertyHint.Range, "0.15,1,0.01")]
    public float GhostAlpha { get; set; } = 0.32f;

    [Export(PropertyHint.Range, "0.3,1.25,0.01")]
    public float ContinuityIntensity { get; set; } = 0.65f;

    [Export(PropertyHint.Range, "0.6,2.5,0.05")]
    public float MarkerHeight { get; set; } = 1.5f;

    public int EffectiveRepeatDepth => CueProfile switch
    {
        CueProfileKind.Disabled => 0,
        CueProfileKind.Balanced => Mathf.Clamp(RepeatDepth, 0, 1),
        CueProfileKind.Emphasized => Mathf.Clamp(RepeatDepth + 1, 0, 2),
        _ => Mathf.Clamp(RepeatDepth, 0, 1)
    };
}
