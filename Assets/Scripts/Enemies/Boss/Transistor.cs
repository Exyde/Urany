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
    public Movement player;

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
        //player = FindObjectOfType<Movement>();

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
        AudioManager.instance.PlayPart1();

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
        AudioManager.instance.StopMusic();

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
        PrepareUranie();
        DestroySpheres();

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

        //Disable white screen
        whiteScreen.gameObject.SetActive(false);

        //Change camera or background color
        cam.backgroundColor = whiteSkyColor;

        //Change uranie anims to dark ones
        uranie.GetComponent<Animator>().SetTrigger("phase3");

        //Re enable player move
        player.canMove = true;
        uranie.state = Uranie.State.Wait;

        AudioManager.instance.PlayPart2();
        //AudioManager.instance.PlaySound(AudioManager.instance.Part2Theme, .1f);
    }
}
