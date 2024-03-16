using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class PlanetDiggerManager : Singleton<PlanetDiggerManager>
{
    public RoundDataSO _roundDataSO;
    public TransformRuntimeSet transformRuntimeSet;
    public GameObject prefab;
    public UnityEvent OnDiggerSpawned;

    private float _nextSpawTime = Mathf.Infinity; // avoid spawning before first round
    private int _diggersInstCount = 0;
    private RoundData _currRoundData;

    public void SetupRound()
    {
        _currRoundData = _roundDataSO.roundDatas[_roundDataSO.currentRound];
        _diggersInstCount = 0;
        _nextSpawTime = Time.time + _currRoundData.timeSec / (_currRoundData.planetDiggerCount + 1);
        transformRuntimeSet.Items.Clear();
    }

    void Update()
    {
        // when to spawn
        if (Time.time >= _nextSpawTime && _diggersInstCount < _currRoundData.planetDiggerCount)
        {
            InstantiateDigger();
            _diggersInstCount += 1;
            OnDiggerSpawned?.Invoke();
            _nextSpawTime = Time.time + Random.Range(1f, _currRoundData.timeSec / (_currRoundData.planetDiggerCount + 1));
        }
    }

    void InstantiateDigger()
    {
        // find position
        var center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Planet.Instance.GetRadius();
        var collisions = Physics.OverlapSphere(center, 5f, 1 << 11);
        while (collisions.Length > 0)
        {
            center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Planet.Instance.GetRadius();
            collisions = Physics.OverlapSphere(center, 5f, 1 << 11);
        }

        // spawn
        var inst = MyObjectPool.Instance.GetInstance(prefab);
        inst.transform.position = center * 10;
    }
}
