float hash21(vec2 input)
{
    return fract(sin(dot(input.xyx, vec3(127.1, 311.7, 74.7))) * 43758.5453123);
}
vec2 hash22(vec2 input)
{
    float a = dot(input.xyx, vec3(127.1, 311.7, 74.7));
    float b = dot(input.yxx, vec3(269.5, 183.3, 246.1));
    return fract(sin(vec2(a, b)) * 43758.5453123);
}
vec3 hash23(vec2 input)
{
    float a = dot(input.xyx, vec3(127.1, 311.7, 74.7));
    float b = dot(input.yxx, vec3(269.5, 183.3, 246.1));
    float c = dot(input.xyy, vec3(113.5, 271.9, 124.6));
    return fract(sin(vec3(a, b, c)) * 43758.5453123);
}

float rand1(float x) { return fract(sin(dot(x, 12.9898)) * 43758.5453); }
float rand2(vec2 v)  { return fract(sin(dot(v, vec2(12.9898, 78.233))) * 43758.5453); }
float rand3(vec3 v)  { return fract(sin(dot(v, vec3(12.9898, 78.233, 45.5432))) * 43758.5453); }