using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNexTLevel : MonoBehaviour
{
	private LevelLoader levelLoader;
	[SerializeField] int levelToLoad;
    void Start()
    {
		levelLoader = FindObjectOfType<LevelLoader>();
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
	    if (collision.gameObject.tag == "Player")
		{
			levelLoader.OnNextLevel(levelToLoad);
		}	
	}
}
