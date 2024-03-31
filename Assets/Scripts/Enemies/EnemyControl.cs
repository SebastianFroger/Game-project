using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;

    private Vector3 _moveTowards;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private float _speed;
    private bool _isKnockedBack;

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

        var rotTowards = Vector3.RotateTowards(transform.position, player.position, .1f, .1f);
        _moveTowards = (rotTowards - transform.position);
        if (_moveTowards == Vector3.zero) return;

        _localMoveDir = lookAtarget.InverseTransformDirection(_moveTowards);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        if (stopped) return;
        _rb.velocity = _moveTowards.normalized * _speed;
    }
}