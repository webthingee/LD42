using System;
using System.Collections;
using UnityEngine;

public class Leak : Interactable
{
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void VisualCompletion()
    {
        base.VisualCompletion();
        
        if (CompletionValue >= 0f)
        {
            spriteRenderer.color = Color.black;
        }
        if (CompletionValue >= 0.25f)
        {
            spriteRenderer.color = Color.red;
        }
        if (CompletionValue >= 0.55f)
        {
            spriteRenderer.color = Color.yellow;
        }
        if (CompletionValue >= 0.75f)
        {
            spriteRenderer.color = Color.green;
        }

        if (!(CompletionValue >= 0.95f)) return;
        spriteRenderer.color = Color.white;
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