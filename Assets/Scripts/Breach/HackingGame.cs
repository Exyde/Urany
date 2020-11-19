using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGame : MonoBehaviour
{
    Transform hackPlayer;
    public Transform player;
    public float speed;

    void Start()
    {
        hackPlayer = transform.GetChild(0);
    }

    void Update()
    {
        if (player.GetComponent<Hacking>().isHacking)
        {
            if (Input.GetButton("HackLeft"))
            {
                //hackPlayer.transform.position += Vector3.left * speed;
                hackPlayer.transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetButton("HackRight"))
            {
                //hackPlayer.transform.position += Vector3.right * speed;
                hackPlayer.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "hackPlayer")
		{
            transform.parent.GetComponent<Breach>().BreachHacked();
            this.gameObject.SetActive(false);
        }
    }
}
