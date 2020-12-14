using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableDestroy : MonoBehaviour
{
	public GameObject prefab;

	private void OnDestroy()
	{
		Instantiate(prefab, transform.transform.position, Quaternion.identity);
	}
}
