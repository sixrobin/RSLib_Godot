class_name DebugLine
extends DebugShape

var _a: Vector2
var _b: Vector2


func _init(a: Vector2, b: Vector2, p_color: Color):
	super._init(p_color)
	self._a = a
	self._b = b


func draw(drawer: CanvasItem):
	drawer.draw_line(self._a, self._b, self.color)
