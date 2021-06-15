using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

/* This class is used with the audio manager
 * for adding audio to the game scenes.
 */
[System.Serializable]
public class Audio
{
    /* Audio name, this is used for searching
     * the audio array in the audio manager.
     */
    public string audioName;

    /* The below four variables are passed
     * into the audio source that is created
     * from the audio manager.
     */
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float audioVolume;
    [Range(0.1f, 3f)]
    public float audioPitch;
    public bool isLooping;
    private AudioSource audioSource;

    /* Assigns volume, pitch, and looping
     * to the audio source component that is
     * created from the audio manager.
     */
    public void SetAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        
        this.audioSource.clip = audioClip;
        this.audioSource.volume = audioVolume;
        this.audioSource.pitch = audioPitch;
        this.audioSource.loop = isLooping;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}
