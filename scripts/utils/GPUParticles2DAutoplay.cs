using Godot;

public partial class GPUParticles2DAutoplay : GpuParticles2D
{
    public async override void _Ready()
    {
        base._Ready();
        
        // Wait for one frame to play to ensure particles are positioned before being drawn.
        Visible = false;
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        Visible = true;
        Restart();
    }
}