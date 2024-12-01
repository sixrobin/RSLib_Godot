extends Node

var debug_mode: bool = false
var _commands: Dictionary = {}
var _keys_just_pressed: Dictionary = {}


func _ready():
	self._commands[KEY_F12] = self.toggle_debug_mode
	self._commands[KEY_F] = self.toggle_screen_mode
	self._commands[KEY_F1] = RSValues.toggle_visible
	self._commands[KEY_F2] = RSDraw.toggle_visible
	self._commands[KEY_F3] = RSCommand.toggle_visible


func _process(delta: float):
	for cmd_key in _commands.keys():
		if not self.debug_mode and cmd_key != KEY_F and cmd_key != KEY_F12:
			continue
		
		var key_pressed := Input.is_key_pressed(cmd_key)
		
		if not self._keys_just_pressed.has(cmd_key):
			self._keys_just_pressed[cmd_key] = false
		
		if not self._keys_just_pressed[cmd_key] and key_pressed:
			self._keys_just_pressed[cmd_key] = true
			self._commands[cmd_key].call()
		elif self._keys_just_pressed[cmd_key] and not key_pressed:
			self._keys_just_pressed[cmd_key] = false


func toggle_debug_mode():
	self.debug_mode = not self.debug_mode


func toggle_screen_mode():
	if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_FULLSCREEN:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_EXCLUSIVE_FULLSCREEN)
	elif DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_EXCLUSIVE_FULLSCREEN:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
	else:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
