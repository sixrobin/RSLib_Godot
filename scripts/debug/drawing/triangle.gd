class_name DebugTriangle
extends DebugShape

var _points := PackedVector2Array()


func _init(a: Vector2, b: Vector2, c: Vector2):
	super._init()
	self._points.push_back(a)
	self._points.push_back(b)
	self._points.push_back(c)
	self._points.push_back(a)


func draw(drawer: CanvasItem):
	drawer.draw_polyline(self._points, self._color, self._width)
