namespace RSLib.GE
{
	using Godot;

	public partial class BounceControl : Control
	{
		[ExportGroup("Motion")]
		[Export] private float _duration = 0.5f;
		[Export] private float _strength = 0.2f;
		[Export] private Tween.TransitionType _easing = Tween.TransitionType.Elastic;

		[ExportGroup("Axis")]
		[Export] private float _strengthX = 1f;
		[Export] private float _strengthY = 1f;

		[ExportGroup("Options")]
		[Export] private bool _smallerY = true;

		private Tween _tween;

		public void PlayBounce(float? duration = null,
							   float? strength = null,
							   Tween.TransitionType? easing = null)
		{
			PivotOffset = Size * 0.5f;

			_tween?.Stop();

			duration ??= _duration;
			strength ??= _strength;
			easing ??= _easing;

			Vector2 bouncedScale = new(1f + strength.Value * _strengthX, 1f + (_smallerY ? -strength.Value : strength.Value) * _strengthY);

			_tween = CreateTween();
			_tween.TweenMethod(Callable.From((Vector2 s) => { Scale = s; }), bouncedScale, Vector2.One, duration.Value)
				  .SetTrans(easing.Value)
				  .SetEase(Tween.EaseType.Out);
			_tween.TweenCallback(Callable.From(() => _tween = null));
		}
	}
}