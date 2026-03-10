using System.Collections.Generic;
using System.Linq;
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

    public enum ControllerBrand
    {
        UNKNOWN,
        XBOX,
        PLAYSTATION,
        SWITCH,
        SWITCH2,
        STEAMDECK,
    }

    public InputDeviceHandler()
    {
        Name = nameof(InputDeviceHandler);
    }

    // Initialize these from every project code.
    public readonly Dictionary<ControllerBrand, Dictionary<JoyButton, Texture2D>> JoypadButtonIcons = new();
    public readonly Dictionary<ControllerBrand, Dictionary<JoyAxis, Texture2D>> JoypadAxisIcons = new();
    public Dictionary<Key, Texture2D> KeyboardIcons = new();
    public Dictionary<MouseButton, Texture2D> MouseIcons = new();

    private ControllerBrand _iconsControllerBrand = ControllerBrand.UNKNOWN;
    
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

    public bool SetIconsControllerBrand(ControllerBrand brand, out string error)
    {
        if (brand != ControllerBrand.UNKNOWN
            && (!JoypadButtonIcons.ContainsKey(brand) || !JoypadAxisIcons.ContainsKey(brand)))
        {
            _iconsControllerBrand = ControllerBrand.UNKNOWN;
            error = $"{nameof(InputDeviceHandler)} cannot use {brand} for icons as {nameof(JoypadButtonIcons)} or {nameof(JoypadAxisIcons)} do not have that key.";
            return false;
        }
        
        _iconsControllerBrand = brand;
        error = string.Empty;
        return true;
    }

    public ControllerBrand GetIconsControllerBrand()
    {
        // TODO: If unknown, automatically detect plugged controller brand.
        return _iconsControllerBrand != ControllerBrand.UNKNOWN ? _iconsControllerBrand : ControllerBrand.XBOX;
    }

    public Texture2D GetJoypadButtonIcon(JoyButton button)
    {
        return JoypadButtonIcons[GetIconsControllerBrand()].GetValueOrDefault(button);
    }
    
    public Texture2D GetJoypadAxisIcon(JoyAxis axis)
    {
        return JoypadAxisIcons[GetIconsControllerBrand()].GetValueOrDefault(axis);
    }

    public Texture2D GetKeyboardIcon(Key key)
    {
        return KeyboardIcons.GetValueOrDefault(key);
    }
    
    public Texture2D GetMouseIcon(MouseButton key)
    {
        return MouseIcons.GetValueOrDefault(key);
    }
    
    public InputEvent GetInputEventForCurrentDevice(string actionName)
    {
        Godot.Collections.Array<InputEvent> actionEvents = InputMap.ActionGetEvents(actionName);
        if (actionEvents.Count == 0)
            return null;

        return Invasion.Instance.InputDeviceHandler.IsUsingController()
               ? actionEvents.FirstOrDefault(actionEvent => actionEvent is InputEventJoypadButton or InputEventJoypadMotion)
               : actionEvents.FirstOrDefault(actionEvent => actionEvent is InputEventKey or InputEventMouseButton or InputEventMouseMotion);
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