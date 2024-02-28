using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InAttackRange", menuName = "ScriptableObjects/InAttackRange", order = 1)]
public class InAttackRange : ScriptableObject
{
    public List<Transform> enemies;

    public void Add(Transform trs)
    {
        enemies.Add(trs);
    }

    public void Remove(Transform trs)
    {
        enemies.Remove(trs);
    }
}
