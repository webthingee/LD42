using System.Collections;
using UnityEngine;

public class Lever : Interactable
{
	public SpriteRenderer blinkIndicator;
	public SpriteRenderer leverHandle;
	public Sprite leverOn;
	public Sprite leverOff;

	protected override void Update()
	{
		base.Update();
		
		isPaused = AllLeaksPatched();

		if (AllLeaksPatched())
		{
			blinkIndicator.color = Color.green;
			WaterMover.moveWater = false;
		}
		else
		{
			blinkIndicator.color = Color.red;
			WaterMover.moveWater = true;
		}

		if (CompletionValue <= 0)
		{
			FindObjectOfType<LevelManager>().LoseCanvas();
		}
		
		if (CompletionValue >= 1)
		{
			FindObjectOfType<LevelManager>().WinCanvas();
		}
		
		if (Input.GetButtonUp("Jump") || !inAction )
		{
//			if (audioSource == null) 
//				audioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
//
//			audioSource.loop = true;
//			blowtorch.Play(audioSource);
			leverHandle.GetComponent<SpriteRenderer>().sprite = leverOff;
		}
	}
	
	private bool AllLeaksPatched()
	{
		Leak[] leaks = FindObjectsOfType<Leak>();

		foreach (Leak leak in leaks)
		{
			if (leak.CompletionValue <= 0.1f)
			{
				return false;
			}
		}
		return true;
	}
	
	protected override IEnumerator InteractableInUse()
	{	
		while (true)
		{
			if (!AllLeaksPatched())
			{
				yield return new WaitForSeconds(rateOfIncrease);
			}
			else
			{
				//StopCoroutine(degradeOverTime);
				leverHandle.GetComponent<SpriteRenderer>().sprite = leverOn;
				CompletionValue += positiveIncrease * Time.deltaTime;
				percentageUI.ChangeValue(CompletionValue);
				yield return new WaitForSeconds(rateOfIncrease);
			}
		}
	}
}