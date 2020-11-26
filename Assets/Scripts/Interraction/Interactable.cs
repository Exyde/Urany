using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class Interactable : MonoBehaviour
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

	[Header("Examine Type")]
	public string descriptionText;
	public Sprite image; 

	private void Reset()
	{
		GetComponent<Collider2D>().isTrigger = true;
		gameObject.layer = 9;
	}

	public void Interact()
	{
		InteractionSystem interractionSystem = FindObjectOfType<InteractionSystem>();
		switch (interactionType)
		{
			case InteractionType.PickUp:
				//This is temporary, for the proto. Maybe this will be updated if we setup an inventory.
				interractionSystem.PickUpItem(this.gameObject);
				gameObject.SetActive(false);
				break;

			case InteractionType.Examine:
				interractionSystem.ExamineItem(this);
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
