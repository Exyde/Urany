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
    private Animator animator;
    public Movement player;

    [Header("Map Phases")]
    public Transform phase1;
    public Transform phase2;
    public Transform phase3;


    private bool moving;

    public Color blackSkyColor;
    public Color whiteSkyColor;
    public float moveToCenterSpeed;
    public float moonSpeed = 2f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        uranie = GetComponent<Uranie>();
        animator = uranie.GetComponent<Animator>();
        //player = FindObjectOfType<Movement>();

        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(false);

        phase1.gameObject.SetActive(true);
        phase2.gameObject.SetActive(false);
        phase3.gameObject.SetActive(false);

    }

    private void Update()
	{
		if (moving)
		{
            transform.position = Vector2.MoveTowards(transform.position, centerPos.position, Time.deltaTime * moveToCenterSpeed);
		}
	}

    public void toPhase1()
    {
        StartCoroutine(Phase1());
    }
    public void toPhase2()
	{
        StartCoroutine(Phase2());
    }

    public void toPhase3()
	{
        StartCoroutine(Phase3());
    }

    public void PrepareUranie()
	{
        uranie.StopAllCoroutines();

        uranie.simpleBall.StopAllCoroutines();
        uranie.multipleBall.StopAllCoroutines();
        uranie.gravityBall.StopAllCoroutines();

        uranie.state = Uranie.State.Transition;
        uranie.isAttacking = false;
    }

    public void DestroySpheres()
	{
        //Remove all current Spheres.
        for (int i = 0; i < sphereHolder.childCount; i++)
        {
            if (sphereHolder.GetChild(i) != null)
            {
                Destroy(sphereHolder.GetChild(i).gameObject);
            }
        }
    }

    IEnumerator Phase1()
    {
        //PrepareUranie();
        //DestroySpheres();

        //Stop music
        AudioManager.instance.StopMusic();

        //Disable player movement
        player.canMove = false;

        //Move Uranie to center
        moving = true;

        while (Vector2.Distance(transform.position, centerPos.position) > .2f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(.2f);

        //Enable White Screen
        whiteScreen.gameObject.SetActive(true);
        moving = false;

        yield return new WaitForSeconds(.2f);

        //Change music
        AudioManager.instance.PlayPart0();

        //Disable white screen
        whiteScreen.gameObject.SetActive(false);

        //Re enable player move
        player.canMove = true;

        //Set good uranie State !
        uranie.state = Uranie.State.Wait;
    }



    IEnumerator Phase2()
	{
        PrepareUranie();
        DestroySpheres();

        //Stop music
        //AudioManager.instance.StopMusic();
        AudioManager.instance.PlayWind();


        //Disable player movement
        player.canMove = false;

        //Move Uranie to center
        moving = true;

        while (Vector2.Distance (transform.position, centerPos.position) > .2f)
		{
            yield return null;
		}

        yield return new WaitForSeconds(.2f);


        //Enable White Screen
        whiteScreen.gameObject.SetActive(true);
        moving = false;

        yield return new WaitForSeconds(.2f);

        //Change music
        AudioManager.instance.PlayPart1();

        //Enable the sun sprite
        sun.gameObject.SetActive(true);

        //Change camera or background color
        cam.backgroundColor = blackSkyColor;

        //Change background
        phase2.gameObject.SetActive(true);
        phase1.gameObject.SetActive(false);


        //Change uranie anims to dark ones
        //uranie.GetComponent<Animator>().SetTrigger("phase2");
        animator.runtimeAnimatorController = uranie.AnimatorControllerBlack;
        animator.SetTrigger("idle");


        //Disable white screen
        whiteScreen.gameObject.SetActive(false);

        //Re enable player move
        player.canMove = true;

        //Set good uranie State !
        uranie.state = Uranie.State.Wait;
    }

    IEnumerator Phase3()
	{
        PrepareUranie();
        DestroySpheres();

        //Stop music
        AudioManager.instance.StopMusic();
	    AudioManager.instance.PlayWind();


        //Disable player movement
        player.canMove = false;

        //Move Uranie to center
        moving = true;

        while (Vector2.Distance(transform.position, centerPos.position) > .2f)
        {
            yield return null;
        }


        yield return new WaitForSeconds(.2f);

        //Moon Rising Part
        //Enable the moon sprite
        moon.gameObject.SetActive(true);

        //Move the moon to it spots

        while (Vector2.Distance(transform.position, moon.position) > .2f)
        {
            moon.position = Vector2.Lerp(moon.position, transform.position, moonSpeed * Time.deltaTime);

            //yield return new WaitForSeconds(.1f);
            yield return null;
        }

        moon.position = transform.position;

        //Flash Screen in White
        whiteScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(.2f);

        //Change camera or background color
        cam.backgroundColor = whiteSkyColor;

        //Change background
        phase3.gameObject.SetActive(true);
        phase2.gameObject.SetActive(false);


        //Change uranie anims to dark ones
        //uranie.GetComponent<Animator>().SetTrigger("phase3");
        animator.runtimeAnimatorController = uranie.AnimatorControllerWhite;
        animator.SetTrigger("idle");

        //Disable white screen
        whiteScreen.gameObject.SetActive(false);


        //Re enable player move
        player.canMove = true;
        uranie.state = Uranie.State.Wait;

        AudioManager.instance.PlayPart2();
    }
}
