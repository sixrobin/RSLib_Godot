extends RefCounted


static func clampi01(value: int) -> int:
	return clamp(value, 0, 1)

static func clampf01(value: float) -> float:
	return clamp(value, 0.0, 1.0)


static func remap01(value: float, istart: float, istop: float) -> float:
	return remap(value, istart, istop, 0.0, 1.0)

static func remap_clamped(value: float, istart: float, istop: float, ostart: float, ostop: float) -> float:
	return clamp(remap(value, istart, istop, ostart, ostop), ostart, ostop)

static func remap01_clamped(value: float, istart: float, istop: float) -> float:
	return clamp(remap(value, istart, istop, 0.0, 1.0), 0.0, 1.0)


static func gcd(a: int, b: int) -> int:
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


static func point_left_to_segment(a: Vector2, b: Vector2, p: Vector2) -> float:
	var f := (b.y - a.y) * (p.x - a.x) - (p.y - a.y) * (b.x - a.x)
	if f > 0.0:
		return 1.0
	elif f < 0.0:
		return -1.0
	else:
		return 0.0
