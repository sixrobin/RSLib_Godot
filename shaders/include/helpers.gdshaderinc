float clamp01(float value)
{
	return clamp(value, 0.0, 1.0);
}

vec2 rotate(vec2 uv, float theta, vec2 pivot)
{
	float t = radians(theta);
    float c = cos(t);
    float s = sin(t);
    return vec2(c * (uv.x - pivot.x) + s * (uv.y - pivot.y) + pivot.x, c * (uv.y - pivot.y) - s * (uv.x - pivot.x) + pivot.y);
}