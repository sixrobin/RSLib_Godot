namespace RSLib.GE.Debug
{
    using System.Threading.Tasks;
    using Godot;

    public partial class Console : Node
    {
        private const int WIDTH = 512;
        private const int HEIGHT = 150;
        private const int MARGIN = 16;

        private CanvasLayer _canvasLayer;
        private Control _entriesContainer;
        private ScrollContainer _scrollContainer;

        public void Init()
        {
            CreatePanel();
            _canvasLayer.Visible = false;
            Entry("[RSLib Console]", Colors.DimGray, false);
        }

        public bool IsVisible()
        {
            return _canvasLayer.Visible;
        }
        
        public void ToggleVisible(bool? visible = null)
        {
            _canvasLayer.Visible = visible ?? !_canvasLayer.Visible;
            _scrollContainer.ScrollVertical = (int)_entriesContainer.Size.Y;
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
                Position = new Vector2(screenResolution.X - WIDTH - MARGIN, screenResolution.Y - HEIGHT - MARGIN),
            };
            background.SetAnchorsPreset(Control.LayoutPreset.RightWide);
            background.MouseEntered += () => background.Color = new Color(background.Color, 0.95f);
            background.MouseExited += () => background.Color = new Color(background.Color, 0.5f);
            control.AddChild(background);

            _scrollContainer = new ScrollContainer
            {
                HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
                VerticalScrollMode = ScrollContainer.ScrollMode.ShowAlways,
            };
            _scrollContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            background.AddChild(_scrollContainer);

            _entriesContainer = new VBoxContainer();
            _entriesContainer.AddThemeConstantOverride("separation", 0);
            _entriesContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            _entriesContainer.SetCustomMinimumSize(new Vector2(WIDTH, 0));
            _scrollContainer.AddChild(_entriesContainer);
        }

        public async void Entry(object text, Color? color = null, bool enginePrint = true)
        {
            Label label = new()
            {
                Text = $"> {text}",
                SelfModulate = color ?? Colors.White,
                AutowrapMode = TextServer.AutowrapMode.WordSmart,
                VerticalAlignment = VerticalAlignment.Center,
                CustomMinimumSize = new Vector2(WIDTH, 0),
            };
            label.AddThemeFontSizeOverride("font_size", 12);
            label.AddThemeConstantOverride("line_spacing", -4);
            _entriesContainer.AddChild(label);

            if (enginePrint)
                GD.Print(text);

            await Task.Delay(2);
            _scrollContainer.ScrollVertical = (int)_entriesContainer.Size.Y;
        }

        public void Warning(object text)
        {
            Entry(text, Colors.Yellow, false);
            GD.PrintErr(text);
        }

        public void Error(object text)
        {
            Entry(text, Colors.Red, false);
            GD.PrintErr(text);
            GD.PushError(text);

            if (!_canvasLayer.Visible && Debugger.Instance.DebugMode)
                ToggleVisible();
        }
    }
}