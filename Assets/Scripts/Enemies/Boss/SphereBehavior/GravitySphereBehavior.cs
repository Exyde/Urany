using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySphereBehavior : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float timeUntilAttack = .3f;
    public int sphereDamage = 1;

    public LayerMask collisionLayer;
    public bool phase2 = true;
    public bool actived = false;
    Vector3 targetPos;
    Vector3 dir;
    Rigidbody2D rb;


    void Start()
    {
        //Base
        player = FindObjectOfType<Movement>().transform;
        rb = GetComponent<Rigidbody2D>();


        //Specific
        targetPos = player.position;
        dir = (targetPos - transform.position).normalized * 3;
        targetPos += dir;

    }

    void Update()
    {
        if (actived)
		{
            //MoveToPoint();
            MoveToPlayer();
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

    public void Explode()
    {
         rb.isKinematic = false;
        //MoveToPlayer();
        //actived = true;
    }
}
