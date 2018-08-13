﻿using UnityEngine;
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
		}
	}

	public void WinCanvas()
	{
		winCanvas.SetActive(true);
		winSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
		GetComponent<TriggerMusic>().Stop();
		Time.timeScale = 0;
	}
	
	public void LoseCanvas()
	{
		loseCanvas.SetActive(true);
		loseSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
		GetComponent<TriggerMusic>().Stop();
		Time.timeScale = 0;
	}

	public void Restart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}