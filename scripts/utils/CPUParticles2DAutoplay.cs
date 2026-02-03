using Godot;

public partial class CPUParticles2DAutoplay : CpuParticles2D
{
    public override void _Ready()
    {
        base._Ready();
        Restart();
    }
}
