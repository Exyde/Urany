using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    void Start()
    {
        int posX = Random.Range(0, 2);
        transform.localPosition = new Vector3(posX, transform.localPosition.y, 0); 
    }

	private void OnEnable()
	{
        int posX = Random.Range(0, 2);
        transform.localPosition = new Vector3(posX, transform.localPosition.y, 0);
    }
}
