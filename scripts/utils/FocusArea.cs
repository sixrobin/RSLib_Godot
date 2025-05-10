namespace RSLib.GE
{
    using Debug;
    using Godot;

    public partial class FocusArea : Node2D
    {
        [Export] private Node2D _target;
        [Export] private Vector2 _size = Vector2.One * 100f;
        [Export] private Color _debugColor = Colors.Blue;

        private void FocusTarget()
        {
            Vector2 halfSize = _size * 0.5f;
            Vector2 offset = _target.GlobalPosition - GlobalPosition;
            
            if (offset.X > halfSize.X)
                GlobalPosition += Vector2.Right * (offset.X - halfSize.X);
            else if (offset.X < -halfSize.X)
                GlobalPosition += Vector2.Right * (offset.X + halfSize.X);
            
            if (offset.Y > halfSize.Y)
                GlobalPosition += Vector2.Down * (offset.Y - halfSize.Y);
            else if (offset.Y < -halfSize.Y)
                GlobalPosition += Vector2.Down * (offset.Y + halfSize.Y);
        }
        
        public override void _Ready()
        {
            base._Ready();
            
            TopLevel = true;
            GlobalPosition = _target.GlobalPosition;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            
            FocusTarget();
            
            Debugger.Drawer?.Marker(_target).SetColor(_debugColor).SetWidth(2);
            Debugger.Drawer?.Rect(this, _size).SetColor(_debugColor).SetWidth(2);
        }
    }
}