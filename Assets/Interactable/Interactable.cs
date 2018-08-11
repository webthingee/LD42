using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool inAction;
    public bool isComplete;
    public float interactionSpeed = 1f;
    
    [SerializeField] private float completionValue;
    private IEnumerator interactableInUse;

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
    }

    protected virtual void Update()
    {
        if (inAction && Input.GetButton("Jump"))
        {
            StartCoroutine(interactableInUse); 
        }
        else
        {
            StopCoroutine(interactableInUse);
        }
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
            Debug.Log(gameObject.name + " is in use by Interactable Object.");
            yield return new WaitForSeconds(interactionSpeed);
        }
    }
}