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

        if (playerStatsSO.movementBattery <= 0)
        {
            return;
        }

        // define target
        playerDistance = Vector3.Distance(transform.position, player.position);

        // move towards enemy if in range
        if (playerDistance < maxPlayerDistance && _miniRobotAttack.target != null)
            target = _miniRobotAttack.target;
        else
            target = player;

        // move
        _moveTowards = (target.position - transform.position).normalized;
        currTargetDistance = Vector3.Distance(transform.position, target.position) - safeDistance;
        currTargetDistance = Mathf.Clamp(currTargetDistance, -1.2f, 1.2f);
        _rb.velocity = Vector3.Slerp(_rb.velocity, _moveTowards.normalized * unitStatsInstance.moveSpeed * currTargetDistance, lerpValue);

        // player battery use
        playerStatsSO.movementBattery -= (playerStatsSO.moveBatteryCostPerSecond * Time.fixedDeltaTime) / (playerStatsSO.numberOfAttackRobots * 2);
    }
}