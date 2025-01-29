namespace RSLib.GE.Debug;
using Godot;

public abstract class Shape {
    public Color Color;
    public float Width;
    public bool AlwaysDraw;

    public abstract void Draw(CanvasItem drawer);

    public Shape SetColor(Color color) {
        Color = color;
        return this;
    }
    
    public Shape SetAlpha(float alpha) {
        Color = new Color(Color, alpha);
        return this;
    }

    public Shape SetWidth(float width) {
        Width = width;
        return this;
    }

    public Shape SetAlwaysDraw(bool alwaysDraw) {
        AlwaysDraw = alwaysDraw;
        return this;
    }
}
