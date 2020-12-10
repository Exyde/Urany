using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNexTLevel : MonoBehaviour
{
	private LevelLoader levelLoader;
	[SerializeField] int levelToLoad = default;

	string region, hub, boss;

    void Start()
    {
		levelLoader = FindObjectOfType<LevelLoader>();
		region = "Region";
		hub = "Hub";
		boss = "Boss";
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
	    if (collision.gameObject.tag == "Player")
		{
			levelLoader.OnNextLevel(levelToLoad);

			if (levelToLoad == 2)
			{
				AudioManager.instance.PlayMusic(region);
			}

		}	
	}
}
