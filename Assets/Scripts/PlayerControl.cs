using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1f;
    public Transform lookRotationTrs;
    public Transform lookAtarget;

    private Vector2 _value;
    private Vector3 _moveDir;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue value)
    {
        _value = value.Get<Vector2>();
        _moveDir = new Vector3(_value.x, 0f, _value.y);
    }

    void FixedUpdate()
    {
        if (_moveDir == Vector3.zero) return;
        _rb.MovePosition(_rb.position + transform.TransformDirection(_moveDir) * speed * Time.deltaTime);
        lookAtarget.localPosition = _moveDir;
        lookRotationTrs.LookAt(lookAtarget, transform.up);
    }
}
