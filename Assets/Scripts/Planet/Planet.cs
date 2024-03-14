using Unity.VisualScripting;
using UnityEngine;

public class Planet : Singleton<Planet>
{
    public float startRadius;
    public float shrinkRate;
    public float minRadius;
    public TransformRuntimeSet harvestersSO;


    void Awake()
    {
        ResetScale();
    }

    void Update()
    {
        if (harvestersSO.Items.Count == 0) return;

        // shrink
        if (transform.localScale.x >= minRadius)
        {
            transform.localScale += new Vector3(shrinkRate, shrinkRate, shrinkRate) * Time.deltaTime;
        }
    }

    public float GetRadius()
    {
        return transform.localScale.x / 2;
    }

    public void ResetScale()
    {
        transform.localScale = new Vector3(startRadius, startRadius, startRadius);
    }
}