using UnityEngine;

[CreateAssetMenu(menuName="Audio Events/Single")]
public class SingleAudioEvent : AudioEvent
{
	public AudioClip clip;

	[Range(0,1)] public float volume = 1;
	[Range(0,2)] public float pitch = 1;

	public override void Play(AudioSource source)
	{
		source.clip = clip;
		source.volume = volume * AdjustVolume(audioType);
		source.pitch = pitch;
		source.loop = false;
		source.Play();
	}
}