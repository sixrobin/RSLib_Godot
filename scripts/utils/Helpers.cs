namespace RSLib.GE;
using Godot;
using System.Collections.Generic;

public static class Helpers {
    public static bool RandomBool() {
	    return GD.Randf() < 0.5f;
    }

    // TODO: tester
    public static List<T> GetChildrenOfType<T>(this Node node) where T : Node {
	    List<T> result = new List<T>();

	    foreach (Node child in node.GetChildren()) {
		    if (child is T typedChild) {
			    result.Add(typedChild);
		    }

		    result.AddRange(child.GetChildrenOfType<T>());
	    }

	    return result;
    }

    // TODO: tester
    public static void QueueFreeChildren(this Node node) {
	    foreach (Node child in node.GetChildren()) {
		    child.QueueFree();
	    }
    }

    public static void Unparent(this Node node) {
	    node.GetParent()?.RemoveChild(node);
    }
    
    // TODO: tester
    public static string FormatByteSize(ulong bytes, bool round = false) {
	    string[] suffixes = { "bytes", "KB", "MB", "GB", "TB", "PB" };
	    int counter = 0;
	    float number = bytes;

	    while (Mathf.Round(number / 1024f) >= 1) {
		    number /= 1024f;
		    counter++;
	    }

	    return (round ? Mathf.Round(number) : number) + suffixes[counter];
    }
}