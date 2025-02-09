namespace RSLib.GE
{
    using Godot;
    using System.Collections.Generic;

    public static class NodeExtensions
    {
        /// <summary>
        /// Loops through all the children of a node, and stores the ones fitting the asked type.
        /// </summary>
        /// <param name="node">Source node.</param>
        /// <param name="recursive">If true, the loop will also check children of children.</param>
        /// <typeparam name="T">Type to check.</typeparam>
        /// <returns>List of found nodes.</returns>
        public static List<T> GetChildrenOfType<T>(this Node node, bool recursive = true) where T : Node
        {
            List<T> result = new();
            foreach (Node child in node.GetChildren())
            {
                if (child is T typedChild)
                    result.Add(typedChild);

                if (recursive)
                    result.AddRange(child.GetChildrenOfType<T>());
            }

            return result;
        }

        /// <summary>
        /// Calls QueueFree method on all children of the given node.
        /// </summary>
        public static void QueueFreeChildren(this Node node)
        {
            foreach (Node child in node.GetChildren())
                child.QueueFree();
        }

        /// <summary>
        /// Removes all children of the given node (the children won't be in the tree until being reparented).
        /// </summary>
        public static void UnparentChildren(this Node node)
        {
            foreach (Node child in node.GetChildren())
                node.RemoveChild(child);
        }

        /// <summary>
        /// Removes the given node from its parent. Does nothing if the node has no parent.
        /// </summary>
        public static void Unparent(this Node node)
        {
            node.GetParent()?.RemoveChild(node);
        }
    }
}