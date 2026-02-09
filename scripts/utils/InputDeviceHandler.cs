using Godot;

public partial class InputDeviceHandler : Node
{
    public enum DeviceType
    {
        UNKNOWN,
        KBM,
        CONTROLLER,
    }
    
    public enum KeyboardMouse
    {
        NONE,
        KEYBOARD,
        MOUSE,
    }

    public InputDeviceHandler()
    {
        Name = nameof(InputDeviceHandler);
    }
    
    public event System.Action<DeviceType, DeviceType> DeviceChanged;

    public KeyboardMouse KeyboardMouseLast { get; private set; }
    
    private DeviceType _currentDevice;
    public DeviceType CurrentDevice
    {
        get => _currentDevice;
        private set
        {
            DeviceType previous = _currentDevice;
            _currentDevice = value;
            if (_currentDevice != previous)
            {
                DeviceChanged?.Invoke(previous, _currentDevice);

                switch (_currentDevice)
                {
                    case DeviceType.UNKNOWN:
                    case DeviceType.KBM:
                        Input.SetMouseMode(Input.MouseModeEnum.Visible);
                        break;
                    case DeviceType.CONTROLLER:
                    default:
                        Input.SetMouseMode(Input.MouseModeEnum.Hidden);
                        break;
                }
            }
        }
    }
    
    public bool IsUsingController()
    {
        return CurrentDevice == DeviceType.CONTROLLER;
    }
    
    public bool IsUsingKBM()
    {
        return CurrentDevice == DeviceType.KBM;
    }
    
    public override void _Ready()
    {
        base._Ready();
    
        ProcessThreadGroup = ProcessThreadGroupEnum.SubThread;
        ProcessThreadMessages = ProcessThreadMessagesEnum.Messages;
    }

    public override void _Input(InputEvent evt)
    {
        base._Input(evt);

        if (evt is InputEventMouseButton)
        {
            CurrentDevice = DeviceType.KBM;
            KeyboardMouseLast = KeyboardMouse.MOUSE;
        }
        else if (evt is InputEventKey)
        {
            CurrentDevice = DeviceType.KBM;
            KeyboardMouseLast = KeyboardMouse.KEYBOARD;
        }
        else if (evt is InputEventJoypadButton || evt is InputEventJoypadMotion joypadMotion && Mathf.Abs(joypadMotion.AxisValue) > 0.5f)
        {
            CurrentDevice = DeviceType.CONTROLLER;
            KeyboardMouseLast = KeyboardMouse.NONE;
        }
    }
}