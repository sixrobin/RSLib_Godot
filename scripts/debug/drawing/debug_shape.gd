class_name DebugShape

var _color: Color
var _width: float
# TODO: always_draw variable, to draw shape whether debug is enabled or not.


func _init():
	pass


func draw(drawer: CanvasItem):
	pass


func set_color(color: Color) -> DebugShape:
	self._color = color
	return self


func set_width(width: float) -> DebugShape:
	self._width = width
	return self
