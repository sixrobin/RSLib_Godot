namespace RSLib.GE
{
    using Godot;

    public static class Sprite2DExtensions
    {
        public static Sprite2D SetAlpha(this Sprite2D sprite, float alpha)
        {
            sprite.Modulate = new Color(sprite.Modulate, alpha);
            return sprite;
        }
    }
}