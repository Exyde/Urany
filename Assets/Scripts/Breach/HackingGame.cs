using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGame : MonoBehaviour
{

    [Header ("BASE")]
    public Breach breach;
    public int bpm;

    [Header ("Display")]
    public InputDisplayer iconDisplayerLeft;
    public InputDisplayer iconDisplayerRight;

    protected bool HoldInputLB()
	{
        return Input.GetButton("HackLeft");
    }
    protected bool InputLB()
    {
        return Input.GetButtonDown("HackLeft");
    }
    protected bool HoldInputRB()
    {
        return Input.GetButton("HackRight");
    }

    protected bool InputRB()
    {
        return Input.GetButtonDown("HackRight");
    }

    protected virtual void WinGame()
	{
        breach.BreachHacked();
	}

    protected virtual void LooseGame()
	{
        breach.ResetHackGame();
	}

    public virtual void ResetGame()
	{
        gameObject.SetActive(false);
       
	}

    public virtual void StartGame()
    {
        gameObject.SetActive(true);
    }
}
