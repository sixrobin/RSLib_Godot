const vec3 ZERO = vec3(0.0);
const vec3 HALF = vec3(0.5);
const vec3 ONE  = vec3(1.0);
const vec3 TWO  = vec3(2.0);

vec3 blend_add(vec3 a, vec3 b)          { return min(a + b, 1); }
vec3 blend_average(vec3 a, vec3 b)      { return (a + b) * 0.5; }
vec3 blend_color_burn(vec3 a, vec3 b)   { return b == ZERO ? b : max(ONE - (ONE - a) / b, 0); }
vec3 blend_color_dodge(vec3 a, vec3 b)  { return b == ONE ? b : min(a / (ONE - b), 1); }
vec3 blend_darken(vec3 a, vec3 b)       { return min(a, b); }
vec3 blend_difference(vec3 a, vec3 b)   { return abs(a - b); }
vec3 blend_exclusion(vec3 a, vec3 b)    { return a + b - TWO * a * b; }
vec3 blend_glow(vec3 a, vec3 b)         { return a == ONE ? a : min(b * b / (ONE - a), 1); }
vec3 blend_hard_light(vec3 a, vec3 b)   { return b.r < 0.5 ? (TWO * a * b) : (ONE - TWO * (ONE - a) * (ONE - b)); }
vec3 blend_lighten(vec3 a, vec3 b)      { return max(a, b); }
vec3 blend_linear_burn(vec3 a, vec3 b)  { return max(a + b - ONE, 0); }
vec3 blend_linear_dodge(vec3 a, vec3 b) { return min(a + b, 1); }
vec3 blend_linear_light(vec3 a, vec3 b) { return b.r < 0.5 ? blend_linear_burn(a, TWO * b) : blend_linear_dodge(a, TWO * (b - HALF)); }
vec3 blend_multiply(vec3 a, vec3 b)     { return a * b; }
vec3 blend_negation(vec3 a, vec3 b)     { return ONE - abs(ONE - a - b); }
vec3 blend_normal(vec3 a, vec3 b)       { return b; }
vec3 blend_overlay(vec3 a, vec3 b)      { return a.r < 0.5 ? (TWO * a * b) : (ONE - TWO * (ONE - a) * (ONE - b)); }
vec3 blend_phoenix(vec3 a, vec3 b)      { return min(a, b) - max(a, b) + ONE; }
vec3 blend_pin_light(vec3 a, vec3 b)    { return b.r < 0.5 ? blend_darken(a, TWO * b) : blend_lighten(a, TWO * (b - HALF)); }
vec3 blend_reflect(vec3 a, vec3 b)      { return b == ONE ? b : min(a * a / (ONE - b), ONE); }
vec3 blend_screen(vec3 a, vec3 b)       { return ONE - (ONE - a) * (ONE - b); }
vec3 blend_soft_light(vec3 a, vec3 b)   { return b.r < 0.5 ? (TWO * a * b + a * a * (ONE - TWO * b)) : (sqrt(a) * (TWO * b - ONE) + (TWO * a) * (ONE - b)); }
vec3 blend_subtract(vec3 a, vec3 b)     { return max(a + b - ONE, 0); }
vec3 blend_vivid_light(vec3 a, vec3 b)  { return b.r < 0.5 ? blend_color_burn(a, TWO * b) : blend_color_dodge(a, TWO * (b - HALF)); }
vec3 blend_hard_mix(vec3 a, vec3 b)     { return blend_vivid_light(a, b).r < 0.5 ? ZERO : ONE; }