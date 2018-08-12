using UnityEngine;
using Prime31;
using Debug = UnityEngine.Debug;

public class PlayerMovement : CharacterMovement 
{	
    [Header("Player Movement DeBug")]
    public bool isClimbing;
    public bool isUnderWater;

    [SerializeField] private bool ignoreOneWays;
    [SerializeField] private float waterPenaltyX;
    [SerializeField] private float waterPenaltyY;
    
    protected override void Update() 
    {
        climbAxis = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.x *= speed;
        MoveCharacter();
        base.Update();
	}

    private void MoveCharacter()
    {
        //// Ladder Climb
        if (IntersectsWithLadder(1 << LayerMask.NameToLayer("Ladder")))
        {
            isClimbing = true;
            LadderClimbing();
        }
        else
        {
            ignoreOneWays = false;
            isClimbing = false;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }

        if (ignoreOneWays)
        {
            GetComponent<CharacterController2D>().ignoreOneWayPlatformsThisFrame = true;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
        }

        //// Gravity Conditional Checks
        stopGravity = StopGravityConditions();

        //// Grounded
        if (isGrounded)
        {
            moveDirection.y = 0; 
        }

        if (!isGrounded && !isClimbing)
        {
            moveDirection.x = 0;
        }

        //// Underwater
        if (isUnderWater)
        {
            isGrounded = false;
            stopGravity = true;
            moveDirection.y = climbAxis * (speed / waterPenaltyY);
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.x *= speed / waterPenaltyX;
            moveDirection.y += gravity / 1.5f * Time.deltaTime;
        }
    }

    private bool StopGravityConditions()
    {
        return isClimbing;
    }

    private void LadderClimbing()
    {
        if (climbAxis != 0) ignoreOneWays = true;
        moveDirection.y = climbAxis * (speed / 2f);
    }

    private bool IntersectsWithLadder(LayerMask llayermask)
    {
        if (Physics2D.CircleCast((Vector2)transform.position + Vector2.down * 0.55f, 0.1f, Vector3.forward, 0f, llayermask)) return true;
        //if (Physics2D.CircleCast((Vector2)transform.position + Vector2.down * 0.55f, 0.1f, Vector3.forward, 0f, llayermask)) return true;
        return false;
    }
}