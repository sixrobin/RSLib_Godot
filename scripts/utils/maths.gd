class_name RSMaths
extends RefCounted


static func clampi01(value: int) -> int:
	return clamp(value, 0, 1)

static func clampf01(value: float) -> float:
	return clamp(value, 0.0, 1.0)
	
static func clampv(value: Variant, range: Vector2) -> float:
	return clamp(value, range.x, range.y)


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
	var f: int = 1
	
	var p: int = value
	while p >= 1:
		f *= p
		p -= 1

	return f


static func point_left_to_segment_v2(a: Vector2, b: Vector2, p: Vector2) -> float:
	var f: float = (b.y - a.y) * (p.x - a.x) - (p.y - a.y) * (b.x - a.x)
	if f > 0.0:
		return 1.0
	elif f < 0.0:
		return -1.0
	else:
		return 0.0
		
static func point_left_to_segment(ax: float, ay: float, bx: float, by: float, px: float, py: float) -> float:
	var f: float = (by - ay) * (px - ax) - (py - ay) * (bx - ax)
	if f > 0.0:
		return 1.0
	elif f < 0.0:
		return -1.0
	else:
		return 0.0


static func winding_number(polygon: Array, point: Vector2) -> int:
	var wn: int = 0;
	
	var poly := Array(polygon)
	poly.append(polygon[0])
	
	for i in range(polygon.size() - 1):
		if poly[i].x <= point.x:
			if poly[i + 1].x > point.x:
				if point_left_to_segment(poly[i].x, poly[i].y, poly[i + 1].x, poly[i + 1].y, point.x, point.y) > 0.0:
					wn -= 1;
		else:
			if poly[i + 1].x <= point.x:
				if point_left_to_segment(poly[i].x, poly[i].y, poly[i + 1].x, poly[i + 1].y, point.x, point.y) < 0.0:
					wn += 1;
	
	return wn
