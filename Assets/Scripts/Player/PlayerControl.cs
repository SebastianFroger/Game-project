using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public float jumpForce = 20;
    public LayerMask groundLayers;

    private Vector3 _inputDir;
    private Rigidbody _rb;
    private Vector3 _movePos;
    private bool onGround;
    private bool jumpPressed = false;

    void Start()
    {
        // _gravityBody = GetComponent<GravityBody>();
        _rb = GetComponent<Rigidbody>();
        _rb.MovePosition(Vector3.up * Planet.Instance.GetRadius());
    }

    // player movement
    void OnMove(InputValue value)
    {
        _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    void OnSecondary()
    {
        jumpPressed = true;
    }

    public void ResetMoveSpeed()
    {
        _inputDir = Vector3.zero;
    }

    void OnTogglePause()
    {
        GameManager.Instance.PauseMenuToggle();
    }

    void FixedUpdate()
    {
        // move battery
        if (!StatsManager.Instance.IsMoveBatteryEnough())
            return;

        _movePos = transform.rotation * _inputDir + transform.position;
        var dir = (_movePos - transform.position) * unitStats.moveSpeed;

        if (onGround && jumpPressed)
        {
            _rb.AddForce(dir, ForceMode.VelocityChange);
            _rb.AddForce(transform.position.normalized * jumpForce, ForceMode.Impulse);
            onGround = false;
        }

        if (!onGround) return;
        _rb.AddForce(dir - _rb.velocity, ForceMode.VelocityChange);

        // apply heat and battery cost
        if (_inputDir != Vector3.zero)
        {
            StatsManager.Instance.CalcMoveCost(Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (jumpPressed)
        {
            jumpPressed = false;
            return;
        }

        if ((groundLayers & (1 << other.gameObject.layer)) != 0)
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if ((groundLayers & (1 << other.gameObject.layer)) != 0)
        {
            onGround = false;
        }
    }

}