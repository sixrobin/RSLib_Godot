class_name SpriteRandomizer
extends Node

@export var textures: Array[Texture2D]
@export var randomize_flip_h: bool = false
@export var randomize_flip_v: bool = false
@export var random_scale: Vector2 = Vector2.ONE


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
		
	sprite2D.scale *= randf_range(random_scale.x, random_scale.y)
