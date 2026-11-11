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
	private Texture2D[] _cachedFrames; // Caching frames to a C# array for a better resource management.

	public void Restart()
	{
		_timer = 0f;
		Texture = _cachedFrames[0];
	}

	private void OnAnimationOver()
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
	}
	
	public override void _Ready()
	{
		base._Ready();
		
		ProcessThreadGroup = ProcessThreadGroupEnum.SubThread;

		_cachedFrames = new Texture2D[_frames.Count];
		for (int i = 0; i < _frames.Count; i++)
			_cachedFrames[i] = _frames[i];

		Restart();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (_callback == AnimationOverCallback.HIDE && !Visible)
			return;
		
		_timer += delta * _fps;

		int frame = Mathf.FloorToInt(_timer);
		if (frame >= _cachedFrames.Length)
		{
			CallDeferred(MethodName.OnAnimationOver);
			return;
		}
		
		CallDeferred(Sprite2D.MethodName.SetTexture, _cachedFrames[frame]);
	}
}
