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

	private void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		lastSpawn = Time.time;
	}

	private void Start()
	{
		GameObject[] leakSpots = GameObject.FindGameObjectsWithTag("LeakSpot");

		foreach (GameObject spot in leakSpots)
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
	}
}