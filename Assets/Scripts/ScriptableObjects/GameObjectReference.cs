using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu]
    public class GameObjectReference : ScriptableObject
    {
        public GameObject reference;
    }
}