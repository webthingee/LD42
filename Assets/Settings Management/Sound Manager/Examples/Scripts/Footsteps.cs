using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioEvent audioEvent;
    public float stepSpeed;
    AudioSource sm;
    private bool walking;

    public void OnMouseDown ()
	{
		sm = FindObjectOfType<SoundManager>().GetOpenAudioSource();
        if (!walking)
            {
                walking = true;
                StartCoroutine(WalkingSounds());
            }
	}
    
    public void OnMouseUp ()
	{
        walking = false;
	}

    IEnumerator WalkingSounds ()
    {
        audioEvent.Play(sm);
        yield return new WaitForSeconds(stepSpeed);
        if (walking)
            StartCoroutine(WalkingSounds());
    }
}
