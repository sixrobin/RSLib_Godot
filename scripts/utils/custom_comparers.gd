class_name CustomComparers


static func closest_to(source, a, b):
	var dist_a: float = (a.global_position - source.global_position).length_squared()
	var dist_b: float = (b.global_position - source.global_position).length_squared()
	return dist_a < dist_b
