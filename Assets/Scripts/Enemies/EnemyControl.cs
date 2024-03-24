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

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (unitStatsSO == null)
        {
            DebugExt.LogError(this, "Missing unitStatsSO");
        }

        player = GlobalObjectsManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
        var rotTowards = Vector3.RotateTowards(transform.position, player.position, .1f, .1f);
        _moveTowards = (rotTowards - transform.position);
        // _moveTowards = GlobalObjectsManager.Instance.player.transform.position - transform.position + transform.position;
        if (_moveTowards == Vector3.zero) return;

        _localMoveDir = lookAtarget.InverseTransformDirection(_moveTowards);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);

        if (stopped) return;
        // _rb.velocity = (_moveTowards);
        _rb.velocity = _moveTowards.normalized * unitStatsSO.moveSpeed.value;

        Debug.DrawRay(transform.position, _moveTowards, Color.red);
    }
}