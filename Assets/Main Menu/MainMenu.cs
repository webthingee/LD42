using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	private void Awake()
	{
		if (!PlayerPrefs.HasKey("MusicVolume"))
		{
			PlayerPrefs.SetFloat("MusicVolume", 0.75f);
		}		
		
		if (!PlayerPrefs.HasKey("SFXVolume"))
		{
			PlayerPrefs.SetFloat("SFXVolume", 0.75f);
		}
	}


	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			LoadGame();
		}
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}