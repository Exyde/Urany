using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1 : HackingGame
{

    [Header ("Game 1")]
    
    public Transform hackPlayer;
    public float speed;

    [Space]

    public Transform points;
    Transform[] path;
    int currentIndex;


    void Start()
    { 
        GameInit();
    }

	private void OnEnable()
	{
        GameInit();
	}

	void Update()
    {

        if (currentIndex == path.Length)
		{
            WinGame();
		}

        if (InputLB())
		{
            //iconDisplayerLeft.LB();
            //iconDisplayerRight.Empty();

            if ( path[currentIndex].localPosition.x  == 0)
			{
                currentIndex++;

                if (currentIndex < path.Length)
                {
                    hackPlayer.position = path[currentIndex].position;
                }
            } 
            else
			{
                LooseGame();
			}
 
        }

        else if (InputRB())
		{

            //iconDisplayerLeft.Empty();
            //iconDisplayerRight.RB();


            if (path[currentIndex].localPosition.x == 1)
            {
                currentIndex++;

                if (currentIndex < path.Length)
                {
                    hackPlayer.position = path[currentIndex].position;
                }

            }
            else
            {
                LooseGame();
            }

        }
    }

    public void GameInit()
	{
        currentIndex = 0;
        path = new Transform[points.childCount];

        for (int i = 0; i < path.Length; i++)
		{
            path[i] = points.GetChild(i);
		}

        hackPlayer.position = path[currentIndex].position;

    }

    protected override void LooseGame()
	{
        base.LooseGame();
	}

	public override void ResetGame()
	{
		base.ResetGame();
	}

	protected override void WinGame()
	{
		base.WinGame();
        GetComponentInParent<Interactable>().InvokeEvent();
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.white;

        Vector3 startPos = points.GetChild(0).position;
        Vector3 previousPos = startPos;

        foreach (Transform t in points)
        {
            Gizmos.DrawWireSphere(t.position, .1f);
            Gizmos.DrawLine(previousPos, t.position);
            previousPos = t.position;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hackPlayer.position, .1f);
        //Gizmos.DrawLine(previousPos, startPos);
    }
}
