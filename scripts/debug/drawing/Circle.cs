namespace RSLib.GE.Debug
{
    using Godot;

    public class Circle : Shape
    {
        private readonly Vector2[] _points;

        public Circle(Vector2 center, float radius, int resolution)
        {
            _points = new Vector2[resolution];
            for (int i = 0; i < resolution; ++i)
            {
                double angle = Mathf.DegToRad(360.0 * i / resolution);
                _points[i] = (center + new Vector2((float)Mathf.Cos(angle), (float)Mathf.Sin(angle)) * radius);
            }
        }

        public override void Draw(CanvasItem drawer)
        {
            drawer.DrawPolyline(_points, _color, _width);
        }
    }
}