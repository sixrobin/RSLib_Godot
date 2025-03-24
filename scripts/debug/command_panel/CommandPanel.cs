namespace RSLib.GE.Debug
{
    using Godot;

    public partial class CommandPanel : Node
    {
        private const int WIDTH = 150;
        private const int HEIGHT = 700;
        private const int MARGIN = 16;

        private readonly System.Collections.Generic.Dictionary<string, VBoxContainer> _categories = new();
        private readonly System.Collections.Generic.List<PanelCommand> _commands = new();
        private readonly System.Collections.Generic.Dictionary<Node, int> _commandsCountPerSource = new();
        private CanvasLayer _canvasLayer;
        private Control _buttonsContainer;
        
        public void Init()
        {
            CreatePanel();
            _canvasLayer.Visible = false;
        }

        public void ToggleVisible(bool? visible = null)
        {
            _canvasLayer.Visible = visible ?? !_canvasLayer.Visible;
        }

        private void CreatePanel()
        {
            Vector2 screenResolution = GetViewport().GetVisibleRect().Size;

            _canvasLayer = new CanvasLayer
            {
                Layer = 1000,
            };
            AddChild(_canvasLayer);

            Control control = new()
            {
                ZIndex = 2 ^ 63 - 1,
            };
            _canvasLayer.AddChild(control);

            ColorRect background = new()
            {
                Color = new Color(0.05f, 0.05f, 0.05f, 0.5f),
                Size = new Vector2(WIDTH, HEIGHT),
                Position = new Vector2(screenResolution.X - WIDTH - MARGIN, MARGIN),
            };
            background.SetAnchorsPreset(Control.LayoutPreset.RightWide);
            control.AddChild(background);

            ScrollContainer scrollContainer = new()
            {
                HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
                VerticalScrollMode = ScrollContainer.ScrollMode.ShowAlways,
            };
            scrollContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            background.AddChild(scrollContainer);

            VBoxContainer vbox = new()
            {
                CustomMinimumSize = new Vector2(WIDTH, 0f),
            };
            vbox.AddThemeConstantOverride("separation", 0);
            vbox.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            scrollContainer.AddChild(vbox);
            _buttonsContainer = vbox;
        }

        public void Add(Node source, string category, string actionName, System.Action action, int key = -1)
        {
            PanelCommand command = new(source, actionName, action, key);
            _commands.Add(command);

            if (string.IsNullOrEmpty(category))
                category = "General";

            if (!_categories.ContainsKey(category))
                AddCategory(category);

            BaseButton button = AddButton(category, command.Label, key);
            button.ButtonDown += () => command.Execute();
            command.SetButton(button);

            if (source != null)
            {
                if (_commandsCountPerSource.ContainsKey(source))
                {
                    _commandsCountPerSource[source]++;
                }
                else
                {
                    source.TreeExited += () => OnSourceTreeExited(source);
                    _commandsCountPerSource[source] = 1;
                }
            }
        }

        public void Remove(PanelCommand command)
        {
            command.OnRemoved();
            _commands.Remove(command);
        }

        private void OnSourceTreeExited(Node source)
        {
            System.Collections.Generic.List<PanelCommand> removedCommands = new();

            foreach (PanelCommand command in _commands)
                if (command.Source == source)
                    removedCommands.Add(command);

            foreach (PanelCommand command in removedCommands)
                Remove(command);
        }

        private VBoxContainer AddCategory(string id)
        {
            VBoxContainer vbox = new();
            vbox.AddThemeConstantOverride("separation", 0);

            Label categoryLabel = new()
            {
                Text = id.ToUpper(),
                SelfModulate = Colors.Yellow,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            categoryLabel.AddThemeFontSizeOverride("font_size", 12);
            vbox.AddChild(categoryLabel);

            Control spacing = new()
            {
                CustomMinimumSize = new Vector2(0f, 12f),
            };
            vbox.AddChild(spacing);

            _categories[id] = vbox;
            _buttonsContainer.AddChild(vbox);
            return vbox;
        }

        private BaseButton AddButton(string category, string text, int key = -1)
        {
            Button button = new()
            {
                CustomMinimumSize = new Vector2(0f, 16f),
            };

            Label label = new()
            {
                Text = text + (key == -1 ? "" : $" [{OS.GetKeycodeString((Key)key)}]"),
                VerticalAlignment = VerticalAlignment.Center,
            };
            label.AddThemeFontSizeOverride("font_size", 12);
            button.AddChild(label);

            VBoxContainer categoryContainer = _categories[category];
            categoryContainer.AddChild(button);
            categoryContainer.MoveChild(button, categoryContainer.GetChildCount() - 2);

            return button;
        }
        
        public override void _UnhandledKeyInput(InputEvent inputEvent)
        {
            base._UnhandledKeyInput(inputEvent);

            if (!Debugger.DebugMode)
                return;

            if (inputEvent is not InputEventKey eventKey || !eventKey.IsPressed())
                return;
            
            foreach (PanelCommand command in this._commands)
                if (command.Key == (int)eventKey.Keycode)
                    command.Execute();
        }
    }
}