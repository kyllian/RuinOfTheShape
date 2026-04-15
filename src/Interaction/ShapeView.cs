using Godot;
using RuinOfTheShape.LevelData;
using RuinOfTheShape.Presentation;
using System.Collections.Generic;

public partial class ShapeView : Node3D
{
    private const string DefaultWrappingSettingsPath = "res://resources/WrappingPresentationSettings_Default.tres";

    // Phase B palette tokens (single source for gameplay-significant states).
    private static readonly Dictionary<string, Color> PaletteTokens = new()
    {
        ["token.active"] = new Color(0.25f, 0.65f, 0.92f),
        ["token.blocked"] = new Color(0.24f, 0.26f, 0.29f),
        ["token.flowing"] = new Color(0.86f, 0.54f, 0.22f),
        ["token.idle"] = new Color(0.24f, 0.52f, 0.36f),
        ["token.unstable"] = new Color(0.83f, 0.24f, 0.20f),
        ["token.portal"] = new Color(0.72f, 0.52f, 0.20f),
        ["token.portal.final"] = new Color(0.90f, 0.74f, 0.22f)
    };

    private readonly Dictionary<string, MeshInstance3D> _faceMeshes = new();
    private readonly List<MeshInstance3D> _portalMeshes = [];
    private Node3D? _generatedRoot;
    private WrappingPresentationSettings? _wrappingSettings;
    private LevelDefinitionResource? _currentLevel;

    private float _pulseTime;
    private bool _hasUnstableState;
    private bool _showFaceLabels;

    [Export] public WrappingPresentationSettings? WrappingSettings { get; set; }

    public void RenderLevel(LevelDefinitionResource level)
    {
        _currentLevel = level;
        ClearVisuals();
        _generatedRoot ??= EnsureGeneratedRoot();
        _wrappingSettings = WrappingSettings
            ?? GD.Load<WrappingPresentationSettings>(DefaultWrappingSettingsPath)
            ?? new WrappingPresentationSettings();

        _hasUnstableState = level.InstabilityTrigger != InstabilityTrigger.None;
        BuildFaceVisuals(level);
        BuildPortalVisuals(level);
        BuildContinuityCues(level);
        if (OS.IsDebugBuild())
        {
            GD.Print($"Wrapping cues active profile={_wrappingSettings.CueProfile} repeatDepth={_wrappingSettings.RepeatDepth} effectiveDepth={_wrappingSettings.EffectiveRepeatDepth}");
        }
        UpdateCameraFraming();
    }

    public override void _Process(double delta)
    {
        if (!_hasUnstableState || _portalMeshes.Count == 0)
        {
            return;
        }

        _pulseTime += (float)delta;
        var t = 0.5f + (0.5f * Mathf.Sin(_pulseTime * 3.5f));
        var unstableColor = PaletteTokens["token.unstable"];
        var normalColor = PaletteTokens["token.portal"];
        var color = normalColor.Lerp(unstableColor, t);

        foreach (var portal in _portalMeshes)
        {
            if (portal.MaterialOverride is StandardMaterial3D material)
            {
                material.AlbedoColor = color;
                material.EmissionEnabled = true;
                material.Emission = color * 0.25f;
            }
        }
    }

    public void CycleWrappingProfile()
    {
        if (_wrappingSettings is null)
        {
            return;
        }

        _wrappingSettings.CueProfile = (WrappingPresentationSettings.CueProfileKind)(((int)_wrappingSettings.CueProfile + 1) % 3);
        GD.Print($"Wrapping cue profile -> {_wrappingSettings.CueProfile} (debug R cycles Disabled/Balanced/Emphasized)");
        RefreshCurrentLevel();
    }

    public void AdjustRepeatDepth(int delta)
    {
        if (_wrappingSettings is null)
        {
            return;
        }

        _wrappingSettings.RepeatDepth = Mathf.Clamp(_wrappingSettings.RepeatDepth + delta, 0, 2);
        GD.Print($"Wrapping repeat depth -> {_wrappingSettings.RepeatDepth}");
        RefreshCurrentLevel();
    }

    public void ToggleFaceLabels()
    {
        _showFaceLabels = !_showFaceLabels;
        GD.Print($"Face labels debug toggle -> {(_showFaceLabels ? "ON" : "OFF")}");
        RefreshCurrentLevel();
    }

    private void BuildFaceVisuals(LevelDefinitionResource level)
    {
        for (var i = 0; i < level.Faces.Count; i++)
        {
            var face = level.Faces[i];
            var faceMesh = new MeshInstance3D
            {
                Name = $"Face_{face.FaceId}",
                Mesh = new BoxMesh
                {
                    Size = new Vector3(2.8f, 0.15f, 2.8f)
                }
            };

            var xOffset = (i * 4.0f) - ((level.Faces.Count - 1) * 2.0f);
            faceMesh.Position = new Vector3(xOffset, 0.0f, 0.0f);

            var stateCue = ResolveFaceStateCue(face.FaceId, level);
            var material = BuildFaceMaterial(stateCue);
            faceMesh.MaterialOverride = material;

            _generatedRoot?.AddChild(faceMesh);
            _faceMeshes[face.FaceId] = faceMesh;

            if (_showFaceLabels)
            {
                var label = new Label3D
                {
                    Name = $"Label_{face.FaceId}",
                    Text = $"{face.FaceId} [{stateCue}]",
                    Billboard = BaseMaterial3D.BillboardModeEnum.Enabled,
                    Position = new Vector3(0.0f, 1.2f, 0.0f),
                    FontSize = 64,
                    PixelSize = 0.012f
                };
                faceMesh.AddChild(label);
            }
            BuildWrappedFaceCopies(faceMesh, stateCue, face.FaceId, level.Faces.Count);
        }
    }

    private void RefreshCurrentLevel()
    {
        if (_currentLevel is not null)
        {
            RenderLevel(_currentLevel);
        }
    }

    private void BuildPortalVisuals(LevelDefinitionResource level)
    {
        foreach (var portal in level.Portals)
        {
            if (!_faceMeshes.TryGetValue(portal.FromFaceId, out var fromFace))
            {
                continue;
            }

            var portalMesh = new MeshInstance3D
            {
                Name = $"Portal_{portal.PortalId}",
                Mesh = new SphereMesh
                {
                    Radius = portal.IsFinalPortalToCore ? 0.42f : 0.32f,
                    Height = portal.IsFinalPortalToCore ? 0.84f : 0.64f
                },
                Position = fromFace.Position + new Vector3(0.0f, 1.0f, 0.0f)
            };

            var material = new StandardMaterial3D
            {
                AlbedoColor = portal.IsFinalPortalToCore
                    ? PaletteTokens["token.portal.final"]
                    : PaletteTokens["token.portal"],
                EmissionEnabled = true,
                Emission = new Color(0.16f, 0.12f, 0.06f)
            };
            portalMesh.MaterialOverride = material;
            _generatedRoot?.AddChild(portalMesh);
            _portalMeshes.Add(portalMesh);
        }
    }

    private static string ResolveFaceStateCue(string faceId, LevelDefinitionResource level)
    {
        var isActive = level.ActiveFaceIds.Contains(faceId);
        if (!isActive)
        {
            return "blocked";
        }

        if (level.InstabilityTrigger != InstabilityTrigger.None)
        {
            return "unstable";
        }

        if (level.ActiveFaceIds.Count > 0 && level.ActiveFaceIds[0] == faceId)
        {
            return "active";
        }

        foreach (var portal in level.Portals)
        {
            if (portal.FromFaceId == faceId)
            {
                return "flowing";
            }
        }

        return "idle";
    }

    private static StandardMaterial3D BuildFaceMaterial(string stateCue)
    {
        var token = stateCue switch
        {
            "active" => "token.active",
            "blocked" => "token.blocked",
            "flowing" => "token.flowing",
            "idle" => "token.idle",
            "unstable" => "token.unstable",
            _ => "token.blocked"
        };

        var color = PaletteTokens[token];

        return new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.72f,
            Metallic = 0.04f
        };
    }

    private void BuildWrappedFaceCopies(MeshInstance3D faceMesh, string stateCue, string faceId, int faceCount)
    {
        var repeatDepth = _wrappingSettings?.EffectiveRepeatDepth ?? 0;
        if (repeatDepth <= 0 || _generatedRoot is null)
        {
            return;
        }

        var wrapStride = Mathf.Max(faceCount * 4.0f, 4.0f);
        var ghostMaterial = BuildGhostMaterial(stateCue);

        for (var layer = 1; layer <= repeatDepth; layer++)
        {
            var offset = wrapStride * layer;
            AddWrappedFaceClone(faceMesh, ghostMaterial, faceId, layer, offset);
            AddWrappedFaceClone(faceMesh, ghostMaterial, faceId, -layer, -offset);
        }
    }

    private void AddWrappedFaceClone(MeshInstance3D sourceFace, StandardMaterial3D ghostMaterial, string faceId, int layer, float xOffset)
    {
        if (_generatedRoot is null)
        {
            return;
        }

        var clone = new MeshInstance3D
        {
            Name = $"Wrap_{faceId}_{layer}",
            Mesh = sourceFace.Mesh,
            Position = sourceFace.Position + new Vector3(xOffset, 0.0f, 0.0f),
            MaterialOverride = ghostMaterial.Duplicate() as Material
        };
        _generatedRoot.AddChild(clone);
    }

    private StandardMaterial3D BuildGhostMaterial(string stateCue)
    {
        var baseMaterial = BuildFaceMaterial(stateCue);
        var alpha = _wrappingSettings?.GhostAlpha ?? 0.32f;
        var intensity = _wrappingSettings?.ContinuityIntensity ?? 0.65f;

        baseMaterial.Transparency = BaseMaterial3D.TransparencyEnum.Alpha;
        baseMaterial.AlbedoColor = new Color(
            baseMaterial.AlbedoColor.R * intensity,
            baseMaterial.AlbedoColor.G * intensity,
            baseMaterial.AlbedoColor.B * intensity,
            alpha
        );
        baseMaterial.EmissionEnabled = true;
        baseMaterial.Emission = baseMaterial.AlbedoColor * 0.07f;
        return baseMaterial;
    }

    private void BuildContinuityCues(LevelDefinitionResource level)
    {
        if (_generatedRoot is null
            || _faceMeshes.Count == 0
            || _wrappingSettings is null
            || _wrappingSettings.CueProfile == WrappingPresentationSettings.CueProfileKind.Disabled
            || _wrappingSettings.EffectiveRepeatDepth <= 0)
        {
            return;
        }

        var center = Vector3.Zero;
        foreach (var face in _faceMeshes.Values)
        {
            center += face.Position;
        }
        center /= _faceMeshes.Count;

        var span = Mathf.Max(level.Faces.Count * 4.0f, 4.0f) * (_wrappingSettings.EffectiveRepeatDepth + 0.2f);
        var markerHeight = _wrappingSettings.MarkerHeight;
        AddDirectionMarker("Direction_Right", center + new Vector3(span * 0.5f, markerHeight, 0.0f), 1);
        AddDirectionMarker("Direction_Left", center + new Vector3(-span * 0.5f, markerHeight, 0.0f), -1);
        AddContinuitySeparator(center + new Vector3(span * 0.5f, 0.08f, 0.0f));
        AddContinuitySeparator(center + new Vector3(-span * 0.5f, 0.08f, 0.0f));
    }

    private void AddDirectionMarker(string name, Vector3 position, int directionSign)
    {
        if (_generatedRoot is null)
        {
            return;
        }

        var markerRoot = new Node3D
        {
            Name = name,
            Position = position
        };
        _generatedRoot.AddChild(markerRoot);

        var alpha = Mathf.Clamp(_wrappingSettings?.ContinuityIntensity ?? 0.65f, 0.25f, 1.0f);
        var cueMaterial = new StandardMaterial3D
        {
            Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
            AlbedoColor = new Color(0.88f, 0.9f, 0.96f, alpha),
            EmissionEnabled = true,
            Emission = new Color(0.16f, 0.18f, 0.24f) * alpha,
            Roughness = 0.35f,
            Metallic = 0.1f
        };

        // Single arrow: shaft + head lie flat in XZ (constant thin Y), same as the shaft — chevron in the ground plane.
        var shaft = new MeshInstance3D
        {
            Name = "MarkerShaft",
            Mesh = new BoxMesh
            {
                Size = new Vector3(0.88f, 0.06f, 0.12f)
            },
            Position = new Vector3(-0.14f * directionSign, 0.0f, 0.0f),
            MaterialOverride = cueMaterial
        };
        markerRoot.AddChild(shaft);

        // Head: two thin slabs in XZ (local Y = world up), each wing's long axis (local +X) aims at the apex.
        // Avoids Euler Y+Z heuristics that can read as "edge-on" or inverted from oblique views.
        var apex = new Vector3(0.44f * directionSign, 0.0f, 0.0f);
        const float wingLen = 0.36f;
        var wingSize = new Vector3(wingLen, 0.06f, 0.06f);

        var vPlus = new Vector3(-directionSign, 0.0f, 1.0f).Normalized();
        var vMinus = new Vector3(-directionSign, 0.0f, -1.0f).Normalized();
        var centerPlus = apex + vPlus * (wingLen * 0.5f);
        var centerMinus = apex + vMinus * (wingLen * 0.5f);
        var dirPlus = (apex - centerPlus).Normalized();
        var dirMinus = (apex - centerMinus).Normalized();

        var headPlusZ = new MeshInstance3D
        {
            Name = "MarkerHeadPlusZ",
            Mesh = new BoxMesh { Size = wingSize },
            Position = centerPlus,
            Basis = BasisFlatWithLocalXAlong(dirPlus),
            MaterialOverride = cueMaterial
        };
        markerRoot.AddChild(headPlusZ);

        var headMinusZ = new MeshInstance3D
        {
            Name = "MarkerHeadMinusZ",
            Mesh = new BoxMesh { Size = wingSize },
            Position = centerMinus,
            Basis = BasisFlatWithLocalXAlong(dirMinus),
            MaterialOverride = cueMaterial
        };
        markerRoot.AddChild(headMinusZ);
    }

    // Local +X along dir (in XZ), local +Y = world up so the wing stays as thin on Y as the shaft.
    private static Basis BasisFlatWithLocalXAlong(Vector3 localXWorld)
    {
        var x = localXWorld.Normalized();
        var worldUp = Vector3.Up;
        var z = x.Cross(worldUp);
        if (z.LengthSquared() < 1e-10f)
        {
            worldUp = Vector3.Forward;
            z = x.Cross(worldUp);
        }

        z = z.Normalized();
        var y = z.Cross(x).Normalized();
        return new Basis(x, y, z);
    }

    private void AddContinuitySeparator(Vector3 position)
    {
        if (_generatedRoot is null || _wrappingSettings is null)
        {
            return;
        }

        var separator = new MeshInstance3D
        {
            Name = $"WrapSeparator_{position.X:F1}",
            Mesh = new BoxMesh
            {
                Size = new Vector3(0.08f, 0.05f, 3.8f)
            },
            Position = position
        };
        separator.MaterialOverride = new StandardMaterial3D
        {
            Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
            AlbedoColor = new Color(0.82f, 0.84f, 0.88f, Mathf.Clamp(_wrappingSettings.GhostAlpha + 0.2f, 0.2f, 0.8f)),
            Roughness = 0.9f,
            Metallic = 0.0f
        };
        _generatedRoot.AddChild(separator);
    }

    private void ClearVisuals()
    {
        _generatedRoot ??= EnsureGeneratedRoot();
        foreach (var child in _generatedRoot.GetChildren())
        {
            child.QueueFree();
        }

        _faceMeshes.Clear();
        _portalMeshes.Clear();
        _pulseTime = 0.0f;
        _hasUnstableState = false;
    }

    private Node3D EnsureGeneratedRoot()
    {
        var existing = GetNodeOrNull<Node3D>("GeneratedVisuals");
        if (existing is not null)
        {
            return existing;
        }

        var generated = new Node3D
        {
            Name = "GeneratedVisuals"
        };
        AddChild(generated);
        return generated;
    }

    private void UpdateCameraFraming()
    {
        var camera = GetNodeOrNull<Camera3D>("Camera3D");
        if (camera is null || _faceMeshes.Count == 0)
        {
            return;
        }

        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        foreach (var face in _faceMeshes.Values)
        {
            var p = face.Position;
            min.X = Mathf.Min(min.X, p.X - 1.6f);
            min.Y = Mathf.Min(min.Y, p.Y);
            min.Z = Mathf.Min(min.Z, p.Z - 1.6f);
            max.X = Mathf.Max(max.X, p.X + 1.6f);
            max.Y = Mathf.Max(max.Y, p.Y + 2.0f);
            max.Z = Mathf.Max(max.Z, p.Z + 1.6f);
        }

        foreach (var portal in _portalMeshes)
        {
            var p = portal.Position;
            min.X = Mathf.Min(min.X, p.X - 0.8f);
            min.Y = Mathf.Min(min.Y, p.Y - 0.8f);
            min.Z = Mathf.Min(min.Z, p.Z - 0.8f);
            max.X = Mathf.Max(max.X, p.X + 0.8f);
            max.Y = Mathf.Max(max.Y, p.Y + 0.8f);
            max.Z = Mathf.Max(max.Z, p.Z + 0.8f);
        }

        var center = (min + max) * 0.5f;
        var width = max.X - min.X;
        var height = max.Y - min.Y;
        var viewport = GetViewport();
        var viewportRect = viewport.GetVisibleRect();
        var aspect = viewportRect.Size.Y > 0.0f
            ? viewportRect.Size.X / viewportRect.Size.Y
            : 16.0f / 9.0f;

        var vFovRadians = Mathf.DegToRad(camera.Fov);
        var hFovRadians = 2.0f * Mathf.Atan(Mathf.Tan(vFovRadians * 0.5f) * aspect);
        var fitDistanceX = (width * 0.5f) / Mathf.Tan(hFovRadians * 0.5f);
        var fitDistanceY = (height * 0.5f) / Mathf.Tan(vFovRadians * 0.5f);
        var framingDistance = Mathf.Clamp(Mathf.Max(fitDistanceX, fitDistanceY) * 1.45f, 4.0f, 14.0f);
        var cameraHeight = Mathf.Clamp(framingDistance * 0.42f, 2.5f, 7.0f);

        camera.Position = center + new Vector3(0.0f, cameraHeight, framingDistance);
        camera.LookAt(center + new Vector3(0.0f, height * 0.2f, 0.0f), Vector3.Up);
    }
}
