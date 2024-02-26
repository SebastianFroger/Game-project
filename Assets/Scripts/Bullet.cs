using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{transform.name} hit by bullet");
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        MyObjectPool.bullet.Release(gameObject);
    }
}
