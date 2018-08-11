using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PlayerMovement : CharacterMovement 
{	
    [Header("Player Movement Variables")]
    public bool isClimbing;
    public bool bumpUp;
    
    [SerializeField] private bool ignoreOneWays;
    [SerializeField] private float lastY;
    [SerializeField] private float currentY;

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
        if (bumpUp)
        {
            //moveDirection.y = doubleJumpSpeed;
            bumpUp = false;
        }

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
        }
        if (ignoreOneWays) GetComponent<CharacterController2D>().ignoreOneWayPlatformsThisFrame = true;

        //// Gravity Conditional Checks
        stopGravity = StopGravityConditions();

        //// Grounded
        if (isGrounded)
        {
            moveDirection.y = 0;      
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
        if (Physics2D.CircleCast((Vector2)transform.position + Vector2.down * 0.55f, 0.1f, Vector3.forward, 0f, llayermask)) return true;
        return false;
    }
}