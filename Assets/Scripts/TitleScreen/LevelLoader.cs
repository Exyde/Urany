using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
	public Animator transition;
	public float transitionTime = 1f;
	public Movement movement;

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
	
	IEnumerator BlackScreen()
	{
		yield return new WaitForSeconds(3f);
		transition.SetTrigger("Start");
	}

	public void FadeToBlack()
	{
		StartCoroutine(BlackScreen());
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
				AudioManager.instance.PlayWind();
				
				break;

			case 4: // Video

				break;

			default:
				break;
		}
	}
		
}
