using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySphereBehavior : MonoBehaviour
{
    public Transform player;
    public float speed;
    public int sphereDamage = 1;

    bool preFire = false;
    bool fire = false;
    Vector3 targetPos;
    Vector3 dir;

    Vector3 preFireTargetPos;

    public GameObject explosionPrefab;

    void Start()
    {
        //Base
        player = FindObjectOfType<Movement>().transform;
        
        //Set target as Ground
        targetPos = new Vector3(transform.position.x, transform.position.y - 10f, 0);

        //Set prefire position above.
        preFireTargetPos = new Vector3(transform.position.x, transform.position.y + .5f, 0);

        preFire = true;
    }

    void Update()
    {
        if (preFire)
		{
            ChargeSphere();
		}

        if (fire)
		{
            MoveToTarget();
		}
    }

    public void InstantiateExplosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uranie")
		{
            //print("Inside Uranie");
		}

        else if (collision.gameObject.tag == "Spawner")
		{
            return;
		}

        else if (collision.gameObject.tag == "Attack Sphere")
		{
            //print("attackSphere");
		}

        else if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Health>().LooseLife(sphereDamage);
            CameraShake.Shake(.1f, .1f);
            InstantiateExplosion();
            AudioManager.instance.UranieAttackImpact();

            Destroy(this.gameObject);
        } else
		{
            //print("else destroy");
            CameraShake.Shake(.15f, .25f);
            InstantiateExplosion();
            AudioManager.instance.UranieAttackImpact();


            Destroy(this.gameObject);
		}
    }

    public void ChargeSphere()
	{
        transform.position = Vector3.Lerp(transform.position, preFireTargetPos, Time.deltaTime * speed);
    }
    public void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }

    public void SetFire()
    {
        preFire = false;
        fire = true;
        GetComponent<Animator>().SetTrigger("release");
        AudioManager.instance.UranieAttackRelease();

    }
}
