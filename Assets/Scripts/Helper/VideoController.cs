﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public LevelLoader loader;
    public float startDelay = 1.5f;
    bool playing = false;

    public float timer;
    public VideoPlayer Player;

	private void Start()
	{
        timer = (float)Player.clip.length;
        //Player = GetComponent<VideoPlayer>();
        print(Player);
    }

    void Update()
    {
        startDelay -= Time.deltaTime;
        //GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.white, startDelay);

        if (startDelay <= 0 && !playing)
		{
            Player.Play();
            playing = true;
        }

        if (playing) timer -= Time.deltaTime;

        if (timer <= 0 && playing)
		{
            loader.OnNextLevel(1);
        }

        HandleInputs();
    }

    public void HandleInputs()
	{
        if (Input.GetKeyDown(KeyCode.M))
        {
            loader.OnNextLevel(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            loader.OnNextLevel(0);
        }

        if (Input.GetButtonDown("Attack"))
        {
            loader.OnNextLevel(1);
        }
    }
}
