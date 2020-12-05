using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{
	[Header ("Attack Data")]
	public float hitForce;
	public int hurtDamage = 1;
	public float attackDelay = .2f;

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
		if (!isAlive)
		{
			patrol.Stop();
		}

		if (state == State.Patrol)
		{
			Collider2D player = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, playerLayer);

			if (player != null)
			{
				StartCoroutine(Attack(player.transform));
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

	IEnumerator Attack(Transform player)
	{
		anim.SetTrigger("attack");
		patrol.Stop();

		yield return new WaitForSeconds(attackDelay);

		patrol.StartPatrol();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
	}
}
