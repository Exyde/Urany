using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach : MonoBehaviour
{
    SpriteRenderer sr;
    Hacking hack;
    Rigidbody2D rb;

    public Transform targetTransform;

    public Transform player;
    public Transform hackingGame;
    public float maxHackRange = 5f;
    bool hackDone = false;


    [Header ("Colors")]
    public Color onBreachColor;
    public Color defaultColor;
    public Color hackColor;

    [Header("Future Breach ")]
    public Vector3 newScale;
    public Vector3 newRotation;
    public Vector3 newPosition;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hack = player.GetComponent<Hacking>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (!hackDone)
		{
            if (Vector3.Distance(transform.position, player.transform.position) > maxHackRange){
                ResetHackGame();
            }
	    }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Player" && !hackDone)
		{
            sr.color = onBreachColor;
            hack.onBreach = true;
            hack.currentBreach = this.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.tag == "Player" && !hackDone)
        {
            ResetBreach();
        }
    }

    void ResetBreach()
	{
        sr.color = defaultColor;
        hack.onBreach = false;
        hack.currentBreach = null;
        hack.isHacking = false;
    }

    void ResetHackGame()
	{
        hackingGame.gameObject.SetActive(false);
    }

    public void BreachHacked()
	{
        hackDone = true;
        hack.onBreach = false;
        hack.currentBreach = null;
        sr.color = hackColor;

        Destroy(hackingGame.gameObject);
        Destroy(GetComponent<CircleCollider2D>());
        TransformBreach();
	}

    public void TransformBreach()
	{
        //Vector3.Lerp(transform.position, transform.position + newPosition, morphTime);
        //Vector3.Lerp(transform.localScale, newScale, morphTime);
        transform.position = transform.position + newPosition;
        transform.localScale = newScale;
        transform.Rotate(newRotation);
        //At the end of the transform, destroy the script.
        //Destroy(this);
    }

    private void OnDrawGizmos()
    {
        //Breach Acces Range
        Gizmos.color = new Color(255, 69, 0, 120);
        Gizmos.DrawWireSphere((Vector2)transform.position, GetComponent<CircleCollider2D>().radius);

        //Hack Limit Range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxHackRange);
    }
}
