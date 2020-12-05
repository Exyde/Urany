﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Health Data")]
    public int maxHealth;
    [SerializeField]
    int currentHealth;

    [Header ("Booleans")]
    public bool isAlive;
    public bool isMoving;
    public bool isAttacking;

    public State state;

    public enum State
    {
        Idle,
        Patrol,
        Attack,
        Hurt,
        Scan,
        Dead
    }

    public LayerMask deadLayer;
    public Animator anim;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        state = State.Patrol;
        isAlive = true;
    }

    public virtual void TakeDamage(int amount)
	{
        currentHealth -= amount;

        //Play hurt animation & fx
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
		{
            Die();
		}
	}

    protected void Die()
	{
        //Die Animation && fx
        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Dead Layer");

        //States
        state = State.Dead;
        isAlive = false;
        isMoving = false;
        isAttacking = false;

        //this.enabled = false;
	}
}
