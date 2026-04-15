using RuinOfTheShape.Core;

namespace RuinOfTheShape.LevelData;

public interface ILevelLoader
{
    Result TryLoad(string resourcePath);
}

