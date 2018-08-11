using UnityEngine;

[CreateAssetMenu(menuName="Audio Events/Music")]
public class MusicAudioEvent : AudioEvent
{
	public AudioClip clip;
	[Range(0,1)] public float volume = 1;
    public bool loop = true;

    public override void Play (AudioSource source)
	{
		source.clip = clip;
		source.volume = volume * AdjustVolume(audioType);
		source.loop = loop;
		source.Play();		
	}

    public void Pause (AudioSource source)
	{
		source.Pause();
	}

    public void Stop (AudioSource source)
	{
		source.Stop();
	}
}