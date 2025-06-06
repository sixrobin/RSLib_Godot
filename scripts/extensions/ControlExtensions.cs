namespace RSLib.GE
{
    using Godot;

    public static class ControlExtensions
    {
        public static readonly NodePath SELF_NODE_PATH = new(".");
        
        /// <summary>
        /// Disables controller navigation to neighbours.
        /// Works by redirecting navigation on self, as navigation to neighbours cannot be discarded.
        /// </summary>
        public static void DisableNeighboursFocus(this Control control)
        {
            control.FocusNeighborLeft = SELF_NODE_PATH;
            control.FocusNeighborRight = SELF_NODE_PATH;
            control.FocusNeighborTop = SELF_NODE_PATH;
            control.FocusNeighborBottom = SELF_NODE_PATH;
        }

        /// <summary>
        /// Releases focus on control if it has focus.
        /// </summary>
        public static void ReleaseIfHasFocus(this Control control)
        {
            if (control.HasFocus())
                control.ReleaseFocus();
        }
    }
}
