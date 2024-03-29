using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public UnitStatsSO playerStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;

    public float safeDistance = 5;
    public float maxPlayerDistance = 15f;
    public bool moveTowardsEnemy;
    public float lerpValue = 0.1f;

    private Vector3 _moveTowards;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private MiniRobotAttack _miniRobotAttack;
    private Vector3 _rotTowards;
    private UnitStatsSO unitStatsInstance;
    private Transform target;
    private float playerDistance;
    private float currTargetDistance;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _miniRobotAttack = GetComponentInChildren<MiniRobotAttack>();
    }

    void OnEnable()
    {
        unitStatsInstance = Instantiate(unitStatsSO);
    }

    void FixedUpdate()
    {
        if (stopped) return;

        // define target
        playerDistance = Vector3.Distance(transform.position, player.position);

        // move towards enemy if in range
        if (playerDistance < maxPlayerDistance && _miniRobotAttack.nearestEnemy != null)
            target = _miniRobotAttack.nearestEnemy;
        else
            target = player;

        // move
        _rotTowards = Vector3.RotateTowards(transform.position, target.position, .1f, .1f);
        _moveTowards = _rotTowards - transform.position;

        _localMoveDir = lookAtarget.InverseTransformDirection(_moveTowards);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        currTargetDistance = Vector3.Distance(transform.position, target.position) - safeDistance;
        currTargetDistance = Mathf.Clamp(currTargetDistance, -1.2f, 1.2f);
        _rb.velocity = Vector3.Slerp(_rb.velocity, _moveTowards.normalized * unitStatsInstance.moveSpeed.value * currTargetDistance, lerpValue);

        // player battery use
        playerStatsSO.currentMoveBattery.value -= unitStatsInstance.moveCostPerSecond.value * Time.fixedDeltaTime;
    }
}