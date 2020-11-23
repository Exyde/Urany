using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{

    public UIManager uiManager;

    //Handle player's hp, death, checkpoints.
    [Header("Life data")]
    public int startHealth = 4;
    public int currentHealth;


    [Header ("Checkpoint Data")]
    public Transform currentCheckpoint;

    void Start()
    {
        currentHealth = startHealth;
        uiManager = FindObjectOfType<UIManager>();
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Deadly")
		{
           //Eventually add a coroutine with delay, and disable player movements while it's working.
           if (currentCheckpoint)
		   {
                transform.position = currentCheckpoint.position;
                currentHealth -= 1;
                CheckLife();
		   }
           else
		   {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                currentHealth -= 1;
                CheckLife();
           }
        }
	}

    public void CheckLife()
	{
        uiManager.SetLife(currentHealth);

        if (currentHealth <= 0)
		{
            currentHealth = 0;
            //uiManager.SetLife(currentHealth);
            print("Game Over");
            //Restart Game
            SceneManager.LoadScene(0);
        }
    }

}
