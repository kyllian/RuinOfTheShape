using RuinOfTheShape.LevelData;

namespace RuinOfTheShape.Topology;

public sealed class TopologyResolver : ITopologyResolver
{
    public void Initialize(LevelDefinitionResource level)
    {
        // Scaffold only: level-specific edge sets are introduced later.
        _ = level;
    }
}

