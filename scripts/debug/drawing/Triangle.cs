namespace RSLib.GE.Debug
{
    using Godot;

    public class Triangle : Shape
    {
        private readonly Vector2[] _points;

        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            _points = new[]
            {
                a, b, c, a,
            };
        }

        public override void Draw(CanvasItem drawer)
        {
            drawer.DrawPolyline(_points, _color, _width);
        }
    }
}