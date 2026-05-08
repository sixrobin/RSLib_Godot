using Godot;
using RSLib.GE;

public partial class Shake : Node2D
{
    [Export] private float _intensity = 1f;
    [Export] private float _maxDistance = 10f;
    [Export] private float _maxSkew;

    public void SetIntensity(float intensity)
    {
        _intensity = intensity;
    }
    
    public override void _Process(double delta)
    {
        base._Process(delta);

        Position = Helpers.RandomVector2() * GD.Randf() * _maxDistance * _intensity;
        Skew = Mathf.DegToRad(GD.Randf() * _maxSkew) * _intensity;
    }
}
