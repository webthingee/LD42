using System.Collections;
using UnityEngine;

public class Lever : Interactable 
{
	private bool AllLeaksPatched()
	{
		Leak[] leaks = FindObjectsOfType<Leak>();

		foreach (Leak leak in leaks)
		{
			if (leak.isComplete != true)
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
				Debug.Log("Not All Leaks Are Patched!");
				yield return new WaitForSeconds(interactionSpeed);
			}
			else
			{
				Debug.Log(gameObject.name + " is in use.");
				yield return new WaitForSeconds(interactionSpeed);
			}
		}
	}
}