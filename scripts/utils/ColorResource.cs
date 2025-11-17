using Godot;

[GlobalClass]
public partial class ColorResource : Resource
{
	[Export] public Color Color { get; private set; }
}
