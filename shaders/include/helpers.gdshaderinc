float clamp1_01(float value) { return clamp(value, 0.0, 1.0); }
vec2 clamp2_01(vec2 value)   { return vec2(clamp1_01(value.x), clamp1_01(value.y)); }
vec3 clamp3_01(vec3 value)   { return vec3(clamp1_01(value.x), clamp1_01(value.y), clamp1_01(value.z)); }
vec4 clamp4_01(vec4 value)   { return vec4(clamp1_01(value.x), clamp1_01(value.y), clamp1_01(value.z), clamp1_01(value.w)); }

vec2 pow2(vec2 value, float power) { return vec2(pow(value.x, power), pow(value.y, power)); }
vec3 pow3(vec3 value, float power) { return vec3(pow(value.x, power), pow(value.y, power), pow(value.z, power)); }
vec4 pow4(vec4 value, float power) { return vec4(pow(value.x, power), pow(value.y, power), pow(value.z, power), pow(value.w, power)); }

vec2 rotate(vec2 uv, float theta, vec2 pivot)
{
	float t = radians(theta);
    float c = cos(t);
    float s = sin(t);
    return vec2(c * (uv.x - pivot.x) + s * (uv.y - pivot.y) + pivot.x, c * (uv.y - pivot.y) - s * (uv.x - pivot.x) + pivot.y);
}

float luminance(vec3 color)
{
    const vec3 w = vec3(0.2125, 0.7154, 0.0721);
    return dot(color, w);
}