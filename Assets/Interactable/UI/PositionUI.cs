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
		float newY = Mathf.Abs(FindObjectOfType<Lever>().CompletionValue * 100);
		imageToMove.localPosition = new Vector3(0, newY, 0);
	}
}