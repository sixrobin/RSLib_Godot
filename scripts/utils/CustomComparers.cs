namespace RSLib.GE
{
    using Godot;

    public static class CustomComparers
    {
        public static bool IsClosestTo(Node2D source, Node2D a, Node2D b)
        {
            return (a.GlobalPosition - source.GlobalPosition).LengthSquared() < (b.GlobalPosition - source.GlobalPosition).LengthSquared();
        }
    }
}