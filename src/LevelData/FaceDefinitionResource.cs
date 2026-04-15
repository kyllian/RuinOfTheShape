using Godot;

namespace RuinOfTheShape.LevelData;

[GlobalClass]
public partial class FaceDefinitionResource : Resource
{
    [Export] public string FaceId { get; set; } = "";
    [Export] public int GridWidth { get; set; } = 8;
    [Export] public int GridHeight { get; set; } = 8;
}

