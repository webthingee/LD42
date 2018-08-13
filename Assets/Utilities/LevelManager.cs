using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public bool isSpawnRoutine;
	public float leakSpawnMin;
	public float leakSpawnMax;

	public GameObject winCanvas;
	public AudioEvent winSound;
	
	public GameObject loseCanvas;
	public AudioEvent loseSound;

	public bool gamePaused;

	private void Awake()
	{
		Time.timeScale = 1;
		winCanvas.SetActive(false);
		loseCanvas.SetActive(false);
	}

	private void Update()
	{
		if (winCanvas.activeSelf || loseCanvas.activeSelf)
		{
			if (Input.GetButtonDown("Jump"))
			{
				Restart();
			}
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			Restart();
		}
		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Application.Quit();
			
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
			
			// WebGL won't close, load Main
			SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
		}
	}

	public void WinCanvas()
	{
		if (!winCanvas.activeSelf)
		{
			winCanvas.SetActive(true);
			var aud = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			aud.loop = false;
			winSound.Play(aud);
			GetComponent<TriggerMusic>().Stop();
			Time.timeScale = 0;
		}
	}
	
	public void LoseCanvas()
	{
		if (!loseCanvas.activeSelf)
		{
			loseCanvas.SetActive(true);
			var aud = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			aud.loop = false;
			loseSound.Play(aud);
			GetComponent<TriggerMusic>().Stop();
			Time.timeScale = 0;
		}
	}

	public void Restart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}