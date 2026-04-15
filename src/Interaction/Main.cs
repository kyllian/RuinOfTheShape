using Godot;
using RuinOfTheShape.LevelData;
using RuinOfTheShape.Simulation;
using RuinOfTheShape.Topology;
using System.Collections.Generic;

public partial class Main : Node
{
    private readonly List<LevelDefinitionResource> _loadedLevels = [];
    private readonly TopologyResolver _topologyResolver = new();
    private int _activeLevelIndex;

    public override void _Ready()
    {
        GD.Print("Ruin of the Shape starting (C#).");

        var tickSystem = new FixedTickSystem();
        var levelLoader = new ResourceLevelLoader();

        var paths = new[]
        {
            "res://levels/Level_01_OneSided.tres",
            "res://levels/Level_02_Portals.tres",
            "res://levels/Level_03_Instability.tres",
        };

        foreach (var p in paths)
        {
            var result = levelLoader.TryLoad(p);
            GD.Print($"LevelLoad path={p} ok={result.IsOk} message={result.Message}");

            var level = ResourceLoader.Load<LevelDefinitionResource>(p);
            if (level is not null)
            {
                _loadedLevels.Add(level);
            }
        }

        tickSystem.Initialize();
        GD.Print("TickSystem initialized.");

        if (_loadedLevels.Count == 0)
        {
            GD.PrintErr("No levels loaded for ShapeView.");
            return;
        }

        var topologyLevel = _loadedLevels[0];
        _topologyResolver.Initialize(topologyLevel);

        var shapeView = GetNodeOrNull<ShapeView>("ShapeView");
        if (shapeView is null)
        {
            GD.PrintErr("ShapeView node not found under Main.");
            return;
        }

        // Presentation consumes level snapshots but does not mutate simulation/topology state.
        shapeView.RenderLevel(topologyLevel);
        GD.Print($"ShapeView rendered level={topologyLevel.LevelId}");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is not InputEventKey keyEvent || !keyEvent.Pressed || keyEvent.Echo)
        {
            return;
        }

        var shapeView = GetNodeOrNull<ShapeView>("ShapeView");
        if (shapeView is null)
        {
            return;
        }

        if (OS.IsDebugBuild())
        {
            if (keyEvent.Keycode == Key.R)
            {
                shapeView.CycleWrappingProfile();
                GetViewport().SetInputAsHandled();
                return;
            }

            if (keyEvent.Keycode == Key.L)
            {
                shapeView.ToggleFaceLabels();
                GetViewport().SetInputAsHandled();
                return;
            }

            if (keyEvent.Keycode == Key.Bracketleft)
            {
                shapeView.AdjustRepeatDepth(-1);
                GetViewport().SetInputAsHandled();
                return;
            }

            if (keyEvent.Keycode == Key.Bracketright)
            {
                shapeView.AdjustRepeatDepth(1);
                GetViewport().SetInputAsHandled();
                return;
            }
        }

        if (keyEvent.Keycode != Key.Space || _loadedLevels.Count == 0)
        {
            return;
        }

        _activeLevelIndex = (_activeLevelIndex + 1) % _loadedLevels.Count;
        var nextLevel = _loadedLevels[_activeLevelIndex];
        _topologyResolver.Initialize(nextLevel);
        shapeView.RenderLevel(nextLevel);
        GD.Print($"ShapeView switched level={nextLevel.LevelId}");
    }
}
