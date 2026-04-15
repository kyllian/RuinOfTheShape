using Godot;
using RuinOfTheShape.Presentation;

/// <summary>
/// Fullscreen canvas post-process placed between world rendering and UI.
/// Tier0 disables the edge response in-shader for fallback readability checks.
/// </summary>
public partial class EdgePostProcessLayer : CanvasLayer
{
    private const string DefaultSettingsPath = "res://resources/EdgePresentationSettings_Default.tres";

    private ColorRect? _colorRect;
    private ShaderMaterial? _material;

    [Export] public EdgePresentationSettings? Settings { get; set; }

    public override void _Ready()
    {
        Layer = 0;

        _colorRect = new ColorRect
        {
            Name = "EdgeColorRect",
            MouseFilter = Control.MouseFilterEnum.Ignore,
            Color = Colors.White
        };
        _colorRect.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        _colorRect.OffsetLeft = 0;
        _colorRect.OffsetTop = 0;
        _colorRect.OffsetRight = 0;
        _colorRect.OffsetBottom = 0;

        var shader = GD.Load<Shader>("res://shaders/screen_space_edges.gdshader");
        if (shader is null)
        {
            GD.PrintErr("EdgePostProcessLayer: missing shader res://shaders/screen_space_edges.gdshader");
            return;
        }

        _material = new ShaderMaterial { Shader = shader };
        _colorRect.Material = _material;
        AddChild(_colorRect);

        Settings ??= GD.Load<EdgePresentationSettings>(DefaultSettingsPath);
        Settings ??= new EdgePresentationSettings();
        ApplySettings();
    }

    public override void _Process(double _)
    {
        if (_material is null || Settings is null)
        {
            return;
        }

        ApplySettings();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!OS.IsDebugBuild() || Settings is null)
        {
            return;
        }

        if (@event is not InputEventKey key || !key.Pressed || key.Echo)
        {
            return;
        }

        if (key.Keycode != Key.E)
        {
            return;
        }

        Settings.QualityTier = (EdgePresentationSettings.QualityTierKind)(((int)Settings.QualityTier + 1) % 3);
        ApplySettings();
        GD.Print($"Edge quality tier -> {Settings.QualityTier} (debug E cycles Tier0/Tier1/Tier2)");
    }

    private void ApplySettings()
    {
        if (_material is null || Settings is null)
        {
            return;
        }

        _material.SetShaderParameter("tier_index", Settings.TierIndex);
        _material.SetShaderParameter("edge_threshold", Settings.EdgeThreshold);
        _material.SetShaderParameter("edge_intensity", Settings.EdgeIntensity);
        _material.SetShaderParameter("neighbor_soften", Settings.NeighborSoften);
        _material.SetShaderParameter("edge_tint", Settings.EdgeTint);
    }
}
