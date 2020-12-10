using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //Music
    private AudioSource musicPlayer;

    public AudioClip HubTheme;
    public AudioClip RegionTheme;
    public AudioClip Part1Theme;
    public AudioClip Part2Theme;

    public AudioClip Typo;
    //Sounds
    //TODO : List all sounds fx needed for the game.
    
    //Urany attack
    //Charge
    //Fire
    //Hit

    //Player

    //Ennemys

    //


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

        musicPlayer = GetComponent<AudioSource>();
    }

    public void PlayPart1()
	{
        musicPlayer.clip = Part1Theme;
        musicPlayer.Play();
	}

    public void PlayPart2()
    {
        musicPlayer.clip = Part2Theme;
        musicPlayer.Play();
    }
    
    public void StopMusic()
	{
        musicPlayer.Stop();
	}

    public void PlayMusic (string level)
	{
        if (level == "Region")
		{
            musicPlayer.clip = RegionTheme;
            musicPlayer.Play();
		}
	}

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.volume = volume;
        go.name = System.DateTime.Now.ToString();
        GameObject.Destroy(go, audioSource.clip.length + 0.10f);
       // return clip.length;
    }

    public void TypoFX()
	{
        PlaySound(Typo, .2f);
	}
}
