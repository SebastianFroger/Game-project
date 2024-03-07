using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float startRadius;
    public float shrinkRate;
    public float growkRate;
    public float minScale;
    public static float currentRadius;

    private Vector3 _scaleVector;
    private Vector3 _targetScale;
    private Vector3 _minScale;
    private float _currentScaleX;

    void Awake()
    {
        // start scale
        transform.localScale = new Vector3(startRadius, startRadius, startRadius);

        // min scale
        _minScale = new Vector3(minScale, minScale, minScale);

        // scale vector
        _scaleVector = new Vector3(shrinkRate, shrinkRate, shrinkRate);

        // set current radius for other scripts to use
        currentRadius = transform.localScale.x / 2;
    }

    void Update()
    {
        _currentScaleX = transform.localScale.x;

        // shrink
        if (_currentScaleX >= _targetScale.x)
        {
            _scaleVector = new Vector3(shrinkRate, shrinkRate, shrinkRate);
            _targetScale = _minScale;
        }

        // grow
        if (_currentScaleX <= _targetScale.x)
        {
            _scaleVector = new Vector3(growkRate, growkRate, growkRate);
        }

        // do nothing 
        if (_currentScaleX <= _minScale.x && _targetScale == _minScale)
            return;

        // change scale
        transform.localScale += _scaleVector;

        currentRadius = _currentScaleX / 2;
    }

    public void Grow(int val)
    {
        _targetScale = transform.localScale + new Vector3(val, val, val);
    }
}