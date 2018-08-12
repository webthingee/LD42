using UnityEngine;

public class WaterMover : MonoBehaviour
{
	public float fillSpeed = 5;
	public static bool moveWater;
	
	private void LateUpdate()
	{		
		if (moveWater)
			transform.localPosition = new Vector3(0, transform.localPosition.y + fillSpeed / 1000 , 0);
	}
}