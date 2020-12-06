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
		SceneManager.LoadScene(levelIndex);

	}
}
