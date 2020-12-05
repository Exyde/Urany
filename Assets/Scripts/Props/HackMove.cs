using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackMove : MonoBehaviour
{
    bool isHacked;
    public Transform target;
    Vector3 targetPos;

    public float speed = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = target.position;
    }

    void Update()
    {

        if (isHacked)
		{
            transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
		}

        if (Vector2.Distance (transform.position, targetPos) < .02f)
		{
            Destroy(this);
		}

    }
    public void IsHacked()
	{
        isHacked = true;
	}
}
