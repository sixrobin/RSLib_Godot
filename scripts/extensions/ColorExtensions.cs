namespace RSLib.GE
{
    using Godot;

    public static class ColorExtensions
    {
        /// <summary>
        /// Gets a color's copy with new red value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="r">New red value.</param>
        /// <returns>Color with new red value.</returns>
        public static Color WithR(this Color color, float r)
        {
            return new Color(r, color.G, color.B, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new green value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="g">New green value.</param>
        /// <returns>Color with new green value.</returns>
        public static Color WithG(this Color color, float g)
        {
            return new Color(color.R, g, color.B, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new blue value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="b">New blue value.</param>
        /// <returns>Color with new blue value.</returns>
        public static Color WithB(this Color color, float b)
        {
            return new Color(color.R, color.G, b, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new alpha value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="a">New alpha value.</param>
        /// <returns>Color with new alpha value.</returns>
        public static Color WithA(this Color color, float a)
        {
            return new Color(color, a);
        }

        /// <summary>
        /// Linear interpolation between two colors.
        /// </summary>
        /// <param name="a">First color.</param>
        /// <param name="b">Second color.</param>
        /// <param name="t">Lerp factor.</param>
        /// <returns>Interpolated color.</returns>
        public static Color Lerp(Color a, Color b, float t)
        {
            return new Color(Mathf.Lerp(a.R, b.R, t), Mathf.Lerp(a.G, b.G, t), Mathf.Lerp(a.B, b.B, t), Mathf.Lerp(a.A, b.A, t));
        }
    }
}