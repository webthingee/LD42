using UnityEngine;
using UnityEngine.UI;

public class PercentageUI : MonoBehaviour 
{
	private Image forgroundImage;
	
	private void Awake()
	{
		forgroundImage = GetComponent<Image>();
	}

//	private void OnEnable()
//	{
//		GetComponentInParent<Leak>().OnCompletionChange += Leak_OnCompletionChange;
//	}
//
//	private void Leak_OnCompletionChange(float f)
//	{
//		forgroundImage.fillAmount = f;
//	}
	
	public void ChangeValue(float f)
	{
		forgroundImage.fillAmount = f;
	}
}