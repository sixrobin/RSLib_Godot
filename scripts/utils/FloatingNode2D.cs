using Godot;
using RSLib.GE;

public partial class FloatingNode2D : Node2D
{
    [Export] private float _amplitude = 30f;
    [Export] private float _speed = 3f;

    private Node2D _movedNode;
    private float _initY;
    private float _timer;
    
    public override void _Ready()
    {
        base._Ready();

        ProcessThreadGroup = ProcessThreadGroupEnum.SubThread;
        
        _movedNode = GetParent<Node2D>();
        _initY = _movedNode.Position.Y;
        _timer = GD.Randf() * Mathf.Pi;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        _timer += (float)delta * _speed;

        float wave = Mathf.Sin(_timer) * _amplitude;
        _movedNode.CallDeferred(Node2D.MethodName.SetPosition, _movedNode.Position.WithY(_initY + wave));
    }
}
