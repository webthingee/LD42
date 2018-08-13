using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	public Text dialogText;
	public string dialogSay;
	public GameObject dialogBox;
	private float showingStart;
	private float showForCurrent;

	private void Update()
	{
		if (dialogSay != "" && !GetComponent<LevelManager>().gamePaused)
		{
			dialogBox.SetActive(true);
			dialogText.text = dialogSay;
			DialogCountdown();
		}
		else
		{
			dialogBox.SetActive(false);
		}
	}

	public void DialogSayThis(string sayString, float showFor = 5f)
	{
		dialogSay = sayString;
		showingStart = Time.time;
		showForCurrent = showFor;
	}
	
	public bool TimeCheck()
	{
		return showingStart + showForCurrent < Time.time;
	}
		
	private void DialogCountdown()
	{
		if (!TimeCheck()) return;
		dialogSay = "";
	}
}