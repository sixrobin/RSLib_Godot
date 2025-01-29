namespace RSLib.GE.Debug;
using Godot;

public class Line : Shape {
    protected Vector2 _a;
    protected Vector2 _b;

    public Line(Vector2 a, Vector2 b) {
        _a = a;
        _b = b;
    }

    public override void Draw(CanvasItem drawer) {
        drawer.DrawLine(_a, _b, Color, Width);
    }

    public float Length() {
        return (_b - _a).Length();
    }
}
