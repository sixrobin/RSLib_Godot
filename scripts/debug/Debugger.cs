namespace RSLib.GE.Debug
{
    using Godot;
    using System;

    public partial class Debugger : Node
    {
        public bool DebugMode = true;

        public static Debugger Instance { get; private set; }

        public static Console Console { get; private set; }
        public static ValuesShow ValuesShow { get; private set; }
        public static Drawer Drawer { get; private set; }
        public static CommandPanel CommandPanel { get; private set; }

        private readonly System.Collections.Generic.Dictionary<Key, Action> _commands = new();
        private readonly System.Collections.Generic.Dictionary<Key, bool> _keysJustPressed = new();
        
        public Debugger(bool debugOn = true)
        {
            Name = "RSLib_Debugger";
            DebugMode = debugOn;
        }
        
        public void Init()
        {
            if (Instance != null)
            {
                GD.PushError($"An instance of {nameof(Debugger)} already exists, which should never happen in application flow.");
                QueueFree();
                return;
            }
            
            Instance = this;

            Console = new Console();
            AddChild(Console);
            Console.Init();

            ValuesShow = new ValuesShow();
            AddChild(ValuesShow);
            ValuesShow.Init();

            Drawer = new Drawer();
            AddChild(Drawer);
            Drawer.Init();
            
            CommandPanel = new CommandPanel();
            AddChild(CommandPanel);
            CommandPanel.Init();

            _commands[Key.F12] = ToggleDebugMode;
            _commands[Key.F] = ToggleScreenMode;
            _commands[Key.F1] = () => ValuesShow.ToggleVisible();
            _commands[Key.F2] = () => Drawer.ToggleVisible();
            _commands[Key.F3] = () => CommandPanel.ToggleVisible();
            _commands[Key.F4] = () => Console.ToggleVisible();
        }

        private void ToggleDebugMode()
        {
            DebugMode = !DebugMode;
        }

        private void ToggleScreenMode()
        {
            if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
            else if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen)
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
            else
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        
        public override void _Process(double delta)
        {
            base._Process(delta);

            foreach (System.Collections.Generic.KeyValuePair<Key, Action> command in _commands)
            {
                if (!DebugMode && command.Key != Key.F12)
                    continue;

                bool keyPressed = Input.IsKeyPressed(command.Key);
                _keysJustPressed.TryAdd(command.Key, false);

                if (keyPressed && !_keysJustPressed[command.Key])
                {
                    _keysJustPressed[command.Key] = true;
                    command.Value?.Invoke();
                }
                else if (!keyPressed && _keysJustPressed[command.Key])
                {
                    _keysJustPressed[command.Key] = false;
                }
            }
        }
    }
}