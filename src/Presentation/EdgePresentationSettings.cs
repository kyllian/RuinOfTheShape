using Godot;

namespace RuinOfTheShape.Presentation;

/// <summary>
/// Tunable edge post-process parameters for presentation iteration without recompiles.
/// </summary>
[GlobalClass]
public partial class EdgePresentationSettings : Resource
{
    public enum QualityTierKind
    {
        Tier0 = 0,
        Tier1 = 1,
        Tier2 = 2
    }

    [Export] public QualityTierKind QualityTier { get; set; } = QualityTierKind.Tier1;

    [Export(PropertyHint.Range, "0,0.35,0.001")]
    public float EdgeThreshold { get; set; } = 0.075f;

    [Export(PropertyHint.Range, "0,1.5,0.001")]
    public float EdgeIntensity { get; set; } = 0.55f;

    [Export(PropertyHint.Range, "0,1,0.01")]
    public float NeighborSoften { get; set; } = 0.38f;

    [Export] public Color EdgeTint { get; set; } = new(0.02f, 0.02f, 0.045f, 1f);

    public float TierIndex => (float)(int)QualityTier;
}
