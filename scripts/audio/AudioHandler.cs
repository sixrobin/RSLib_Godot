using Godot;

public partial class AudioHandler : Node2D
{
    public const string SFX_BUS_NAME = "sfx";

    public AudioHandler(string sfxParentFolder)
    {
        _sfxParentFolder = sfxParentFolder;
        
        AudioServer.AddBus(AudioServer.GetBusCount());
        _sfxBusID = AudioServer.GetBusCount() - 1;
        AudioServer.SetBusName(_sfxBusID, SFX_BUS_NAME);
    }

    private readonly string _sfxParentFolder;
    private readonly int _sfxBusID;

    public void SetSFXVolume(float volume)
    {
        AudioServer.SetBusVolumeLinear(_sfxBusID, volume);
    }
    
    public void SFX(string path, SFXArgs args = null)
    {
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
