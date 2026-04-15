using Godot;

namespace RuinOfTheShape.LevelData;

[GlobalClass]
public partial class LevelDefinitionResource : Resource
{
    [Export] public string LevelId { get; set; } = "";
    [Export] public ShapeType ShapeType { get; set; } = ShapeType.TwoSidedSquare;

    [Export] public Godot.Collections.Array<FaceDefinitionResource> Faces { get; set; } = [];
    [Export] public Godot.Collections.Array<string> ActiveFaceIds { get; set; } = [];

    [Export] public Godot.Collections.Array<PortalDefinitionResource> Portals { get; set; } = [];
    [Export] public InstabilityTrigger InstabilityTrigger { get; set; } = InstabilityTrigger.None;

    [Export] public GoalDefinitionResource? Goal { get; set; }
}

