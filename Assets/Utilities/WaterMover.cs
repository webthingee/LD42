using UnityEngine;

public class WaterMover : MonoBehaviour
{
	public float fillSpeed = 5;
	
	private void LateUpdate()
	{		
		transform.localPosition = new Vector3(0, Mathf.Abs(FindObjectOfType<Lever>().CompletionValue) * -fillSpeed, 0);
	}
}