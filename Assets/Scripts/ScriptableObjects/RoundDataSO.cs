using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float spawnChance;
}

[CreateAssetMenu]
public class RoundDataSO : ScriptableObject
{
    public float timeCountDown;
    public int currentRound;
    public RoundData[] roundDatas;
}
