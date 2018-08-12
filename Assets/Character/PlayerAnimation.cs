using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	public GameObject character;
	//Animator animator;
	private CharacterMovement characterMovement;

	public AudioEvent footstep;
	public float stepSpeed;
	public AudioSource sm;
	public bool walking;
	private IEnumerator walkingSounds;

	public float previousPosition;
	[SerializeField] private bool hasFootsteps;

	private void Awake()
	{
		characterMovement = GetComponent<CharacterMovement>();
		//animator = GetComponentInChildren<Animator>();
		walkingSounds = WalkingSounds();      
	}

	private void Update ()
	{        
		//float yAxis = Input.GetAxis("Vertical");
		ChangeDirection(characterMovement.GetMoveDirection.x);

		// animator.SetFloat("Forward", Mathf.Abs(characterMovement.GetMoveDirection.x));
		//anim.SetFloat("Looking", yAxis);

		if (walking && characterMovement.isGrounded)
		{
			StartWalking();
		}
		else
		{
			StopWalking();
		}

		previousPosition = transform.position.x;
	}

	private void LateUpdate()
	{
		walking = previousPosition != transform.position.x;
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
}
