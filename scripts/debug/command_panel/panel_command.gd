class_name PanelCommand

var source: Node = null
var label: String = "[Cmd]"
var action: Callable = func(): printerr("Not implemented command!")
var button: Button = null


func _init(p_source: Node, p_label: String, p_action: Callable):
	self.source = p_source
	self.label = p_label
	self.action = p_action


func set_button(p_button: Button) -> PanelCommand:
	self.button = p_button
	return self


func execute() -> PanelCommand:
	print("[RSLib] Executing command %s" % self.label)
	self.action.call()
	return self
