using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{

    [CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/Variables/IntVariable", order = 1)]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public int Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(IntVariable amount)
        {
            Value += amount.Value;
        }
    }
}