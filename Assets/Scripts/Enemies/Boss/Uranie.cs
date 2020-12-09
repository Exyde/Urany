using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranie : MonoBehaviour
{
    [Header ("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Transform player;
    private Transistor transistor;

    [Header ("Phases")]
    public int maxHealth;
    [SerializeField] int currentHealth;
    public int phase = 1;
    public int healthTresholdPhase2;
    public int healthTresholdPhase3;


    [Header("Movement")]
    public float speed;
    public float waitTime;
    public Transform randomPositions;
    Vector3 randomPos;

    [Header ("Attack")]
    private SimpleBall simpleBall;
    private MultipleBall multipleBall;
    private GravityBall gravityBall;

    [Space]
    [Header("Booleans")]
    public bool isAttacking;
    public bool isMoving;
    public bool isWaiting;

    public enum State
	{
        Move,
        Attack,
        Wait,
        Dead
	}

    public State state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transistor = GetComponent<Transistor>();

        gravityBall = GetComponent<GravityBall>();
        multipleBall = GetComponent<MultipleBall>();
        simpleBall = GetComponent<SimpleBall>();

        currentHealth = maxHealth;

        state = State.Move;

        randomPos = GetRandomPos();
        player = FindObjectOfType<Movement>().transform;

    }

    void Update()
	{
        LookPlayer();

        if (currentHealth < healthTresholdPhase2 && phase == 1)
		{
            SetPhase(2);
            print("To phase 2");
            phase = 2;
		}

        if (currentHealth < healthTresholdPhase3 && phase == 2)
		{
            SetPhase(3);
            print("To phase 3");
            phase = 3;
        }


        switch (state)
		{
            case State.Move:
                HandleMove();
                break;

            case State.Attack:
                HandleAttack();
                break;

            case State.Wait:
                HandleWait();
                break;

            default:
                break;
		}
    }

    #region Handlers

    public void HandleMove()
	{
        transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);
        isMoving = true;

        if (transform.position == randomPos)
        {
            randomPos = GetRandomPos();

            state = State.Wait;
            isMoving = false;
        }
    }

    public void LookPlayer()
	{
        Vector3 dir = (player.position - transform.position).normalized;
        float side = Mathf.Sign(dir.x);
        sr.flipX = side == 1 ? false : true;
    }

    public void HandleAttack()
	{
        if (!isAttacking)
		{
            isAttacking = true;
            int randAttack = Random.Range(1, 4);

            switch (randAttack)
			{
                case 1:
                    simpleBall.Attack();
                    break;
                case 2:
                    multipleBall.Attack();
                    break;
                case 3:
                    gravityBall.Attack();
                    break;
                default:
                    break;
			}
		}
	}

    public void HandleWait()
	{
        StartCoroutine(Wait());
	}

	#endregion

	Vector3 GetRandomPos()
	{
        Vector3 oldPos = randomPos;
        randomPos = randomPositions.GetChild(Random.Range(0, randomPositions.childCount)).position;

        if (randomPos == oldPos)
		{
            randomPos = GetRandomPos();
		}

        return randomPos;
    }

    IEnumerator Wait()
	{
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        state = State.Attack;
        isWaiting = false;
	}
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        //anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
	{
        state = State.Dead;
        StopAllCoroutines();
        print("dead");
    }

    void SetPhase(int phase)
	{
        switch (phase)
		{
            case 1:

                //Urany
                speed = 8f;
                waitTime = 1.2f;

                //Attacks
                simpleBall.SetPhase1();
                multipleBall.SetPhase1();
                gravityBall.SetPhase1();

                phase = 1;
                break;

            case 2:

                transistor.toPhase2();

                //Urany
                speed =  9f;
                waitTime = 1f;

                //Attacks
                simpleBall.SetPhase2();
                multipleBall.SetPhase2();
                gravityBall.SetPhase2();

                break;

            case 3:

                transistor.toPhase3();

                //Urany
                speed = 10f;
                waitTime = .8f;

                //Attacks
                simpleBall.SetPhase3();
                multipleBall.SetPhase3();
                gravityBall.SetPhase3();

                break;
        }
	}
}
