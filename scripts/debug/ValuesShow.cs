using Godot;

public partial class ValuesShow : Node {
    private static readonly Color COLOR = Colors.Yellow;

    private string _valuesText;
    private Label _label;
    private readonly System.Collections.Generic.Dictionary<Label, int> _positionedTexts = new();

    public override void _Ready() {
        SetProcessPriority(-2^63);

        _label = new Label {
            ZIndex = 2 ^ 63 - 1,
            Modulate = COLOR,
            Position = new Vector2(20, 600),
        };

        LabelSettings labelSettings = new() {
            LineSpacing = -4,
            FontSize = 12,
        };
        _label.LabelSettings = labelSettings;

        CanvasLayer canvasLayer = new() {
            Layer = 128,
        };
        
        AddChild(canvasLayer);
        canvasLayer.AddChild(_label);
        
        _label.SetVisible(DebugManager.DebugMode);
    }

    public override void _Process(double delta) {
        NewLine();
        Show("fps", Performance.GetMonitor(Performance.Monitor.TimeFps));
        // 	self.show("mem", RSHelp.format_byte_size(OS.get_static_memory_usage())) // TODO C#
        Show("obj", Performance.GetMonitor(Performance.Monitor.ObjectCount));
        Show("obj_nodes", Performance.GetMonitor(Performance.Monitor.ObjectNodeCount));
        Show("orphans", Performance.GetMonitor(Performance.Monitor.ObjectOrphanNodeCount));
        Show("draw_calls", Performance.GetMonitor(Performance.Monitor.RenderTotalDrawCallsInFrame));
        
        _label.Text = _valuesText;
        _valuesText = string.Empty;

        System.Collections.Generic.Dictionary<Label, int>.KeyCollection positionedTextsKeys = _positionedTexts.Keys;
        foreach (Label label in positionedTextsKeys) {
            _positionedTexts[label]++;
            if (_positionedTexts[label] == 2) { // Buffer to avoid destroying label right after its spawn.
                _positionedTexts.Remove(label);
                label.QueueFree();
            }
        }
    }

    public void ToggleVisible() {
        _label.Visible = !_label.Visible;
    }

    public string Format(object key, object value) {
        return $"{key}: {value}\n";
    }

    public void Show(object key, object value, Vector2? position = null) {
        string debugText = Format(key, value);

        if (position.HasValue) {
            Label label = CreatePositionedText(debugText, position.Value);
            _positionedTexts[label] = 0;
        }
        else {
            _valuesText += debugText;
        }
    }

    private void NewLine() {
        _valuesText += "\n";
    }

    private Label CreatePositionedText(string debugText, Vector2 position) {
        Label label = new() {
            ZIndex = 2 ^ 63 - 1,
            Modulate = COLOR,
            Size = new Vector2(1000, 100),
            GlobalPosition = position,
            Text = debugText,
        };
        
        AddChild(label);
        return label;
    }
}
