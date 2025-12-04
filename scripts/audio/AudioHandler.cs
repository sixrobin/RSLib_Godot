using Godot;
using RSLib.GE.Debug;

public partial class AudioHandler : Node2D
{
    public const string SFX_BUS_NAME = "sfx";

    public AudioHandler(string sfxParentFolder)
    {
        _sfxParentFolder = sfxParentFolder;
        
        AudioServer.AddBus(AudioServer.GetBusCount());
        _sfxBusID = AudioServer.GetBusCount() - 1;
        AudioServer.SetBusName(_sfxBusID, SFX_BUS_NAME);
        
        // TODO: FmodUtils is not part of RSLib_Godot -> move these commands to FmodUtils itself. 
        Debugger.CommandPanel.Add(this, "audio", "fmod debug audible", () =>
        {
            FmodUtils.DebugAudible = !FmodUtils.DebugAudible;
            FmodUtils.RaiseEvent(FmodUtils.DebugAudible ? "event:/DEBUG/debugIsAudible" : "event:/DEBUG/debugIsInaudible");
        });
        Debugger.CommandPanel.Add(this, "audio", "mute fmod events", () => FmodUtils.DebugFmodEventsMuted = !FmodUtils.DebugFmodEventsMuted);
        Debugger.CommandPanel.Add(this, "audio", "mute placeholders", () => _sfxMuted = !_sfxMuted);
    }

    private readonly string _sfxParentFolder;
    private readonly int _sfxBusID;
    private bool _sfxMuted;

    public void SetSFXVolume(float volume)
    {
        AudioServer.SetBusVolumeLinear(_sfxBusID, volume);
    }
    
    public void SFX(string path, SFXArgs args = null)
    {
        if (_sfxMuted)
            return;
        
        string streamPath = $"{_sfxParentFolder}/{path}";
        
        AudioStreamPlayer2D audioPlayer2D = new()
        {
            Stream = GD.Load<AudioStream>(streamPath),
            Bus = SFX_BUS_NAME,
            GlobalPosition = args?.GlobalPosition ?? GlobalPosition,
            VolumeLinear = args?.Volume ?? 1f,
            Attenuation = 0,
            MaxDistance = float.MaxValue,
        };
        
        AddChild(audioPlayer2D);

        audioPlayer2D.Finished += audioPlayer2D.QueueFree;
        audioPlayer2D.Play();
    }

    public class SFXArgs
    {
        public Vector2? GlobalPosition = null;
        public float Volume = 1f;
    }
}
