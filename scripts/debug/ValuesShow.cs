namespace RSLib.GE.Debug
{
    using Godot;

    public partial class ValuesShow : Node
    {
        private const int MARGIN = 16;
        private const int POSITIONED_TEXTS_FONT_SIZE = 36;
        private static readonly Color COLOR = Colors.Yellow;

        private Label _label;
        private string _valuesText;
        private readonly System.Collections.Generic.Dictionary<Label, int> _positionedTexts = new();

        public void Init()
        {
            SetProcessPriority(-2 ^ 63);

            _label = new Label
            {
                ZIndex = 2 ^ 63 - 1,
                Modulate = COLOR,
                VerticalAlignment = VerticalAlignment.Bottom,
                Size = GetViewport().GetVisibleRect().Size,
                MouseFilter = Control.MouseFilterEnum.Ignore,
            };
            _label.SetAnchorsPreset(Control.LayoutPreset.BottomLeft);
            _label.Position = new Vector2(MARGIN, -_label.Size.Y - MARGIN);

            LabelSettings labelSettings = new()
            {
                LineSpacing = -4,
                FontSize = 10,
            };
            _label.LabelSettings = labelSettings;

            CanvasLayer canvasLayer = new()
            {
                Layer = 128,
            };

            AddChild(canvasLayer);
            canvasLayer.AddChild(_label);

            _label.Visible = Debugger.Instance.DebugMode;
        }

        public bool IsVisible()
        {
            return _label.Visible;
        }
        
        public void ToggleVisible(bool? visible = null)
        {
            _label.Visible = visible ?? !_label.Visible;
        }

        private string Format(object key, object value)
        {
            return string.IsNullOrEmpty(key.ToString()) ? value.ToString() : $"{key}: {value}\n";
        }

        public void Show(object key, object value, Vector2? position = null)
        {
            string debugText = Format(key, value);

            if (position.HasValue)
            {
                Label label = CreatePositionedText(debugText, position.Value);
                _positionedTexts[label] = 0;
            }
            else
            {
                _valuesText += debugText;
            }
        }

        private Label CreatePositionedText(string debugText, Vector2 position)
        {
            Label label = new()
            {
                ZIndex = 2 ^ 63 - 1,
                Modulate = COLOR,
                Size = new Vector2(1000, 100),
                GlobalPosition = position,
                Text = debugText,
            };
            
            label.AddThemeFontSizeOverride("font_size", POSITIONED_TEXTS_FONT_SIZE);

            AddChild(label);
            return label;
        }
        
        public override void _Process(double delta)
        {
            _valuesText += "\n";

            Show("fps", Performance.GetMonitor(Performance.Monitor.TimeFps));
            Show("mem", Helpers.FormatByteSize(OS.GetStaticMemoryUsage(), true));
            Show("obj", Performance.GetMonitor(Performance.Monitor.ObjectCount));
            Show("obj_nodes", Performance.GetMonitor(Performance.Monitor.ObjectNodeCount));
            Show("orphans", Performance.GetMonitor(Performance.Monitor.ObjectOrphanNodeCount));
            Show("draw_calls", Performance.GetMonitor(Performance.Monitor.RenderTotalDrawCallsInFrame));

            _label.Text = _valuesText;
            _valuesText = string.Empty;

            System.Collections.Generic.Dictionary<Label, int>.KeyCollection positionedTextsKeys = _positionedTexts.Keys;
            foreach (Label label in positionedTextsKeys)
            {
                // Buffer to avoid destroying label right after its spawn.
                _positionedTexts[label]++;
                if (_positionedTexts[label] < 2)
                    continue;
                
                _positionedTexts.Remove(label);
                label.QueueFree();
            }
        }
    }
}