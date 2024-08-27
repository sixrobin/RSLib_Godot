vec3 rgb_to_hsv(vec3 color)
{
	vec4 k = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	vec4 p = mix(vec4(color.bg, k.wz), vec4(color.gb, k.xy), step(color.b, color.g));
	vec4 q = mix(vec4(p.xyw, color.r), vec4(color.r, p.yzx), step(p.x, color.r));
	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

vec3 hsv_to_rgb(vec3 color)
{
	vec4 k = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	vec3 p = abs(fract(color.xxx + k.xyz) * 6.0 - k.www);
	return color.z * mix(k.xxx, clamp(p - k.xxx, 0.0, 1.0), color.y);
}

// Hue range: [0,1]
vec3 shift_hue(vec3 color, float hue)
{
	vec3 color_hsv = rgb_to_hsv(color);
	color_hsv.r += hue / 360.0;
    return hsv_to_rgb(color_hsv);
}

// Saturation range: [0, 1]
vec3 saturate(vec3 color, float saturation)
{
	vec3 color_hsv = rgb_to_hsv(color);
	color_hsv.g += saturation;
    return hsv_to_rgb(color_hsv);
}

// Desaturation range: [0, 1]
vec3 desaturate(vec3 color, float desaturation)
{
    vec3 grayXfer = vec3(0.3, 0.59, 0.11);
    float grayf = dot(grayXfer, color);
    vec3 gray = vec3(grayf);
    return vec3(mix(color, gray, desaturation));
}