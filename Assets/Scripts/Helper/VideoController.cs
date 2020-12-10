using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    public LevelLoader loader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
		{
            loader.OnNextLevel(1);
		}

        if (Input.GetKeyDown(KeyCode.L))
		{
            loader.OnNextLevel(0);
        }
    }
}
