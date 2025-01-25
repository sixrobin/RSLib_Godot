using Godot;

public class Ring : Shape {
    private Vector2 _center;
    private float _radius1;
    private float _radius2;
    private int _resolution;

    public Ring(Vector2 center, float radius1, float radius2, int resolution) {
        _center = center;
        _radius1 = radius1;
        _radius2 = radius2;
        _resolution = resolution;
    }

    public override void Draw(CanvasItem drawer) {
        Shape circle1 = new Circle(_center, _radius1, _resolution).SetColor(Color).SetWidth(Width);
        Shape circle2 = new Circle(_center, _radius2, _resolution).SetColor(Color).SetWidth(Width);
        circle1.Draw(drawer);
        circle2.Draw(drawer);
    }
}
