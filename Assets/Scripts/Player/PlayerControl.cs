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
        _rb.MovePosition(Vector3.up * Planet.currentRadius);
    }

    // player movement
    void OnMove(InputValue value)
    {
        _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    void OnTogglePause()
    {
        GameManager.Instance.PauseMenuToggle();
    }

    void FixedUpdate()
    {
        _movePos = transform.rotation * _inputDir + transform.position;
        _rb.velocity = (_movePos - transform.position) * unitStats.speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            GameManager.Instance.ShopMenuOpen();
        }
    }
}