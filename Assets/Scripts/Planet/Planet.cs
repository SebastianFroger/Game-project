using Unity.VisualScripting;
using UnityEngine;

public class Planet : Singleton<Planet>
{

    public float GetRadius()
    {
        return transform.localScale.x / 2;
    }
}