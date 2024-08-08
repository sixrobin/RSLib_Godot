extends Node

var _values_text: String = ""
var _label: Label
var _key_just_pressed: bool = false


func _ready():
	self.process_priority = -2^63
	
	self._label = Label.new()
	self._label.z_index = 2^63 - 1
	self._label.modulate = Color(1, 1, 0, 1)
	self._label.visible = false
	
	var label_settings := LabelSettings.new()
	label_settings.line_spacing = -4.0
	self._label.label_settings = label_settings
	
	self.add_child(self._label)


func _process(delta: float):
	var key_pressed := Input.is_key_pressed(KEY_F1)
	
	if not self._key_just_pressed and key_pressed:
		self._key_just_pressed = true
		self._label.visible = not self._label.visible
	elif self._key_just_pressed and not key_pressed:
		self._key_just_pressed = false
	
	self._label.text = self._values_text
	self._values_text = ""


func format(key, value) -> String:
	const FORMAT: String = "%s: %s"
	return (FORMAT + "\n") % [str(key), str(value)]


func debug_value(key, value):
	self._values_text += self.format(key, value)
