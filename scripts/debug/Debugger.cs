namespace RSLib.GE.Debug
{
    using Godot;
    using System;
    using System.Collections.Generic;

    public partial class Debugger : Node
    {
        private class Command
        {
            public Command(Key key, Action action)
            {
                Key = key;
                Action = action;
            }
            
            public Key Key { get; }
            public Action Action { get; }
            public bool Ctrl { get; init; }
            public bool Alt { get; init; }
            public bool Shift { get; init; }

            public bool AreSecondaryKeysPressed()
            {
                if (Ctrl && !Input.IsPhysicalKeyPressed(Key.Ctrl))
                    return false;
                if (Alt && !Input.IsPhysicalKeyPressed(Key.Alt))
                    return false;
                if (Shift && !Input.IsPhysicalKeyPressed(Key.Shift))
                    return false;

                return true;
            }
        }

        private const Key DEBUG_TOGGLE_KEY = Key.F12;
        private const double MONITORING_LOG_INTERVAL = 30;
        
        public bool DebugMode = true;

        public static Debugger Instance { get; private set; }

        public static Console Console { get; private set; }
        public static ValuesShow ValuesShow { get; private set; }
        public static Drawer Drawer { get; private set; }
        public static CommandPanel CommandPanel { get; private set; }

        private readonly List<Command> _commands = new();
        private readonly Dictionary<Key, bool> _keysJustPressed = new();

        private double _monitoringLogTimer;
        private int _monitoringLogCounter;
        
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

            _commands.Add(new Command(DEBUG_TOGGLE_KEY, ToggleDebugMode)
            {
                Ctrl = true,
                Shift = true,
            });
            
            _commands.Add(new Command(Key.F, ToggleScreenMode));
            _commands.Add(new Command(Key.F1, () => ValuesShow.ToggleVisible()));
            _commands.Add(new Command(Key.F2, () => Drawer.ToggleVisible()));
            _commands.Add(new Command(Key.F3, () => CommandPanel.ToggleVisible()));
            _commands.Add(new Command(Key.F4, () => Console.ToggleVisible()));
        }

        public void ToggleDebugMode()
        {
            DebugMode = !DebugMode;
        }

        private void ToggleScreenMode()
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen
                                        ? DisplayServer.WindowMode.Windowed
                                        : DisplayServer.WindowMode.ExclusiveFullscreen);
        }

        private void LogMonitoring()
        {
            _monitoringLogCounter++;

            double totalSeconds = _monitoringLogCounter * MONITORING_LOG_INTERVAL;
            int seconds = Mathf.FloorToInt(totalSeconds % 60);
            int minutes = Mathf.FloorToInt(totalSeconds / 60);
            
            string log = $"Monitoring log #{_monitoringLogCounter} ({minutes}m{seconds:d2}s): ";

            log += $"mem:{Helpers.FormatByteSize(OS.GetStaticMemoryUsage())} | ";
            log += $"video_mem:{Helpers.FormatByteSize((ulong)Performance.GetMonitor(Performance.Monitor.RenderVideoMemUsed))} | ";
            log += $"obj:{Performance.GetMonitor(Performance.Monitor.ObjectCount)} | ";
            log += $"obj_nodes:{Performance.GetMonitor(Performance.Monitor.ObjectNodeCount)} | ";
            log += $"orphans:{Performance.GetMonitor(Performance.Monitor.ObjectOrphanNodeCount)}";
            
            GD.Print(log);
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            foreach (Command command in _commands)
            {
                if (!DebugMode && command.Key != DEBUG_TOGGLE_KEY)
                    continue;
                
                bool keyPressed = Input.IsKeyPressed(command.Key);
                _keysJustPressed.TryAdd(command.Key, false);

                if (keyPressed && !_keysJustPressed[command.Key])
                {
                    _keysJustPressed[command.Key] = true;
                    
                    if (command.AreSecondaryKeysPressed())
                        command.Action?.Invoke();
                }
                else if (!keyPressed && _keysJustPressed[command.Key])
                {
                    _keysJustPressed[command.Key] = false;
                }
            }

            _monitoringLogTimer += delta;
            if (_monitoringLogTimer > MONITORING_LOG_INTERVAL)
            {
                LogMonitoring();
                _monitoringLogTimer = 0f;
            }
        }
    }
}