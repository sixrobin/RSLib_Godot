shader_type canvas_item;

#include "res://externals/RSLib_Godot/shaders/include/colorblind_matrices.gdshaderinc"
#include "res://externals/RSLib_Godot/shaders/include/helpers.gdshaderinc"

uniform float mode: hint_range(0.0, 2.0, 1.0);
uniform float severity: hint_range(0.0, 1.0, 0.1);
uniform bool show_difference;

uniform sampler2D screen_texture: hint_screen_texture;

vec3 compute_color_blindness(vec3 color, mat3 severities[11])
{
    int s = int(min(10.0, floor(severity * 10.0)));
	mat3 blindness = severities[s];
    vec3 result = color.rgb * blindness;

    if (show_difference)
    {
        vec3 diff = abs(color.rgb - result);
        result = mix(vec3(luminance(color.rgb)), vec3(1.0, 0.0, 0.0), clamp1_01(dot(diff, vec3(1.0))));
    }

    return clamp3_01(result);
}

void fragment()
{
	vec3 color = texture(screen_texture, SCREEN_UV).rgb;
	
	if (mode == 0.0)
		COLOR.rgb = compute_color_blindness(color, protanomaly);
	else if (mode == 1.0)
		COLOR.rgb = compute_color_blindness(color, deuteranomaly);
	else if (mode == 2.0)
		COLOR.rgb = compute_color_blindness(color, tritanomaly);
}