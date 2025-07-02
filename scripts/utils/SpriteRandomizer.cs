using Godot;
using Godot.Collections;
using RSLib.GE;

public partial class SpriteRandomizer : Node2D
{
	[Export] private Sprite2D _target;
	[Export] private Array<Texture2D> _textures;
	[Export] private bool _randomizeFlipH;
	[Export] private bool _randomizeFlipV;
	[Export] private bool _randomizeFlipScaleX;
	[Export] private bool _randomizeFlipScaleY;
	[Export] private Vector2 _randomScale = Vector2.One;
	[Export] private Gradient _randomColor;
	[Export] private float _randomOffsetMax;

	private bool _initValuesStored;
	private Texture2D _initTexture;
	private bool _initFlipH;
	private bool _initFlipV;
	private Vector2 _initScale;
	private Color _initColor;
	private Vector2 _initOffset;
    
	private void StoreInitValues()
	{
		_initTexture = _target.Texture;
		_initFlipH = _target.FlipH;
		_initFlipV = _target.FlipV;
		_initScale = _target.Scale;
		_initColor = _target.Modulate;
		_initOffset = _target.Position;
        
		_initValuesStored = true;
	}
	
	public void Reset()
	{
		if (_target == null)
			return;

		_target.Texture = _initTexture;
		_target.FlipH = _initFlipH;
		_target.FlipV = _initFlipV;
		_target.Scale = _initScale;
		_target.Modulate = _initColor;
		_target.Position = _initOffset;
	}
	
	public void Randomize()
	{
		if (_target == null)
			return;

		if (!_initValuesStored)
			StoreInitValues();

		if (_textures is {Count: > 0})
			_target.Texture = _textures.PickRandom();

		if (_randomizeFlipH)
			_target.FlipH = Helpers.RandomBool();
		if (_randomizeFlipV)
			_target.FlipV = Helpers.RandomBool();

		if (_randomColor != null)
			_target.Modulate = _randomColor.Sample(GD.Randf());

		_target.Scale *= _randomScale.Random();
		if (_randomizeFlipScaleX)
			_target.Scale = new Vector2(_target.Scale.X * (Helpers.RandomBool() ? 1f : -1f), _target.Scale.Y);
		if (_randomizeFlipScaleY)
			_target.Scale = new Vector2(_target.Scale.X, _target.Scale.Y * (Helpers.RandomBool() ? 1f : -1f));

		_target.Position += Vector2.Right.Rotated(GD.Randf() * Mathf.Tau) * GD.Randf() * _randomOffsetMax;
	}
	
	public override void _Ready()
	{
		base._Ready();
		Randomize();
	}
}
