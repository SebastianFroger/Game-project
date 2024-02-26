using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 1f;
    public Transform lookRotationTrs;
    public Transform lookAtarget;

    private Vector3 _moveDir;
    private Vector3 _localMoveDir;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _moveDir = ObjectManager.player.transform.position - transform.position + transform.position;
        if (_moveDir == Vector3.zero) return;
        _localMoveDir = lookAtarget.InverseTransformDirection(_moveDir);
        lookAtarget.localPosition = new Vector3(_localMoveDir.x, 0f, _localMoveDir.z);
        _rb.MovePosition(_rb.position + (lookAtarget.position - transform.position).normalized * speed * Time.deltaTime);
        lookRotationTrs.LookAt(lookAtarget, transform.up);
    }
}
