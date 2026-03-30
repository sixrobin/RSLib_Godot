using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

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

    private const double CUSTOM_ECHO_DELAY = 0.5;
    private const double CUSTOM_ECHO_INTERVAL = 0.05;
    
    public InputDeviceHandler()
    {
        Name = nameof(InputDeviceHandler);
    }

    // Initialize these from every project code.
    public readonly System.Collections.Generic.Dictionary<ControllerBrand, System.Collections.Generic.Dictionary<JoyButton, Texture2D>> JoypadButtonIcons = new();
    public readonly System.Collections.Generic.Dictionary<ControllerBrand, System.Collections.Generic.Dictionary<JoyAxis, Texture2D>> JoypadAxisIcons = new();
    public System.Collections.Generic.Dictionary<Key, Texture2D> KeyboardIcons = new();
    public System.Collections.Generic.Dictionary<MouseButton, Texture2D> MouseIcons = new();

    private ControllerBrand _autoControllerBrand = ControllerBrand.UNKNOWN;
    private ControllerBrand _iconsControllerBrand = ControllerBrand.UNKNOWN;

    private Side? _customEchoCurrentSide;
    private Side? CustomEchoCurrentSide
    {
        get => _customEchoCurrentSide;
        set
        {
            _customEchoCurrentSide = value;
            _customEchoDelayTimer = 0;
            _customEchoIntervalTimer = double.MaxValue;
        }
    }
    
    private double _customEchoDelayTimer;
    private double _customEchoIntervalTimer;
    
    public event Action<DeviceType, DeviceType> DeviceChanged;

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

    private void OnJoyConnectionChanged(long device, bool connected)
    {
        Array<int> connectedJoypads = Input.GetConnectedJoypads();
        _autoControllerBrand = ControllerBrand.UNKNOWN;

        if (connectedJoypads.Count == 0)
            return;

        string firstControllerName = Input.GetJoyName(connectedJoypads[0]).ToLower();

        if (firstControllerName.Contains("xbox"))
            _autoControllerBrand = ControllerBrand.XBOX;
        else if (firstControllerName.Contains("ps3") || firstControllerName.Contains("ps4") || firstControllerName.Contains("ps5"))
            _autoControllerBrand = ControllerBrand.PLAYSTATION;
        else if (firstControllerName.Contains("Nintendo Switch"))
            _autoControllerBrand = ControllerBrand.SWITCH;
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
        if (_iconsControllerBrand != ControllerBrand.UNKNOWN)
            return _iconsControllerBrand;
        
        return _autoControllerBrand != ControllerBrand.UNKNOWN
               ? _autoControllerBrand
               : ControllerBrand.XBOX;
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

        ProcessMode = ProcessModeEnum.Always;

        Input.JoyConnectionChanged += OnJoyConnectionChanged;
        OnJoyConnectionChanged(0, false);
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

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (IsUsingController())
        {
            if (CustomEchoCurrentSide == null && Input.IsActionPressed("ui_left"))
                CustomEchoCurrentSide = Side.Left;
            else if (CustomEchoCurrentSide == Side.Left && !Input.IsActionPressed("ui_left"))
                CustomEchoCurrentSide = null;
            
            if (CustomEchoCurrentSide == null && Input.IsActionPressed("ui_right"))
                CustomEchoCurrentSide = Side.Right;
            else if (CustomEchoCurrentSide == Side.Right && !Input.IsActionPressed("ui_right"))
                CustomEchoCurrentSide = null;
            
            if (CustomEchoCurrentSide == null && Input.IsActionPressed("ui_up"))
                CustomEchoCurrentSide = Side.Top;
            else if (CustomEchoCurrentSide == Side.Top && !Input.IsActionPressed("ui_up"))
                CustomEchoCurrentSide = null;
            
            if (CustomEchoCurrentSide == null && Input.IsActionPressed("ui_down"))
                CustomEchoCurrentSide = Side.Bottom;
            else if (CustomEchoCurrentSide == Side.Bottom && !Input.IsActionPressed("ui_down"))
                CustomEchoCurrentSide = null;
            
            if (CustomEchoCurrentSide.HasValue)
            {
                _customEchoDelayTimer += TimeHandler.GetUnscaledDeltaTime();
                if (_customEchoDelayTimer > CUSTOM_ECHO_DELAY)
                {
                    if (_customEchoIntervalTimer > CUSTOM_ECHO_INTERVAL)
                    {
                        string sideActionName = CustomEchoCurrentSide.Value switch
                        {
                            Side.Left   => "ui_left",
                            Side.Right  => "ui_right",
                            Side.Top    => "ui_up",
                            Side.Bottom => "ui_down",
                            _           => string.Empty,
                        };
                        
                        Input.ParseInputEvent(new InputEventAction
                        {
                            Action = sideActionName,
                            Pressed = true,
                        });
                        
                        Input.ParseInputEvent(new InputEventAction
                        {
                            Action = sideActionName,
                            Pressed = false,
                        });
                        
                        _customEchoIntervalTimer = 0;
                    }

                    _customEchoIntervalTimer += TimeHandler.GetUnscaledDeltaTime();
                }
            }
        }
    }
}