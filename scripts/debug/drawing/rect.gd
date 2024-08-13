class_name DebugRect
extends DebugShape

var _center: Vector2
var _size: Vector2


func _init(center: Vector2, size: Vector2):
	super._init()
	self._size = size
	self._center = center - self._size * 0.5


func draw(drawer: CanvasItem):
	var rect := Rect2(self._center, self._size)
	drawer.draw_rect(rect, self._color, false, self._width)
