class_name DebugRing
extends DebugShape

var circle1: DebugCircle
var circle2: DebugCircle


func _init(center: Vector2, radius1: float, radius2: float, resolution: int):
	super._init()
	
	self.circle1 = DebugCircle.new(center, radius1, resolution)
	self.circle2 = DebugCircle.new(center, radius2, resolution)
	

func draw(drawer: CanvasItem):
	self.circle1.draw(drawer)
	self.circle2.draw(drawer)


func set_color(color: Color) -> DebugShape:
	super.set_color(color)
	self.circle1.set_color(color)
	self.circle2.set_color(color)
	return self
	
	
func set_width(width: float) -> DebugShape:
	super.set_width(width)
	self.circle1.set_width(width)
	self.circle2.set_width(width)
	return self
