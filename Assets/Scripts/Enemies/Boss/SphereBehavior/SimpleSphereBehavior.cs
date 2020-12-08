using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSphereBehavior : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float timeUntilFire = .4f;
    public int sphereDamage = 1;

    public bool fire = false;
    public bool preFire = false;

    Vector3 targetPos;
    Vector3 dir;

    void Start()
    {
        player = FindObjectOfType<Movement>().transform;

        SetTargetWithOffset(3);
        CameraShake.Shake(.05f, .05f);
    }

    void Update()
    {
        timeUntilFire -= Time.deltaTime;

        //SetTargetWithOffset(3);

        if (timeUntilFire <= 0)
		{
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
            CameraShake.Shake(.1f, .1f);
            Destroy(this.gameObject);

        }
        else
        {
            print("else destroy");
            CameraShake.Shake(.1f, .1f);
            Destroy(this.gameObject);
        }
    }

    public void SetTargetWithOffset(float  offset)
	{
        targetPos = player.position;
        dir = (targetPos - transform.position).normalized * offset;
        targetPos += dir;
    }

    public void MoveToTarget()
	{
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveToPlayer()
	{
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }
}
