using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Health Data")]
    public int maxHealth;
    [SerializeField]
    int currentHealth;

    public LayerMask deadLayer;
    Animator anim;

    protected void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
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

    protected void Die()
	{
        //Die Animation && fx
        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Dead Layer");
        this.enabled = false;
	}
}
