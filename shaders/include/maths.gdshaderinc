float remap(float value, float from1, float to1, float from2, float to2) { return (value - from1) / (to1 - from1) * (to2 - from2) + from2; }
vec2 remap2(vec2 value, vec2 from1, vec2 to1, vec2 from2, vec2 to2)      { return (value - from1) / (to1 - from1) * (to2 - from2) + from2; }
vec3 remap3(vec3 value, vec3 from1, vec3 to1, vec3 from2, vec3 to2)      { return (value - from1) / (to1 - from1) * (to2 - from2) + from2; }
vec4 remap4(vec4 value, vec4 from1, vec4 to1, vec4 from2, vec4 to2)      { return (value - from1) / (to1 - from1) * (to2 - from2) + from2; }

float inverse_lerp(float a, float b, float v) { return (v - a) / (b - a); }
vec2 inverse_lerp2(vec2 a, vec2 b, vec2 v)    { return (v - a) / (b - a); }
vec3 inverse_lerp3(vec3 a, vec3 b, vec3 v)    { return (v - a) / (b - a); }
vec4 inverse_lerp4(vec4 a, vec4 b, vec4 v)    { return (v - a) / (b - a); }

float atan2(float x, float y)
{
    if (x == 0.0 && y == 0.0)
        return 0.0;

    float a = atan(y / x);
    if (x < 0.0)
		a += PI * sign(y);

    return a;
}

vec2 cartesian_to_polar(vec2 cartesian) { return vec2(atan2(cartesian.y, cartesian.x) / (PI * 2.0), length(cartesian)); }
vec2 polar_to_cartesian(vec2 polar)     { return vec2(sin(polar.x * (PI * 2.0)), cos(polar.x * (PI * 2.0))) * polar.y; }