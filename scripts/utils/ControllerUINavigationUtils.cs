namespace RSLib.GE
{
    using System.Linq;
    using System.Collections.Generic;
    using Godot;

    public static class ControllerUINavigationUtils
    {
        public static void InitGridContainerNavigation(GridContainer grid, bool resetBefore)
        {
            Control[] controls = grid.GetChildren()
                                     .Where(o => !o.IsQueuedForDeletion())
                                     .Cast<Control>()
                                     .ToArray();

            int columns = grid.Columns;
            int rows = Mathf.CeilToInt(controls.Length / (float)columns);

            if (resetBefore)
                foreach (Control control in controls)
                    control.DisableNeighboursFocus();

            for (int i = 0; i < controls.Length; ++i)
            {
                Control current = controls[i];
                bool left = i % columns == 0;
                bool right = (i + 1) % columns == 0;
                bool top = i < columns;
                bool bottom = i >= controls.Length - columns;

                if (columns > 1)
                {
                    if (!left)
                        current.FocusNeighborLeft = new NodePath($"../{controls[i - 1].Name}");

                    if (!right && i + 1 < controls.Length)
                        current.FocusNeighborRight = new NodePath($"../{controls[i + 1].Name}");
                }

                if (rows > 1)
                {
                    if (!top)
                        current.FocusNeighborTop = new NodePath($"../{controls[i - columns].Name}");

                    if (!bottom && i + columns < controls.Length)
                        current.FocusNeighborBottom = new NodePath($"../{controls[i + columns].Name}");
                }
            }
        }

        public static void InitVBoxContainerNavigation(VBoxContainer box, bool resetBefore)
        {
            List<Control> controls = box.GetChildren()
                                        .Where(o => !o.IsQueuedForDeletion())
                                        .Cast<Control>()
                                        .ToList();

            InitVerticalNavigation(controls, resetBefore);
        }

        public static void InitHBoxContainerNavigation(HBoxContainer box, bool resetBefore)
        {
            List<Control> controls = box.GetChildren()
                                        .Where(o => !o.IsQueuedForDeletion())
                                        .Cast<Control>()
                                        .ToList();

            InitHorizontalNavigation(controls, resetBefore);
        }

        public static void InitVerticalNavigation(List<Control> controls, bool resetBefore)
        {
            if (resetBefore)
                foreach (Control control in controls)
                    control.DisableNeighboursFocus();
            
            for (int i = 0; i < controls.Count; ++i)
            {
                Control current = controls[i];

                if (i > 0)
                    current.FocusNeighborTop = current.GetPathTo(controls[i - 1]);

                if (i < controls.Count - 1)
                    current.FocusNeighborBottom = current.GetPathTo(controls[i + 1]);
            }
        }
        
        public static void InitHorizontalNavigation(List<Control> controls, bool resetBefore)
        {
            if (resetBefore)
                foreach (Control control in controls)
                    control.DisableNeighboursFocus();

            for (int i = 0; i < controls.Count; ++i)
            {
                Control current = controls[i];

                if (i > 0)
                    current.FocusNeighborLeft = current.GetPathTo(controls[i - 1]);

                if (i < controls.Count - 1)
                    current.FocusNeighborRight = current.GetPathTo(controls[i + 1]);
            }
        }

        public static void GrabFocusIf(this Control control, bool condition)
        {
            if (condition)
                control.GrabFocus();
        }
    }
}