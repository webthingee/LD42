using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Random")]
public class RandomAudioEvent : AudioEvent
{
	public AudioClip[] clips;

	[Range(0.1f, 1.9f)] public float volumeMin = 0.5f;
	[Range(0.11f, 1)] public float volumeMax = 0.51f;
	
	[Range(0.1f, 1.9f)] public float pitchMin = 1.0f;
	[Range(0.11f, 2f)] public float pitchMax = 1.1f;

	public override void Play(AudioSource source)
	{
		if (clips.Length == 0) return;

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volumeMin, volumeMax) * AdjustVolume(audioType);
		source.pitch = Random.Range(pitchMin, pitchMax);
		source.Play();
	}
}