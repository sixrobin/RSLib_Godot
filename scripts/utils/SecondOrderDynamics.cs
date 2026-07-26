namespace RSLib.GE
{
    using Godot;

    public class SecondOrderDynamics
    {
        public SecondOrderDynamics(float frequency, float damping, float responsiveness, Vector2 initPosition)
        {
            _k1 = damping / (Mathf.Pi * frequency);
            _k2 = 1f / ((Mathf.Pi * frequency * 2f) * (Mathf.Pi * frequency * 2f));
            _k3 = responsiveness * damping / (Mathf.Pi * frequency * 2f);
            
            RefreshInitPosition(initPosition);
        }
        
        private readonly float _k1;
        private readonly float _k2;
        private readonly float _k3;
        
        private Vector2 _xp;
        private Vector2 _y;
        private Vector2 _yd;

        public void RefreshInitPosition(Vector2 initPosition)
        {
            _xp = initPosition;
            _y = initPosition;
            _yd = Vector2.Zero;
        }
        
        public Vector2 Update(float time, Vector2 to, Vector2? velocity = null)
        {
            // Velocity estimation.
            if (velocity == null)
            {
                velocity = (to - _xp) / time;
                _xp = to;
            }

            float k2Stable = Mathf.Max(_k2, 1.1f * (time * time / 4f + time * _k1 / 2f));
            _y += time * _yd;
            _yd += time * (to + _k3 * velocity.Value - _y - _k1 * _yd) / k2Stable;

            return _y;
        }
    }
}