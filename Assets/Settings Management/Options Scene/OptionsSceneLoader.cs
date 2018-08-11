using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsSceneLoader : MonoBehaviour
{
	/// <summary>
	/// Place in game to provide access to the options menu.
	/// Requires a Sound Manager to be in place within the scene
	/// </summary>	
	[SerializeField] public string optionsScene;
	
	private void Start()
	{
		if (SoundManager.instance == null)
		{
			Debug.Log("OptionsScene requires a Sound Manager to be present in the game.");
		}
	}

	private void Update()
	{
		// ReSharper disable once InvertIf
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.Escape))
		{
			if (!SceneManager.GetSceneByName(optionsScene).IsValid())
				SceneManager.LoadScene(optionsScene, LoadSceneMode.Additive);
		}
	}
}