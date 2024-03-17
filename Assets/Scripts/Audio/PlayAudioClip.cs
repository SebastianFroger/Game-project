using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioClip : MonoBehaviour
{

    public AudioClip clip;
    public float volume = 1;
    public bool playOnEnable;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        if (playOnEnable)
            _audioSource.Play();
    }
}
