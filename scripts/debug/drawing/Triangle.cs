namespace RSLib.GE.Debug
{
    using System.Linq;
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
            if (_filled)
                drawer.DrawPolygon(_points, Enumerable.Repeat(_color, _points.Length).ToArray());
            else
                drawer.DrawPolyline(_points, _color, _width);
        }
    }
}