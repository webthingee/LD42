using UnityEngine;

public class Trigger : MonoBehaviour
{
    public AudioEvent audioEvent;
    public bool interuptable;
	
    private AudioSource audioSource;

	private void OnMouseDown()
	{
		audioSource = SoundManager.instance.GetOpenAudioSource();
        audioEvent.Play(audioSource);
	}	
    
    public void OnMouseUp ()
	{
        if (interuptable)
            audioSource.Stop();
	}
}