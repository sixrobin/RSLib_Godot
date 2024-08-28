extends RefCounted


static func random_bool() -> bool:
	return randf() < 0.5


static func clipboard_set(value: String) -> void:
	DisplayServer.clipboard_set(value)


static func remove(item, array: Array) -> bool:
	for i in array.size():
		if array[i] == item:
			array.remove_at(i)
			return true
	
	return false
