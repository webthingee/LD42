using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	public GameObject character;
	private Animator animator;
	private CharacterMovement characterMovement;
	private PlayerMovement playerMovement;

	public AudioEvent footstep;
	public float stepSpeed;
	private AudioSource walkingAudioSource;
	private IEnumerator walkingSounds;
	[SerializeField] private bool hasStepAudio;
	
	public AudioEvent climbingSound;
	private AudioSource climbingAudioSource;
	private IEnumerator climbingSounds;
	[SerializeField] private bool hasClimbAudio;

	private void Awake()
	{
		characterMovement = GetComponent<CharacterMovement>();
		playerMovement = GetComponent<PlayerMovement>();
		animator = GetComponentInChildren<Animator>();
		walkingSounds = WalkingSounds();      
		climbingSounds = ClimbingSounds();      
	}

	private void Update ()
	{        
		ChangeDirection(characterMovement.GetMoveDirection.x);

		animator.SetFloat("Forward", Mathf.Abs(characterMovement.GetMoveDirection.x));
		animator.SetBool("IsClimbing", ClimbCheck());

		if (Input.GetAxisRaw("Horizontal") != 0 && !playerMovement.ignoreOneWays)
		{
			StartWalking();
			//@TODO player makes sound while bumping into walls.
			// ALL "is walking" logic, probable in it's own script.
			// maybe define is walking in other file, where move.x != 0 and isRight/Left is false?
		}
		else
		{
			StopWalking();
		}
		
		if (playerMovement.isClimbing && Input.GetAxis("Vertical") != 0)
		{
			StartClimbing();
		}
		else
		{
			StopClimbing();
		}
	}

	private bool ClimbCheck()
	{
		return playerMovement.isClimbing && characterMovement.GetMoveDirection != Vector3.zero;
	}

	void ChangeDirection (float fdirection)
	{        
		if (fdirection > 0) character.transform.eulerAngles = new Vector3(0, 0, 0);
		if (fdirection < 0) character.transform.eulerAngles = new Vector3(0, 180, 0);
	}
	
	private void StartWalking ()
	{
		if (hasStepAudio) return;
		hasStepAudio = true;
		
		StartCoroutine(walkingSounds);
	}

	private void StopWalking ()
	{
		if (!hasStepAudio) return;
		hasStepAudio = false;
		
		StopCoroutine(walkingSounds);

		if (walkingAudioSource == null) return;
		walkingAudioSource.Stop();
		walkingAudioSource = null;
	}

	private IEnumerator WalkingSounds ()
	{		
		while (true)
		{
			hasStepAudio = true;
			
			if (walkingAudioSource == null)
			{
				walkingAudioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			}
			
			footstep.Play(walkingAudioSource);
			yield return new WaitForSeconds(stepSpeed * 0.75f);
			if (walkingAudioSource != null) walkingAudioSource.Stop();
			yield return new WaitForSeconds(stepSpeed * 0.25f);
		}
	}
	
	private void StartClimbing ()
	{
		if (hasClimbAudio) return;
		hasClimbAudio = true;
		
		StartCoroutine(climbingSounds);
	}

	private void StopClimbing ()
	{
		if (!hasClimbAudio) return;
		hasClimbAudio = false;

		StopCoroutine(climbingSounds);
		
		if (climbingAudioSource == null) return;
		climbingAudioSource.Stop();
		climbingAudioSource = null;
	}
	
	private IEnumerator ClimbingSounds ()
	{		
		while (true)
		{
			hasClimbAudio = true;
			
			if (climbingAudioSource == null)
			{
				climbingAudioSource = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			}
			
			climbingSound.Play(climbingAudioSource);
			yield return new WaitForSeconds(stepSpeed);
		}
	}
}