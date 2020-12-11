using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    InputDisplayer ip;

    void Start()
    {
        ip = GetComponentInChildren<InputDisplayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
            ip.Y();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            ip.Empty();
        }
    }
}
