using UnityEngine;

public class Planet : MonoBehaviour
{
    public float startRadius;
    public float shrinkRateSec;
    public float minRadius;
    public static float currentRadius;

    private Vector3 _shrinkVector;

    void Start()
    {
        transform.localScale = new Vector3(startRadius, startRadius, startRadius);
        _shrinkVector = new Vector3(shrinkRateSec, shrinkRateSec, shrinkRateSec);
    }

    void Update()
    {
        if (transform.localScale.x >= minRadius)
            transform.localScale -= _shrinkVector * Time.deltaTime;

        currentRadius = transform.localScale.x / 2;
    }

    void Grow(float amount)
    {
        transform.localScale += new Vector3(amount, amount, amount);
    }
}
