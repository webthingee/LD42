using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Range(0.01f, 1f)] public float positiveIncrease = 0.1f;
    public float rateOfIncrease = 1f;
    [Range(0.001f, 1f)] public float negitiveIncrease = 0.01f;
    public float rateOfDecrease = 1f;

    public AudioEvent startingSound;
    public AudioEvent successSound;
    
    public bool inAction;
    public bool isComplete;
    
    [SerializeField] private float completionValue;
    public PercentageUI percentageUI;
    
    private IEnumerator interactableInUse;
    protected IEnumerator degradeOverTime;
    public bool isPaused;
    [SerializeField] private float timeOccured;
    [SerializeField] private float timeInterval = 10f;

    public float CompletionValue
    {
        get { return completionValue; }
        set
        {
            completionValue = value;
            if (completionValue >= 1f)
            {
                completionValue = 1f;

                if (!isComplete)
                {
                    timeOccured = Time.time;
                    if (successSound)
                    {
                        var aud = FindObjectOfType<SoundManager>().GetOpenAudioSource();
                        aud.loop = false;
                        successSound.Play(FindObjectOfType<SoundManager>().GetOpenAudioSource());
                    }
                }
                
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

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        CompletionTracking();
        
        if (inAction && Input.GetButton("Jump"))
        {
            StartCoroutine(interactableInUse);
        }
        else
        {
            StopCoroutine(interactableInUse);
        }

        if (isComplete)
        {
            CompletionHold();
        }
    }
    
    private void CompletionHold()
    {
        if (TimeCheck())
        {
            isComplete = false;
        }
    }
    
    public bool TimeCheck()
    {
        return timeOccured + timeInterval < Time.time;
    }
    
    protected virtual void CompletionTracking()
    {
        if (CompletionValue >= 0f && !isPaused)
        {
            StartCoroutine(degradeOverTime);
        }
        else
        {
            StopCoroutine(degradeOverTime);
        }
        
        if (!(CompletionValue >= 0.99f)) return;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tag))
            inAction = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (inAction && other.CompareTag(tag)) inAction = false;
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
            if (!isComplete)
                CompletionValue -= negitiveIncrease * Time.deltaTime;
            if (percentageUI)
                percentageUI.ChangeValue(CompletionValue);
            
            yield return new WaitForSeconds(rateOfDecrease);
        }
    }
}