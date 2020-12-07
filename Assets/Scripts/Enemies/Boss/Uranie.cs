using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranie : MonoBehaviour
{
    [Header ("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    [Header("Movement")]
    public float speed;
    public float waitTime;
    public Transform randomPositions;
    [SerializeField] Vector3 randomPos;

    [Header ("Attack")]
    private Attack1 attack1;
    private Attack2 attack2;
    private Attack3 attack3;

    [Header ("Attack 1")]

    public GameObject sphereAttackPrefab;
    public float sphereSpeed;
    public int sphereNumber;
    public float attackDuration;
    public float spawnRadius = 3f;

    [Space]
    [Header("Booleans")]
    public bool isAttacking;
    public bool isMoving;
    public bool isWaiting;


    //Moving from phase to update
    /* Update movespeed, attack damage, rate, range, intensy...
     * Change background & sprite
     */

    public enum State
	{
        Move,
        Attack,
        Wait
	}

    public State state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        attack1 = GetComponent<Attack1>();
        attack2 = GetComponent<Attack2>();
        attack3 = GetComponent<Attack3>();

        state = State.Move;

        randomPos = GetRandomPos();
    }

    void Update()
	{
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

    public void HandleAttack()
	{
        if (!isAttacking)
		{
            print("Yo");
            StartCoroutine(Attack1());
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
        print("Waiting !");
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        state = State.Attack;
        isWaiting = false;
	}

    IEnumerator Attack1()
	{
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();

        for (int i = 0; i < sphereNumber; i++)
		{
            Vector2 spherePos = (Vector2) transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject sphere = Instantiate(sphereAttackPrefab, spherePos, Quaternion.identity);

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
		}


        yield return new WaitForSeconds(.2f);
        //Empty currentSphere

        for (int i = 0; i < sphereNumber; i++)
        {
            Destroy(currentSpheres[i].gameObject);
        }

        EndAttack();
    }

    public void BeginAttack()
	{
        print("Attack ! ");
        isAttacking = true;
        isMoving = false;
        isWaiting = false;
    }

    public void EndAttack()
	{
        state = State.Move;
        isAttacking = false;
        isMoving = true;
    }

	private void OnDrawGizmos()
	{
        //Attack Range
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, spawnRadius);
	}
}
