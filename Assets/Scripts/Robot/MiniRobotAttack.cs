using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MiniRobotAttack : MonoBehaviour
{
    public GameObject bullet;
    public UnitStatsSO unitStats;
    public List<Transform> _enemiesInRange = new();
    public UnityEvent OnShoot;
    public float laserRandomRange = 0.2f;


    private float _nextAttackTime;
    private Transform _nearestEnemy;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;
    private List<Transform> _targets;
    private UnitStatsSO unitStatsInstance;

    private void Start()
    {
        _enemiesInRange.Clear();
        _targets = new();
        unitStatsInstance = Instantiate(unitStats);
    }

    void OnEnable()
    {
        _enemiesInRange.Clear();
        unitStatsInstance = Instantiate(unitStats);
        Health.OnDeathEventDelegate += RemoveDeadEnemy;
    }

    void OnDisable()
    {
        Health.OnDeathEventDelegate -= RemoveDeadEnemy;
    }

    void RemoveDeadEnemy(Transform enemy)
    {
        _enemiesInRange.Remove(enemy.transform);
    }

    private void Update()
    {
        if (Time.time < _nextAttackTime || _enemiesInRange.Count == 0) return;
        _nextAttackTime = Time.time + (1f / unitStatsInstance.attackSpeed.value);


        SelectTargets();

        for (int i = 0; i < unitStatsInstance.laserCount.value; i++)
        {
            _bulletInst = MyObjectPool.Instance.GetInstance(bullet);
            _bulletInst.transform.localPosition = transform.position + new Vector3(Random.Range(-laserRandomRange, laserRandomRange), 0, 0);
            _bulletInst.transform.LookAt(_targets[i % _targets.Count]);
        }

        OnShoot?.Invoke();
    }

    void SelectTargets()
    {
        _targets.Clear();
        _targets.Add(GetClosestEnemy());
        if (unitStatsInstance.targetsCount.value > 1)
        {
            while (_targets.Count < unitStatsInstance.targetsCount.value)
            {
                _targets.Add(_enemiesInRange[Random.Range(0, _enemiesInRange.Count)]);
            }
        }
    }

    Transform GetClosestEnemy()
    {
        _nearestEnemy = null;
        _smallestDistance = Mathf.Infinity;
        foreach (var enemy in _enemiesInRange)
        {
            _distance = Vector3.Distance(transform.position, enemy.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
            _nearestEnemy = enemy;
        }

        return _nearestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            _enemiesInRange.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemiesInRange.Remove(other.transform);
    }
}