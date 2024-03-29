﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;
    public SpriteRenderer scytheRendered;

    private string currentState;
	void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetHorizontalMovement(int xRaw, float y, float yVel)
    {

       // anim.SetInteger("HorizontalAxis", xRaw);   
        anim.SetFloat("HorizontalAxis", xRaw);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);
    }

    public void SetTrigger (string trigger)
	{
        anim.SetTrigger(trigger);
	}

    public void Flip(int side)
	{
        if (move.wallGrab || move.wallSlide)
		{
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
                return;
		}

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
        scytheRendered.flipX = state;
	}
}
