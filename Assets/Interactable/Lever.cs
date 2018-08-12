using System.Collections;
using UnityEngine;

public class Lever : Interactable
{
	private SpriteRenderer spriteRenderer;

	protected override void Awake()
	{
		base.Awake();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	protected override void Update()
	{
		base.Update();
		spriteRenderer.color = AllLeaksPatched() ? Color.green : Color.red;

		if (CompletionValue <= 0)
		{
			FindObjectOfType<LevelManager>().LoseCanvas();
		}
		
		if (CompletionValue >= 1)
		{
			FindObjectOfType<LevelManager>().WinCanvas();
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
				StopCoroutine(degradeOverTime);

				CompletionValue += positiveIncrease * Time.deltaTime;
				percentageUI.ChangeValue(CompletionValue);
				yield return new WaitForSeconds(rateOfIncrease);
			}
		}
	}
}