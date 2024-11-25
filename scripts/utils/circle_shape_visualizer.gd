extends CollisionShape2D

@export var _color: Color = Color.MAGENTA


func _process(delta: float):
	if self.shape is CircleShape2D:
		RSDraw.circle(self, self.shape.radius * self.global_scale.x).set_color(self._color)
