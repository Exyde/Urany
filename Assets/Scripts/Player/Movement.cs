using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collision coll;

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

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);


        //Walk
        Walk(dir);
        //anim.SetHorizontalMovement(x, y, rb.velocity.y);

        //WallGrab - ButtonHold
        if (coll.onWall && Input.GetButton("Fire3") && canMove)
		{
            if (side != coll.wallSide)
                print("flipSide"); //Add later

            wallGrab = true;
            wallSlide = false;
		}

        //WallGrab - ButtonRelease
        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
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

        if(wallGrab && !isDashing)
		{
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
			{
                rb.velocity = new Vector2(rb.velocity.x, 0);
			}

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
		} else
		{
            rb.gravityScale = 3f;
		}

        //Jumps
        if (Input.GetButtonDown("Jump"))
        {
            //anim.SetTrigger("jump");

            if (coll.onGround) {
                Jump(Vector2.up, false);
            }
            if (coll.onWall && !coll.onGround)
			{
                WallJump();
            }
        }

        if (coll.onWall && !coll.onGround)
		{
            wallSlide = true;
            WallSlide();
		}

        if (!coll.onWall || coll.onGround)
		{
            wallSlide = false;
		}

        if (x > 0)
        {
            side = 1;
            //anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
           //anim.Flip(side);
        }

    }

    private void Walk(Vector2 dir)
    {
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
        //ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        //Can adjust the force if wallAttached
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        //particle.Play();
    }

    private void WallJump()
	{
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
		{
            side *= -1;
            //anim.Flip(side);
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
            //anim.Flip(side * -1);
		}

        if (!canMove)
            return;

        bool pushingWall = false;

        if((rb.velocity.x > 0 && coll.onRightWall)|| (rb.velocity.x < 0 && coll.onLeftWall))
		{
            pushingWall = true;
		}

        float push = pushingWall ? 0 : rb.velocity.x;

        //rb.velocity = new Vector2(0, -slideSpeed);
        rb.velocity = new Vector2(push, -slideSpeed);

    }

    IEnumerator DisableMovement(float time)
	{
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
	}
}
