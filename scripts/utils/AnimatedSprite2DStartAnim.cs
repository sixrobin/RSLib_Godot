using Godot;

public partial class AnimatedSprite2DStartAnim : AnimatedSprite2D
{
    [Export] private string _animationName;
    [Export] private bool _randomize;
	
    public override void _Ready()
    {
        if (!string.IsNullOrEmpty(_animationName))
        {
            Play(_animationName);
            
            if (_randomize)
                FrameProgress = GD.Randf();
        }
    }
}