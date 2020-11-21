using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{

    [Header ("Detection settings")]
    public Transform interractionPoint;
    public float interractionRadius;
    public LayerMask interractionLayer;

    [Header ("Item Info")]
    public GameObject currentObject;

    [Header("Temporary Inventory")]
    public List<GameObject> pickedItems = new List<GameObject>();

    void Start()
    {
        currentObject = null;
    }

    void Update()
    {
        if (DetectObject()) //If in range
		{
			if (InterractInput()) // and player input
			{
                currentObject.GetComponent<Interactable>().Interact();
			}
		}
    }

    bool InterractInput()
	{
        return (Input.GetKeyDown(KeyCode.A) ^ Input.GetButtonDown("Fire1"));
    }

    bool DetectObject()
	{
        Collider2D coll = Physics2D.OverlapCircle(interractionPoint.position, interractionRadius, interractionLayer);
        
        if(coll == null)
		{
            currentObject = null;
            return false;
		} else
		{
            currentObject = coll.gameObject;
            return true;
		}
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interractionPoint.position, interractionRadius); 
 	}

    public void PickUpItem(GameObject item)
	{
        pickedItems.Add(item);
	}
}
