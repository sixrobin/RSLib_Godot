shader_type canvas_item;

uniform bool fractional = false;

void fragment()
{
	vec2 uv = fractional ? fract(UV) : UV;
	COLOR = vec4(uv, 0.0, 1.0);
}