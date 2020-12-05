using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1 : HackingGame
{
    public Transform hackPlayer;
    public float speed;

    public Transform points;
    Vector3[] path;

    void Start()
    {
        path = new Vector3[points.childCount];

        GameInit();
    }

    void Update()
    {
        if (InputLB())
		{
            WinGame();
        }

        else if (InputRB())
		{
        }
    }

    public void GameInit()
	{
        for (int i = 0; i < path.Length; i++)
		{
            path[i] = points.GetChild(i).position;
		}
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
            Gizmos.DrawSphere(t.position, .1f);
            Gizmos.DrawLine(previousPos, t.position);
            previousPos = t.position;
        }

        //Gizmos.DrawLine(previousPos, startPos);
    }
}
