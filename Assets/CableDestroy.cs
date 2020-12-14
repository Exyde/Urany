using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableDestroy : MonoBehaviour
{
	public GameObject prefab;
	public bool flipX = false;

	private void OnDestroy()
	{
		Vector3 pos = transform.position;
		
		GameObject Fx = Instantiate(prefab, pos, Quaternion.identity);
		if (flipX)
		{
			Fx.transform.localScale = new Vector3(-1, 1, 1);
		}
	}
}
