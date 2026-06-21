namespace RSLib.GE
{
    using Godot;

    public static class InputEventExtensions
    {
        public static string SimplifiedName(this InputEvent evt)
        {
            return evt switch
            {
                InputEventKey key                   => DisplayServer.KeyboardGetKeycodeFromPhysical(key.PhysicalKeycode).ToString(),
                InputEventMouseButton mouseButton   => mouseButton.ButtonIndex.ToString(),
                InputEventMouseMotion mouseMotion   => mouseMotion.ButtonMask.ToString(),
                InputEventJoypadButton joypadButton => joypadButton.ButtonIndex.ToString(),
                InputEventJoypadMotion joypadMotion => joypadMotion.Axis.ToString(),
                _                                   => evt.AsText(),
            };
        }
        
        public static string LocalizedName(this InputEvent inputEvent)
        {
            return inputEvent switch
            {
                InputEventKey key                   => Localizer.TryGet($"key_{DisplayServer.KeyboardGetKeycodeFromPhysical(key.PhysicalKeycode).ToString().ToLower()}", out string result) ? result : inputEvent.SimplifiedName(),
                InputEventMouseButton mouseButton   => Localizer.TryGet($"mouse_button_{mouseButton.ButtonIndex.ToString().ToLower()}", out string result) ? result : inputEvent.SimplifiedName(),
                InputEventMouseMotion mouseMotion   => Localizer.TryGet($"mouse_motion_{mouseMotion.ButtonMask.ToString().ToLower()}", out string result) ? result : inputEvent.SimplifiedName(),
                InputEventJoypadButton joypadButton => Localizer.TryGet($"joypad_button_{joypadButton.ButtonIndex.ToString().ToLower()}", out string result) ? result : inputEvent.SimplifiedName(),
                InputEventJoypadMotion joypadMotion => Localizer.TryGet($"joypad_motion_{joypadMotion.Axis.ToString().ToLower()}", out string result) ? result : inputEvent.SimplifiedName(),
                _                                   => inputEvent.SimplifiedName(),
            };
        }
    }
}