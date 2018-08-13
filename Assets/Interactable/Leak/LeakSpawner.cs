using System.Collections.Generic;
using UnityEngine;

public class LeakSpawner : MonoBehaviour
{
	public float spawnInterval;
	public GameObject leakPrefab;
	public List<Transform> leakPositions = new List<Transform>();
	public List<Transform> usedLeakPositions = new List<Transform>();

	private LevelManager levelManager;
	
	[Header("Debug")]
	[SerializeField] private float lastSpawn;

	[SerializeField] private int leakCount;
	
	public bool firstCompleteSay;


	private void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		lastSpawn = Time.time;
	}

	private void Start()
	{
		LeakPoint[] leakSpots = FindObjectsOfType<LeakPoint>();

		foreach (LeakPoint spot in leakSpots)
		{
			leakPositions.Add(spot.transform);
		}
	}

	private void Update()
	{
		if (levelManager.isSpawnRoutine)
		{
			SpawnLeak();
		}
		
		if (usedLeakPositions.Count == leakPositions.Count)
		{
			levelManager.isSpawnRoutine = false;
		}
	}

	public bool TimeCheck()
	{
		return lastSpawn + spawnInterval < Time.time;
	}

	public void SpawnLeak()
	{
		if (!TimeCheck()) return;
		if (FindObjectOfType<LevelManager>().gamePaused) return;
		
		lastSpawn = Time.time;
		spawnInterval = Random.Range(levelManager.leakSpawnMin, levelManager.leakSpawnMax);
		
		int randomLeak = Random.Range(0, leakPositions.Count);

		while (usedLeakPositions.Contains(leakPositions[randomLeak]))
		{
			randomLeak = Random.Range(0, leakPositions.Count);
		}

		GameObject leak = Instantiate(leakPrefab, leakPositions[randomLeak].position, Quaternion.identity);
		usedLeakPositions.Add(leakPositions[randomLeak]);
		leak.name = "leak";

		if (leakCount <= 0)
		{
			FindObjectOfType<DialogManager>().DialogSayThis("Ah, a leak! Gotta pactch that quick!", 5f);
		}

		if (leakCount == 2)
		{
			FindObjectOfType<DialogManager>().DialogSayThis("Oh no, Another?!?!", 5f);
		}
		
		if (leakCount == 4)
		{
			FindObjectOfType<DialogManager>().DialogSayThis("I got this!", 5f);
		}
		
		if (leakCount == 6)
		{
			FindObjectOfType<DialogManager>().DialogSayThis("This is outa control!", 5f);
		}
		
		leakCount++;
	}
}