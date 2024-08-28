extends Node

const RS_HELP := preload("res://RSLib/scripts/utils/helpers.gd")
const MARGIN: int = 16
const WIDTH: int = 256

var _buttons_container: Control = null
var _commands: Array[PanelCommand] = []


func _ready():
	self.create_panel()


# TODO: Process function to toggle panel visibility.


func create_panel():
	var screen_resolution: Vector2 = get_viewport().get_visible_rect().size
	
	var canvas_layer: CanvasLayer = CanvasLayer.new()
	self.add_child(canvas_layer)
	
	var control: Control = Control.new()
	control.z_index = 2^63 - 1
	canvas_layer.add_child(control)
	
	var background: ColorRect = ColorRect.new()
	background.color = Color(0.1, 0.1, 0.1, 1.0)
	background.set_anchors_preset(Control.PRESET_RIGHT_WIDE)
	background.size = Vector2(WIDTH, screen_resolution.y - MARGIN * 2)
	background.position = Vector2(screen_resolution.x - WIDTH - MARGIN, MARGIN)
	control.add_child(background)
	
	var scroll_container: ScrollContainer = ScrollContainer.new()
	scroll_container.set_anchors_preset(Control.PRESET_FULL_RECT)
	scroll_container.horizontal_scroll_mode = ScrollContainer.SCROLL_MODE_DISABLED
	background.add_child(scroll_container)
	
	var vbox: VBoxContainer = VBoxContainer.new()
	vbox.set_anchors_preset(Control.PRESET_FULL_RECT)
	vbox.custom_minimum_size = Vector2(WIDTH, 0)
	scroll_container.add_child(vbox)
	self._buttons_container = vbox
	
	#for i in 100:
		#self.add(self, "Test %s" % [i], func(): print("Test %s" % [i]))


func add(source: Node, action_name: String, action: Callable):
	var command: PanelCommand = PanelCommand.new(source, action_name, action)
	self._commands.append(command)
	
	var button: Button = self.add_button()
	button.text = command.label
	button.button_down.connect(command.execute)
	command.set_button(button)
	
	source.tree_exited.connect(func(): self.remove(command))


func remove(command: PanelCommand):
	command.button.queue_free()
	RS_HELP.remove(command, self._commands)


func add_button() -> Button:
	var button: Button = Button.new()
	self._buttons_container.add_child(button)
	return button
