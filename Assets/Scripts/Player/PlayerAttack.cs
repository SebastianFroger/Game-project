using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public UnitStatsSO unitStats;
    public TransformRuntimeSet _enemiesInRange;
    public UnityEvent OnShoot;
    public float laserRandomRange = 0.3f;


    private float _nextAttackTime;
    private Transform _nearestEnemy;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;
    private List<Transform> _targets;

    private void Start()
    {
        _enemiesInRange.Items.Clear();
        _targets = new();
    }

    private void Update()
    {
        if (Time.time < _nextAttackTime || _enemiesInRange.Items.Count == 0) return;

        // check if enough attack battery
        if (!StatsManager.Instance.CanAttack())
            return;

        _nextAttackTime = StatsManager.Instance.NextAttackTime();

        // select targets
        if (_enemiesInRange.Items.Count == 0)
            return;
        SelectTargets();

        for (int i = 0; i < unitStats.lasersPerShot; i++)
        {
            transform.localPosition = new Vector3(Random.Range(-laserRandomRange, laserRandomRange), 0, 0);
            _bulletInst = MyObjectPool.Instance.GetInstance(bullet);
            _bulletInst.transform.position = transform.position;
            _bulletInst.transform.LookAt(_targets[i % _targets.Count].position + new Vector3(0, .5f, 0));
        }


        // lasers in all directions
        if (unitStats.quadLaserCount > 0)
            LaserCircle();

        OnShoot?.Invoke();

    }

    void SelectTargets()
    {
        _targets.Clear();
        _targets.Add(GetClosestEnemy());
        if (unitStats.numberOfTargets > 1)
        {
            while (_targets.Count < unitStats.numberOfTargets)
            {
                _targets.Add(_enemiesInRange.Items[Random.Range(0, _enemiesInRange.Items.Count)]);
            }
        }
    }

    Transform GetClosestEnemy()
    {
        _nearestEnemy = null;
        _smallestDistance = Mathf.Infinity;
        foreach (var e in _enemiesInRange.Items)
        {
            _distance = Vector3.Distance(transform.position, e.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
            _nearestEnemy = e;
        }

        return _nearestEnemy;
    }

    void LaserCircle()
    {
        if (!StatsManager.Instance.CanAttack())
            return;
        var rotIncrement = 360 / (unitStats.quadLaserCount * 4);
        for (int i = 0; i < unitStats.quadLaserCount * 4; i++)
        {
            _bulletInst = MyObjectPool.Instance.GetInstance(bullet);
            _bulletInst.transform.localPosition = transform.position;
            _bulletInst.transform.rotation = transform.rotation;
            _bulletInst.transform.Rotate(new Vector3(0, rotIncrement * i, 0), Space.Self);
        }
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