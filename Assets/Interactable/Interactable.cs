using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool inAction;
    public bool isComplete;
    public float interactionSpeed = 1f;
    
    [SerializeField] private float completionValue;
    public PercentageUI percentageUI;
    
    private IEnumerator interactableInUse;
    protected IEnumerator degradeOverTime;


    public float CompletionValue
    {
        get { return completionValue; }
        set
        {
            completionValue = value;
            if (completionValue >= 1f)
            {
                completionValue = 1f;
                isComplete = true;
            }

            if (completionValue < 1)
            {
                isComplete = false;
            }            
            
            if (completionValue <= 0)
            {
                completionValue = 0;
            }
        }
    }

    protected virtual void Awake()
    {
        interactableInUse = InteractableInUse();
        degradeOverTime = DegradeOverTime();
    }

    protected virtual void Update()
    {
        VisualCompletion();
        
        if (inAction && Input.GetButton("Jump"))
        {
            StartCoroutine(interactableInUse); 
        }
        else
        {
            StopCoroutine(interactableInUse);
        }
    }

    protected virtual void VisualCompletion()
    {
        if (CompletionValue >= 0f)
        {
            StartCoroutine(degradeOverTime);
        }
        
        if (!(CompletionValue >= 0.95f)) return;
        StopCoroutine(degradeOverTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        inAction = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inAction = false;
    }

    protected virtual IEnumerator InteractableInUse()
    {
        while (true)
        {
            StopCoroutine(degradeOverTime);
            yield return new WaitForSeconds(interactionSpeed);
        }
    }
    
    protected virtual IEnumerator DegradeOverTime()
    {
        while (true)
        {
            CompletionValue -= 0.001f * Time.deltaTime;
            if (percentageUI)
                percentageUI.ChangeValue(CompletionValue);
            
            yield return new WaitForSeconds(interactionSpeed);
        }
    }
}