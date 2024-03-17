using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectLife : MonoBehaviour
{
    private ParticleSystem _particleEffect;
    void Awake()
    {
        _particleEffect = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _particleEffect.Play();
        MyObjectPool.Instance.Release(gameObject, _particleEffect.main.duration);
    }
}
