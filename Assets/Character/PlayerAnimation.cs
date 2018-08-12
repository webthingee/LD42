using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	public GameObject character;
	Animator animator;
	CharacterMovement characterMovement;

	public AudioEvent footstep;
	public float stepSpeed;
	public AudioSource sm;
	public bool walking;
	IEnumerator walkingSounds;

	public float previousPosition;
	[SerializeField] private bool hasFootsteps;

	void Awake ()
	{
		characterMovement = GetComponent<CharacterMovement>();
		animator = GetComponentInChildren<Animator>();
		walkingSounds = WalkingSounds();      
	}

	void Update ()
	{        
		//float yAxis = Input.GetAxis("Vertical");
		ChangeDirection(characterMovement.GetMoveDirection.x);

		// animator.SetFloat("Forward", Mathf.Abs(characterMovement.GetMoveDirection.x));
		//anim.SetFloat("Looking", yAxis);

		//walking = Mathf.Abs(characterMovement.GetMoveDirection.x) > 0 ? true : false;
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
		/// Rotate Player
		if (fdirection > 0) character.transform.eulerAngles = new Vector3(0, 0, 0);
		if (fdirection < 0) character.transform.eulerAngles = new Vector3(0, 180, 0);
	}

	public void StartWalking ()
	{
		if (!hasFootsteps)
		{
			hasFootsteps = true;
			StartCoroutine(walkingSounds);
		}
		
	}
    
	public void StopWalking ()
	{
		if (sm != null) sm.Stop();
		StopCoroutine(walkingSounds);
		if (sm != null) sm = null;
		hasFootsteps = false;
	}

	IEnumerator WalkingSounds ()
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
