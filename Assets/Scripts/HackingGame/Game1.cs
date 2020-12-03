using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1 : HackingGame
{
    Transform hackPlayer;
    Vector3 startPos;
    public Transform player;
    public float speed;

    void Start()
    {
        startPos = hackPlayer.position;
    }

    void Update()
    {
        if (InputLB())
		{
            hackPlayer.Translate(Vector3.left * speed * Time.deltaTime);
            
        }

        else if (InputRB())
		{
            hackPlayer.Translate(Vector3.right * speed * Time.deltaTime);
        }

        else if (InputLB() && InputRB())
		{
            hackPlayer.position = startPos;
		}
    }
}
