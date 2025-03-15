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
        public static Color WithR(this Color color, float r)
        {
            return new Color(r, color.G, color.B, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new green value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="g">New green value.</param>
        public static Color WithG(this Color color, float g)
        {
            return new Color(color.R, g, color.B, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new blue value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="b">New blue value.</param>
        public static Color WithB(this Color color, float b)
        {
            return new Color(color.R, color.G, b, color.A);
        }
        
        /// <summary>
        /// Gets a color's copy with new alpha value.
        /// </summary>
        /// <param name="color">Source color.</param>
        /// <param name="a">New alpha value.</param>
        public static Color WithA(this Color color, float a)
        {
            return new Color(color, a);
        }
    }
}