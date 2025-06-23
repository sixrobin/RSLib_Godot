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


// Deuteranomaly (M cone - Green).

const mat3 deuteranomaly0 = mat3
(
	vec3(+0.866435, +0.177704, -0.044139),
	vec3(+0.049567, +0.939063, +0.011370),
	vec3(-0.003453, +0.007233, +0.996220)
);
const mat3 deuteranomaly1 = mat3
(
	vec3(+0.760729, +0.319078, -0.079807),
	vec3(+0.090568, +0.889315, +0.020117),
	vec3(-0.006027, +0.013325, +0.992702)
);
const mat3 deuteranomaly2 = mat3
(
	vec3(+0.675425, +0.433850, -0.109275),
	vec3(+0.125303, +0.847755, +0.026942),
	vec3(-0.007950, +0.018572, +0.989378)
);
const mat3 deuteranomaly3 = mat3
(
	vec3(+0.605511, +0.528560, -0.134071),
	vec3(+0.155318, +0.812366, +0.032316),
	vec3(-0.009376, +0.023176, +0.986200)
);
const mat3 deuteranomaly4 = mat3
(
	vec3(+0.547494, +0.607765, -0.155259),
	vec3(+0.181692, +0.781742, +0.036566),
	vec3(-0.010410, +0.027275, +0.983136)
);
const mat3 deuteranomaly5 = mat3
(
	vec3(+0.498864, +0.674741, -0.173604),
	vec3(+0.205199, +0.754872, +0.039929),
	vec3(-0.011131, +0.030969, +0.980162)
);
const mat3 deuteranomaly6 = mat3
(
	vec3(+0.457771, +0.731899, -0.189670),
	vec3(+0.226409, +0.731012, +0.042579),
	vec3(-0.011595, +0.034333, +0.977261)
);
const mat3 deuteranomaly7 = mat3
(
	vec3(+0.422823, +0.781057, -0.203881),
	vec3(+0.245752, +0.709602, +0.044646),
	vec3(-0.011843, +0.037423, +0.974421)
);
const mat3 deuteranomaly8 = mat3
(
	vec3(+0.392952, +0.823610, -0.216562),
	vec3(+0.263559, +0.690210, +0.046232),
	vec3(-0.011910, +0.040281, +0.971630)
);
const mat3 deuteranomaly9 = mat3
(
	vec3(+0.367322, +0.860646, -0.227968),
	vec3(+0.280085, +0.672501, +0.047413),
	vec3(-0.011820, +0.042940, +0.968881)
);

const mat3 deuteranomaly[11] =
{
    null_anomaly,
    deuteranomaly0,
    deuteranomaly1,
    deuteranomaly2,
    deuteranomaly3,
    deuteranomaly4,
    deuteranomaly5,
    deuteranomaly6,
    deuteranomaly7,
    deuteranomaly8,
    deuteranomaly9
};


// Tritanomaly (S cone - Blue).

const mat3 tritanomaly0 = mat3
(
	vec3(+0.926670, +0.092514, -0.019184),
	vec3(+0.021191, +0.964503, +0.014306),
	vec3(+0.008437, +0.054813, +0.936750)
);
const mat3 tritanomaly1 = mat3
(
	vec3(+0.895720, +0.133330, -0.029050),
	vec3(+0.029997, +0.945400, +0.024603),
	vec3(+0.013027, +0.104707, +0.882266)
);
const mat3 tritanomaly2 = mat3
(
	vec3(+0.905871, +0.127791, -0.033662),
	vec3(+0.026856, +0.941251, +0.031893),
	vec3(+0.013410, +0.148296, +0.838294)
);
const mat3 tritanomaly3 = mat3
(
	vec3(+0.948035, +0.089490, -0.037526),
	vec3(+0.014364, +0.946792, +0.038844),
	vec3(+0.010853, +0.193991, +0.795156)
);
const mat3 tritanomaly4 = mat3
(
	vec3(+1.017277, +0.027029, -0.044306),
	vec3(-0.006113, +0.958479, +0.047634),
	vec3(+0.006379, +0.248708, +0.744913)
);
const mat3 tritanomaly5 = mat3
(
	vec3(+1.104996, -0.046633, -0.058363),
	vec3(-0.032137, +0.971635, +0.060503),
	vec3(+0.001336, +0.317922, +0.680742)
);
const mat3 tritanomaly6 = mat3
(
	vec3(+1.193214, -0.109812, -0.083402),
	vec3(-0.058496, +0.979410, +0.079086),
	vec3(-0.002346, +0.403492, +0.598854)
);
const mat3 tritanomaly7 = mat3
(
	vec3(+1.257728, -0.139648, -0.118081),
	vec3(-0.078003, +0.975409, +0.102594),
	vec3(-0.003316, +0.501214, +0.502102)
);
const mat3 tritanomaly8 = mat3
(
	vec3(+1.278864, -0.125333, -0.153531),
	vec3(-0.084748, +0.957674, +0.127074),
	vec3(-0.000989, +0.601151, +0.399838)
);
const mat3 tritanomaly9 = mat3
(
	vec3(+1.255528, -0.076749, -0.178779),
	vec3(-0.078411, +0.930809, +0.147602),
	vec3(+0.004733, +0.691367, +0.303900)
);

const mat3 tritanomaly[11] =
{
    null_anomaly,
    tritanomaly0,
    tritanomaly1,
    tritanomaly2,
    tritanomaly3,
    tritanomaly4,
    tritanomaly5,
    tritanomaly6,
    tritanomaly7,
    tritanomaly8,
    tritanomaly9
};