using Godot;
using RSLib.GE.Debug;

public partial class AudioHandler : Node
{
    public const string MASTER_BUS_NAME = "Master";
    public const string SFX_BUS_NAME = "sfx";

    public AudioHandler(string sfxParentFolder)
    {
        _sfxParentFolder = sfxParentFolder;
        
        AudioServer.AddBus(AudioServer.GetBusCount());
        _sfxBusID = AudioServer.GetBusCount() - 1;
        AudioServer.SetBusName(_sfxBusID, SFX_BUS_NAME);
        AudioServer.SetBusSend(_sfxBusID, MASTER_BUS_NAME);
        
        _sfxMuted = Invasion.Instance.Config.MutePlaceholderSFX;
        
        Debugger.CommandPanel.Add(this, "audio", "mute fmod events", () => FmodUtils.DebugFmodEventsMuted = !FmodUtils.DebugFmodEventsMuted);
        Debugger.CommandPanel.Add(this, "audio", "mute placeholders", () => SetSFXMuted(!_sfxMuted));
    }

    private readonly string _sfxParentFolder;
    private readonly int _sfxBusID;
    private bool _sfxMuted;

    public void SetSFXMuted(bool muted)
    {
        _sfxMuted = muted;
    }
    
    public void SetMasterVolume(float volume)
    {
        AudioServer.SetBusVolumeLinear(AudioServer.GetBusIndex(MASTER_BUS_NAME), volume);
    }
    
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
            GlobalPosition = args?.GlobalPosition ?? Vector2.Zero,
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
