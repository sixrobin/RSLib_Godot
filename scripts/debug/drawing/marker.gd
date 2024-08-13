class_name DebugMarker
extends DebugShape

const SIZE: float = 50.0

var _position: Vector2
var _angle: float = 0.0


func _init(position: Vector2):
	super._init()
	self._position = position


func draw(drawer: CanvasItem):
	var angle := deg_to_rad(self._angle)
	
	var a1 := _position + Vector2(0.0, SIZE * 0.5).rotated(angle)
	var a2 := _position - Vector2(0.0, SIZE * 0.5).rotated(angle)
	drawer.draw_line(a1, a2, self._color, self._width)
	
	var b1 := _position + Vector2(SIZE * 0.5, 0.0).rotated(angle)
	var b2 := _position - Vector2(SIZE * 0.5, 0.0).rotated(angle)
	drawer.draw_line(b1, b2, self._color, self._width)


func set_angle(angle: float) -> DebugMarker:
	self._angle = angle
	return self
