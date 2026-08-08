shader_type canvas_item;

#include "include/color_correction.gdshaderinc"

uniform float desaturation: hint_range(0.0, 1.0) = 0.0;

void fragment()
{
	COLOR.rgb = desaturate(COLOR.rgb, desaturation);
}
