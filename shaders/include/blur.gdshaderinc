// https://godotshaders.com/shader/gaussian-blur-functions-for-gles2/

vec4 texture_xorgaussian(sampler2D tex, vec2 uv, vec2 pixel_size, float blurriness, int iterations, int quality)
{
	vec2 radius = blurriness / (1.0 / pixel_size).xy;
	vec4 blurred_tex = texture(tex, uv);
	
	for (float d = 0.0; d < TAU; d += TAU / float(iterations))
	{
		for (float i = 1.0 / float(quality); i <= 1.0; i += 1.0 / float(quality))
		{
			vec2 directions = uv + vec2(cos(d), sin(d)) * radius * i;
			blurred_tex += texture(tex, directions);
		}
	}
	
	blurred_tex /= float(quality) * float(iterations) + 1.0;
	return blurred_tex;
}