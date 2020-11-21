using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : MonoBehaviour
{

    //Small script attached to the player to get a reference to the current Breach.
    // Also check is the player is hacking it or not.
    // Allow the game inside the breach to start or not.

    public PostProcessController pp;

    public bool isHacking;
    public bool onBreach;

    public Transform currentBreach;

    void Start()
    {
        currentBreach = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("Fire1") && onBreach)
		{
            Hack();
		}
    }

    public void Hack()
	{
        isHacking = true;
        
        if (!currentBreach)
		{
            isHacking = false;
            return;

		} else
		{
            currentBreach.gameObject.GetComponent<Breach>().hackingGame.gameObject.SetActive(true);
		}
	}
}
