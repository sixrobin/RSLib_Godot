extends Node

const RS_HELP := preload("res://RSLib/scripts/utils/helpers.gd")
const WIDTH: int = 150
const HEIGHT: int = 700
const MARGIN: int = 16

var _canvas_layer: CanvasLayer = null
var _buttons_container: Control = null
var _categories: Dictionary = {}  # Container per category ID
var _commands: Array[PanelCommand] = []
var _sources: Dictionary = {}  # Commands count per source


func _ready():
	self.create_panel()
	self._canvas_layer.visible = false


func _unhandled_key_input(event: InputEvent):
	if not RSDebugManager.debug_mode:
		return
		
	var event_key: InputEventKey = event as InputEventKey
	if event_key != null and event.is_pressed():
		for command in _commands:
			if command.key == int(event_key.keycode):
				command.execute()


func toggle_visible():
	self._canvas_layer.visible = not self._canvas_layer.visible


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
	background.position = Vector2(screen_resolution.x - WIDTH - MARGIN, MARGIN)
	control.add_child(background)
	
	var scroll_container: ScrollContainer = ScrollContainer.new()
	scroll_container.set_anchors_preset(Control.PRESET_FULL_RECT)
	scroll_container.horizontal_scroll_mode = ScrollContainer.SCROLL_MODE_DISABLED
	scroll_container.vertical_scroll_mode = ScrollContainer.SCROLL_MODE_SHOW_ALWAYS
	background.add_child(scroll_container)
	
	var vbox: VBoxContainer = VBoxContainer.new()
	vbox.add_theme_constant_override("separation", 0)
	vbox.set_anchors_preset(Control.PRESET_FULL_RECT)
	vbox.custom_minimum_size = Vector2(WIDTH, 0)
	scroll_container.add_child(vbox)
	self._buttons_container = vbox
	
	#for i in 100:
		#self.add(self, "Test", "Test %s" % [i], func(): print("Test %s" % [i]))


func add(source: Node, category: String, action_name: String, action: Callable, key: int = -1):
	var command: PanelCommand = PanelCommand.new(source, action_name, action, key)
	self._commands.append(command)
	
	if category == "":
		category = "General"
	
	if not self._categories.has(category):
		self.add_category(category)
	
	var button: BaseButton = self.add_button(category, command.label, key)
	button.button_down.connect(command.execute)
	command.set_button(button)
	
	if self._sources.has(source):
		self._sources[source] += 1
	else:
		source.tree_exited.connect(func(): self.on_source_tree_exited(source))
		self._sources[source] = 1


func remove(command: PanelCommand):
	command.button.queue_free()
	RS_HELP.remove(command, self._commands)


func on_source_tree_exited(source: Node):
	var removed_commands: Array[PanelCommand] = []
	for command in self._commands:
		if command.source == source:
			removed_commands.append(command)
	
	for command in removed_commands:
		remove(command)


func add_category(id: String) -> VBoxContainer:
	var vbox: VBoxContainer = VBoxContainer.new()
	vbox.add_theme_constant_override("separation", 0)
	
	var category_label: Label = Label.new()
	category_label.text = id.to_upper()
	category_label.self_modulate = Color.YELLOW
	category_label.vertical_alignment = VERTICAL_ALIGNMENT_BOTTOM
	category_label.add_theme_font_size_override("font_size", 12)
	vbox.add_child(category_label)
	
	var spacing: Control = Control.new()
	spacing.custom_minimum_size.y = 12
	vbox.add_child(spacing)
	
	self._categories[id] = vbox
	self._buttons_container.add_child(vbox)
	return vbox


func add_button(category: String, text: String, key: int = -1) -> BaseButton:
	var button: Button = Button.new()
	button.custom_minimum_size.y = 16
	
	var label: Label = Label.new()
	label.text = text + ("" if key == -1 else " [%s]" % [OS.get_keycode_string(key)])
	label.vertical_alignment = VERTICAL_ALIGNMENT_CENTER
	label.add_theme_font_size_override("font_size", 12)
	button.add_child(label)
	
	var category_container := self._categories[category] as VBoxContainer
	category_container.add_child(button)
	category_container.move_child(button, category_container.get_child_count() - 2)
	
	return button
