using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    public Transform interractionPoint;
    public float interractionRadius;
    public LayerMask interractionLayer;

    public GameObject currentObject;

    void Start()
    {
        currentObject = null;
    }

    void Update()
    {
        if (DetectObject())
		{
			if (InterractInput())
			{
                currentObject.GetComponent<Item>().Interact();
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
}
