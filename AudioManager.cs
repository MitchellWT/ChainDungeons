using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Audio[] audioClips;
    public static AudioManager instance;
    private int currentBuildIndex;
    public bool leverPulled = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance.leverPulled = false;
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Audio audio in audioClips)
        {
            audio.SetAudioSource(gameObject.AddComponent<AudioSource>());
        }
    }

    void Start()
    {
        PlayClip("player-footsteps");
        PlayClip("dungeon-ambient");
    }

    private void Update()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (!leverPulled)
        {
            if (currentBuildIndex == 2 || currentBuildIndex == 3)
            {
                if (!CheckIfPlaying("track-four"))
                {
                    PlayClip("track-four");
                }
            }
            else if (currentBuildIndex == 4 || currentBuildIndex == 5)
            {
                if (!CheckIfPlaying("track-one"))
                {
                    PlayClip("track-one");
                }
            }
            else if (currentBuildIndex == 6 || currentBuildIndex == 7)
            {  
                if (!CheckIfPlaying("track-three"))
                {
                    PlayClip("track-three");
                }
            }
            else if (currentBuildIndex == 9 || currentBuildIndex == 10)
            {
                if (!CheckIfPlaying("track-two"))
                {
                    PlayClip("track-two");
                }
            }
        }
    }

    public void PlayClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Play();   
    }

    public bool CheckIfPlaying(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);

        if (audio == null)
        {
            return false;
        }

        return audio.GetAudioSource().isPlaying;  
    }

    public void PauseClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Pause();   
    }

    public void UnPauseClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().UnPause(); 
    }

    public void StopClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);

        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Stop(); 
    }

    public void StopMusic()
    {
        StopClip("track-one");
        StopClip("track-two");
        StopClip("track-three");
        StopClip("track-four");

        leverPulled = true;
    }

    public bool CheckIfMusicPlaying()
    {
        if (CheckIfPlaying("track-one") || CheckIfPlaying("track-one") 
        || CheckIfPlaying("track-one") || CheckIfPlaying("track-one"))
        {
            return true;
        }

        return false;
    }
}
