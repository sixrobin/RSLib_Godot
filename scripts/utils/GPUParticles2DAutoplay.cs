using Godot;

public partial class GPUParticles2DAutoplay : GpuParticles2D
{
    public override void _Ready()
    {
        base._Ready();
        Restart();
    }
}