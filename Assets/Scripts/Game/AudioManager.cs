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



    [Header("Player")]
    public AudioClip playerJump;
    public AudioClip playerDash;
    public AudioClip playerLand;
    public AudioClip playerRun;
    public AudioClip playerAttack;
    public AudioClip playerHit;

    [Header("Uranie")]
    public AudioClip uranieAttackCharge;
    public AudioClip uranieAttackRelease;
    public AudioClip uranieAttackImpact;
    public AudioClip uranieHit;
    public AudioClip uranieDeath;

    [Header("Hacks")]
    public AudioClip hackSucces;
    public AudioClip hackFail;
    public AudioClip hackFeedback;

    [Header("Misc")]
    public AudioClip cableBreak;
    public AudioClip Typo;

    [Header("Pnj 1")]
    public AudioClip pnjDeath;
    public AudioClip pnjAttack;
    public AudioClip pnjHit;

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

	#region PlayerSounds
	public void Jump()
	{
        PlaySound(playerJump, 1f);
	}

    public void Dash()
    {
        PlaySound(playerDash, 1f);
    }

    public void Land()
	{
        PlaySound(playerLand, 1f);
    }

    //Run

    public void Attack()
	{
        PlaySound(playerAttack, 1f);
	}
    public void PlayerHit()
    {
        PlaySound(playerHit, 1f);
    }

    #endregion

    #region Uranie

    public void UranieAttackCast()
    {
        PlaySound(uranieAttackCharge, 1f);
    }

    public void UranieAttackRelease()
    {
        PlaySound(uranieAttackRelease, 1f);
    }
    public void UranieAttackImpact()
    {
        PlaySound(uranieAttackImpact, 1f);
    }
    public void UranieHit()
	{
        PlaySound(uranieHit, 1f);
	}

    public void UranieDeath()
	{
        PlaySound(uranieDeath, 1f);
    }
    #endregion
}
