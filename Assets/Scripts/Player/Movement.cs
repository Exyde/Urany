﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class Movement : MonoBehaviour
{
	#region Fields
    private Rigidbody2D rb;
    private Collision coll;
    private AnimationScript anim;
    private Animator animator;
    public PostProcessController pp;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 5;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float ascentClimbSpeed;
    public float downClimbSpeed;

    private float wallClimbOffsetX = .75f;
    private float wallClimbOffsetY = .75f;

    [Space]

    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space] 
    [Header ("Debug / Cheating")]
    public bool infiniteDash = true;
    public AudioSource SoundStep;
    private bool stepPlaying = false;

    [Space]
    [Header ("Other bools")]
    [SerializeField]
    private bool groundTouch;
    [SerializeField]
    private bool hasDashed;

    [Space]
    public int side = 1;
    public Transform interactionPoint;
    public Transform attackPoints;

    [Space]
    [Header("Particle System")]
    public ParticleSystem dashPS;
    public ParticleSystem jumpPS;
    public ParticleSystem wallJumpPS;
    public ParticleSystem slidePS;
    public ParticleSystem groundImpactPS;

    [Space]
    [Header("Juice Elements Prefabs")]
    public GameObject jumpSmoke;
    public GameObject wallJumpSmoke;
    public GameObject groundImpactSmoke;
    public GameObject wallSlideSmoke;
    public GameObject wallSlideSmokeInstance;

    //Inputs
    [HideInInspector]
    public Vector2 inputs;
    Vector2 dir;
    float x, y, xRaw, yRaw;

    float grabInput;
    float dashInput;
    bool dashKeyboardInput = false;
    bool isWallSmokeInstantiated = false;

	#endregion

	void Awake()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        animator = GetComponentInChildren<Animator>();
        
    }

    void Start()
	{
        pp = FindObjectOfType<PostProcessController>();
	}
    void Update()
    {
        HandleInputs();
        Walk(inputs);
        HandleWalls();
        HandleJump();
        HandleDash();
        HandleAnim();

        if (stepPlaying)
		{
            if ((!coll.onGround || dir.x == 0) && !wallGrab)
			{
                SoundStep.Stop();
                stepPlaying = false;
			}

            
		}
    }

	#region Handlers

    void HandleInputs()
	{
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        dir = new Vector2(x, y);
        grabInput = Input.GetAxisRaw("Grab");
        dashInput = Input.GetAxisRaw("Dash");
        

        MapRaw();
        //MapX();

        inputs = new Vector2(xRaw, yRaw);

    }

    public void MapRaw()
	{
        if (xRaw > 0) xRaw = 1;
        if (xRaw < 0) xRaw = -1f;

        if (yRaw > 0) yRaw = 1;
        if (yRaw < 0) yRaw = -1f;

        //print(inputs);
    }

    IEnumerator ClimbWallTop()
	{
        int direction = coll.wallSide;
        canMove = false;

        rb.MovePosition(transform.position + new Vector3( -direction * wallClimbOffsetX, wallClimbOffsetY, 0));

        yield return new WaitForSeconds(.1f);

        //rb.MovePosition(transform.position + new Vector3( -direction * wallClimbOffsetX, .1f, 0));

        canMove = true;
	}

    void HandleWalls() {

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            dashKeyboardInput = true;
        } else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            dashKeyboardInput = false;
        }

        //WallGrab - ButtonHold
        //if (coll.onWall && Input.GetButtonDown("Fire 3") && canMove)
        if (coll.onWall && (grabInput == 1 || dashKeyboardInput) && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);

            wallGrab = true;
            wallSlide = false;
        }

        //WallGrab - ButtonRelease
        //if (Input.GetButtonUp("Fire 3") || !coll.onWall || !canMove)
        if ((grabInput == 0 && !dashKeyboardInput) || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        // arrive en haut du mur
        if (coll.onWall && wallGrab && !coll.onWallTop)
        {
            StopCoroutine(ClimbWallTop());
            StartCoroutine(ClimbWallTop());
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

        if (coll.onWall && !coll.onGround && (coll.onRightWall || coll.onLeftWall))
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround || (!coll.onRightWall && !coll.onLeftWall))
        {
            wallSlide = false;
            if(isWallSmokeInstantiated) {
            isWallSmokeInstantiated = false;
            wallSlideSmokeInstance = null;
        }
        }
    }
    void HandleJump()
	{
        if (Input.GetButtonDown("Jump") && canMove)
        {

            if (coll.onGround)
            {
                Jump(Vector2.up, false);

            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }
    }
	void HandleDash()
	{
        if ((Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.D) || dashInput == 1) && !hasDashed && !isDashing && canMove)
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

        //Update the side of the interraction.
        interactionPoint.localPosition = new Vector3(side * .5f, 0, 0);
        attackPoints.localScale = new Vector3(side , 1, 1);

    }

	#endregion

	private void Walk(Vector2 movement)
    {
        anim.SetHorizontalMovement((int)xRaw, y, rb.velocity.y);


        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (movement.x != 0 && coll.onGround && !stepPlaying)
        {
            SoundStep.Play();
            stepPlaying = true;
        }

        if (!wallJumped)
        {
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }
    private void Jump(Vector2 dir, bool wall)
    {

        //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        //ParticleSystem particle = wall ? wallJumpPS : jumpPS;
        //particle.transform.localScale = new Vector3(side, 1, 1);

        anim.SetTrigger("jump");
        AudioManager.instance.Jump();

        //Can adjust the force if wallAttached
        rb.velocity = new Vector2(rb.velocity.x, 0);

        rb.velocity += dir * jumpForce;

        //particle.Play();

        if(!wall) {

            float yTempDecalage = (int)Math.Round(transform.position.y) - transform.position.y;
            Vector3 temp;
            if(yTempDecalage > 0.25f) {
                temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y) - 0.5f, transform.position.z);
            } else if((yTempDecalage <= 0.25f && yTempDecalage >= 0) || (yTempDecalage >= -0.25f && yTempDecalage <= 0)) {
                temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y), transform.position.z);
            } else {
                temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y) + 0.5f, transform.position.z);
            }

            GameObject smoke = Instantiate(jumpSmoke, temp, Quaternion.identity);
            smoke.transform.localScale = new Vector3(side, 1, 1); 
        } else {
            // This is a dirty temp fix for the landing bug (which makes the landings coordinates sometimes under the ground)
            Vector3 temp = new Vector3((int)Math.Round(transform.position.x), transform.position.y, transform.position.z);
            float height = GetComponentInChildren<SpriteRenderer>().sprite.rect.y;

            //temp = new Vector3(transform.position.x, transform.position.y + height / 2, 0);


            GameObject smoke = Instantiate(wallJumpSmoke, temp, Quaternion.identity);
            smoke.transform.localScale = new Vector3(side, 1, 1);
        }     
    }

    private void WallJump()
	{

        anim.SetTrigger("jump");

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

        if (coll.wallSide != side)
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

        //Push Allow moving out of the flow while sliding
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
        //slidePS.Play();

        if(wallSlide && !isWallSmokeInstantiated) {
            wallSlideSmokeInstance = Instantiate(wallSlideSmoke, transform.position, Quaternion.identity);
            wallSlideSmokeInstance.transform.localScale = new Vector3(side, 1, 1);
            isWallSmokeInstantiated = true;
        }

        if(wallSlideSmokeInstance != null) {
            
            wallSlideSmokeInstance.transform.position = new Vector3(wallSlideSmokeInstance.transform.position.x, transform.position.y, wallSlideSmokeInstance.transform.position.z);
        }

    }

    private void WallGrab()
	{
        rb.gravityScale = 0;

        if (x > .2f || x < -.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (!stepPlaying && yRaw != 0)
        {
            SoundStep.Play();
            stepPlaying = true;
        }

        if (yRaw == 0)
		{
            SoundStep.Stop();
            stepPlaying = false;
		}

        //float speedModifier = y > 0 ? .5f : 1;
        float _speed = yRaw > 0 ? ascentClimbSpeed : downClimbSpeed;

        //rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        rb.velocity = new Vector2(rb.velocity.x, yRaw * _speed);

    }

    private void Dash(float x, float y)
	{
        //Not working right now
        //CameraShake.Shake(.15f, .0001f);

        hasDashed = true; 
        anim.SetTrigger("dash");
        AudioManager.instance.Dash();

        //Velocity
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        rb.gravityScale = 0;

        rb.velocity += dir.normalized * dashSpeed;

        // Dash TP
        //rb.velocity = new Vector2(xRaw * dashSpeed * 2, yRaw * dashSpeed);

        //Follow Up
        StartCoroutine(DashWait());
	}

    IEnumerator DashWait()
	{
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        //dashPS.Play();
        
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        pp.SetDashPostProcess();
        //anim.SetTrigger("dashBegin");
        isDashing = true;

        yield return new WaitForSeconds(.2f);
        
        //anim.SetTrigger("dashEnd");
        //yield return new WaitForSeconds(.2f);
        /// animator.ResetTrigger("dashEnd");
        
        //dashPS.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
        pp.ResetPostProcess();
    }

    IEnumerator GroundDash()
	{
        yield return new WaitForSeconds(2f);

        if (coll.onGround)
            hasDashed = false;
	}

    IEnumerator DisableMovement(float time)
	{
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
	}

    IEnumerator DisableMovementOnHit(float time)
    {
        canMove = false;
        Time.timeScale = .5f;
        pp.SetHitPostProcess();
        yield return new WaitForSeconds(time);
        canMove = true;
        Time.timeScale = 1f;
        pp.ResetPostProcess();

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
        //groundImpactPS.Play();

        AudioManager.instance.Land();

        // This is a dirty temp fix for the landing bug (which makes the landings coordinates sometimes under the ground)
        float height = GetComponentInChildren<SpriteRenderer>().sprite.rect.y;

        float yTempDecalage = (int)Math.Round(transform.position.y) - transform.position.y;
        Vector3 temp;
        if(yTempDecalage > 0.25f) {
            temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y) - 0.5f, transform.position.z);
        } else if((yTempDecalage <= 0.25f && yTempDecalage >= 0) || (yTempDecalage >= -0.25f && yTempDecalage <= 0)) {
            temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y), transform.position.z);
        } else {
            temp = new Vector3(transform.position.x, (int)Math.Round(transform.position.y) + 0.5f, transform.position.z);
        }

        

        //temp = new Vector3(transform.position.x, transform.position.y + height / 2, 0);
        GameObject smoke = Instantiate(groundImpactSmoke, temp, Quaternion.identity);
        //GameObject smoke = Instantiate(groundImpactSmoke, transform.position, Quaternion.identity);

        smoke.transform.localScale = new Vector3(side, 1, 1);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.tag == "Savant")
		{
            HitSavant(other);
		}

        if (other.gameObject.tag == "Guard")
		{
            HitGuard(other);
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Mnemo")
		{
            FindObjectOfType<CameraFollow>().Dezoom();
            //Add Lerp
		}

        if (other.gameObject.tag == "Attack Sphere")
        {
            StartCoroutine(DisableMovementOnHit(.12f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.tag == "Mnemo")
        {
            FindObjectOfType<CameraFollow>().FocusPlayer();
            //Add Lerp
        }
    }
    public void HitGuard(Collision2D other)
	{
        Guard guard = other.gameObject.GetComponent<Guard>();
        Vector3 direction = (guard.transform.position - transform.position);

        StartCoroutine(DisableMovementOnHit(.12f));
        anim.SetTrigger("hurt");
        AudioManager.instance.PlayerHit();

        rb.velocity = Vector2.zero;
        direction.y = Mathf.Sign(direction.y) / 2;
        rb.AddForce(-direction * guard.hitForce, ForceMode2D.Impulse);

        GetComponent<Health>().LooseLife(guard.hurtDamage);
    }

    public void HitSavant(Collision2D other)
	{
        return;
	}

	#region Debug

    void dbgInput()
	{
        print("X :" + x + " / Y : " + y);
    }

	#endregion
}
