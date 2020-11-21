using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    public Transform interractionPoint;
    public float interractionRadius;
    public LayerMask interractionLayer;

    void Start()
    {
        
    }

    void Update()
    {
        if (DetectObject())
		{
			if (InterractInput())
			{
                print("Interraction !");
			}
		}
    }

    bool InterractInput()
	{
        return (Input.GetKeyDown(KeyCode.A) ^ Input.GetButtonDown("Fire1"));
    }

    bool DetectObject()
	{
        bool isDetected = Physics2D.OverlapCircle(interractionPoint.position, interractionRadius, interractionLayer);
        return isDetected;
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interractionPoint.position, interractionRadius); 
	}
}
