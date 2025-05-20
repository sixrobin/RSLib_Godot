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

	public void Reset()
	{
		if (_target == null)
			return;
		
		// TODO: store default values and restore them.
	}
	
	public void Randomize()
	{
		if (_target == null)
			return;

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
