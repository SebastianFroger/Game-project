using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform lookRotationTrs;
    public Transform lookAtarget;
    public bool stopped;

    private Vector3 _moveDir;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (unitStatsSO == null)
        {
            DebugExt.LogError(this, "Missing unitStatsSO");
        }
    }

    void Update()
    {
        _moveDir = GlobalObjectsManager.Instance.player.transform.position - transform.position + transform.position;
        if (_moveDir == Vector3.zero) return;
        _localMoveDir = lookAtarget.InverseTransformDirection(_moveDir);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        lookRotationTrs.LookAt(lookAtarget, transform.up);
    }

    void FixedUpdate()
    {
        if (stopped) return;
        _rb.MovePosition(_rb.position + (lookAtarget.position - transform.position).normalized * unitStatsSO.speed * Time.deltaTime);
    }
}