using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1 : HackingGame
{

    [Header ("Game 1")]
    //public Breach breach;
    public float speed;
    public GameObject hackPlayerPrefab;
    public Color currentPointColor;
    public Color defaultColor;
    //public LineRenderer lr;
    public PostProcessController pp;

    [Space]

    public Transform points;
    Transform[] path;
    int currentIndex;
    public Transform playerHolder;

    void Start()
    {
        
        //lr = GetComponent<LineRenderer>();
        GameInit();
    }

	private void OnEnable()
	{
        GameInit();
	}

	private void OnDisable()
	{
        pp.ResetPostProcess();
	}

	void Update()
    {
        if (currentIndex == path.Length)
		{
            StartCoroutine(Win(.2f));
		} 
        else
		{
            path[currentIndex].GetComponent<SpriteRenderer>().color = currentPointColor;
            if (InputLB())
            {
                //iconDisplayerLeft.LB();
                //iconDisplayerRight.Empty();

                if (path[currentIndex].localPosition.x == 0)
                {

                    if (currentIndex < path.Length)
                    {
                        //hackPlayer.position = path[currentIndex].position;
                        GameObject playerPrefab = Instantiate(hackPlayerPrefab, path[currentIndex].position, Quaternion.identity);
                        playerPrefab.transform.parent = playerHolder;
                        currentIndex++;
                        AudioManager.instance.HackFeedback();
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


                    if (currentIndex < path.Length)
                    {
                        //hackPlayer.position = path[currentIndex].position;
                        GameObject playerPrefab = Instantiate(hackPlayerPrefab, path[currentIndex].position, Quaternion.identity);
                        playerPrefab.transform.parent = playerHolder;
                        currentIndex++;
                        AudioManager.instance.HackFeedback();


                    }

                }
                else
                {
                    LooseGame();
                }

            }
        }
    }

    IEnumerator Win(float delay)
	{
        yield return new WaitForSeconds(delay);
        WinGame();
    }
    public void GameInit()
	{
        currentIndex = 0;
        path = new Transform[points.childCount];
        //lr.positionCount = points.childCount;
        pp.SetHackPostProcess();

        //Set the path
        for (int i = 0; i < path.Length; i++)
		{
            path[i] = points.GetChild(i);
            path[i].GetComponent<SpriteRenderer>().color = defaultColor;

		}

        //Remove all player instances.
        for (int i = 0; i < playerHolder.childCount; i++)
		{
            Destroy(playerHolder.GetChild(i).gameObject);
		}

        //Line Renderer stuff 
        /*
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, path[i].position);
        }

        */
    }

    protected override void LooseGame()
	{
        base.LooseGame();
        AudioManager.instance.HackFail();
	}

	public override void ResetGame()
	{
		base.ResetGame();
	}

	protected override void WinGame()
	{
		base.WinGame();
        AudioManager.instance.HackSucces();

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
    }
}
