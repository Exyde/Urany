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

        if (currentHealth <= 0)
		{
            StartCoroutine(PlayerDie());
            //SceneManager.LoadScene(0);
        }
    }

    IEnumerator PlayerDie()
	{
        Movement player = GetComponent<Movement>();
        player.canMove = false;
        player.enabled = false;

        yield return new WaitForSeconds(1f);

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Time.timeScale = 1;
        currentHealth = 0;

        uiManager.SetLife(currentHealth);

        GetComponentInChildren<Animator>().SetBool("Dead", true);
        AudioManager.instance.StopMusic();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
