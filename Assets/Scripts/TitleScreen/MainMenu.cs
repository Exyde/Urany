using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
	
public class MainMenu : MonoBehaviour
{
	public LevelLoader levelLoader;

	private void Start()
	{
		Time.timeScale = 1;
	}
	public void PlayGame()
	{
		//levelLoader.OnNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
		levelLoader.OnNextLevel(4); //Video scene
	}

	private void Update()
	{
		if (Input.GetButtonDown("Attack"))
		{
			levelLoader.OnNextLevel(4); //Video scene
		}
	}

	public void QuitGame()
	{
		Debug.Log("Leaving Game...");
		Application.Quit();
	}
}
