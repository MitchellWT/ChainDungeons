using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Audio manager, this component is used
 * for creating audio sources in the scene.
 * It also provides abilities for playing, pausing,
 * and stopping said audio sources.
 */
public class AudioManager : MonoBehaviour
{
    /* 'audioClips', stores all the audio
     * objects.
     */
    public Audio[] audioClips;
    /* Instance is used to ensure that
     * only one audio manager exists.
     * This is similar to a singleton pattern.
     */
    public static AudioManager instance;
    // Current scene build index.
    private int currentBuildIndex;
    // Updated when the end of level lever is activated.
    public bool leverPulled = false;

    void Awake()
    {
        /* This If statement ensures that only 
         * one audio manager instance exists.
         */
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            /* Ensures the 'leverPulled' is always false
             * at the start of the level.
             */
            instance.leverPulled = false;
            Destroy(gameObject);
            return;
        }

        // Makes the audio manager live after the scene.
        DontDestroyOnLoad(gameObject);

        /* Creates audio source for each audio object in 
         * the 'audioClips' array. These audio sources are attached
         * to the audio manager.
         */
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

    /* In the Update method a check for the current scene happens in 
     * order to play the right music for the level. This does not need to be
     * done in the update method. It could be added in the awake singleton If
     * statement. The current implementation is unnecessarily computationally
     * expensive.
     */
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

    // Searches the audio array and play the clip, If found.
    public void PlayClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Play();   
    }

    // Searches the audio array and checks If the clip us player, If found.
    public bool CheckIfPlaying(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);

        if (audio == null)
        {
            return false;
        }

        return audio.GetAudioSource().isPlaying;  
    }

    // Searches the audio array and pauses the clip, If found.
    public void PauseClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Pause();   
    }

    // Searches the audio array and unpauses the clip, If found.
    public void UnPauseClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);
        
        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().UnPause(); 
    }

    // Searches the audio array and stops the clip, If found.
    public void StopClip(string name)
    {
        Audio audio = Array.Find(audioClips, audio => audio.audioName == name);

        if (audio == null)
        {
            return;
        }

        audio.GetAudioSource().Stop(); 
    }

    // Stops all music in the game.
    public void StopMusic()
    {
        StopClip("track-one");
        StopClip("track-two");
        StopClip("track-three");
        StopClip("track-four");

        leverPulled = true;
    }

    // Checks If the music clips are playing.
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
