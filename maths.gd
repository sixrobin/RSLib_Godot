extends RefCounted


static func clampi01(value: int) -> int:
	return clamp(value, 0, 1)


static func clampf01(value: float) -> float:
	return clamp(value, 0.0, 1.0)


static func greatest_common_divisor(a: int, b: int) -> int:
	while b != 0:
		var c: int = a % b
		a = b
		b = c

	return abs(a)


static func factorial(value: int) -> int:
	var factorial: int = 1
	
	var p: int = value
	while p >= 1:
		factorial *= p
		p -= 1

	return factorial


static func remap01(value: float, istart: float, istop: float):
	return remap(value, istart, istop, 0.0, 1.0)


static func remap_clamped(value: float, istart: float, istop: float, ostart: float, ostop: float):
	return clamp(remap(value, istart, istop, ostart, ostop), ostart, ostop)


static func remap01_clamped(value: float, istart: float, istop: float):
	return clamp(remap(value, istart, istop, 0.0, 1.0), 0.0, 1.0)
