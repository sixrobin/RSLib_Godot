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
        
        /// <summary>
        /// Resets all navigation neighbors.
        /// </summary>
        static public void ResetNavigation(this Control control)
        {
            control.FocusNeighborLeft = SELF_NODE_PATH;
            control.FocusNeighborRight = SELF_NODE_PATH;
            control.FocusNeighborTop = SELF_NODE_PATH;
            control.FocusNeighborBottom = SELF_NODE_PATH;
        }
	
        /// <summary>
        /// Sets right navigation neighbor.
        /// </summary>
        static public void SetFocusRight(this Control control, Node target)
        {
            control.FocusNeighborRight = control.GetPathTo(target);
        }
	
        /// <summary>
        /// Sets left navigation neighbor.
        /// </summary>
        static public void SetFocusLeft(this Control control, Node target)
        {
            control.FocusNeighborLeft = control.GetPathTo(target);
        }
        
        /// <summary>
        /// Sets top navigation neighbor.
        /// </summary>
        static public void SetFocusTop(this Control control, Node target)
        {
            control.FocusNeighborTop = control.GetPathTo(target);
        }
	
        /// <summary>
        /// Sets bottom navigation neighbor.
        /// </summary>
        static public void SetFocusBottom(this Control control, Node target)
        {
            control.FocusNeighborBottom = control.GetPathTo(target);
        }
    }
}
