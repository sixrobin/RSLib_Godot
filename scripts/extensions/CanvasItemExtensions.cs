namespace RSLib.GE
{
    using Godot;

    public static class CanvasItemExtensions
    {
        public static CanvasItem SetModulateRed(this CanvasItem canvasItem, float r)
        {
            Color color = canvasItem.Modulate;
            color.R = r;
            canvasItem.Modulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetSelfModulateRed(this CanvasItem canvasItem, float r)
        {
            Color color = canvasItem.SelfModulate;
            color.R = r;
            canvasItem.SelfModulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetModulateGreen(this CanvasItem canvasItem, float g)
        {
            Color color = canvasItem.Modulate;
            color.G = g;
            canvasItem.Modulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetSelfModulateGreen(this CanvasItem canvasItem, float g)
        {
            Color color = canvasItem.SelfModulate;
            color.G = g;
            canvasItem.SelfModulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetModulateBlue(this CanvasItem canvasItem, float b)
        {
            Color color = canvasItem.Modulate;
            color.B = b;
            canvasItem.Modulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetSelfModulateBlue(this CanvasItem canvasItem, float b)
        {
            Color color = canvasItem.SelfModulate;
            color.B = b;
            canvasItem.SelfModulate = color;
            return canvasItem;
        }
        
        public static CanvasItem SetModulateAlpha(this CanvasItem canvasItem, float a)
        {
            canvasItem.Modulate = new Color(canvasItem.Modulate, a);
            return canvasItem;
        }
        
        public static CanvasItem SetSelfModulateAlpha(this CanvasItem canvasItem, float a)
        {
            canvasItem.SelfModulate = new Color(canvasItem.SelfModulate, a);
            return canvasItem;
        }
    }
}