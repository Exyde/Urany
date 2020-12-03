using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    void Awake()
    {
        //Singleton Minimum code.
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(1);
		}

        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
}
