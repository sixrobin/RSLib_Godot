extends Node2D

const DEFAULT_COLOR: Color = Color.YELLOW

var _shapes: Array[DebugShape] = []


func _ready():
	self.z_index = 2^63 - 1
	

func _process(delta):
	self.queue_redraw()


func _draw():
	for shape in self._shapes:
		shape.draw(self)
		
	self._shapes.clear()


func line(a, b, color := self.DEFAULT_COLOR):
	a = a if (a is Vector2) else a.global_position
	b = b if (b is Vector2) else b.global_position
	_shapes.append(DebugLine.new(a, b, color))


func triangle(a, b, c, color := self.DEFAULT_COLOR):
	a = a if (a is Vector2) else a.global_position
	b = b if (b is Vector2) else b.global_position
	c = c if (c is Vector2) else c.global_position
	_shapes.append(DebugTriangle.new(a, b, c, color))
