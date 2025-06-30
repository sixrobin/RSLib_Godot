namespace RSLib.GE.Debug
{
    using Godot;

    public abstract class Shape
    {
        protected Color _color;
        protected float _width;
        protected bool _filled;
        
        public bool AlwaysDraw { get; private set; }

        public abstract void Draw(CanvasItem drawer);

        public Shape SetColor(Color color)
        {
            _color = color;
            return this;
        }

        public Shape SetAlpha(float alpha)
        {
            _color = new Color(_color, alpha);
            return this;
        }

        public Shape SetWidth(float width)
        {
            _width = width;
            return this;
        }

        public Shape SetFilled(bool filled)
        {
            _filled = filled;
            return this;
        }
        
        public Shape SetAlwaysDraw(bool alwaysDraw)
        {
            AlwaysDraw = alwaysDraw;
            return this;
        }
    }
}