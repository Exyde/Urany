using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackMove : MonoBehaviour
{
    //bool isHacked;
    public Transform target;

    public float speed = 1f;
    Rigidbody2D rb;

    public float lerpDuration = 5f;
    Vector3 startPos;
    Vector3 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = target.position;
    }

    void Update()
    {

        /*
        if (Vector2.Distance (transform.position, targetPos) < .02f)
		{
            transform.position = targetPos;
            Destroy(this);
		}

        */
    }
    public void IsHacked()
	{
        //isHacked = true;
        StartCoroutine(LerpPosition());
	}

    
    public IEnumerator LerpPosition()
	{
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
		{
            transform.position = Vector2.Lerp(transform.position, targetPos, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
		}

        transform.position = targetPos;
	}
}