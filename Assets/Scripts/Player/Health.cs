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

    public bool alive;


    [Header ("Checkpoint Data")]
    public Transform currentCheckpoint;

    void Start()
    {
        currentHealth = startHealth;
        uiManager = FindObjectOfType<UIManager>();
        alive = true;
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Deadly")
		{
           //Eventually add a coroutine with delay, and disable player movements while it's working.
           if (currentCheckpoint)
		   {
                transform.position = currentCheckpoint.position;
                LooseLife(1);
            }
            else
		   {
                LooseLife(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);          
           }
        }
	}

    public void LooseLife(int amount)
	{
        currentHealth -= amount;
        CheckLife();
	}

    public void CheckLife()
	{
        //print(currentHealth);
        uiManager.SetLife(currentHealth);

        if (currentHealth <= 0 && alive)
		{
            StartCoroutine(PlayerDie());
            //SceneManager.LoadScene(0);
        }
    }

    IEnumerator PlayerDie()
	{
        alive = false;

        Movement player = GetComponent<Movement>();
        player.canMove = false;
        player.enabled = false;
        player.SoundStep.Stop();

        yield return new WaitForSeconds(1f);

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Time.timeScale = 1;
        currentHealth = 0;

        uiManager.SetLife(currentHealth);

        GetComponentInChildren<Animator>().SetTrigger("Dead");
        AudioManager.instance.StopMusic();

        yield return new WaitForSeconds(2f);

        FindObjectOfType<LevelLoader>().OnNextLevel(SceneManager.GetActiveScene().buildIndex);
    }

}
