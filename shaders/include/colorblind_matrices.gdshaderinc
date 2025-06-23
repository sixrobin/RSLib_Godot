const mat3 null_anomaly = mat3
(
	vec3(1.0, 0.0, 0.0),
	vec3(0.0, 1.0, 0.0),
	vec3(0.0, 0.0, 1.0)
);


// Protanomaly (L cone - Red).

const mat3 protanomaly0 = mat3
(
	vec3(+0.856167, +0.182038, -0.038205),
	vec3(+0.029342, +0.955115, +0.015544),
	vec3(-0.002880, -0.001563, +1.004443)
);
const mat3 protanomaly1 = mat3
(
	vec3(+0.734766, +0.334872, -0.069637),
	vec3(+0.051840, +0.919198, +0.028963),
	vec3(-0.004928, -0.004209, +1.009137)
);
const mat3 protanomaly2 = mat3
(
	vec3(+0.630323, +0.465641, -0.095964),
	vec3(+0.069181, +0.890046, +0.040773),
	vec3(-0.006308, -0.007724, +1.014032)
);
const mat3 protanomaly3 = mat3
(
	vec3(+0.539009, +0.579343, -0.118352),
	vec3(+0.082546, +0.866121, +0.051332),
	vec3(-0.007136, -0.011959, +1.019095)
);
const mat3 protanomaly4 = mat3
(
	vec3(+0.458064, +0.679578, -0.137642),
	vec3(+0.092785, +0.846313, +0.060902),
	vec3(-0.007494, -0.016807, +1.024301)
);
const mat3 protanomaly5 = mat3
(
	vec3(+0.385450, +0.769005, -0.154455),
	vec3(+0.100526, +0.829802, +0.069673),
	vec3(-0.007442, -0.022190, +1.029632)
);
const mat3 protanomaly6 = mat3
(
	vec3(+0.319627, +0.849633, -0.169261),
	vec3(+0.106241, +0.815969, +0.077790),
	vec3(-0.007025, -0.028051, +1.035076)
);
const mat3 protanomaly7 = mat3
(
	vec3(+0.259411, +0.923008, -0.182420),
	vec3(+0.110296, +0.804340, +0.085364),
	vec3(-0.006276, -0.034346, +1.040622)
);
const mat3 protanomaly8 = mat3
(
	vec3(+0.203876, +0.990338, -0.194214),
	vec3(+0.112975, +0.794542, +0.092483),
	vec3(-0.005222, -0.041043, +1.046265)
);
const mat3 protanomaly9 = mat3
(
	vec3(+0.152286, +1.052583, -0.204868),
	vec3(+0.114503, +0.786281, +0.099216),
	vec3(-0.003882, -0.048116, +1.051998)
);

const mat3 protanomaly[11] =
{
    null_anomaly,
    protanomaly0,
    protanomaly1,
    protanomaly2,
    protanomaly3,
    protanomaly4,
    protanomaly5,
    protanomaly6,
    protanomaly7,
    protanomaly8,
    protanomaly9
};