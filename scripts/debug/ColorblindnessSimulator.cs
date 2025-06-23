namespace RSLib.GE.Debug
{
    using Godot;

    public partial class ColorblindnessSimulator : CanvasLayer
    {
        public ColorblindnessSimulator()
        {
            Name = nameof(ColorblindnessSimulator);
            Layer = 128;

            _material = new ShaderMaterial
            {
                Shader = GD.Load<Shader>("res://externals/RSLib_Godot/shaders/debug/colorblind_filter.gdshader"),
            };
            
            ColorRect filterRect = new()
            {
                Material = _material,
                MouseFilter = Control.MouseFilterEnum.Ignore,
            };
            filterRect.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            AddChild(filterRect);
            
            Debugger.CommandPanel.Add(this, "colorblindness", "none", () => _material.SetShaderParameter("severity", 0f));
            Debugger.CommandPanel.Add(this, "colorblindness", "protanomaly", () =>
            {
                _material.SetShaderParameter("mode", 0);
                _material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "deuteranomaly", () =>
            {
                _material.SetShaderParameter("mode", 1);
                _material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "tritanomaly", () =>
            {
                _material.SetShaderParameter("mode", 2);
                _material.SetShaderParameter("severity", 1f);
            });
            Debugger.CommandPanel.Add(this, "colorblindness", "achromatopsia", () =>
            {
                _material.SetShaderParameter("mode", 3);
                _material.SetShaderParameter("severity", 1f);
            });
        }
        
        private ShaderMaterial _material;
    }
}