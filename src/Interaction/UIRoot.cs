using Godot;

public partial class UIRoot : CanvasLayer
{
    public override void _Ready()
    {
        var panel = new PanelContainer
        {
            Name = "PhaseOneLegendPanel",
            Position = new Vector2(16, 16),
            Size = new Vector2(530, 170)
        };

        var label = new Label
        {
            Text =
                "Phase 1 Graphics Baseline\n" +
                "Press Space to cycle validation levels (A/B/C).\n" +
                "State cues: active=blue, blocked=gray, flowing=amber, idle=green, unstable=red pulse.",
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };

        panel.AddChild(label);
        AddChild(panel);
    }
}
