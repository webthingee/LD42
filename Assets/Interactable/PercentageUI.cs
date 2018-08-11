using UnityEngine;
using UnityEngine.UI;

public class PercentageUI : MonoBehaviour 
{
	private Image forgroundImage;
	
	private void Awake()
	{
		forgroundImage = GetComponent<Image>();
		forgroundImage.fillAmount = 0;
	}

	private void OnEnable()
	{
		GetComponentInParent<Leak>().OnCompletionChange += Leak_OnCompletionChange;
	}

	private void Leak_OnCompletionChange(float f)
	{
		Debug.Log(f);
		forgroundImage.fillAmount = f;
	}
}