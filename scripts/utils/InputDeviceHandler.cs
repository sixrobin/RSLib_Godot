using Godot;

public partial class InputDeviceHandler : Node
{
    public enum DeviceType
    {
        UNKNOWN,
        KBM,
        CONTROLLER,
    }

    public InputDeviceHandler()
    {
        Name = nameof(InputDeviceHandler);
    }
    
    public event System.Action<DeviceType, DeviceType> DeviceChanged;
    
    private DeviceType _currentDevice;
    public DeviceType CurrentDevice
    {
        get => _currentDevice;
        private set
        {
            DeviceType previous = _currentDevice;
            _currentDevice = value;
            if (_currentDevice != previous)
                DeviceChanged?.Invoke(previous, _currentDevice);
        }
    }
    
    public bool IsUsingController()
    {
        return CurrentDevice == DeviceType.CONTROLLER;
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

        if (evt is InputEventMouseButton or InputEventKey)
            CurrentDevice = DeviceType.KBM;
        else if (evt is InputEventJoypadButton || evt is InputEventJoypadMotion joypadMotion && Mathf.Abs(joypadMotion.AxisValue) > 0.5f)
            CurrentDevice = DeviceType.CONTROLLER;
    }
}