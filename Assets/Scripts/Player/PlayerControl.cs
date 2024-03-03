using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter
{
    public class PlayerControl : MonoBehaviour
    {
        public float moveSpeed = 10f;

        private Vector3 _inputDir;
        private Rigidbody _rb;
        private Vector3 _movePos;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.MovePosition(Vector3.up * Planet.currentRadius);
        }

        void OnMove(InputValue value)
        {
            _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
        }

        void FixedUpdate()
        {
            _movePos = transform.rotation * _inputDir + transform.position;
            _rb.velocity = (_movePos - transform.position) * moveSpeed;
        }
    }
}