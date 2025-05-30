using Godot;
using Godot.Collections;

public partial class OneShotSpriteSheet : Sprite2D
{
	[Export] private Array<Texture2D> _frames;
	[Export] private int _fps = 12;

	private double _timer;
	
	public override void _Ready()
	{
		base._Ready();
		Texture = _frames[0];
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		
		_timer += delta * _fps;

		int frame = Mathf.FloorToInt(_timer);
		if (frame >= _frames.Count)
		{
			QueueFree();
			return;
		}
		
		Texture = _frames[frame];
	}
}
