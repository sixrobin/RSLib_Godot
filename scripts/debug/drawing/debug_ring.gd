class_name DebugRing
extends DebugShape

var _center: Vector2
var _radius1: float
var _radius2: float
var _resolution: int


func _init(center: Vector2, radius1: float, radius2: float, resolution: int):
	super._init()
	self._center = center
	self._radius1 = radius1
	self._radius2 = radius2
	self._resolution = resolution
	

func draw(drawer: CanvasItem):
	var circle1 = DebugCircle.new(self._center, self._radius1, self._resolution).set_color(self._color).set_width(self._width)
	var circle2 = DebugCircle.new(self._center, self._radius2, self._resolution).set_color(self._color).set_width(self._width)
	
	circle1.draw(drawer)
	circle2.draw(drawer)
