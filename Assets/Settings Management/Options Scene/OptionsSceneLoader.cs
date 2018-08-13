using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsSceneLoader : MonoBehaviour
{
	/// <summary>
	/// Place in game to provide access to the options menu.
	/// Requires a Sound Manager to be in place within the scene
	/// </summary>	
	[SerializeField] public string optionsScene;

	public bool optionSceneLoaded;
	
	private void Start()
	{
		if (SoundManager.instance == null)
		{
			Debug.Log("OptionsScene requires a Sound Manager to be present in the game.");
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!optionSceneLoaded)
			{
				SceneManager.LoadScene(optionsScene, LoadSceneMode.Additive);
				optionSceneLoaded = true;
				Time.timeScale = 0;
				FindObjectOfType<LevelManager>().gamePaused = true;
			}
			else
			{
				SceneManager.UnloadSceneAsync(optionsScene);
				optionSceneLoaded = false;
				Time.timeScale = 1;
				FindObjectOfType<LevelManager>().gamePaused = false;
			}
		}
	}
}