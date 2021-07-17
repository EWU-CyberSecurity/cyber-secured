//FILE: AudioController.cs

//Purpose
//This script controls the audio

//FUNCTIONS
//
//Awake
//	INPUT ME
//
//Sound Click
//	INPUT ME
//
//SoundStartTalking
//	INPUT ME

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioController instance; //for singleton

    public AudioSource[] audio_sources;

    void Awake()
    {
        //singleton design pattern
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start()
	{
        //TODO: load up all audio sources into the array
	}

    public void SoundClick()
    {
        audio_sources[0].Play();
    }

    public void SoundStartTalking()
    {
        if(!audio_sources[1].isPlaying)
        {
            audio_sources[1].Play();
        }
    }

    public void SoundStopTalking()
    {
        audio_sources[1].Stop();
    }
}
