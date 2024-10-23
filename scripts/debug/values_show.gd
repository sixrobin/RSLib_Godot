extends Node

const COLOR: Color = Color.YELLOW

var _values_text: String = ""
var _label: Label
var _positioned_texts = {}


func _ready():
	self.process_priority = -2^63
	
	self._label = Label.new()
	self._label.z_index = 2^63 - 1
	self._label.modulate = self.COLOR
	self._label.position.x = 20
	self._label.position.y = 256
	
	var label_settings := LabelSettings.new()
	label_settings.line_spacing = -4.0
	label_settings.font_size = 12
	self._label.label_settings = label_settings
	
	var canvas_layer := CanvasLayer.new()
	canvas_layer.layer = 128
	
	self.add_child(canvas_layer)
	
	canvas_layer.add_child(self._label)


func _process(delta: float):
	self.new_line()
	self.show("fps", Performance.get_monitor(Performance.TIME_FPS))
	self.show("mem", RSHelp.format_byte_size(OS.get_static_memory_usage()))
	self.show("obj", Performance.get_monitor(Performance.OBJECT_COUNT))
	self.show("obj_nodes", Performance.get_monitor(Performance.OBJECT_NODE_COUNT))
	self.show("orphans", Performance.get_monitor(Performance.OBJECT_ORPHAN_NODE_COUNT))
	self.show("draw_calls", Performance.get_monitor(Performance.RENDER_TOTAL_DRAW_CALLS_IN_FRAME))
	
	self._label.text = self._values_text
	self._values_text = ""
	
	var positioned_texts_keys := self._positioned_texts.keys()
	for key in positioned_texts_keys:
		self._positioned_texts[key] += 1
		if self._positioned_texts[key] == 2:  # Buffer to avoid destroying label right after its spawn.
			self._positioned_texts.erase(key)
			key.queue_free()


func toggle_visible():
	self._label.visible = not self._label.visible


func format(key, value) -> String:
	const FORMAT: String = "%s: %s"
	return (FORMAT + "\n") % [str(key), str(value)]


func show(key, value, position: Vector2 = Vector2.INF):
	var debug_text: String = self.format(key, value)
	
	if not position.is_finite():
		self._values_text += debug_text
	elif self._label.visible:
		var positioned_label = self.create_positioned_text(debug_text, position)
		self._positioned_texts[positioned_label] = 0


func new_line():
	self._values_text += "\n"


func create_positioned_text(debug_text: String, position: Vector2) -> Label:
	var positioned_label = Label.new()
	positioned_label.z_index = 2^63 - 1
	positioned_label.modulate = self.COLOR
	positioned_label.size.x = 1000.0
	positioned_label.global_position = position
	positioned_label.text = debug_text
	
	self.add_child(positioned_label)
	return positioned_label
