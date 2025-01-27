using Godot;
using System;

public partial class DebugManager : Node {
	public static bool DebugMode = true;

	private System.Collections.Generic.Dictionary<Key, Action> _commands = new();
	private System.Collections.Generic.Dictionary<Key, bool> _keysJustPressed = new();

	public override void _Ready() {
		base._Ready();

		_commands[Key.F12] = ToggleDebugMode;
		_commands[Key.F] = ToggleScreenMode;
		// TODO C#
		//     self._commands[KEY_F1] = RSValues.toggle_visible
		//     self._commands[KEY_F2] = RSDraw.toggle_visible
		//     self._commands[KEY_F3] = RSCommand.toggle_visible
		//     self._commands[KEY_F4] = RSLog.toggle_visible
	}

	public override void _Process(double delta) {
		base._Process(delta);

		foreach (System.Collections.Generic.KeyValuePair<Key, Action> command in _commands) {
			if (!DebugMode && command.Key != Key.F && command.Key != Key.F12) {
				continue;
			}
			
			bool keyPressed = Input.IsKeyPressed(command.Key);
			_keysJustPressed.TryAdd(command.Key, false);

			if (!_keysJustPressed[command.Key] && keyPressed) {
				_keysJustPressed[command.Key] = true;
				command.Value?.Invoke();
			}
			else if (_keysJustPressed[command.Key] && !keyPressed) {
				_keysJustPressed[command.Key] = false;
			}
		}
	}

	private void ToggleDebugMode() {
		DebugMode = !DebugMode;
	}

	private void ToggleScreenMode() {
		if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
		}
		else if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
		else {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
	}
}
