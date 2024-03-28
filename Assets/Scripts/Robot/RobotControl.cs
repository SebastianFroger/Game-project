using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;
    public float playerDistance = 5f;

    private Vector3 _moveTowards;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;
    private Transform player;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
    }

    private void OnEnable()
    {

    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.position) < playerDistance)
            return;
        var rotTowards = Vector3.RotateTowards(transform.position, player.position, .1f, .1f);
        _moveTowards = (rotTowards - transform.position);

        _localMoveDir = lookAtarget.InverseTransformDirection(_moveTowards);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        if (stopped) return;
        _rb.velocity = _moveTowards.normalized * unitStatsSO.moveSpeed.value;
    }
}