using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Health Bar")]
    public Image healthBar;
    public Image life1;
    public Image life2;
    public Image life3;
    public Image life4;


    public void SetLife(int hp)
	{
        //One day I'll refactor this.
		switch (hp)
		{
            case 0:
                life1.enabled = false;
                life2.enabled = false;
                life3.enabled = false;
                life4.enabled = false;

                break;

            case 1:
                life1.enabled = true;
                life2.enabled = false;
                life3.enabled = false;
                life4.enabled = false;
                break;

            case 2:
                life1.enabled = true;
                life2.enabled = true;
                life3.enabled = false;
                life4.enabled = false;
                break;

            case 3:
                life1.enabled = true;
                life2.enabled = true;
                life3.enabled = true;
                life4.enabled = false;
                break;

            case 4:
                life1.enabled = true;
                life2.enabled = true;
                life3.enabled = true;
                life4.enabled = true;
                break;

            default:
                Debug.LogError("Invalid hp amount");
                break;
		}
	}
}
