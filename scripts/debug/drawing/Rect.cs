namespace RSLib.GE.Debug
{
    using Godot;

    public class Rect : Shape
    {
        private readonly Vector2 _center;
        private readonly Vector2 _size;

        public Rect(Vector2 center, Vector2 size)
        {
            _size = size;
            _center = center - size * 0.5f;
        }

        public override void Draw(CanvasItem drawer)
        {
            Rect2 rect = new Rect2(_center, _size);
            
            if (_filled)
                drawer.DrawRect(rect, _color);
            else
                drawer.DrawRect(rect, _color, false, _width);
        }
    }
}