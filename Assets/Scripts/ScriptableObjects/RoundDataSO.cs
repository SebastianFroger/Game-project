using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class RoundData
{
    public int timeSec;
    public float spawnPrSec;
    public float spawnPrSecIncrement;
    public float maxspawnPrSec;
    public EnemyData[] enemies;
}

[Serializable]
public class EnemyData
{
    public GameObject prefab;
    [Range(0, 100)] public int spawnChance;
}

[CreateAssetMenu]
public class RoundDataSO : ScriptableObject
{
    public float timeCountDown;
    public int currentRound;
    public RoundData[] roundDatas;
}
