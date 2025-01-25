using Godot;

public class Rect : Shape {
    private Vector2 _center;
    private Vector2 _size;

    public Rect(Vector2 center, Vector2 size) {
        _size = size;
        _center = _center - size / 2f;
    }

    public override void Draw(CanvasItem drawer) {
        Rect2 rect = new Rect2(_center, _size);
        drawer.DrawRect(rect, Color, false, Width);
    }

}
