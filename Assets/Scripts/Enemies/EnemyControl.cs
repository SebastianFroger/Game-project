using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public LayerMask layerMask;
    public Transform unitMeshTrs;
    public float turnSpeed = 1;

    private Rigidbody _rb;
    private Transform player;
    private float _startSpeed;
    private bool _isKnockedBack;
    NavMeshAgent navMeshAgent;
    Quaternion targetRot;
    float _nextDestinationResetTime = 0f;
    EnemyAttack _enemyAttack;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _enemyAttack = GetComponent<EnemyAttack>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = _enemyAttack.attackRange;
        navMeshAgent.destination = player.position;
        _startSpeed = unitStatsSO.moveSpeed;
        navMeshAgent.speed = _startSpeed;
    }

    private void OnEnable()
    {
        navMeshAgent.speed = _startSpeed;
        _isKnockedBack = false;
    }

    private void Update()
    {
        // set destination
        navMeshAgent.destination = player.position;

        if (Time.time > _nextDestinationResetTime)
        {
            navMeshAgent.SetDestination(player.position);
            _nextDestinationResetTime = Time.time + 5f;
        }

        // player rotation to ground
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1f, layerMask))
        {
            targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        else
        {
            targetRot = Quaternion.FromToRotation(transform.up, new Vector3(0, 1, 0)) * transform.rotation;
        }

        unitMeshTrs.rotation = Quaternion.Slerp(unitMeshTrs.rotation, targetRot, turnSpeed * Time.deltaTime);
    }

    public void SlowDown(float slowAmount)
    {
        _startSpeed += slowAmount;
        if (_startSpeed < 0f)
            _startSpeed = 0f;
    }

    public void KnockBack(float force)
    {
        if (gameObject.activeSelf)
            StartCoroutine(KnockBackCoroutine(force));
    }

    IEnumerator KnockBackCoroutine(float force)
    {
        _isKnockedBack = true;
        _rb.velocity = (transform.position - player.position).normalized * force;
        yield return new WaitForSeconds(1);
        _isKnockedBack = false;
    }
}