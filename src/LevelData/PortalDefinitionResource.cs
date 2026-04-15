using Godot;

namespace RuinOfTheShape.LevelData;

[GlobalClass]
public partial class PortalDefinitionResource : Resource
{
    [Export] public string PortalId { get; set; } = "";
    [Export] public string FromFaceId { get; set; } = "";
    [Export] public string ToFaceId { get; set; } = "";

    // Optional: allow designers to tag the “final portal to core”.
    [Export] public bool IsFinalPortalToCore { get; set; } = false;
}

