using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Shooter
{

    public class Point : MonoBehaviour
    {
        public int value = 1;
        public IntVariable intVariable;
        public UnityEvent OnPickUpEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("PickUpCollider")) return;
            intVariable.ApplyChange(value);
            MyObjectPool.points.Release(gameObject);
            OnPickUpEvent.Invoke();
        }
    }
}
