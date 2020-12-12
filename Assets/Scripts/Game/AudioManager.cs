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
    public AudioClip part0;
    public AudioClip Part1Theme;
    public AudioClip Part2Theme;

    public float fxVolume = .2f;

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

    public void PlayPart0()
	{
        musicPlayer.clip = part0;
        musicPlayer.Play();
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

    public float PlaySteps()
    {
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = playerRun;
        audioSource.Play();
        audioSource.volume = .2f;
        go.name = System.DateTime.Now.ToString();
        GameObject.Destroy(go, audioSource.clip.length + 0.10f);

        return playerRun.length;
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
        PlaySound(Typo, fxVolume);
	}

    public void CableBreak()
	{
        PlaySound(cableBreak, fxVolume);
	}

	#region PlayerSounds
	public void Jump()
	{
        PlaySound(playerJump, fxVolume);
	}

    public void Dash()
    {
        PlaySound(playerDash, fxVolume);
    }

    public void Land()
	{
        PlaySound(playerLand, fxVolume);
    }

    //Run
    public void PlayerRun()
	{
        PlaySound(playerRun, fxVolume);
	}

    public void Attack()
	{
        PlaySound(playerAttack, fxVolume);
	}
    public void PlayerHit()
    {
        PlaySound(playerHit, fxVolume);
    }

    #endregion

    #region Uranie

    public void UranieAttackCast()
    {
        PlaySound(uranieAttackCharge, fxVolume);
    }

    public void UranieAttackRelease()
    {
        PlaySound(uranieAttackRelease, fxVolume);
    }
    public void UranieAttackImpact()
    {
        PlaySound(uranieAttackImpact, fxVolume);
    }
    public void UranieHit()
	{
        PlaySound(uranieHit, fxVolume);
    }

    public void UranieDeath()
	{
        PlaySound(uranieDeath, fxVolume);
    }
	#endregion

	#region PNJ
	public void PnjHit()
	{
        PlaySound(pnjHit, fxVolume + .2f);
	}

    public void PnjAttack()
	{
        PlaySound(pnjAttack, fxVolume +.1f);
	}

    public void PnjDeath()
	{
        PlaySound(pnjDeath, fxVolume + .2f);
    }

    #endregion

    #region Hack

    public void HackSucces()
    {
        PlaySound(hackSucces, fxVolume + .2f);
    }

    public void HackFail()
    {
        PlaySound(hackFail, fxVolume +.2f);
    }

    public void HackFeedback()
    {
        PlaySound(hackFeedback, fxVolume +.2f);
    }

    #endregion
}
