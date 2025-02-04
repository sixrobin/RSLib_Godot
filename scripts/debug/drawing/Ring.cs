namespace RSLib.GE.Debug
{
    using Godot;

    public class Ring : Shape
    {
        private readonly Vector2 _center;
        private readonly float _radius1;
        private readonly float _radius2;
        private readonly int _resolution;

        public Ring(Vector2 center, float radius1, float radius2, int resolution)
        {
            _center = center;
            _radius1 = radius1;
            _radius2 = radius2;
            _resolution = resolution;
        }

        public override void Draw(CanvasItem drawer)
        {
            Shape circle1 = new Circle(_center, _radius1, _resolution).SetColor(_color).SetWidth(_width);
            Shape circle2 = new Circle(_center, _radius2, _resolution).SetColor(_color).SetWidth(_width);
            circle1.Draw(drawer);
            circle2.Draw(drawer);
        }
    }
}