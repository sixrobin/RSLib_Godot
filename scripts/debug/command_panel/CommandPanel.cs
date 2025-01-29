namespace RSLib.GE.Debug;
using Godot;

public partial class CommandPanel : Node {
    private const int WIDTH = 150;
    private const int HEIGHT = 700;
    private const int MARGIN = 16;

    private CanvasLayer _canvasLayer;
    private Control _buttonsContainer;

    private System.Collections.Generic.Dictionary<string, VBoxContainer> _categories = new();
    private System.Collections.Generic.List<PanelCommand> _commands = new();
    private System.Collections.Generic.Dictionary<Node, int> _sources = new(); // Commands count per source

    public void Init() {
        CreatePanel();
        _canvasLayer.Visible = false;
    }

    public override void _UnhandledKeyInput(InputEvent evt) {
        base._UnhandledKeyInput(evt);

        if (!Debugger.DebugMode) {
            return;
        }

        if (evt is InputEventKey eventKey && eventKey.IsPressed()) {
            foreach (PanelCommand cmd in _commands) {
                if (cmd.Key == (int)eventKey.Keycode) {
                    cmd.Execute();
                }
            }
        }
    }

    public void ToggleVisible() {
        _canvasLayer.Visible = !_canvasLayer.Visible;
    }

    private void CreatePanel() {
        Vector2 screenResolution = GetViewport().GetVisibleRect().Size;

        _canvasLayer = new CanvasLayer {
            Layer = 1000,
        };
        AddChild(_canvasLayer);

        Control control = new() {
            ZIndex = 2 ^ 63 - 1,
        };
        _canvasLayer.AddChild(control);
        
        ColorRect background = new() {
            Color = new Color(0.1f, 0.1f, 0.1f, 0.6f),
            Size = new Vector2(WIDTH, HEIGHT),
            Position = new Vector2(screenResolution.X - WIDTH - MARGIN, MARGIN),
        };
        background.SetAnchorsPreset(Control.LayoutPreset.RightWide);
        control.AddChild(background);
        
        ScrollContainer scrollContainer = new() {
            HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
            VerticalScrollMode = ScrollContainer.ScrollMode.ShowAlways,
        };
        scrollContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        background.AddChild(scrollContainer);

        VBoxContainer vbox = new() {
            CustomMinimumSize = new Vector2(WIDTH, 0),
        };
        vbox.AddThemeConstantOverride("separation", 0);
        vbox.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        scrollContainer.AddChild(vbox);
        _buttonsContainer = vbox;
        
        // 	#for i in 100:
        // 		#self.add(self, "Test", "Test %s" % [i], func(): print("Test %s" % [i]))
    }

    public void Add(Node source, string category, string actionName, System.Action action, int key = -1) {
        PanelCommand cmd = new PanelCommand(source, actionName, action, key);
        _commands.Add(cmd);

        if (string.IsNullOrEmpty(category)) {
            category = "General";
        }

        if (!_categories.ContainsKey(category)) {
            AddCategory(category);
        }

        BaseButton button = AddButton(category, cmd.Label, key);
        button.ButtonDown += () => cmd.Execute();
        cmd.SetButton(button);

        if (_sources.ContainsKey(source)) {
            _sources[source]++;
        }
        else {
            source.TreeExited += () => OnSourceTreeExited(source);
            _sources[source] = 1;
        }
    }

    public void Remove(PanelCommand cmd) {
        cmd.Button.QueueFree();
        _commands.Remove(cmd);
    }

    public void OnSourceTreeExited(Node source) {
        System.Collections.Generic.List<PanelCommand> removedCommands = new();

        foreach (PanelCommand cmd in _commands) {
            if (cmd.Source == source) {
                removedCommands.Add(cmd);
            }
        }

        foreach (PanelCommand cmd in removedCommands) {
            Remove(cmd);
        }
    }

    public VBoxContainer AddCategory(string id) {
        VBoxContainer vbox = new();
        vbox.AddThemeConstantOverride("separation", 0);

        Label categoryLabel = new() {
            Text = id.ToUpper(),
            SelfModulate = Colors.Yellow,
            VerticalAlignment = VerticalAlignment.Bottom,
        };
        categoryLabel.AddThemeFontSizeOverride("font_size", 12);
        vbox.AddChild(categoryLabel);

        Control spacing = new() {
            CustomMinimumSize = new Vector2(0, 12),
        };
        vbox.AddChild(spacing);

        _categories[id] = vbox;
        _buttonsContainer.AddChild(vbox);
        return vbox;
    }

    private BaseButton AddButton(string category, string text, int key = -1) {
        Button button = new() {
            CustomMinimumSize = new Vector2(0, 16),
        };

        Label label = new() {
            Text = text + (key == -1 ? "" : $" {OS.GetKeycodeString((Key)key)}"),
            VerticalAlignment = VerticalAlignment.Center,
        };
        label.AddThemeFontSizeOverride("font_size", 12);
        button.AddChild(label);

        VBoxContainer categoryContainer = _categories[category];
        categoryContainer.AddChild(button);
        categoryContainer.MoveChild(button, categoryContainer.GetChildCount() - 2);

        return button;
    }
}
