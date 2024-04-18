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
    Attack _enemyAttack;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _enemyAttack = GetComponent<Attack>();

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
        navMeshAgent.enabled = true;
        BridgeTimer.onBridgeDestroyed += AgentOnNavmeshCheckEvent;
    }

    private void OnDisable()
    {
        BridgeTimer.onBridgeDestroyed -= AgentOnNavmeshCheckEvent;
    }

    private void Update()
    {
        if (!navMeshAgent.enabled)
            return;

        if (_isKnockedBack)
            return;

        if (_enemyAttack.inAttackRange || _enemyAttack.armed)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

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

    private void AgentOnNavmeshCheckEvent()
    {
        AgentOnNavmeshCheck();
    }

    private bool AgentOnNavmeshCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1f, layerMask))
        {
            return true;
        }

        navMeshAgent.enabled = false;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        return false;
    }

    public void SlowDown(float slowAmount)
    {
        _startSpeed -= slowAmount;
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
        _rb.isKinematic = false;
        navMeshAgent.enabled = false;
        _rb.velocity = (transform.position - player.position).normalized * force;
        yield return new WaitForSeconds(.5f);

        _rb.isKinematic = true;

        if (AgentOnNavmeshCheck())
        {
            navMeshAgent.enabled = true;
        }

        _isKnockedBack = false;
    }
}