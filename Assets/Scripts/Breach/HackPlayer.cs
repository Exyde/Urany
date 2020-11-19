using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPlayer : MonoBehaviour
{
    public Breach breach;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "HackGameObjective")
		{
            breach.BreachHacked();
		}
	}
}
