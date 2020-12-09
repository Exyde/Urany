using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistor : MonoBehaviour
{
    public Transform visuals;
    public Transform moon;
    public Transform sun;
    public Transform centerPos;

    private Uranie uranie;
    private Movement player;



    [Header("Phase 2")]
    public Color blackSkyColor = new Color(6,6,8,1); //Black

    [Header("Phase 3")]
    public Color whiteSkyColor = new Color(234, 255, 255, 1);

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        uranie = GetComponent<Uranie>();
        player = FindObjectOfType<Movement>();

        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(false);

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
        //Move Uranie to center
        uranie.transform.position = centerPos.position;

        //Disable player movement
        player.canMove = false;

        //Flash Screen in White

        yield return new WaitForSeconds(1f);

        //Enable the sun sprite
        sun.gameObject.SetActive(true);

        //Change camera or background color
        cam.backgroundColor = blackSkyColor;

        //Change uranie anims to dark ones

        //Re enable player move
        player.canMove = true;

    }

    IEnumerator Phase3()
	{
        //Move Uranie to center
        uranie.transform.position = centerPos.position;

        //Disable player movement
        player.canMove = false;

        //Flash Screen in White

        yield return new WaitForSeconds(1f);

        //Enable the sun sprite
        moon.gameObject.SetActive(true);

        //Change camera or background color
        cam.backgroundColor = whiteSkyColor;

        //Change uranie anims to dark ones

        //Re enable player move
        player.canMove = true;
    }
}
