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
       hackPlayer = GetComponentInChildren<HackPlayer>().transform;
    }

    void Update()
    {
        if (player.GetComponent<InteractionSystem>().isHacking)
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
}
