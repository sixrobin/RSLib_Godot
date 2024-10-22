class_name RSVec
extends RefCounted


static func clamp_v2(vector: Vector2, min: float, max: float) -> Vector2:
	return vector.clamp(Vector2(min, min), Vector2(max, max))

static func clamp01_v2(vector: Vector2) -> Vector2:
	return vector.clamp(Vector2(0.0, 0.0), Vector2(1.0, 1.0))

static func clamp_v3(vector: Vector3, min: float, max: float) -> Vector3:
	return vector.clamp(Vector3(min, min, min), Vector3(max, max, max))

static func clamp01_v3(vector: Vector3) -> Vector3:
	return vector.clamp(Vector3(0.0, 0.0, 0.0), Vector3(1.0, 1.0, 1.0))

static func clamp_v4(vector: Vector4, min: float, max: float) -> Vector4:
	return vector.clamp(Vector4(min, min, min, min), Vector4(max, max, max, max))

static func clamp01_v4(vector: Vector4) -> Vector4:
	return vector.clamp(Vector4(0.0, 0.0, 0.0, 0.0), Vector4(1.0, 1.0, 1.0, 1.0))


static func lerp_xy(vector: Vector2, weight: float) -> float:
	return lerp(vector.x, vector.y, weight)

static func lerp_xy_clamped(vector: Vector2, weight: float) -> float:
	return clampf(lerp(vector.x, vector.y, weight), 0.0, 1.0)


static func min_axis(vector: Variant) -> float:
	return vector[vector.min_axis_index()]

static func max_axis(vector: Variant) -> float:
	return vector[vector.max_axis_index()]


static func randomf(vector: Vector2) -> float:
	return randf_range(vector.x, vector.y)

static func randomi(vector: Vector2i) -> int:
	return randi_range(vector.x, vector.y)


static func random_vec2() -> Vector2:
	return Vector2(randf_range(-1, 1), randf_range(-1, 1)).normalized()

static func random_vec3() -> Vector3:
	return Vector3(randf_range(-1, 1), randf_range(-1, 1), randf_range(-1, 1)).normalized()


static func mod(vector: Vector2i, factor: int) -> Vector2i:
	return Vector2i(vector.x % factor, vector.y % factor)

static func pos_mod(vector: Vector2i, factor: int) -> Vector2i:
	return Vector2i(posmod(vector.x, factor), posmod(vector.y, factor))

static func modv(vector: Vector2i, factor: Vector2i) -> Vector2i:
	return Vector2i(vector.x % factor.x, vector.y % factor.y)

static func pos_modv(vector: Vector2i, factor: Vector2i) -> Vector2i:
	return Vector2i(posmod(vector.x, factor.x), posmod(vector.y, factor.y))
