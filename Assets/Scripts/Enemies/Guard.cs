using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{
	[Header ("Attack Data")]
	public float hitForce;
	public int hurtDamage = 1;
	public float attackDelay = .5f;
	public bool hacked = false;

	[Header ("Detection Data")]
	public Transform detectionPoint;
	public float detectionRadius = .3f;
	public LayerMask playerLayer;


	Patrol patrol;

	override protected void Start()
	{
		base.Start();
		patrol = GetComponent<Patrol>();
	}

	private void Update()
	{
		if (isAlive)
		{
			detectionPoint.localPosition= new Vector3(patrol.dir / 2f, detectionPoint.localPosition.y, detectionPoint.localPosition.z);
		}

		if (state == State.Patrol && !hacked)
		{
			if (detectionPoint != null)
			{
				Collider2D player = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, playerLayer);

				if (player != null && player.GetComponent<Health>().alive)
				{
					StartCoroutine(Attack(player.transform));
				}
			}

		}

		if (isMoving)
		{
			anim.SetBool("isMoving", true);
		} else
		{
			anim.SetBool("isMoving", false);

		}
	}

	public void SetIdle()
	{
		anim.SetBool("isMoving", false);
		isMoving = false;
	}

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);
		StopAllCoroutines();
		patrol.isPaused = false;
		//state = State.Patrol;
		isAttacking = false;

		StartCoroutine(BackToPatrol(.4f));
	}

	IEnumerator BackToPatrol(float delay)
	{
		yield return new WaitForSeconds(delay);
		state = State.Patrol;
	}
	IEnumerator Attack(Transform player)
	{
		anim.SetTrigger("attack");
		patrol.isPaused = true;
		state = State.Attack;
		isAttacking = true;
		AudioManager.instance.PnjAttack();

		yield return new WaitForSeconds(attackDelay);

		patrol.isPaused = false;
		state = State.Patrol;
		isAttacking = false;      
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			print("Hit");
		}
	}

	private void OnDrawGizmos()
	{
		if (detectionPoint != null)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
		}
	}

	public void SetHacked()
	{
		hacked = true;
	}
}
