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

    private Vector3 _moveDir;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private float _speed;
    private bool _isKnockedBack;
    private bool onGround = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _speed = unitStatsSO.moveSpeed;
    }

    private void OnEnable()
    {
        _speed = unitStatsSO.moveSpeed;
        _isKnockedBack = false;
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
        _rb.velocity = (transform.position - GlobalObjectsManager.Instance.player.transform.position).normalized * force;
        yield return new WaitForSeconds(1);
        _isKnockedBack = false;
    }

    void FixedUpdate()
    {
        if (_isKnockedBack) return;

        if (!onGround)
            return;

        var angleTowardsPlayer = Vector3.RotateTowards(transform.position, player.position, .1f, .1f);
        _moveDir = (angleTowardsPlayer - transform.position).normalized * unitStatsSO.moveSpeed;

        _localMoveDir = lookAtarget.InverseTransformDirection(_moveDir);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        if (stopped) return;
        _rb.AddForce(_moveDir - _rb.velocity, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision other)
    {
        // if planet
        if (other.gameObject.CompareTag("Planet"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            onGround = false;
        }
    }
}