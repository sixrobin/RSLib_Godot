namespace RSLib.GE
{
    using Godot;

    public static class InputEventExtensions
    {
        public static string SimplifiedName(this InputEvent evt)
        {
            return evt switch
            {
                InputEventKey inputEventKey                   => DisplayServer.KeyboardGetKeycodeFromPhysical(inputEventKey.PhysicalKeycode).ToString(),
                InputEventMouseButton inputEventMouseButton   => inputEventMouseButton.ButtonIndex.ToString(),
                InputEventJoypadButton inputEventJoypadButton => inputEventJoypadButton.ButtonIndex.ToString(),
                InputEventJoypadMotion inputEventJoypadMotion => inputEventJoypadMotion.Axis.ToString(),
                _                                             => evt.AsText()
            };
        }
    }
}