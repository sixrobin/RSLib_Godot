class_name FocusArea
extends Node2D

@export var _target: Node2D
@export var _size: Vector2 = Vector2.ONE * 100.0
@export var _debug_color: Color = Color.BLUE


func _ready():
	self.top_level = true
	self.global_position = self._target.global_position


func _process(delta: float):
	self.focus_target()
	
	RSDraw.marker(self._target).set_color(self._debug_color).set_always_draw(true)
	RSDraw.rect(self, _size).set_color(self._debug_color).set_always_draw(true)


func focus_target():
	var half_size := self._size * 0.5
	var offset := self._target.global_position - self.global_position
	
	if offset.x > half_size.x:
		self.global_position.x += offset.x - half_size.x
	elif offset.x < -half_size.x:
		self.global_position.x += offset.x + half_size.x
		
	if offset.y > half_size.y:
		self.global_position.y += offset.y - half_size.y
	elif offset.y < -half_size.y:
		self.global_position.y += offset.y + half_size.y
