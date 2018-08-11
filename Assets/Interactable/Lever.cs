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
		FindObjectOfType<DebugTextOnscreen>().ChangeDebugText("");

		if (CompletionValue <= 0)
		{
			Debug.Log("FAIL");
			FindObjectOfType<LevelManager>().LoseCanvas();
		}
		
		if (CompletionValue >= 1)
		{
			Debug.Log("WIN");		
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
				string txt = "HALT! Not All Leaks Are Patched!";
				FindObjectOfType<DebugTextOnscreen>().ChangeDebugText(txt);
				yield return new WaitForSeconds(interactionSpeed);
			}
			else
			{
				StopCoroutine(degradeOverTime);
				string txt = "We're going UP!!!";
				FindObjectOfType<DebugTextOnscreen>().ChangeDebugText(txt);
				CompletionValue += 0.01f * Time.deltaTime;
				percentageUI.ChangeValue(CompletionValue);
				yield return new WaitForSeconds(interactionSpeed);
			}
		}
	}
}