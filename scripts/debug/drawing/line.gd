class_name DebugLine
extends DebugShape

var _points := PackedVector2Array()


func _init(a: Vector2, b: Vector2, p_color: Color):
	super._init(p_color)
	self._points.push_back(a)
	self._points.push_back(b)


func get_points():
	return self._points
