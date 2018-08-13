using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	public GameObject character;
	Animator animator;
	private CharacterMovement characterMovement;

	public AudioEvent footstep;
	public float stepSpeed;
	public AudioSource sm;
	public bool walking;
	private IEnumerator walkingSounds;

	public float previousPosition;
	[SerializeField] private bool hasFootsteps;
	private PlayerMovement playerMovement;
	
	public AudioEvent climbingSound;
	[SerializeField] private bool hasClimbingSound;
	private IEnumerator climbingSounds;
	private AudioSource sm2;



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
		//float yAxis = Input.GetAxis("Vertical");
		ChangeDirection(characterMovement.GetMoveDirection.x);

		animator.SetFloat("Forward", Mathf.Abs(characterMovement.GetMoveDirection.x));
		animator.SetBool("IsClimbing", ClimbCheck());
		//anim.SetFloat("Looking", yAxis);

		if (walking && (characterMovement.isGrounded || playerMovement.isClimbing))
		{
			StartWalking();
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

		previousPosition = transform.position.x;
	}

	private void LateUpdate()
	{
		walking = previousPosition != transform.position.x;
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
		if (hasFootsteps) return;
		hasFootsteps = true;
		StartCoroutine(walkingSounds);
	}

	private void StopWalking ()
	{
		StopCoroutine(walkingSounds);
		
		if (sm != null)
		{
			sm.Stop();
			sm = null;
		}
		
		hasFootsteps = false;
	}

	private IEnumerator WalkingSounds ()
	{		
		while (true)
		{
			hasFootsteps = true;
			
			if (sm == null)
			{
				sm = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			}
			
			footstep.Play(sm);
			yield return new WaitForSeconds(stepSpeed);
		}
	}
	
	private void StartClimbing ()
	{
		if (hasClimbingSound) return;
		hasClimbingSound = true;
		StartCoroutine(climbingSounds);
	}

	private void StopClimbing ()
	{
		StopCoroutine(climbingSounds);
		
		if (sm2 != null)
		{
			sm2.Stop();
			sm2 = null;
		}
		
		hasClimbingSound = false;
	}
	
	private IEnumerator ClimbingSounds ()
	{		
		while (true)
		{
			hasClimbingSound = true;
			
			if (sm2 == null)
			{
				sm2 = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			}
			
			climbingSound.Play(sm2);
			yield return new WaitForSeconds(stepSpeed);
		}
	}
}