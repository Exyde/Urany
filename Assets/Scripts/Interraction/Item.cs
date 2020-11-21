using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class Item : MonoBehaviour
{
	public enum InteractionType
	{
		NONE,
		PickUp,
		Examine,
		Hack,
		Talk
	}

	public InteractionType interactionType;

	private void Reset()
	{
		GetComponent<Collider2D>().isTrigger = true;
		gameObject.layer = 9;
	}

	public void Interact()
	{
		switch (interactionType)
		{
			case InteractionType.PickUp:
				Debug.Log("Pick up !");
				break;

			case InteractionType.Examine:
				Debug.Log("Examine !");
				break;

			case InteractionType.Hack:
				Debug.Log("Hacking -- !");
				break;

			case InteractionType.Talk:
				Debug.Log("Talking");
				break;
			default:
				Debug.Log("Null item");
				break;
		}
	}
}
