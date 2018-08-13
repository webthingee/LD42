using UnityEngine;

public class FishMover : MonoBehaviour 
{
	public Vector2 targetPos;
	public float moveSpeed;

	private void Awake()
	{
		targetPos = transform.position;
		moveSpeed = Random.Range(0.5f, 1f);
	}

	private void Update()
	{
		if (Vector2.Distance(transform.position, targetPos) < 0.5f)
		{
			targetPos = PositionToMoveFish();
			moveSpeed = Random.Range(0.5f, 1f);
			ChangeDirection(transform.position.x - targetPos.x);
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		}
	}

	void ChangeDirection (float fdirection)
	{        
		if (fdirection > 0) transform.eulerAngles = new Vector3(0, 0, 0);
		if (fdirection < 0) transform.eulerAngles = new Vector3(0, 180, 0);
	}
	
	private Vector2 PositionToMoveFish()
	{		
		float randX = Random.Range(-24f, 24f);
		float randY = Random.Range(-14f, 14f);
		
		return targetPos = new Vector2(randX, randY);
	}
}