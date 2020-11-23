using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private AnimationScript anim;
    public bool isAttacking;
    public float attackRate = 2f;
    float nextAttackTime = 0;

    [Header ("Side Attack Data")]
    public Transform sideAttackPoint;
    public float sideAttackRange = .5f;
    public int sideAttackDamage = 50;
    public float sideAttackForce = 10000f;

    public LayerMask enemyLayer;

    void Start()
    {
        anim = GetComponentInChildren<AnimationScript>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
		{
            if (Input.GetButtonDown("Attack"))
            {
                HandleAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

	private void HandleAttack()
	{
        anim.SetTrigger("attack");

        //Transfrom this into a capsule or a box later.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sideAttackPoint.position, sideAttackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
		{
            enemy.GetComponent<Enemy>().TakeDamage(sideAttackDamage);

            //Fun but meh.
            //enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - enemy.transform.position).normalized * sideAttackForce *-1);
		}

	}

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sideAttackPoint.position, sideAttackRange);
	}
}
