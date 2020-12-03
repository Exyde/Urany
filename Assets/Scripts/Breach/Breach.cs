using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach : MonoBehaviour
{
    public bool alwaysDrawGizmos;

    SpriteRenderer sr;
    InteractionSystem interactionSystem;
    Rigidbody2D rb;

    public Transform player;
    public Transform hackingGame;

    public float maxHackRange = 5f;
    bool hackDone = false;

    [Header ("Colors")]
    public Color onBreachColor;
    public Color defaultColor;
    public Color hackColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        interactionSystem = player.GetComponent<InteractionSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hackDone)
		{
            if (Vector3.Distance(transform.position, player.transform.position) > maxHackRange && hackingGame.gameObject.activeSelf){
                ResetHackGame();
            }
	    }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Interaction Point" && !hackDone)
		{
            sr.color = onBreachColor;
            interactionSystem.onBreach = true;
            interactionSystem.currentBreach = this.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.tag == "Interaction Point" && !hackDone)
        {
            if (!interactionSystem.isHacking)
			{
                ResetBreach();
			}
            
        }
    }

    void ResetBreach()
	{
        sr.color = defaultColor;
        interactionSystem.onBreach = false;
        interactionSystem.currentBreach = null;
        interactionSystem.isHacking = false;
    }

    void ResetHackGame()
	{
        ResetBreach();
        hackingGame.gameObject.SetActive(false);
    }

    public void BreachHacked()
	{
        hackDone = true;
        interactionSystem.onBreach = false;
        interactionSystem.currentBreach = null;
        interactionSystem.isHacking = false;
        sr.color = hackColor;
        

        Destroy(hackingGame.gameObject);
        Destroy(GetComponent<CircleCollider2D>());
	}

    private void OnDrawGizmos()
    {
        if (alwaysDrawGizmos)
		{
            //Breach Acces Range
            Gizmos.color = new Color(255, 69, 0, 120);
            Gizmos.DrawWireSphere((Vector2)transform.position, GetComponent<CircleCollider2D>().radius);

            //Hack Limit Range
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, maxHackRange);
        } 
    }

    private void OnDrawGizmosSelected()
    {
        //Breach Acces Range
        Gizmos.color = new Color(255, 69, 0, 120);
        Gizmos.DrawWireSphere((Vector2)transform.position, GetComponent<CircleCollider2D>().radius);

        //Hack Limit Range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxHackRange);
    }
}
