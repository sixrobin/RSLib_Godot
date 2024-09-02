class_name RandomFree
extends Node

@export_range(0.0, 1.0) var _free_chances: float = 0.0
@export var _freed_node: Node = null


func _ready():
	if randf() < self._free_chances:
		(self._freed_node if self._freed_node != null else self).queue_free()
