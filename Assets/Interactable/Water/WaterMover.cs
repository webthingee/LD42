using UnityEngine;

public class WaterMover : MonoBehaviour
{
	public float fillSpeed = 5;
	public static bool moveWater;
	public AudioEvent waterSound;
	private AudioSource _audioSource;
	public bool startAudio;
	public bool audioPlaying;
	
	private void Update()
	{
		if (moveWater && !FindObjectOfType<LevelManager>().gamePaused)
		{
			transform.localPosition = new Vector3(0, transform.localPosition.y + fillSpeed / 1000 , 0);

			if (!audioPlaying)
			{
				startAudio = true;
			}
		}

		if (startAudio)
		{
			if (!_audioSource) _audioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			waterSound.Play(_audioSource);
			startAudio = false;
			audioPlaying = true;
		}

		if (!audioPlaying)
		{
			if (_audioSource) _audioSource.Stop();
		}

		audioPlaying = moveWater;
	}
}