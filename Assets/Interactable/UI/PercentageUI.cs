using UnityEngine;
using UnityEngine.UI;

public class PercentageUI : MonoBehaviour 
{
	private Image forgroundImage;
	
	private void Awake()
	{
		forgroundImage = GetComponent<Image>();
	}
	
	public void ChangeValue(float f)
	{
		if (forgroundImage != null) forgroundImage.fillAmount = f;
	}
}