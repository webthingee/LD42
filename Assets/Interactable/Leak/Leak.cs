using System;
using System.Collections;
using UnityEngine;

public class Leak : Interactable
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        if (startingSound)
            startingSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
        FindObjectOfType<CamShake>().shakeDuration = 0.3f;
    }

    protected override void VisualCompletion()
    {
        base.VisualCompletion();
        
        animator.SetFloat("PatchPower", CompletionValue);
    }
    
    protected override IEnumerator InteractableInUse()
    {   
        while (true)
        {
            StopCoroutine(degradeOverTime);
            
            CompletionValue += positiveIncrease * Time.deltaTime;
            percentageUI.ChangeValue(CompletionValue);
            yield return new WaitForSeconds(rateOfIncrease);
        }
    }
}