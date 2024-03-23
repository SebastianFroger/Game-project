using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleEffectPlay : MonoBehaviour
{
    public GameObject effect;

    public void Play()
    {
        MyObjectPool.Instance.GetInstance(effect, transform.position, transform.rotation);
    }
}
