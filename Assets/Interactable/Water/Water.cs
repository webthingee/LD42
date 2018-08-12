using UnityEngine;

public class Water : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		PlayerMovement pm = other.GetComponent<PlayerMovement>();

		if (pm != null)
		{
			pm.isUnderWater = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		PlayerMovement pm = other.GetComponent<PlayerMovement>();

		if (pm != null)
		{
			pm.isUnderWater = false;
		}
	}
}