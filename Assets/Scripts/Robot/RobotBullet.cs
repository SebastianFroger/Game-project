using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


public class RobotBullet : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public UnitStatsSO playerStatsSO;
    public float distance = 100f;
    public int speed = 100;
    public GameObject hitEffect;
    public UnityEvent OnHitEvent;
    public LayerMask layerMask;
    public bool useRaycast;
    public float flashIntensity = 10f;
    public float normalLightIntensity = 1f;

    private Vector3 startPosition;
    private Vector3 _prevPosition;
    private IHealth _targetHP;
    Light _light;

    private void Start()
    {
        _light = GetComponentInChildren<Light>();
        normalLightIntensity = _light.intensity;
    }

    private void OnEnable()
    {
        startPosition = transform.position;
        _prevPosition = GlobalObjectsManager.Instance.player.transform.position;
    }

    private void Update()
    {
        _light.intensity = Mathf.Lerp(_light.intensity, normalLightIntensity, .7f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distance)
            MyObjectPool.Instance.Release(gameObject);

        if (!useRaycast) return;

        // Check if the bullet hit something, by using a linecast from previous position to current position
        if (Physics.Linecast(_prevPosition, transform.position, out RaycastHit hit, layerMask))
        {
            _targetHP = hit.collider.gameObject.GetComponent<IHealth>();

            // check if we hit environment
            if (_targetHP == null)
            {
                MyObjectPool.Instance.Release(gameObject);
                return;
            }

            _targetHP.TakeDamage(unitStatsSO.damage * playerStatsSO.numberOfAttackRobots);

            // hit effect
            MyObjectPool.Instance.GetInstance(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            // relsease bullet
            MyObjectPool.Instance.Release(gameObject);
        }

        _prevPosition = transform.position;
    }
}


