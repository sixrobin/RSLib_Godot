namespace RSLib.GE.Debug
{
    using Godot;
    using System;

    public class PanelCommand
    {
        private readonly Action _action;
        private BaseButton _button;

        public ulong SourceInstanceID { get; private set; }
        public string Label { get; private set; }
        public int Key { get; private set; }

        public PanelCommand(ulong sourceInstanceID, string label, Action action, int key = -1)
        {
            SourceInstanceID = sourceInstanceID;
            Label = label;
            _action = action;
            Key = key;
        }

        public void SetButton(BaseButton button)
        {
            _button = button;
        }

        public void Execute()
        {
            GD.Print($"[RSLib] Executing command {Label}");
            _action?.Invoke();
        }

        public void OnRemoved()
        {
            _button?.QueueFree();
        }
    }
}