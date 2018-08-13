using UnityEngine;

public class Torch : MonoBehaviour 
{
	public AudioEvent blowtorch;
	public bool isTorchable;

	private AudioSource audioSource;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		animator.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetButtonUp("Jump"))
		{
			if (audioSource != null)
			{
				audioSource.Stop();
				audioSource = null;
			}

			if (animator.gameObject.activeSelf)
			{
				animator.SetBool("IsTorching", false);
				animator.gameObject.SetActive(false);
			}
		}

		if (Input.GetButton("Jump") && isTorchable)
		{
			if (audioSource == null)
			{
				audioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
				audioSource.loop = true;
				blowtorch.Play(audioSource);
			}			

			animator.gameObject.SetActive(true);
			animator.SetBool("IsTorching", true);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == tag)
		{
			isTorchable = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == tag)
		{
			if (isTorchable) isTorchable = false;
		}
	}
}