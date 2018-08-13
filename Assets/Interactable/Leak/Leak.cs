using System;
using System.Collections;
using UnityEngine;

public class Leak : Interactable
{
    private Animator animator;
    
    [SerializeField] private bool gushingPlaying;
    private AudioSource audioSource;
    public AudioEvent gushingSound;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        if (startingSound)
        {
            var aud = FindObjectOfType<SoundManager>().GetOpenAudioSource();
            aud.loop = false;
            startingSound.Play(aud);
        }
        
        FindObjectOfType<CamShake>().shakeDuration = 0.3f;
    }

    protected override void CompletionTracking()
    {
        base.CompletionTracking();
        
        if (CompletionValue <= 0.25f && CompletionValue >= 0f)
        {
            if (!gushingPlaying)
            {
                gushingPlaying = true;
                audioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
                gushingSound.Play(audioSource, true);
            }
        }
        
        if (CompletionValue >= 0.25f)
        {
            if (gushingPlaying)
            {
                audioSource.Stop();
                gushingPlaying = false;
            }
        }
        
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