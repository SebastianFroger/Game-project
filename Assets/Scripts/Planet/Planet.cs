using UnityEngine;

namespace Shooter
{
    public class Planet : MonoBehaviour
    {
        public float startRadius;
        public float shrinkRateSec;
        public float growRateSec;
        public float minRadius;
        public static float currentRadius;

        private Vector3 _shrinkVector;
        private Vector3 _growVector;
        private float _sizeTarget;
        private bool _isGrowing;

        void Awake()
        {
            transform.localScale = new Vector3(startRadius, startRadius, startRadius);
            _shrinkVector = new Vector3(shrinkRateSec, shrinkRateSec, shrinkRateSec);
            _growVector = new Vector3(growRateSec, growRateSec, growRateSec);
            currentRadius = transform.localScale.x / 2;
        }

        void Update()
        {
            if (_isGrowing)
            {
                if (transform.localScale.x < _sizeTarget)
                    transform.localScale += _growVector * Time.deltaTime;
                else
                    _isGrowing = false;
            }
            else
            {
                if (transform.localScale.x >= minRadius)
                    transform.localScale -= _shrinkVector * Time.deltaTime;
            }

            currentRadius = transform.localScale.x / 2;
        }

        public void Grow(float amount)
        {
            _sizeTarget = transform.localScale.x + amount;
            _isGrowing = true;
        }
    }
}