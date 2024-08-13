class_name DebugShape

var color: Color


func _init(p_color: Color):
	self.color = p_color


func get_points() -> PackedVector2Array:
	return PackedVector2Array()
