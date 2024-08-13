class_name DebugArrow
extends DebugLine

const HEAD_LENGTH: float = 50.0
const HEAD_ANGLE: float = 60.0


func _init(a: Vector2, b: Vector2):
	super._init(a, b)


func draw(drawer: CanvasItem):
	super.draw(drawer)
	
	var arrow_direction := (self._a - self._b).normalized()
	var angle := deg_to_rad(HEAD_ANGLE * 0.5)
	
	drawer.draw_line(self._b, self._b + arrow_direction.rotated(-angle) * HEAD_LENGTH, self._color, self._width)
	drawer.draw_line(self._b, self._b + arrow_direction.rotated(angle) * HEAD_LENGTH, self._color, self._width)
