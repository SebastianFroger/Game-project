using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public float moveForce;
    public bool stopped;
    public LayerMask layerMask;

    private Vector3 _moveDir;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private float _speed;
    private bool _isKnockedBack;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    GravityBody playerGravityBody;
    GravityBody gravityBody;
    Vector3 target;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        playerGravityBody = player.GetComponent<GravityBody>();
        gravityBody = GetComponent<GravityBody>();
        _speed = unitStatsSO.moveSpeed;
        target = player.position;
    }

    private void OnEnable()
    {
        _speed = unitStatsSO.moveSpeed;
        _isKnockedBack = false;
        target = GlobalObjectsManager.Instance.player.transform.position;
    }

    public void SlowDown(float slowAmount)
    {
        _speed += slowAmount;
        if (_speed < 0f)
            _speed = 0f;
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

    private void Update()
    {
        if (_isKnockedBack || stopped) return;

        target = player.position;
        DebugExt.Log(this, $"target player");
        if (playerGravityBody.isInside != gravityBody.isInside)
        {
            DebugExt.Log(this, $"target lastInsideTrigger");
            target = PlayerControl.lastInsideTrigger;
        }

        // var angleTowardsPlayer = Vector3.RotateTowards(transform.position, target, .1f, .1f);
        // _moveDir = (angleTowardsPlayer - transform.position).normalized;
        // Vector3 targetMoveAmount = _moveDir * unitStatsSO.moveSpeed;
        // moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);

        _moveDir = target - transform.position;
        _localMoveDir = transform.InverseTransformDirection(_moveDir).normalized;
        _localMoveDir = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        transform.LookAt(_localMoveDir, transform.up);

    }

    void FixedUpdate()
    {
        if (_isKnockedBack || stopped) return;

        _rb.MovePosition(_rb.position + transform.forward.normalized * unitStatsSO.moveSpeed * Time.fixedDeltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _moveDir);
    }
}