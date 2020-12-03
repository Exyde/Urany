using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPlayer : MonoBehaviour
{
    public Breach breach;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "HackGameObjective")
		{
            breach.BreachHacked();
		}
	}
}
