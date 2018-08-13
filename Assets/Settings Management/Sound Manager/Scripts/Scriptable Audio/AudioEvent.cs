using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
	public enum AudioType { SFX, Music };
	public AudioType audioType;

	protected float AdjustVolume(AudioType audioType)
	{
		if (!SoundManager.instance) return 1f;
		
		switch (audioType)
		{
			case AudioType.Music:
				return SoundManager.instance.MusicVolume;
			case AudioType.SFX:
				return SoundManager.instance.SfxVolume;
			default:
				return 0f;
		}
	}
	
	public abstract void Play(AudioSource source, bool canLoop = false);
}