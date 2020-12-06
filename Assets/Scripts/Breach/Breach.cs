using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach : MonoBehaviour
{
    //Private fields
    SpriteRenderer sr;
    InteractionSystem interactionSystem;
    Rigidbody2D rb;
    public InputDisplayer inputDisplayer;

    [Header("Hacking Game Data")]
    public Transform player;
    public HackingGame hackingGame;
    public float maxHackRange = 5f;
    bool hackDone = false;

    [Header ("Colors")]
    public Color onBreachColor;
    public Color defaultColor;
    public Color hackColor;

    [Space]
    public bool alwaysDrawGizmos;

    void Start()
    {
        hackingGame = GetComponentInChildren<HackingGame>();
        sr = GetComponent<SpriteRenderer>();
        interactionSystem = player.GetComponent<InteractionSystem>();
        rb = GetComponent<Rigidbody2D>();
        //inputDisplayer = GetComponentInChildren<InputDisplayer>();
    }

    void Update()
    {
        // If player is hacking and too far from the limit range, disable the hack game.
        if (!hackDone)
		{
            if (hackingGame.gameObject.activeSelf && Vector3.Distance(transform.position, player.transform.position) > maxHackRange ){
                ResetHackGame();
            }
	    }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{

        //Entering the hacking range radius et enable the breachs feedback/fx/visuals.
        if (other.tag == "Interaction Point" && !hackDone && !hackingGame.gameObject.activeSelf)
		{
            ActivateBreach();
        }
    }

	private void OnTriggerExit2D(Collider2D other)
	{
        //Reset the breach if not hacking and leaving the trigger zone.
        if (other.tag == "Interaction Point" && !hackDone)
        {
            if (!interactionSystem.isHacking)
			{
				ResetBreach();
			}
        }
    }

    void ActivateBreach()
	{
        //Backend
        interactionSystem.onBreach = true;
        interactionSystem.currentBreach = this.transform;

        //Front end 
        sr.color = onBreachColor;
        inputDisplayer.Y();
    }

    public void ResetBreach()
	{
        //Backend
        interactionSystem.onBreach = false;
        interactionSystem.currentBreach = null;
        interactionSystem.isHacking = false;

        //Frontend
        sr.color = defaultColor;
        inputDisplayer.Empty();

    }

    public void StartHackGame()
	{
        //Back
        hackingGame.StartGame();


        //Front
        inputDisplayer.Empty();
    }

    public void ResetHackGame()
	{
        hackingGame.ResetGame();
        ResetBreach();
    }

    public void BreachHacked()
	{
        hackDone = true;

        //Reset the interraction system.
        interactionSystem.onBreach = false;
        interactionSystem.currentBreach = null;
        interactionSystem.isHacking = false;

        //Visuals adjustements.
        sr.color = hackColor;
        inputDisplayer.Empty();
        
        //Cleaning
        Destroy(hackingGame.gameObject);
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(this);
	}

	#region Gizmos
	private void OnDrawGizmos()
    {
        if (alwaysDrawGizmos && !hackDone)
		{
            //Breach Acces Range
            Gizmos.color = new Color(255, 69, 0, 120);
            //Gizmos.DrawWireSphere((Vector2)transform.position, GetComponent<CircleCollider2D>().radius);

            //Hack Limit Range
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, maxHackRange);
        } 
    }

    private void OnDrawGizmosSelected()
    {
        
        //Breach Acces Range
        Gizmos.color = new Color(255, 69, 0, 120);
        //Gizmos.DrawWireSphere((Vector2)transform.position, GetComponent<CircleCollider2D>().radius);

        //Hack Limit Range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxHackRange);
    }

	#endregion
}
