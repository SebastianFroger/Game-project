using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 50f;
        public int damage = 10;
        public float distance = 100f;

        private Vector3 startPosition;

        private void OnEnable()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(startPosition, transform.position) >= distance)
                MyObjectPool.bullet.Release(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            MyObjectPool.bullet.Release(gameObject);
        }
    }
}