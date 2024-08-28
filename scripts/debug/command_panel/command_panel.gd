extends Node

var _buttons_container: Control = null
var _commands: Array[PanelCommand] = []


func _ready():
	self.create_panel()


func create_panel():
	var canvas_layer: CanvasLayer = CanvasLayer.new()
	self.add_child(canvas_layer)
	
	var control: Control = Control.new()
	control.z_index = 2^63 - 1
	canvas_layer.add_child(control)
	
	var background: ColorRect = ColorRect.new()
	background.color = Color(0.1, 0.1, 0.1, 1.0)
	background.set_anchors_preset(Control.PRESET_RIGHT_WIDE)
	background.size = Vector2(378, 1016)
	background.position = Vector2(1510, 32)
	control.add_child(background)
	
	var scroll_container: ScrollContainer = ScrollContainer.new()
	scroll_container.set_anchors_preset(Control.PRESET_FULL_RECT)
	scroll_container.horizontal_scroll_mode = ScrollContainer.SCROLL_MODE_DISABLED
	background.add_child(scroll_container)
	
	var vbox: VBoxContainer = VBoxContainer.new()
	vbox.set_anchors_preset(Control.PRESET_FULL_RECT)
	vbox.custom_minimum_size = Vector2(378, 0)
	scroll_container.add_child(vbox)
	self._buttons_container = vbox


func add_command(source: Node, action_name: String, action: Callable):
	var command: PanelCommand = PanelCommand.new(source, action_name, action)
	self._commands.append(command)
	
	var button: Button = self.add_button()
	button.text = command.label
	button.button_down.connect(command.execute)
	command.set_button(button)
	
	source.tree_exited.connect(func(): self.remove_command(command))


func remove_command(command: PanelCommand):
	command.button.queue_free()
	
	for i in self._commands.size():
		if self._commands[i] == command:
			self._commands.remove_at(i)
			break


func add_button() -> Button:
	var button: Button = Button.new()
	self._buttons_container.add_child(button)
	return button
