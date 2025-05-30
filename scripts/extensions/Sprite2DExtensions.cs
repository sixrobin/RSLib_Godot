namespace RSLib.GE
{
    using Godot;

    public static class Sprite2DExtensions
    {
        public static Sprite2D SetModulateRed(this Sprite2D sprite, float r)
        {
            Color color = sprite.Modulate;
            color.R = r;
            sprite.Modulate = color;
            return sprite;
        }
        
        public static Sprite2D SetSelfModulateRed(this Sprite2D sprite, float r)
        {
            Color color = sprite.SelfModulate;
            color.R = r;
            sprite.SelfModulate = color;
            return sprite;
        }
        
        public static Sprite2D SetModulateGreen(this Sprite2D sprite, float g)
        {
            Color color = sprite.Modulate;
            color.G = g;
            sprite.Modulate = color;
            return sprite;
        }
        
        public static Sprite2D SetSelfModulateGreen(this Sprite2D sprite, float g)
        {
            Color color = sprite.SelfModulate;
            color.G = g;
            sprite.SelfModulate = color;
            return sprite;
        }
        
        public static Sprite2D SetModulateBlue(this Sprite2D sprite, float b)
        {
            Color color = sprite.Modulate;
            color.B = b;
            sprite.Modulate = color;
            return sprite;
        }
        
        public static Sprite2D SetSelfModulateBlue(this Sprite2D sprite, float b)
        {
            Color color = sprite.SelfModulate;
            color.B = b;
            sprite.SelfModulate = color;
            return sprite;
        }
        
        public static Sprite2D SetModulateAlpha(this Sprite2D sprite, float a)
        {
            sprite.Modulate = new Color(sprite.Modulate, a);
            return sprite;
        }
        
        public static Sprite2D SetSelfModulateAlpha(this Sprite2D sprite, float a)
        {
            sprite.SelfModulate = new Color(sprite.SelfModulate, a);
            return sprite;
        }
    }
}