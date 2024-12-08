extends Node

const WIDTH: int = 512
const HEIGHT: int = 150
const MARGIN: int = 16

var _canvas_layer: CanvasLayer = null
var _entries_container: Control = null
var _scroll_container: ScrollContainer = null


func _ready():
	self.create_panel()
	self._canvas_layer.visible = false
	self.entry("[RSLib Console]", Color.DIM_GRAY, false)


func toggle_visible():
	self._canvas_layer.visible = not self._canvas_layer.visible
	self._scroll_container.scroll_vertical = self._entries_container.size.y


func create_panel():
	var screen_resolution: Vector2 = get_viewport().get_visible_rect().size

	self._canvas_layer = CanvasLayer.new()
	self._canvas_layer.layer = 1000
	self.add_child(self._canvas_layer)
	
	var control: Control = Control.new()
	control.z_index = 2^63 - 1
	self._canvas_layer.add_child(control)
	
	var background: ColorRect = ColorRect.new()
	background.color = Color(0.1, 0.1, 0.1, 0.6)
	background.set_anchors_preset(Control.PRESET_RIGHT_WIDE)
	background.size = Vector2(WIDTH, HEIGHT)
	background.position = Vector2(screen_resolution.x - WIDTH - MARGIN, screen_resolution.y - HEIGHT - MARGIN)
	control.add_child(background)
	
	self._scroll_container = ScrollContainer.new()
	self._scroll_container.set_anchors_preset(Control.PRESET_FULL_RECT)
	self._scroll_container.horizontal_scroll_mode = ScrollContainer.SCROLL_MODE_DISABLED
	self._scroll_container.vertical_scroll_mode = ScrollContainer.SCROLL_MODE_SHOW_ALWAYS
	background.add_child(self._scroll_container)
	
	self._entries_container = VBoxContainer.new()
	self._entries_container.add_theme_constant_override("separation", 0)
	self._entries_container.set_anchors_preset(Control.PRESET_FULL_RECT)
	self._entries_container.custom_minimum_size = Vector2(WIDTH, 0)
	self._scroll_container.add_child(self._entries_container)


func entry(text: String, color: Color = Color.WHITE, engine_print: bool = true):
	var label: Label = Label.new()
	label.text = "> " + text
	label.self_modulate = color
	label.autowrap_mode = TextServer.AUTOWRAP_WORD_SMART
	label.vertical_alignment = VERTICAL_ALIGNMENT_CENTER
	label.custom_minimum_size.x = WIDTH
	label.add_theme_font_size_override("font_size", 12)
	label.add_theme_constant_override("line_spacing", -4)
	self._entries_container.add_child(label)
	
	if engine_print:
		print(text)


func warning(text: String):
	entry(text, Color.YELLOW, false)
	printerr(text)


func error(text: String):
	entry(text, Color.RED, false)
	printerr(text)
	push_error(text)
