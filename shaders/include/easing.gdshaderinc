float in_back(float t)    { return t * t * (2.70158 * t - 1.70158); }
float out_back(float t)   { return 1.0 + (t - 1.0) * (t - 1.0) * (2.70158 * (t - 1.0) + 1.70158); }
float inout_back(float t) { return t < 0.5 ? 0.5 * (4.0 * t * t * (3.595 * (t * 2.0) - 2.595)) : 0.5 * ((t * 2.0 - 2.0) * (t * 2.0 - 2.0) * (3.595 * (t * 2.0 - 2.0) + 2.595) + 2.0); }


float out_bounce(float t)
{
	if (t < 1.0 / 2.75)
		return 7.5625 * t * t;
	if (t < 2.0 / 2.75)
		return 7.5625 * (t - 1.5 / 2.75) * (t - 1.5 / 2.75) + 0.75;
	if (t < 2.5 / 2.75)
		return 7.5625 * (t - 2.25 / 2.75) * (t - 2.25 / 2.75) + 0.9375;
	return 7.5625 * (t - 2.625 / 2.75) * (t - 2.625 / 2.75) + 0.984375;
}
float in_bounce(float t)    { return 1.0 - out_bounce(1.0 - t); }
float inout_bounce(float t) { return t < 0.5 ? in_bounce(t * 2.0) * 0.5 : out_bounce(t * 2.0 - 1.0) * 0.5 + 0.5; }


float in_circ(float t)    { return -float(sqrt(1.0 - t * t) - 1.0); }
float out_circ(float t)   { return sqrt(1.0 - (t * t - 2.0 * t + 1.0)); }
float inout_circ(float t) { return t < 0.5 ? -0.5 * (sqrt(1.0 - 4.0 * t * t) - 1.0) : 0.5 * (sqrt(1.0 - (4.0 * t * t - 8.0 * t + 4.0)) + 1.0); }


float in_cubic(float t)    { return t * t * t; }
float out_cubic(float t)   { return --t * t * t + 1.0; }
float inout_cubic(float t) { return t < 0.5 ? 4.0 * t * t * t : (t - 1.0) * (4.0 * t * t - 8.0 * t + 4.0) + 1.0; }

float in_elastic(float t)
{
	if (t == 0.0 || t == 1.0)
		return t;
	
	t -= 1.0;
	return -pow(2.0, 10.0 * t) * sin((t * 1.0 - 0.075) * (2.0 * 3.1416) / 0.3);
}
float out_elastic(float t)
{
	if (t == 0.0 || t == 1.0)
		return t;
	
	return pow(2.0, -10.0 * t) * sin((t - 0.075) * 2.0 * 3.1416 / 0.3) + 1.0;
}
float inout_elastic(float t)
{
	if (t == 0.0 || (t / 0.5 == 2.0))
		return t;
	
	t /= 0.5;
	return t < 1.0 ? -0.5 * (pow(2.0, 10.0 * --t) * sin((t - 0.075) * 2.0 * 3.1416 / 0.3)) : (pow(2.0, -10.0 * --t) * sin((t - 0.075) * 2.0 * 3.1416 / 0.3) * 0.5 + 1.0);
}

float in_expo(float t)    { return pow(2.0, 10.0 * (t - 1.0)); }
float out_expo(float t)   { return -pow(2.0, -10.0 * t) + 1.0; }
float inout_expo(float t) { return t < 0.5 ? 0.5 * pow(2.0, 10.0 * (t * 2.0 - 1.0)) : 0.5 * (-pow(2.0, -10.0 * (t * 2.0 - 1.0)) + 2.0); }

float in_quad(float t)    { return t * t; }
float out_quad(float t)   { return t * (2.0 - t); }
float inout_quad(float t) { return t < 0.5 ? 2.0 * t * t : (4.0 - 2.0 * t) * t - 1.0; }

float in_quart(float t)    { return t * t * t * t; }
float out_quart(float t)   { return 1.0 - (--t) * t * t * t; }
float inout_quart(float t) { return t < 0.5 ? 8.0 * t * t * t * t : 1.0 - 8.0 * --t * t * t * t; }

float in_quint(float t)    { return t * t * t * t * t; }
float out_quint(float t)   { return --t * t * t * t * t + 1.0; }
float inout_quint(float t) { return t < 0.5 ? 16.0 * t * t * t * t * t : 16.0 * --t * t * t * t * t + 1.0; }

float in_sine(float t)    { return -cos(t * (3.1416 / 2.0)) + 1.0; }
float out_sine(float t)   { return sin(t * (3.1416 / 2.0)); }
float inout_sine(float t) { return -0.5 * (cos(3.1416 * t) - 1.0); }