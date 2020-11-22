using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
	
public class MainMenu : MonoBehaviour
{
	public LevelLoader levelLoader;
	public void PlayGame()
	{
		levelLoader.OnNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Debug.Log("Leaving Game...");
		Application.Quit();
	}
}
