using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Character Movement Settings")]
    public float speed = 12.0f;
    public float gravity = 36.0f;

    [Header("Character Movement Variables")]
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float climbAxis;
    [SerializeField] protected bool stopGravity;
    protected bool isAbove;
    protected bool isRight;
    protected bool isLeft;
    protected Vector3 moveDirection = Vector3.zero;
    
    CharacterController2D cc2d;
    CharacterController2D.CharacterCollisionState2D flags;

    public Vector3 GetMoveDirection
    {
        get
        {
            return moveDirection;
        }
    }

    private void Awake ()
	{
        cc2d = GetComponent<CharacterController2D>();
    }

    protected virtual void Update ()
    {
        if (!stopGravity)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        cc2d.move(moveDirection * Time.deltaTime);

        // report what is around us
        flags = cc2d.collisionState;

        //// Check if we are on the ground
        isGrounded = flags.below;
        //isAbove = flags.above;
        isRight = flags.right;
        isLeft = flags.left;
    }
}