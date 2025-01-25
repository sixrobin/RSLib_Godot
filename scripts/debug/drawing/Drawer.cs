using Godot;

public partial class Drawer : Node2D {
	private static readonly Color DEFAULT_COLOR = Colors.Yellow;
	private const float DEFAULT_WIDTH = 1f;

	private readonly System.Collections.Generic.List<Shape> _shapes = new();
	private bool _enabled = false;

	public override void _Ready() {
		base._Ready();
		SetZIndex(2^63 - 1);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		QueueRedraw();
	}

	public override void _Draw() {
		base._Draw();

		foreach (Shape shape in _shapes) {
			if (_enabled || shape.AlwaysDraw) {
				shape.Draw(this);
			}
		}
		
		_shapes.Clear();
	}

	public void ToggleVisible() {
		_enabled = !_enabled;
	}

	private Vector2 Vec(object input) {
		return input is Vector2 v ? v : (input as Node2D)!.GlobalPosition;
		
	}

	public Shape Add(Shape shape) {
		_shapes.Add(shape);
		return shape;
	}

	public Shape Line(object a, object b, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Line(Vec(a), Vec(b)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}

	public Shape Arrow(object a, object b, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Arrow(Vec(a), Vec(b)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}

	public Shape Triangle(object a, object b, object c, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Triangle(Vec(a), Vec(b), Vec(c)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}
	
	public Shape Circle(object c, float r, int res, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Circle(Vec(c), r, res).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}
	
	public Shape Ring(object c, float r1, float r2, int res, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Ring(Vec(c), r1, r2, res).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}

	public Shape Rect(object c, Vector2 size, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Rect(Vec(c), size).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}

	public Shape Marker(object position, Color? color = null, float width = DEFAULT_WIDTH) {
		return Add(new Marker(Vec(position)).SetColor(color ?? DEFAULT_COLOR).SetWidth(width));
	}
}
