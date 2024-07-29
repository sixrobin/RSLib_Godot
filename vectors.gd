extends RefCounted


static func clampv2(vector: Vector2, min: float, max: float) -> Vector2:
	return vector.clamp(Vector2(min, min), Vector2(max, max))
	
	
static func clampv201(vector: Vector2) -> Vector2:
	return vector.clamp(Vector2(0.0, 0.0), Vector2(1.0, 1.0))


static func clampv3(vector: Vector3, min: float, max: float) -> Vector3:
	return vector.clamp(Vector3(min, min, min), Vector3(max, max, max))
	
	
static func clampv301(vector: Vector3) -> Vector3:
	return vector.clamp(Vector3(0.0, 0.0, 0.0), Vector3(1.0, 1.0, 1.0))


static func clampv4(vector: Vector4, min: float, max: float) -> Vector4:
	return vector.clamp(Vector4(min, min, min, min), Vector4(max, max, max, max))


static func clampv401(vector: Vector4) -> Vector4:
	return vector.clamp(Vector4(0.0, 0.0, 0.0, 0.0), Vector4(1.0, 1.0, 1.0, 1.0))


# TODO: Lerp vectors.
# TODO: Lerp (using vector2 as A and B).
# TODO: Min/max (using min/maxAxisIndex function).
# TODO: Random.
