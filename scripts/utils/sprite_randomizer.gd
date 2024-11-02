class_name SpriteRandomizer
extends Node

@export var textures: Array[Texture2D]
@export var randomize_flip_h: bool = false
@export var randomize_flip_v: bool = false
@export var randomize_flipscale_x: bool = false
@export var randomize_flipscale_y: bool = false
@export var random_scale: Vector2 = Vector2.ONE
@export var random_color: Gradient = null
@export var random_offset_max: float = 0.0


func _ready():
	self.randomize_sprite()


func randomize_sprite():
	var sprite2D = self.get_parent() as Sprite2D
	if not sprite2D:
		print("Invalid parent node to randomize sprite!")
		return
	
	if self.textures != null and self.textures.size() > 0:
		sprite2D.texture = self.textures.pick_random()
	
	if self.randomize_flip_h:
		sprite2D.flip_h = randf() < 0.5
	
	if self.randomize_flip_v:
		sprite2D.flip_v = randf() < 0.5
	
	if self.random_color:
		sprite2D.modulate = self.random_color.sample(randf())
	
	sprite2D.scale *= randf_range(self.random_scale.x, self.random_scale.y)
	if self.randomize_flipscale_x:
		sprite2D.scale.x *= 1 if randf() < 0.5 else -1
	if self.randomize_flipscale_y:
		sprite2D.scale.y *= 1 if randf() < 0.5 else -1
		
	sprite2D.global_position += Vector2.RIGHT.rotated(randf() * TAU) * randf() * self.random_offset_max
