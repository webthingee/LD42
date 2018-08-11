using System.Collections.Generic;
using UnityEngine;

public class LeakSpawner : MonoBehaviour
{
	public float spawnInterval;
	public GameObject leakPrefab;
	public Transform[] leakPositions;
	public List<Transform> usedLeakPositions = new List<Transform>();

	private LevelManager levelManager;
	
	[Header("Debug")]
	[SerializeField] private float lastSpawn;

	private void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		lastSpawn = Time.time;
	}

	private void Update()
	{
		if (levelManager.isSpawnRoutine)
		{
			SpawnLeak();
		}
		
		if (usedLeakPositions.Count == leakPositions.Length)
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
		
		int randomLeak = Random.Range(0, leakPositions.Length);

		while (usedLeakPositions.Contains(leakPositions[randomLeak]))
		{
			randomLeak = Random.Range(0, leakPositions.Length);
		}

		GameObject leak = Instantiate(leakPrefab, leakPositions[randomLeak].position, Quaternion.identity);
		usedLeakPositions.Add(leakPositions[randomLeak]);
		leak.name = "leak";
	}
}