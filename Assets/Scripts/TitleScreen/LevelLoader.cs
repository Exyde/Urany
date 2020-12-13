using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
	public Animator transition;
	public float transitionTime = 1f;
	public Movement movement;

	string region, hub, boss;

	private void Start()
	{
		region = "Region";
		hub = "Hub";
		boss = "Boss";
	}

	public void OnNextLevel(int levelIndex)
	{
		StartCoroutine(LoadLevel(levelIndex));
	}

	IEnumerator LoadLevel(int levelIndex)
	{
		transition.SetTrigger("Start");
		if (movement) movement.canMove = false;
		yield return new WaitForSeconds(transitionTime);
		SetMusic(levelIndex);
		SceneManager.LoadScene(levelIndex);

	}

	public void SetMusic(int level) {

		switch (level)
		{
			case 0: //Title Screen
				AudioManager.instance.StopMusic();
				break;
			case 1: //Hub
				AudioManager.instance.PlayHub();
				break;
			case 2: //Region
				AudioManager.instance.PlayRegion();
				break;
			case 3: //Boss
				break;

			case 4: // Video

				break;

			default:
				break;
		}
	}
		
}
