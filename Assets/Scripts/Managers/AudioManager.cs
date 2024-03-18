using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] musicClips;

    private AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = musicClips[Random.Range(0, musicClips.Length - 1)];
            _audioSource.Play();
        }
    }
}
