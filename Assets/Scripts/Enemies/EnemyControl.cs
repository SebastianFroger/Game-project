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
    Vector3 targetVector;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GlobalObjectsManager.Instance.player.transform;
        _speed = unitStatsSO.moveSpeed;
        targetVector = player.position;
    }

    private void OnEnable()
    {
        _speed = unitStatsSO.moveSpeed;
        _isKnockedBack = false;
        targetVector = GlobalObjectsManager.Instance.player.transform.position;
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


    }


    void FixedUpdate()
    {
        if (_isKnockedBack || stopped) return;

        _rb.MovePosition(_rb.position + transform.forward.normalized * unitStatsSO.moveSpeed * Time.fixedDeltaTime);
    }
}