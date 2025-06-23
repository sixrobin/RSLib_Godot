namespace RSLib.GE.Debug
{
    using Godot;

    public partial class ColorblindnessSimulator : CanvasLayer
    {
        public ColorblindnessSimulator()
        {
            Name = nameof(ColorblindnessSimulator);
            Layer = 128;

            ShaderMaterial material = new()
            {
                Shader = GD.Load<Shader>("res://externals/RSLib_Godot/shaders/debug/colorblind_filter.gdshader"),
            };
            
            ColorRect filterRect = new()
            {
                Material = material,
                MouseFilter = Control.MouseFilterEnum.Ignore,
            };
            filterRect.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(filterRect);
            
            Debugger.CommandPanel.Add(this, "colorblindness", "none", () => material.SetShaderParameter("severity", 0f));
            Debugger.CommandPanel.Add(this, "colorblindness", "protanomaly", () =>
            {
                material.SetShaderParameter("mode", 0);
                material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "deuteranomaly", () =>
            {
                material.SetShaderParameter("mode", 1);
                material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "tritanomaly", () =>
            {
                material.SetShaderParameter("mode", 2);
                material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "achromatopsia", () =>
            {
                material.SetShaderParameter("mode", 3);
                material.SetShaderParameter("severity", 1f);
            });
        }
    }
}