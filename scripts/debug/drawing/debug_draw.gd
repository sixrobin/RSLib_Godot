extends Node2D

const DEFAULT_COLOR: Color = Color.YELLOW
const DEFAULT_WIDTH: float = 1.0

var _shapes: Array[DebugShape] = []


func _ready():
	self.z_index = 2^63 - 1
	

func _process(delta):
	# TODO: Add a debug key to toggle drawing.
	self.queue_redraw()


func _draw():
	for shape in self._shapes:
		shape.draw(self)
		
	self._shapes.clear()


func vec(input):
	return input if (input is Vector2) else input.global_position


func add(shape: DebugShape) -> DebugShape:
	self._shapes.append(shape)
	return shape


func line(a, b, color := self.DEFAULT_COLOR, width := self.DEFAULT_WIDTH) -> DebugShape:
	return self.add(DebugLine.new(vec(a), vec(b)).set_color(color).set_width(width))


func arrow(a, b, color := self.DEFAULT_COLOR, width := self.DEFAULT_WIDTH) -> DebugShape:
	return self.add(DebugArrow.new(vec(a), vec(b)).set_color(color).set_width(width))


func triangle(a, b, c, color := self.DEFAULT_COLOR, width := self.DEFAULT_WIDTH) -> DebugShape:
	return self.add(DebugTriangle.new(vec(a), vec(b), vec(c)).set_color(color).set_width(width))


func circle(c, r: float, resolution: int = 32, color := self.DEFAULT_COLOR, width := self.DEFAULT_WIDTH) -> DebugShape:
	return self.add(DebugCircle.new(vec(c), r, resolution).set_color(color).set_width(width))


func ring(c, r1: float, r2: float, resolution: int = 32, color := self.DEFAULT_COLOR, width := self.DEFAULT_WIDTH) -> DebugShape:
	return self.add(DebugRing.new(vec(c), r1, r2, resolution).set_color(color).set_width(width))


# TODO: Point
# TODO: Rect
# TODO: Cross
