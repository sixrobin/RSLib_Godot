namespace RSLib.GE
{
    using Godot;

    public partial class Bounce : Node2D
    {
        [Export] private float _duration = 0.5f;
        [Export] private float _strength = 0.2f;
        [Export] private Tween.TransitionType _easing = Tween.TransitionType.Elastic;

        private Tween _tween;
        
        public void PlayBounce(float? duration = null,
                               float? strength = null,
                               Tween.TransitionType? easing = null)
        {
            _tween?.Stop();

            duration ??= _duration;
            strength ??= _strength;
            easing ??= _easing;
            
            Vector2 bouncedScale = new(1f + strength.Value, 1f - strength.Value);
        
            _tween = CreateTween();
            _tween.TweenMethod(Callable.From((Vector2 s) => { Scale = s; }), bouncedScale, Vector2.One, duration.Value)
                  .SetTrans(easing.Value)
                  .SetEase(Tween.EaseType.Out);
            _tween.TweenCallback(Callable.From(() => _tween = null));
        }
    }
}