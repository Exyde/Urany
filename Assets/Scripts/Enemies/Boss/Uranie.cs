using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranie : MonoBehaviour
{
    [Header ("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;

    [Header("Movement")]
    public float speed;
    public float waitTime;
    public Transform randomPositions;
    [SerializeField] Vector3 randomPos;

    [Header ("Attack")]
    private Attack1 attack1;
    private Attack2 attack2;
    private Attack3 attack3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        attack1 = GetComponent<Attack1>();
        attack2 = GetComponent<Attack2>();
        attack3 = GetComponent<Attack3>();

        randomPos = GetRandomPos();
    }

    void Update()
	{
        transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);

        if (transform.position == randomPos)
		{
            randomPos = GetRandomPos();
		}

    }

    Vector3 GetRandomPos()
	{
        Vector3 oldPos = randomPos;
        randomPos = randomPositions.GetChild(Random.Range(0, randomPositions.childCount)).position;

        if (randomPos == oldPos)
		{
            randomPos = GetRandomPos();
            print("meh");
		}

        return randomPos;
    }

    IEnumerator Wait()
	{
        yield return new WaitForSeconds(waitTime);
        randomPos = GetRandomPos();
	}
}
