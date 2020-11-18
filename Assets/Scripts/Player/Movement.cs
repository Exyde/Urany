﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collision coll;
    private AnimationScript anim;

    [Space]
    [Header ("Stats")]
    public float speed = 10;
    public float jumpForce = 5;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space] 
    private bool groundTouch;
    private bool hasDashed;
    //Facing side
    public int side = 1;

    [Space]
    [Header("Particle System")]
    public ParticleSystem dashPS;
    public ParticleSystem jumpPS;
    public ParticleSystem wallJumpPS;
    public ParticleSystem slidePS;
    public ParticleSystem groundImpactPS;


    #region Animation Names

    public const string IDLE = "idle";
    public const string WALK = "walk";
    public const string JUMP = "jump";
    public const string FALL = "fall";
    public const string WALL_SLIDE = "wall_slide";
    public const string WALL_GRAB = "wall_grab";
    public const string DASH = "dash";

    #endregion

    [HideInInspector]
    public Vector2 inputs;
    Vector2 dir;
    float x, y, xRaw, yRaw;
    float grab;

    [Header ("PostProcessing")]
    public GameObject PostProcessing;
    public Volume volume;
    Bloom bloom;
    ChromaticAberration chrom;

    void Awake()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        PostProcessing = GameObject.FindGameObjectWithTag("PostProcessing");
        volume = PostProcessing.GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chrom);

    }

    void Update()
    {
        //Get Input
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        dir = new Vector2(x, y);
        inputs = new Vector2(xRaw, yRaw);
        grab = Input.GetAxis("Trigger"); // -1 = Left Trigger / 1 = Right Trigger 
        
        Walk(dir);
        HandleWalls();
        HandleJump();
        HandleDash();
        HandleAnim();
    }

	#region Handlers
	void HandleDash()
	{
        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
            {
                Dash(xRaw, yRaw);
            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }
    void HandleAnim()
	{
        //Sprite Facing
        if (wallGrab || wallSlide || !canMove)
            return;

        if (x > 0)
        {
            side = 1;
            anim.Flip(side);

        }

        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }

        if ((x > 0 || x < 0) && coll.onGround)
        {
            anim.SetAnimationState(WALK);
            return;
        }

        if (x == 0 && coll.onGround)
        {
            anim.SetAnimationState(IDLE);
            return;
        }
    }
    void HandleJump()
	{
        if (Input.GetButtonDown("Jump"))
        {
           // anim.SetTrigger("jump");

            if (coll.onGround)
            {
                Jump(Vector2.up, false);
                anim.SetAnimationState(JUMP);

            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
                anim.SetAnimationState(JUMP);
                
            }
        }
    }
    void HandleWalls()
	{

        //WallGrab - ButtonHold
        if (coll.onWall && grab != 0 && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);

            wallGrab = true;
            wallSlide = false;
        }


        /////////////////////////////////////////////////////////////////////
        // Wall Actions 

        //WallGrab - ButtonRelease
        if (grab == 0 || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        //Grounded ?
        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if (wallGrab && !isDashing)
        {
            WallGrab();
        }
        else
        {
            rb.gravityScale = 3f;
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
                anim.SetAnimationState(WALL_SLIDE);
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }
    }

	#endregion

	private void Walk(Vector2 dir)
    {
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpPS : jumpPS;
   
        //Can adjust the force if wallAttached
        rb.velocity = new Vector2(rb.velocity.x, 0);

        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    private void WallJump()
	{
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
		{
            side *= -1;
            anim.Flip(side);
		}

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);
        wallJumped = true;
    }

    private void WallSlide()
	{
        if(coll.wallSide != side)
		{
            anim.Flip(side * -1);
		}

        if (!canMove)
            return;

        bool pushingWall = false;

        if((rb.velocity.x > 0 && coll.onRightWall)|| (rb.velocity.x < 0 && coll.onLeftWall))
		{
            pushingWall = true;
		}

        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(0, -slideSpeed);

    }

    private void WallGrab()
	{
        rb.gravityScale = 0;

        if (x > .2f || x < -.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        float speedModifier = y > 0 ? .5f : 1;

        rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        anim.SetAnimationState(WALL_GRAB);
    }

    private void Dash(float x, float y)
	{
        //Todo : Camera  Shaking

        hasDashed = true;

        //Animations
        anim.SetTrigger("dash");
        anim.SetAnimationState(JUMP);

        //Velocity
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        rb.velocity += dir.normalized * dashSpeed;

        //Follow Up
        StartCoroutine(DashWait());
	}

    IEnumerator DashWait()
	{
    
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashPS.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;
        //PostProcessing.gameObject.SetActive(true);
        chrom.active = true;

        yield return new WaitForSeconds(.3f);

        dashPS.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
        // PostProcessing.gameObject.SetActive(false);
        chrom.active = false;
        
        


        //Cheat mode
        //hasDashed = false;

    }

    IEnumerator GroundDash()
	{
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
	}

    IEnumerator DisableMovement(float time)
	{
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
	}

    void RigidbodyDrag(float x)
	{
        //Function used in DashWait by DotWeen to animate a float.
        rb.drag = x;
	}

    void GroundTouch()
	{
        hasDashed = false;
        isDashing = false;
        side = anim.sr.flipX ? -1 : 1;

        groundImpactPS.Play();
	}
}
