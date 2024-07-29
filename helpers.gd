extends RefCounted


static func random_bool() -> bool:
	return randf() < 0.5


static func clipboard_set(value: String) -> void:
	DisplayServer.clipboard_set(value)
