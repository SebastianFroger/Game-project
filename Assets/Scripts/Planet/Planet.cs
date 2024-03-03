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

        void Awake()
        {
            transform.localScale = new Vector3(startRadius, startRadius, startRadius);
            _shrinkVector = new Vector3(shrinkRateSec, shrinkRateSec, shrinkRateSec);
            _growVector = new Vector3(growRateSec, growRateSec, growRateSec);
            currentRadius = transform.localScale.x / 2;
        }

        void Update()
        {
            if (transform.localScale.x >= minRadius)
                transform.localScale -= _shrinkVector * Time.deltaTime;

            currentRadius = transform.localScale.x / 2;
        }

        public void Grow(float val)
        {

        }
    }
}