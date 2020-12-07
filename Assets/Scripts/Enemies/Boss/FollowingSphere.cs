﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSphere : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float timeUntilAttack = .3f;
    public int sphereDamage = 1;

    public LayerMask collisionLayer;
    public bool phase2 = true;

    Vector3 targetPos;
    Vector3 dir;
    Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<Movement>().transform;
        rb = GetComponent<Rigidbody2D>();

        targetPos = player.position;

        dir = (targetPos - transform.position).normalized * 3;
        targetPos += dir;

    }

    void Update()
    {
        timeUntilAttack -= Time.deltaTime;

        if (timeUntilAttack <= 0)
		{
            if (phase2 == false )
                MoveToPoint();
            if (phase2)
                MoveToPlayer();
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == collisionLayer)
		{
            Destroy(this.gameObject);
        }

        if (collision.tag == player.tag)
		{
            //player.GetComponent<Health>().LooseLife(sphereDamage);
            Destroy(this.gameObject);
		}


    }

    public void MoveToPoint()
	{
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveToPlayer()
	{
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }
}
