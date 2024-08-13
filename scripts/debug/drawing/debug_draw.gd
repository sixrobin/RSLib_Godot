extends Node2D

const DEFAULT_COLOR: Color = Color(1.0, 1.0, 0.0)

var _shapes: Array[DebugShape] = []


func _ready():
	self.z_index = 2^63 - 1
	

func _process(delta):
	self.queue_redraw()
	self._shapes.clear()


func _draw():
	for shape in self._shapes:
		var points := shape.get_points()
		for i in points.size() - 1:
			var a := points[i]
			var b := points[i + 1]
			super.draw_line(a, b, shape.color)


func line(a, b, color: Color = self.DEFAULT_COLOR):
	a = a if (a is Vector2) else a.global_position
	b = b if (b is Vector2) else b.global_position
	_shapes.append(DebugLine.new(a, b, color))
