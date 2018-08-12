using UnityEngine;

public class PositionUI : MonoBehaviour
{
	public RectTransform imageToMove;

	private void Awake()
	{
		imageToMove = GetComponent<RectTransform>();
	}

	private void Update()
	{
		MoveImage();
	}

	public void MoveImage()
	{
		//Debug.Log(FindObjectOfType<Lever>().CompletionValue * 100);
		
		float newY = Mathf.Abs(FindObjectOfType<Lever>().CompletionValue * 100);
		Debug.Log(newY);
		//imageToMove.pivot = new Vector3(0.5f, newY, 0);
		imageToMove.localPosition = new Vector3(0, newY, 0);
	}
}