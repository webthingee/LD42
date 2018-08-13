using UnityEngine;

[CreateAssetMenu(menuName="Audio Events/Single")]
public class SingleAudioEvent : AudioEvent
{
	public AudioClip clip;

	[Range(0,1)] public float volume = 1;
	[Range(0,2)] public float pitch = 1;

	public override void Play(AudioSource source, bool canLoop = false)
	{
		source.clip = clip;
		source.volume = volume * AdjustVolume(audioType);
		source.pitch = pitch;
		source.loop = canLoop;
		source.Play();
	}
}