class_name RSHelp
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


static func queue_free_children(node: Node):
	for child in node.get_children():
		node.remove_child(child)
		child.queue_free()


static func unparent(node: Node):
	var parent: Node = node.get_parent()
	if parent:
		parent.remove_child(node)


static func format_byte_size(bytes: int, round: bool = false):
	var counter: int = 0
	var number := bytes
	while round(number / 1024) >= 1:
		number /= 1024
		counter += 1
	
	var result: String = ""
	result += "%.0" % [number] if round else str(number)
	result += ["bytes", "KB", "MB", "GB", "TB", "PB"][counter]
	return result
