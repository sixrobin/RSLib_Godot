namespace RSLib.GE.Debug
{
    using Godot;

    public partial class ColorblindnessSimulator : CanvasLayer
    {
        public readonly static StringName SHADER_PARAM_MODE = new("mode");
        public readonly static StringName SHADER_PARAM_SEVERITY = new("severity");
        
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
            
            Debugger.CommandPanel.Add(this, "colorblindness", "none", () => material.SetShaderParameter(SHADER_PARAM_SEVERITY, 0f));
            Debugger.CommandPanel.Add(this, "colorblindness", "protanomaly", () =>
            {
                material.SetShaderParameter(SHADER_PARAM_MODE, 0);
                material.SetShaderParameter(SHADER_PARAM_SEVERITY, 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "deuteranomaly", () =>
            {
                material.SetShaderParameter(SHADER_PARAM_MODE, 1);
                material.SetShaderParameter(SHADER_PARAM_SEVERITY, 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "tritanomaly", () =>
            {
                material.SetShaderParameter(SHADER_PARAM_MODE, 2);
                material.SetShaderParameter(SHADER_PARAM_SEVERITY, 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "achromatopsia", () =>
            {
                material.SetShaderParameter(SHADER_PARAM_MODE, 3);
                material.SetShaderParameter(SHADER_PARAM_SEVERITY, 1f);
            });
        }
    }
}