using Godot;
using RuinOfTheShape.Core;

namespace RuinOfTheShape.LevelData;

public sealed class ResourceLevelLoader : ILevelLoader
{
    public Result TryLoad(string resourcePath)
    {
        var res = ResourceLoader.Load(resourcePath);
        if (res is null)
        {
            return Result.Fail("ResourceLoader.Load returned null");
        }

        if (res is not LevelDefinitionResource)
        {
            return Result.Fail($"Loaded resource is {res.GetType().Name}, expected {nameof(LevelDefinitionResource)}");
        }

        return Result.Ok("loaded");
    }
}

