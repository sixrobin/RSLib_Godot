using Godot;
using Godot.Collections;
using RSLib.GE.Debug;

public partial class OneShotSpriteSheet : Sprite2D
{
	private enum AnimationOverCallback
	{
		QUEUE_FREE,
		FREE,
		HIDE,
	}
	
	[Export] private Array<Texture2D> _frames;
	[Export] private int _fps = 12;
	[Export] private AnimationOverCallback _callback = AnimationOverCallback.QUEUE_FREE; 

	private double _timer;

	public void Restart()
	{
		_timer = 0f;
		Texture = _frames[0];
	}
	
	public override void _Ready()
	{
		base._Ready();

		Restart();
		ProcessThreadGroup = ProcessThreadGroupEnum.SubThread;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (_callback == AnimationOverCallback.HIDE && !Visible)
			return;
		
		_timer += delta * _fps;

		int frame = Mathf.FloorToInt(_timer);
		if (frame >= _frames.Count)
		{
			switch (_callback)
			{
				case AnimationOverCallback.QUEUE_FREE:
					QueueFree();
					break;
				case AnimationOverCallback.FREE:
					Free();
					break;
				case AnimationOverCallback.HIDE:
					CallDeferred(CanvasItem.MethodName.SetVisible, false);
					break;
				default:
					Debugger.Console.Warning($"Unhandled {nameof(OneShotSpriteSheet)} callback type {_callback}.");
					break;
			}
			
			return;
		}
		
		CallDeferred(Sprite2D.MethodName.SetTexture, _frames[frame]);
	}
}
