using Godot;

public partial class AnimationPlayerStartAnim : AnimationPlayer
{
	[Export] private string _animationName;
	
	public override void _Ready()
	{
		if (!string.IsNullOrEmpty(_animationName))
			Play(_animationName);
	}
}
