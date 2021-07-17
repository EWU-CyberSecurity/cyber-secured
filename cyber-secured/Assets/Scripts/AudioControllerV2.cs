using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.SoundManagerNamespace;

public class AudioControllerV2 : MonoBehaviour
{
    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    // initialize
    void Start()
    {
        PlayMusic(0);
    }

    public void PlaySound(int index)
    {
        SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
    }

    public void PlayMusic(int index)
    {
        //sets looping music
        MusicAudioSources[index].PlayLoopingMusicManaged(0.5f, 0.5f, false);
    }
}