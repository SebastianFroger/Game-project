using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PlayAtPoint()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
