﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Health Data")]
    public int maxHealth;
    [SerializeField]
    int currentHealth;

    public Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int amount)
	{
        currentHealth -= amount;

        //Play hurt animation & fx
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
		{
            Die();
		}
	}

    void Die()
	{
        //Die Animation && fx
        anim.SetBool("isDead", true);


        //Disable Enemy - Stock the corpse
        this.enabled = false;
	}
}
