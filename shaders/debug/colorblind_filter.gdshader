shader_type canvas_item;

#include "res://externals/RSLib_Godot/shaders/include/colorblind_matrices.gdshaderinc"
#include "res://externals/RSLib_Godot/shaders/include/helpers.gdshaderinc"

uniform float mode: hint_range(0.0, 2.0, 1.0);
uniform float severity: hint_range(0.0, 1.0, 0.1);
uniform float difference: hint_range(0.0, 1.0, 1.0);

uniform sampler2D screen_texture: hint_screen_texture;

vec3 compute_color_blindness(vec3 color, mat3 severities[11])
{
    int s1 = int(min(10.0, floor(severity * 10.0)));
	mat3 blindness = severities[s1];
    vec3 result = blindness * color.rgb;

    if (difference == 1.0)
    {
        vec3 diff = abs(color.rgb - result);
        result = mix(vec3(luminance(color.rgb)), vec3(1.0, 0.0, 0.0), clamp1_01(dot(difference, 1.0)));
    }

    return clamp3_01(result);
}

void fragment()
{
	vec3 color = texture(screen_texture, SCREEN_UV).rgb;
	
	if (mode == 0.0)
	{
		color = compute_color_blindness(color, protanomaly);
	}
	// TODO: other modes
	
	COLOR.rgb = color;
}