namespace RSLib.GE
{
    using Godot;

    public partial class CircleShapeVisualizer : CollisionShape2D
    {
        [Export] private Color _color = Colors.Magenta;

        public override void _Process(double delta)
        {
            base._Process(delta);

            if (Shape is CircleShape2D circleShape2D)
                Debug.Debugger.Drawer?.Circle(this, circleShape2D.Radius * GlobalScale.X).SetColor(_color);
        }
    }
}