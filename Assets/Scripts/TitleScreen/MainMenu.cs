using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
	
public class MainMenu : MonoBehaviour, IPointerEnterHandler
{
	public LevelLoader levelLoader;

	private void Start()
	{
		Time.timeScale = 1;
	}
	public void PlayGame()
	{
		//levelLoader.OnNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
		AudioManager.instance.HackSucces();
		levelLoader.OnNextLevel(4); //Video scene
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		AudioManager.instance.HackFeedback();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Attack"))
		{
			AudioManager.instance.HackSucces();
			levelLoader.OnNextLevel(4); //Video scene
		}
	}

	public void QuitGame()
	{
		Debug.Log("Leaving Game...");
		AudioManager.instance.HackFail();

		Application.Quit();
	}
}
