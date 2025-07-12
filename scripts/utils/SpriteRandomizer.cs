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

	private static RandomNumberGenerator _rng;

	private bool _initValuesStored;
	private Texture2D _initTexture;
	private bool _initFlipH;
	private bool _initFlipV;
	private Vector2 _initScale;
	private Color _initColor;
	private Vector2 _initOffset;

	public static void SetRandomNumberGenerator(RandomNumberGenerator rng)
	{
		_rng = rng;
	}
	
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
				
		_rng ??= new RandomNumberGenerator();
		
		if (_textures is {Count: > 0})
			_target.Texture = _textures[_rng.RandiRange(0, _textures.Count - 1)];

		if (_randomizeFlipH)
			_target.FlipH = Helpers.RandomBool(_rng);
		if (_randomizeFlipV)
			_target.FlipV = Helpers.RandomBool(_rng);

		if (_randomColor != null)
			_target.Modulate = _randomColor.Sample(_rng.Randf());

		_target.Scale *= _rng.RandfRange(_randomScale.X, _randomScale.Y);
		if (_randomizeFlipScaleX)
			_target.Scale = new Vector2(_target.Scale.X * (Helpers.RandomBool(_rng) ? 1f : -1f), _target.Scale.Y);
		if (_randomizeFlipScaleY)
			_target.Scale = new Vector2(_target.Scale.X, _target.Scale.Y * (Helpers.RandomBool(_rng) ? 1f : -1f));
		
		_target.Position += Vector2.Right.Rotated(_rng.Randf() * Mathf.Tau) * _rng.Randf() * _randomOffsetMax;
	}
	
	public override void _Ready()
	{
		base._Ready();
		Randomize();
	}
}
