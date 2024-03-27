using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;
    public float maxMagnitude = 1f;

    private Vector3 _moveTowards;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;
    private float _speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _speed = unitStatsSO.moveSpeed.value;
    }

    private void OnEnable()
    {
        _speed = unitStatsSO.moveSpeed.value;
    }

    public void SlowDown(float slowAmount)
    {
        _speed += slowAmount;
        if (_speed < 0f)
            _speed = 0f;
    }

    void FixedUpdate()
    {
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