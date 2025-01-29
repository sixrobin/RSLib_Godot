namespace RSLib.GE.Debug;
using Godot;

public class Marker : Shape {
    private const float SIZE = 50f;

    private Vector2 _position;
    private float _angle;

    public Marker(Vector2 position) {
        _position = position;
    }

    public override void Draw(CanvasItem drawer) {
        float angle = Mathf.DegToRad(_angle);

        Vector2 a1 = _position + new Vector2(0f, SIZE * 0.5f).Rotated(angle);
        Vector2 a2 = _position - new Vector2(0f, SIZE * 0.5f).Rotated(angle);
        drawer.DrawLine(a1, a2, Color, Width);

        Vector2 b1 = _position + new Vector2(SIZE * 0.5f, 0f).Rotated(angle);
        Vector2 b2 = _position - new Vector2(SIZE * 0.5f, 0f).Rotated(angle);
        drawer.DrawLine(b1, b2, Color, Width);
    }

    public Marker SetAngle(float angle) {
        _angle = angle;
        return this;
    }
}
