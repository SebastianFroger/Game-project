using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public PlayerStatsSO playerStatsSO;
    public float distance = 100f;

    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * playerStatsSO.speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distance)
            MyObjectPool.Instance.bullet.Release(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Health>()?.TakeDamage(playerStatsSO.dammage);
        MyObjectPool.Instance.bullet.Release(gameObject);
    }
}
