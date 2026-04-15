using Godot;

namespace RuinOfTheShape.LevelData;

[GlobalClass]
public partial class GoalDefinitionResource : Resource
{
    [Export] public GoalType GoalType { get; set; } = GoalType.CoreViaFinalPortal;
}

