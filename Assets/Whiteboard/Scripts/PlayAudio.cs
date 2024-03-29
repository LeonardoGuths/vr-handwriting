using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void PlayTheAudio()
    {
        audioData.Play(0);
    }
}