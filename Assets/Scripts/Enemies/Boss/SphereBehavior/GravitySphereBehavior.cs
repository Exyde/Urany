using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySphereBehavior : MonoBehaviour
{
    public Transform player;
    public float speed = 10f;
    public float timeUntilAttack = .3f;
    public int sphereDamage = 1;

    public LayerMask collisionLayer;
    bool preFire = false;
    bool fire = false;
    Vector3 targetPos;
    Vector3 dir;
    Rigidbody2D rb;

    Vector3 preFireTargetPos;

    void Start()
    {
        //Base
        player = FindObjectOfType<Movement>().transform;
        rb = GetComponent<Rigidbody2D>();

        targetPos = new Vector3(transform.position.x, transform.position.y - 10f, 0);

        preFireTargetPos = new Vector3(transform.position.x, transform.position.y + .5f, 0);

        preFire = true;
    }

    void Update()
    {
        if (preFire)
		{
            transform.position = Vector3.Lerp(transform.position, preFireTargetPos, Time.deltaTime * speed);
		}

        if (fire)
		{
            //MoveToPoint();
            //MoveToPlayer();
            MoveToTarget();
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uranie")
		{
            print("Inside Uranie");
		}

        else if (collision.gameObject.tag == "Spawner")
		{
            return;
		}

        else if (collision.gameObject.tag == "Attack Sphere")
		{
            print("attackSphere");
		}

        else if (collision.gameObject.tag == "Player")
        {
            //player.GetComponent<Health>().LooseLife(sphereDamage);
            Destroy(this.gameObject);
        } else
		{
            print("else destroy");
            CameraShake.Shake(.1f, .1f);
            Destroy(this.gameObject);
		}


    }

    public void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }

    public void MoveToGround()
	{
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);

    }

    public void SetFire()
    {
        preFire = false;
        fire = true;
    }


    public void SetTargetWithOffset(float offset)
    {
        targetPos = player.position;
        dir = (targetPos - transform.position).normalized * offset;
        targetPos += dir;
    }
}
