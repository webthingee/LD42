using UnityEngine;

public class WaterMover : MonoBehaviour
{
	public float fillSpeed = 5;
	public static bool moveWater;
	public AudioEvent waterSound;
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
			waterSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
			startAudio = false;
			audioPlaying = true;
		}

		audioPlaying = moveWater;
	}
}