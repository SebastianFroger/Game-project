using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu]
    public class PointSO : ScriptableObject
    {
        public GameObject prefab;
        public AudioClip audio;
        public int value = 1;
    }
}
