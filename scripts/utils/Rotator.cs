using Godot;
using RSLib.GE;
using RSLib.GE.Debug;

public partial class Rotator : Node2D
{
    private enum RotationMode
    {
        NONE,
        CONTINUOUS,
        PENDULUM,
    }
    
    [Export] private RotationMode _mode = RotationMode.CONTINUOUS;
    [Export] private Vector2 _speedMinMax = new(90f, 90f);
    [Export(PropertyHint.Range, "0,360,")] private float _maxPendulumAngle = 15f;
    [Export] private bool _clockwise = true;

    private Node2D _rotatedNode;
    private double _pendulumTimer;
    private float _initRotationDegrees;
    private float _speed;

    private void RotateContinuous(double delta)
    {
        float speed = (_clockwise ? _speed : -_speed) * (float)delta;
        _rotatedNode.RotationDegrees += speed;
    }
    
    private void RotatePendulum(double delta)
    {
        float speed = _clockwise ? _speed : -_speed;
        float angle = _maxPendulumAngle * (float)Mathf.Sin(_pendulumTimer * speed);
        _rotatedNode.RotationDegrees = angle;
    }
    
    public override void _Ready()
    {
        base._Ready();

        _rotatedNode = GetParentOrNull<Node2D>();
        if (_rotatedNode == null)
        {
            Debugger.Console.Warning($"No valid {nameof(Node2D)} parent for {nameof(Rotator)}");
            QueueFree();
        }
        
        _pendulumTimer = GD.Randf() * 10f;
        _initRotationDegrees = RotationDegrees;
        _speed = _speedMinMax.Random();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        switch (_mode)
        {
            case RotationMode.CONTINUOUS:
                RotateContinuous(delta);
                break;
            case RotationMode.PENDULUM:
                _pendulumTimer += delta;
                RotatePendulum(delta);
                break;
            default:
                Debugger.Console.Warning($"Unhandled {nameof(Rotator)} mode {_mode}");
                break;
        }
    }
}
