using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MiniRobotAttack : MonoBehaviour
{
    public GameObject bullet;
    public UnitStatsSO unitStats;
    public UnitStatsSO playerStats;
    public List<Transform> _enemiesInRange = new();
    public UnityEvent OnShoot;
    public float laserRandomRange = 0.2f;
    public Transform target;

    private float _nextAttackTime;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;
    private UnitStatsSO unitStatsInstance;

    private void Start()
    {
        _enemiesInRange.Clear();
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
        if (target == enemy)
            target = null;
    }

    public void ResetEnemies()
    {
        _enemiesInRange.Clear();
        target = null;
    }

    private void Update()
    {
        if (playerStats.currentAttackBattery.value <= 0) return;

        if (Time.time < _nextAttackTime || _enemiesInRange.Count == 0) return;
        _nextAttackTime = Time.time + (1f / unitStatsInstance.attackSpeed.value);

        if (target == null)
            target = GetClosestEnemy();

        _bulletInst = MyObjectPool.Instance.GetInstance(bullet);
        _bulletInst.transform.localPosition = transform.position + new Vector3(Random.Range(-laserRandomRange, laserRandomRange), 0, 0);
        _bulletInst.transform.LookAt(target);

        playerStats.currentAttackBattery.value -= playerStats.attackCost.value / (playerStats.attackRobotCount.value * 2);

        OnShoot?.Invoke();
    }

    Transform GetClosestEnemy()
    {
        _smallestDistance = Mathf.Infinity;
        Transform closest = null;
        foreach (var enemy in _enemiesInRange)
        {
            closest = enemy;
            _distance = Vector3.Distance(transform.position, enemy.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
        }
        return closest;
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