using Godot;

public class Arrow : Line {
    private const float HEAD_LENGTH = 50f;
    private const float HEAD_ANGLE = 60f;

    public Arrow(Vector2 a, Vector2 b) : base(a, b) {
    }

    public override void Draw(CanvasItem drawer) {
        base.Draw(drawer);

        Vector2 arrowDirection = (_a - _b).Normalized();
        float angle = Mathf.DegToRad(HEAD_ANGLE * 0.5f);

        drawer.DrawLine(_b, _b + arrowDirection.Rotated(-angle) * HEAD_LENGTH, Color, Width);
        drawer.DrawLine(_b, _b + arrowDirection.Rotated(angle) * HEAD_LENGTH, Color, Width);
    }
}
