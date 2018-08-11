using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	public GameObject character;
	Animator animator;
	CharacterMovement characterMovement;

	public AudioEvent footstep;
	public float stepSpeed;
	AudioSource sm;
	public bool walking;
	IEnumerator walkingSounds;
        
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

		walking = Mathf.Abs(characterMovement.GetMoveDirection.x) > 0 ? true : false;
		if (walking)
		{
			StartWalking();
		}
		else
		{
			StopWalking();
		}
	}

	void ChangeDirection (float fdirection)
	{        
		/// Rotate Player
		if (fdirection > 0) character.transform.eulerAngles = new Vector3(0, 0, 0);
		if (fdirection < 0) character.transform.eulerAngles = new Vector3(0, 180, 0);
	}

	public void StartWalking ()
	{
		if (!sm)
		{
			sm = FindObjectOfType<SoundManager>().GetOpenAudioSource();
			StartCoroutine(walkingSounds);
		}
	}
    
	public void StopWalking ()
	{
		StopCoroutine(walkingSounds);
		sm = null;
	}

	IEnumerator WalkingSounds ()
	{		
		while (true)
		{
			footstep.Play(sm);
			yield return new WaitForSeconds(stepSpeed);
		}
	}
}
