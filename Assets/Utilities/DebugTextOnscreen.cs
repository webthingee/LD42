using UnityEngine;
using UnityEngine.UI;

public class DebugTextOnscreen : MonoBehaviour 
{
	private Text debugText;
	
	private void Awake()
	{
		debugText = GetComponent<Text>();
	}

	public void ChangeDebugText(string text)
	{
		debugText.text = text;
	}
}