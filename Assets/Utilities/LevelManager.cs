using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public bool isSpawnRoutine;
	public float leakSpawnMin;
	public float leakSpawnMax;

	public GameObject winCanvas;
	public GameObject loseCanvas;

	private void Awake()
	{
		Time.timeScale = 1;
		winCanvas.SetActive(false);
		loseCanvas.SetActive(false);
	}

	public void WinCanvas()
	{
		Time.timeScale = 0;
		winCanvas.SetActive(true);
	}
	
	public void LoseCanvas()
	{
		Time.timeScale = 0;
		loseCanvas.SetActive(true);
	}

	public void Restart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}