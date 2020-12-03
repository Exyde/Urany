using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public int count = 100;

    void Awake()
    {
        //Singleton Minimum code.
        if (instance != null)
		{
            Destroy(this.gameObject);
		} else
		{
            instance = this;
            DontDestroyOnLoad(gameObject);
		}
    }

	private void Start()
	{
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onBeat.AddListener(onOnBeatDetected);
        
	}
    
    void onOnBeatDetected()
	{
        print("Beat !");
	}
}
