using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistor : MonoBehaviour
{
    public Transform visuals;
    public Transform moon;
    public Transform sun;
    public Transform centerPos;
    public Transform whiteScreen;
    public Transform sphereHolder;

    private Uranie uranie;
    private Movement player;

    private bool moving;

    public Color blackSkyColor;
    public Color whiteSkyColor;
    public float moveToCenterSpeed;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        uranie = GetComponent<Uranie>();
        player = FindObjectOfType<Movement>();

        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(false);
    }

	private void Update()
	{
		if (moving)
		{
            transform.position = Vector2.MoveTowards(transform.position, centerPos.position, Time.deltaTime * moveToCenterSpeed);
		}
	}

	public void toPhase2()
	{
        StartCoroutine(Phase2());
    }

    public void toPhase3()
	{
        StartCoroutine(Phase3());
    }

    IEnumerator Phase2()
	{
        uranie.StopAllCoroutines();

        uranie.simpleBall.StopAllCoroutines();
        uranie.multipleBall.StopAllCoroutines();
        uranie.gravityBall.StopAllCoroutines();

        uranie.state = Uranie.State.Transition;
        uranie.isAttacking = false;

        //Remove all current Spheres.
        for (int i = 0; i < sphereHolder.childCount; i++)
		{
            if (sphereHolder.GetChild(i) != null)
			{
                Destroy(sphereHolder.GetChild(i).gameObject);
			}
		}

        //Disable player movement
        player.canMove = false;

        //Move Uranie to center
        moving = true;

        yield return new WaitForSeconds(1.2f);
        //Enable White Scree
        whiteScreen.gameObject.SetActive(true);
        moving = false;

        yield return new WaitForSeconds(.2f);

        //Change music
        //²²²²AudioManager.instance.PlayPart1();

        //Enable the sun sprite
        sun.gameObject.SetActive(true);

        //Change camera or background color
        cam.backgroundColor = blackSkyColor;

        //Change uranie anims to dark ones
        uranie.GetComponent<Animator>().SetTrigger("phase2");

        //Disable white screen
        whiteScreen.gameObject.SetActive(false);

        //Re enable player move
        player.canMove = true;

        //Set good uranie State !
        uranie.state = Uranie.State.Wait;
    }

    IEnumerator Phase3()
	{
        uranie.state = Uranie.State.Transition;
        uranie.StopAllCoroutines();
        //Move Uranie to center
        uranie.transform.position = centerPos.position;

        //Disable player movement
        player.canMove = false;

        //Flash Screen in White
        whiteScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(.1f);

        whiteScreen.gameObject.SetActive(false);

        //Enable the sun sprite
        moon.gameObject.SetActive(true);

        //Change camera or background color
        cam.backgroundColor = whiteSkyColor;

        //Change uranie anims to dark ones
        uranie.GetComponent<Animator>().SetTrigger("phase3");

        //Re enable player move
        player.canMove = true;
        uranie.state = Uranie.State.Wait;

        //AudioManager.instance.PlayPart2();
        //AudioManager.instance.PlaySound(AudioManager.instance.Part2Theme, .1f);


    }
}
