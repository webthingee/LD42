using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Range(0.01f, 1f)] public float positiveIncrease = 0.1f;
    public float rateOfIncrease = 1f;
    [Range(0.001f, 1f)] public float negitiveIncrease = 0.01f;
    public float rateOfDecrease = 1f;
    
    public bool inAction;
    public bool isComplete;
    
    [SerializeField] private float completionValue;
    public PercentageUI percentageUI;
    
    private IEnumerator interactableInUse;
    protected IEnumerator degradeOverTime;
    public bool isPaused;

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
        if (CompletionValue >= 0f && !isPaused)
        {
            StartCoroutine(degradeOverTime);
        }
        else
        {
            StopCoroutine(degradeOverTime);
        }
        
        //if (!(CompletionValue >= 0.99f)) return;
        //StopCoroutine(degradeOverTime);
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
            yield return new WaitForSeconds(1f);
        }
    }
    
    protected virtual IEnumerator DegradeOverTime()
    {
        while (true)
        {
            CompletionValue -= negitiveIncrease * Time.deltaTime;
            if (percentageUI)
                percentageUI.ChangeValue(CompletionValue);
            
            yield return new WaitForSeconds(rateOfDecrease);
        }
    }
}