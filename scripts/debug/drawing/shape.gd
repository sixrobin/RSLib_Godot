class_name DebugShape

var _color: Color
var _width: float
var always_draw: bool


func _init():
	pass


func draw(drawer: CanvasItem):
	pass


func set_color(color: Color) -> DebugShape:
	self._color = color
	return self


func set_alpha(alpha: float) -> DebugShape:
	self._color.a = alpha
	return self


func set_width(width: float) -> DebugShape:
	self._width = width
	return self


func set_always_draw(value: bool) -> DebugShape:
	self.always_draw = value
	return self
