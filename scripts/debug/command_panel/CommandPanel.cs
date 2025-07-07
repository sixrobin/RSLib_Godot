namespace RSLib.GE.Debug
{
    using System.Linq;
    using Godot;

    public partial class CommandPanel : Node
    {
        private const int WIDTH = 150;
        private const int HEIGHT = 700;
        private const int MARGIN = 16;

        private readonly System.Collections.Generic.Dictionary<string, VBoxContainer> _categoryContainers = new();
        private readonly System.Collections.Generic.List<PanelCommand> _commands = new();
        private readonly System.Collections.Generic.Dictionary<Node, int> _commandsCountPerSource = new();
        private CanvasLayer _canvasLayer;
        private Control _buttonsContainer;
        private ColorRect _background;
        
        public bool IsHovered { get; private set; }
        
        public void Init()
        {
            CreatePanel();
            _canvasLayer.Visible = false;
        }

        public bool IsVisible()
        {
            return _canvasLayer.Visible;
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

            _background = new ColorRect()
            {
                Color = new Color(0.05f, 0.05f, 0.05f, 0.5f),
                Size = new Vector2(WIDTH, HEIGHT),
                Position = new Vector2(screenResolution.X - WIDTH - MARGIN, MARGIN),
            };
            _background.SetAnchorsPreset(Control.LayoutPreset.RightWide);
            control.AddChild(_background);

            ScrollContainer scrollContainer = new()
            {
                HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
                VerticalScrollMode = ScrollContainer.ScrollMode.ShowAlways,
            };
            scrollContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            _background.AddChild(scrollContainer);

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

            if (!_categoryContainers.ContainsKey(category))
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

            string[] categories = _categoryContainers.Keys.ToArray();
            foreach (string category in categories)
            {
                if (!_categoryContainers.TryGetValue(category, out VBoxContainer categoryContainer))
                    continue;
                
                // > 2 as label and spacing are never destroyed.
                if (categoryContainer.GetChildren().Count(o => !o.IsQueuedForDeletion()) > 2)
                    continue;
                
                categoryContainer.QueueFree();
                _categoryContainers.Remove(category);
            }
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

            Label label = new()
            {
                Text = id.ToUpper(),
                SelfModulate = Colors.Yellow,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            label.AddThemeFontSizeOverride("font_size", 12);
            vbox.AddChild(label);

            Control spacing = new()
            {
                CustomMinimumSize = new Vector2(0f, 12f),
            };
            vbox.AddChild(spacing);

            _categoryContainers[id] = vbox;
            _buttonsContainer.AddChild(vbox);
            return vbox;
        }

        private BaseButton AddButton(string category, string text, int key = -1)
        {
            Button button = new()
            {
                CustomMinimumSize = new Vector2(0f, 16f),
                FocusMode = Control.FocusModeEnum.Click,
            };

            Label label = new()
            {
                Text = text + (key == -1 ? "" : $" [{OS.GetKeycodeString((Key)key)}]"),
                VerticalAlignment = VerticalAlignment.Center,
            };
            label.AddThemeFontSizeOverride("font_size", 12);
            button.AddChild(label);

            VBoxContainer categoryContainer = _categoryContainers[category];
            categoryContainer.AddChild(button);
            categoryContainer.MoveChild(button, categoryContainer.GetChildCount() - 2);

            return button;
        }
        
        public override void _UnhandledKeyInput(InputEvent inputEvent)
        {
            base._UnhandledKeyInput(inputEvent);

            if (!Debugger.Instance.DebugMode)
                return;

            if (inputEvent is not InputEventKey eventKey || !eventKey.IsPressed())
                return;
            
            foreach (PanelCommand command in _commands)
                if (command.Key == (int)eventKey.Keycode)
                    command.Execute();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            if (IsVisible())
            {
                IsHovered = _background.GetGlobalRect().HasPoint(_background.GetGlobalMousePosition());
                _background.SetModulateAlpha(IsHovered ? 1f : 0.25f);
            }
            else
            {
                IsHovered = false;
            }
        }
    }
}