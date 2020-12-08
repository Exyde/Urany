using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSphereBehavior : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float timeUntilAttack = .3f;
    public int sphereDamage = 1;

    public LayerMask collisionLayer;
    public bool phase2 = true;
    public bool isShooted = false;

    Vector3 targetPos;
    Vector3 dir;
    Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<Movement>().transform;
        rb = GetComponent<Rigidbody2D>();

        SetTargetWithOffset(3.5f);
        CameraShake.Shake(.05f, .05f);
    }

    void Update()
    {
        if (isShooted)
		{
            MoveToPoint();
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == collisionLayer)
        {
            //Destroy(this.gameObject);
        }

        if (collision.tag == player.tag)
        {
            //player.GetComponent<Health>().LooseLife(sphereDamage);
            //Destroy(this.gameObject);
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

    public void SetFire()
	{
        //SetTargetWithOffset(3f);
        isShooted = true;
    }

    public void SetTargetWithOffset(float offset)
    {
        targetPos = player.position;
        dir = (targetPos - transform.position).normalized * offset;
        targetPos += dir;
    }
}
