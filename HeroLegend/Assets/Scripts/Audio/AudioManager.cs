using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        FXEvent.onEventRaised += OnFXEvent;
        BGMEvent.onEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvent.onEventRaised -= OnFXEvent;
        BGMEvent.onEventRaised -= OnBGMEvent;

    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }


}
