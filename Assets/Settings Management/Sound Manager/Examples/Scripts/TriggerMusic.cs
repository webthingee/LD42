using UnityEngine;

public class TriggerMusic : MonoBehaviour 
{
	public MusicAudioEvent audioEvent;	
	private AudioSource audioSource;

	public void Start()
	{
		audioSource = SoundManager.instance.GetOpenAudioSource();
		audioEvent.Play(audioSource, true);
		SoundManager.instance.OnMuiscVolumeChange += SoundManager_OnMusicVolumeChange;
	}

	private void SoundManager_OnMusicVolumeChange()
	{
		if (audioSource)
			audioSource.volume = audioEvent.volume * SoundManager.instance.MusicVolume;
	}

	public void Pause ()
	{
		audioSource.Pause();
	}

	public void UnPause()
	{
		audioSource.UnPause();
	}

	public void Stop ()
	{
		if (audioSource) audioSource.Stop();
	}
}