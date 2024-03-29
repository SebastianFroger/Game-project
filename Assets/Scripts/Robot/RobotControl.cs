using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;
    public float targetDistance = 5f;
    public float maxPlayerDistance = 15f;
    public bool moveTowardsEnemy;

    private Vector3 _moveTowards;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private MiniRobotAttack _miniRobotAttack;
    private Vector3 _rotTowards;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _miniRobotAttack = GetComponentInChildren<MiniRobotAttack>();
    }

    void FixedUpdate()
    {
        if (moveTowardsEnemy && _miniRobotAttack.nearestEnemy != null && Vector3.Distance(transform.position, player.position) < maxPlayerDistance)
        {
            if (Vector3.Distance(transform.position, _miniRobotAttack.nearestEnemy.position) < targetDistance)
                return;
            _rotTowards = Vector3.RotateTowards(transform.position, _miniRobotAttack.nearestEnemy.position, .1f, .1f);
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) < targetDistance)
                return;
            _rotTowards = Vector3.RotateTowards(transform.position, player.position, .1f, .1f);
        }

        _moveTowards = _rotTowards - transform.position;
        _localMoveDir = lookAtarget.InverseTransformDirection(_moveTowards);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        if (stopped) return;
        _rb.velocity = _moveTowards.normalized * unitStatsSO.moveSpeed.value;
    }
}