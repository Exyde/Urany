using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGame : MonoBehaviour
{
    Breach breach;
    public int bpm;

    protected bool InputLB()
	{
        return Input.GetButtonDown("HackLeft");
    }

    protected bool InputRB()
    {
        return Input.GetButtonDown("HackRight");
    }

    protected void WinGame()
	{
        breach.BreachHacked();
	}

    protected void LooseGame()
	{
        breach.ResetHackGame();
	}
}
