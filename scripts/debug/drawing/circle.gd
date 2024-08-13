class_name DebugCircle
extends DebugShape

var _points := PackedVector2Array()


func _init(center: Vector2, radius: float, resolution: int):
	super._init()
	
	for i in (resolution + 1):
		var angle = deg_to_rad((360.0 * i) / resolution)
		self._points.push_back(center + Vector2(cos(angle), sin(angle)) * radius)


func draw(drawer: CanvasItem):
	drawer.draw_polyline(self._points, self._color, self._width)
