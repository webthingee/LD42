using System.Collections;
using UnityEngine;

public class Lever : Interactable
{
	public SpriteRenderer blinkIndicator;
	public SpriteRenderer leverHandle;
	public Sprite leverOn;
	public Sprite leverOff;
	public AudioEvent holdingLeverSound;
	public AudioEvent releasingLeverSound;
	public AudioEvent klaxSound;


	public Animator propAnimator;

	private AudioSource audioSource;
	[SerializeField] private bool isHoldingLever;
	[SerializeField] private bool klaxPlaying;

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


		if (isHoldingLever && AllLeaksPatched())
		{
			propAnimator.enabled = isHoldingLever;
		}
		else
		{
			propAnimator.enabled = false;
		}
		
		if (CompletionValue <= 0.2f)
		{
			if (CompletionValue >= 0.01f && !klaxPlaying)
			{
				klaxPlaying = true;
				audioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
				klaxSound.Play(audioSource, true);
			}

			if (CompletionValue <= 0.05f)
			{
				if (klaxPlaying)
				{
					audioSource.Stop();
					klaxPlaying = false;
				}
			}
		}
		
		if (Input.GetButtonDown("Jump") && inAction && AllLeaksPatched())
		{
			if (!isHoldingLever)
			{
				var aud = FindObjectOfType<SoundManager>().GetOpenAudioSource();
				aud.loop = false;
				holdingLeverSound.Play(aud);
				isHoldingLever = true;
			}
		}
		
		if (Input.GetButtonUp("Jump") || !inAction )
		{
			leverHandle.GetComponent<SpriteRenderer>().sprite = leverOff;
			if (isHoldingLever)
			{
				releasingLeverSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
			}
			isHoldingLever = false;
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
				leverHandle.GetComponent<SpriteRenderer>().sprite = leverOn;
				CompletionValue += positiveIncrease * Time.deltaTime;
				percentageUI.ChangeValue(CompletionValue);
				yield return new WaitForSeconds(rateOfIncrease);
			}
		}
	}
}