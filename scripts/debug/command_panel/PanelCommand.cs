using Godot;
using System;

public partial class PanelCommand : Node {
    public Node Source;
    public string Label;
    private readonly Action _action;
    public BaseButton Button;
    public int Key;

    public PanelCommand(Node source, string label, Action action, int key = -1) {
        Source = source;
        Label = label;
        _action = action;
        Key = key;
    }

    public PanelCommand SetButton(BaseButton button) {
        Button = button;
        return this;
    }

    public PanelCommand Execute() {
        GD.Print($"[RSLib] Executing command {Label}");
        _action?.Invoke();
        return this;
    }
}
