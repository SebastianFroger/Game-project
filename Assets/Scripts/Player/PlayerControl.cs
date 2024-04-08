using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public float jumpForce = 20;
    public LayerMask layerMask;
    public bool onGround;
    public static Vector3 lastInsideTrigger;

    Vector3 _inputDir;
    Rigidbody _rb;
    bool jumpPressed = false;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // player movement
    void OnMove(InputValue value)
    {
        _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    public void ResetMoveSpeed()
    {
        _inputDir = Vector3.zero;
    }

    void OnTogglePause()
    {
        GameManager.Instance.PauseMenuToggle();
    }

    private void Update()
    {
        Vector3 moveDir = _inputDir.normalized;
        Vector3 targetMoveAmount = moveDir * unitStats.moveSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);

        onGround = false;
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.1f, layerMask))
        {
            onGround = true;
        }

        if (onGround && jumpPressed)
        {
            _rb.AddForce(transform.up * jumpForce);
            jumpPressed = false;
        }

        // apply heat and battery cost
        if (_inputDir != Vector3.zero)
        {
            StatsManager.Instance.CalcMoveCost(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // move battery
        if (!StatsManager.Instance.IsMoveBatteryEnough())
            return;

        _rb.MovePosition(_rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InsidePlanetTrigger"))
        {
            lastInsideTrigger = other.ClosestPointOnBounds(transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InsidePlanetTrigger"))
        {
            lastInsideTrigger = other.ClosestPointOnBounds(transform.position);
        }
    }
}