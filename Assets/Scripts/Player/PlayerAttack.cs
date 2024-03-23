using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public UnitStatsSO unitStats;
    public TransformRuntimeSet _enemiesInRange;
    public UnityEvent OnShoot;


    private float _nextAttackTime;
    private Transform _nearestEnemy;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;
    [SerializeField] private bool _ignoreBatteryCost;

    private void Start()
    {
        _enemiesInRange.Items.Clear();
    }

    private void Update()
    {
        if (Time.time < _nextAttackTime || _enemiesInRange.Items.Count == 0) return;
        _nextAttackTime = Time.time + (1f / unitStats.attackSpeed.value);

        // attack battery
        if (!_ignoreBatteryCost && unitStats.currentAttackBattery.value <= 0)
            return;

        // find nearest enemy
        _nearestEnemy = null;
        _smallestDistance = Mathf.Infinity;
        foreach (var e in _enemiesInRange.Items)
        {
            _distance = Vector3.Distance(transform.position, e.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
            _nearestEnemy = e;
        }

        // fire
        if (_nearestEnemy == null) return;
        _bulletInst = MyObjectPool.Instance.GetInstance(bullet);
        _bulletInst.transform.position = transform.position;
        _bulletInst.transform.LookAt(_nearestEnemy);

        // heat
        unitStats.currentHeat.value += unitStats.attackHeatCostPerShot.value;

        // attack battery cost
        if (unitStats.currentAttackBattery.value >= unitStats.attackCost.value)
            unitStats.currentAttackBattery.value -= unitStats.attackCost.value;

        OnShoot?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemiesInRange.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemiesInRange.Remove(other.transform);
    }
}