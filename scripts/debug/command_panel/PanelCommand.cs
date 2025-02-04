namespace RSLib.GE.Debug
{
    using Godot;
    using System;

    public partial class PanelCommand : Node
    {
        private readonly Action _action;
        private BaseButton _button;

        public Node Source { get; private set; }
        public string Label { get; private set; }
        public int Key { get; private set; }

        public PanelCommand(Node source, string label, Action action, int key = -1)
        {
            Source = source;
            Label = label;
            _action = action;
            Key = key;
        }

        public PanelCommand SetButton(BaseButton button)
        {
            _button = button;
            return this;
        }

        public PanelCommand Execute()
        {
            GD.Print($"[RSLib] Executing command {Label}");
            _action?.Invoke();
            return this;
        }

        public void OnRemoved()
        {
            _button?.QueueFree();
        }
    }
}