using System.Collections;
using UnityEngine;

public class Leak : Interactable
{
    private SpriteRenderer spriteRenderer;
    private IEnumerator degradeOverTime;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        degradeOverTime = DegradeOverTime();
    }

    protected override void Update()
    {
        base.Update();
        VisualCompletion();
    }

    private void VisualCompletion()
    {
        if (CompletionValue >= 0f)
        {
            spriteRenderer.color = Color.black;
            StartCoroutine(degradeOverTime);
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
        StopCoroutine(degradeOverTime);
    }
    
    protected override IEnumerator InteractableInUse()
    {
        while (true)
        {
            CompletionValue += 0.1f * Time.deltaTime;
            Debug.Log(gameObject.name + " is in use. at " + CompletionValue);
            yield return new WaitForSeconds(interactionSpeed);
        }
    }
    
    private IEnumerator DegradeOverTime()
    {
        while (true)
        {
            CompletionValue -= 0.01f * Time.deltaTime;
            Debug.Log(gameObject.name + " is in use. at " + CompletionValue);
            yield return new WaitForSeconds(interactionSpeed);
        }
    }

}