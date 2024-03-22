using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public UnitStatsSO unitStats;

    private Vector3 _inputDir;
    private Rigidbody _rb;
    private Vector3 _movePos;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.MovePosition(Vector3.up * Planet.Instance.GetRadius());
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

    void FixedUpdate()
    {
        // move battery
        if (unitStats.currentMoveBattery.value <= 0)
            return;

        // move
        _movePos = transform.rotation * _inputDir + transform.position;
        _rb.velocity = (_movePos - transform.position) * unitStats.moveSpeed.value;

        // apply heat and battery cost
        if (_inputDir != Vector3.zero)
        {
            unitStats.currentHeat.value += unitStats.moveHeatCostPerSecond.value * Time.fixedDeltaTime;
            unitStats.currentMoveBattery.value -= unitStats.moveCostPerSecond.value * Time.fixedDeltaTime;
        }
    }
}