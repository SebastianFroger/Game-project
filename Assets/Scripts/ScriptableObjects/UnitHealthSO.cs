using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu]
    public class UnitHealthSO : ScriptableObject
    {
        public int currentHP;
        public int startHP;
        public int maxHP;
    }
}