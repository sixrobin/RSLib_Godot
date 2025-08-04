shader_type canvas_item;

uniform float radius_multiplier: hint_range(0.001, 1.0, 0.001) = 1.0;
uniform vec2 smoothing = vec2(0.0, 1.0);

const float BORDER_AA = 0.005f;

void fragment()
{
	float dist = length(UV * 2.0 - 1.0) / max(0.001, radius_multiplier);
	COLOR.a *= smoothstep(0.0, BORDER_AA, 1.0 - dist);
	COLOR.a *= smoothstep(smoothing.x, smoothing.y, dist);
}
