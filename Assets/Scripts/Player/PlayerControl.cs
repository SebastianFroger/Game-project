using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public float jumpSpeed = 20;
    public float fallForceIncrese = 1;
    public LayerMask layerMask;
    public float gravity = -9.81f;
    public float turnSpeed = 1;
    public Transform playerMeshTrs;
    public float slopeForce;
    public float slopeForceRayLength;

    Vector3 _inputDir;
    CharacterController _controller;
    Vector3 moveVelocity;
    bool jumpPressed;
    Quaternion targetRot;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // player movement
    void OnMove(InputValue value)
    {
        _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    void OnJump()
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

    bool OnSlope()
    {
        if (jumpPressed) return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _controller.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        // move battery
        if (!StatsManager.Instance.IsMoveBatteryEnough())
            return;

        // apply heat and battery cost
        if (_inputDir != Vector3.zero)
        {
            StatsManager.Instance.CalcMoveCost(Time.deltaTime);
        }

        // apply movement
        if (_controller.isGrounded)
        {
            moveVelocity = _inputDir * unitStats.moveSpeed;
            if (jumpPressed)
            {
                moveVelocity.y = jumpSpeed;
                jumpPressed = false;
            }
        }
        else    // fall acc
        {
            var velY = moveVelocity.y;
            moveVelocity = _inputDir * unitStats.moveSpeed;
            moveVelocity.y = velY;
            moveVelocity.y += gravity * fallForceIncrese * Time.deltaTime;

            targetRot = Quaternion.FromToRotation(transform.up, new Vector3(0, 1, 0)) * transform.rotation;
        }

        moveVelocity.y += gravity * Time.deltaTime;
        _controller.Move(moveVelocity * Time.deltaTime);

        // slope force
        if ((_inputDir.x != 0 || _inputDir.z != 0) && OnSlope())
        {
            _controller.Move(Vector3.down * _controller.height / 2 * slopeForce * Time.deltaTime);
        }

        // player rotation to ground
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.5f, layerMask))
        {
            targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        playerMeshTrs.rotation = Quaternion.Slerp(playerMeshTrs.rotation, targetRot, turnSpeed * Time.deltaTime);
    }
}