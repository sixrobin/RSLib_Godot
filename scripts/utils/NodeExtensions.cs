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
        public static T[] GetChildrenOfType<T>(this Node node, bool recursive = true) where T : Node
        {
            List<T> result = new();
            foreach (Node child in node.GetChildren())
            {
                if (child is T typedChild)
                    result.Add(typedChild);

                if (recursive)
                    result.AddRange(child.GetChildrenOfType<T>());
            }

            return result.ToArray();
        }

        /// <summary>
        /// Loops upward through all the node hierarchy, and returns the first parent fitting the asked type.
        /// </summary>
        /// <param name="node">Source node.</param>
        /// <typeparam name="T">Type to check.</typeparam>
        /// <returns>Fitting parent found.</returns>
        public static T GetFirstParentOfType<T>(this Node node)
        {
            Node result = node;
            
            do
                result = result.GetParent();
            while (result != null && result is not T);

            return result is T resultCast ? resultCast : default;
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
        public static void RemoveChildren(this Node node)
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
        
        /// <summary>
        /// Moves the node up by one step in its siblings hierarchy.
        /// </summary>
        public static void MoveSiblingUp(this Node node)
        {
            node.GetParent()?.MoveChild(node, node.GetIndex() - 1);
        }

        /// <summary>
        /// Moves the node down by one step in its siblings hierarchy.
        /// </summary>
        public static void MoveSiblingDown(this Node node)
        {
            node.GetParent()?.MoveChild(node, node.GetIndex() + 1);
        }
        
        /// <summary>
        /// GetNode version that returns a boolean depending on the fetch success, and the fetched node itself as an out parameter.
        /// </summary>
        /// <param name="node">Source node.</param>
        /// <param name="nodePath">Path to the node to fetch.</param>
        /// <param name="result">Fetched node.</param>
        /// <returns>True if a node was fetched, else false.</returns>
        public static bool TryGetNode(this Node node, string nodePath, out Node result)
        {
            result = node.GetNode(nodePath);
            return result != null;
        }

        /// <summary>
        /// GetNode version that returns a boolean depending on the fetch success, and the fetched node itself as an out parameter.
        /// </summary>
        /// <param name="node">Source node.</param>
        /// <param name="nodePath">Path to the node to fetch.</param>
        /// <param name="result">Fetched node.</param>
        /// <returns>True if a node was fetched, else false.</returns>
        public static bool TryGetNode<T>(this Node node, string nodePath, out T result) where T : Node
        {
            result = node.GetNode<T>(nodePath);
            return result != null;
        }
    }
}