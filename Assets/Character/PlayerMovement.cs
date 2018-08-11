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
            moveDirection.y = doubleJumpSpeed;
            bumpUp = false;
        }

        //// Jump : Double Jump
        if (canDoubleJump && Input.GetButtonDown("Jump")) // ?? not sure this even works?
        {
            if (canDoubleJump)
            {
                moveDirection.y = doubleJumpSpeed;
                canDoubleJump = false;
            }
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
            isJumping = false;
        
            //// Jump
            if (jumpAvailable && Input.GetAxisRaw("Jump") != 0)
            {
                //moveDirection.y = 0;
                isClimbing = false;
                moveDirection.y = jumpSpeed;
                canDoubleJump = true;
                isJumping = true;
                jumpAvailable = false;
            }
            if (Input.GetAxisRaw("Jump") == 0)
            {
                jumpAvailable = true;
            }
        }
        
        //// Jump : Hold for full height
        if (Input.GetButtonUp("Jump"))
        {
            if (moveDirection.y > 0)
            {
                moveDirection.y = moveDirection.y * 0.5f;
            }
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