namespace RSLib.GE
{
    using Godot;

    public static class Node2DExtensions
    {
        /// <summary>
        /// Sets both the rotation and skew to the same value.
        /// </summary>
        /// <param name="node2D">Source node.</param>
        /// <param name="radians">Rotation and skew to set (in radians).</param>
        public static void SetRotationAndSkew(this Node2D node2D, float radians)
        {
            node2D.Rotation = radians;
            node2D.Skew = radians;
        }
    }
}