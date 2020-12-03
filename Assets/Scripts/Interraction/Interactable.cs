using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

	[Space]

	[Header("Custom Event")]
	public UnityEvent customEvent;

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
				interractionSystem.Hack(this.gameObject);
				break;

			case InteractionType.Talk:
				GetComponent<Talking>().TriggerDialogue();
				break;
			default:
				Debug.Log("Null item");
				break;
		}

		//InvokeEvent();
	}

	public void InvokeEvent()
	{
		customEvent.Invoke();
	}

}
