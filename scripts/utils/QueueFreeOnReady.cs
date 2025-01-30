using Godot;

public partial class QueueFreeOnReady : Node
{
	public override void _Ready() {
		base._Ready();
		QueueFree();
	}
}
