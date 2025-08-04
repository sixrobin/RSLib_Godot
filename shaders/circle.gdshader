shader_type canvas_item;

const float REF_SCALE = 100.0;
const float BORDER_AA = 0.005f;

uniform float radius_multiplier: hint_range(0.001, 1.0, 0.001) = 1.0;
uniform vec2 smoothing = vec2(0.0, 1.0);

// Designed to be set from code to keep the same circle width between nodes of different scales.
uniform float node_scale = 100.0; // = REF_SCALE.

void fragment()
{
	float dist = length(UV * 2.0 - 1.0) / max(0.001, radius_multiplier);
	float width_ratio = 1.0 - REF_SCALE / node_scale;
	
	float smoothingX = smoothing.x + (1.0 - smoothing.x) * width_ratio;
	float smoothingY = smoothing.y + (1.0 - smoothing.y) * width_ratio;
	
	COLOR.a *= smoothstep(0.0, BORDER_AA, 1.0 - dist);
	COLOR.a *= smoothstep(smoothingX, smoothingY, dist);
}
