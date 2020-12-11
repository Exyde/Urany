using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    InputDisplayer ip;
    DialogueManager dm;

    bool set = false;

    void Start()
    {
        ip = GetComponentInChildren<InputDisplayer>();
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dm.isTalking && !set)
        {
            ip.X();
            set = true;
        }

        if (dm.endDialogue && set)
		{
            ip.Empty();
            set = false;
		}
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
