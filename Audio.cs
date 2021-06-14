using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string audioName;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float audioVolume;
    [Range(0.1f, 3f)]
    public float audioPitch;
    public bool isLooping;
    private AudioSource audioSource;

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
