namespace RSLib.GE.Debug
{
    using Godot;

    public partial class Drawer : Node2D
    {
        private static readonly Color DEFAULT_COLOR = Colors.Yellow;
        private const float DEFAULT_WIDTH = 1f;

        private readonly System.Collections.Generic.List<Shape> _shapes = new();
        private bool _enabled;

        private static Vector2 ToVector2(object input)
        {
            return input is Vector2 v ? v : (input as Node2D)!.GlobalPosition;
        }
        
        public void Init()
        {
            SetZIndex(2 ^ 63 - 1);
        }

        public void ToggleVisible()
        {
            _enabled = !_enabled;
        }

        public Shape Add(Shape shape)
        {
            _shapes.Add(shape);
            return shape;
        }

        public Shape Line(object a, object b, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Line(ToVector2(a), ToVector2(b)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Arrow(object a, object b, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Arrow(ToVector2(a), ToVector2(b)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Triangle(object a, object b, object c, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Triangle(ToVector2(a), ToVector2(b), ToVector2(c)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Circle(object center, float radius, int resolution = 16, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Circle(ToVector2(center), radius, resolution).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Ring(object center, float radius1, float radius2, int resolution = 16, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Ring(ToVector2(center), radius1, radius2, resolution).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Rect(object center, Vector2 size, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Rect(ToVector2(center), size).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }

        public Shape Marker(object position, Color? color = null, float width = DEFAULT_WIDTH)
        {
            return Add(new Marker(ToVector2(position)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
        }
        
        public override void _Process(double delta)
        {
            base._Process(delta);
            QueueRedraw();
        }

        public override void _Draw()
        {
            base._Draw();

            foreach (Shape shape in _shapes)
                if (_enabled || shape.AlwaysDraw)
                    shape.Draw(this);

            _shapes.Clear();
        }
    }
}