using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.SoundManagerNamespace;

public class AudioControllerV2 : MonoBehaviour
{
    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    private readonly int GAME_MUSIC = 0;
    private readonly int QUIZ_MUSIC = 1;

    // initialize
    void Start()
    {
        PlayGameMusic();
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

    public void PlayQuizMusic()
    {
        MusicAudioSources[QUIZ_MUSIC].PlayLoopingMusicManaged(0.25f, 0.5f, false);
    }

    public void PlayGameMusic()
    {
        MusicAudioSources[GAME_MUSIC].PlayLoopingMusicManaged(0.25f, 0.5f, false);
    }
}