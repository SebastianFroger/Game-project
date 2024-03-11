using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlanetDiggerManager : Singleton<PlanetDiggerManager>
{
    public GameObject prefab;
    public UnityEvent OnDiggerLand;
    public UnityEvent OnNoDiggers;

    private RoundDataSO _roundDataSO;
    private List<float> _spawnTimes = new();
    private float _nextSpawTime = 0f;
    private int _spawnCount = 0;
    private int _diggersCount = 0;
    private bool _previouslyEnabled;

    public void SetRoundData(RoundDataSO roundDataSO)
    {
        _roundDataSO = roundDataSO;
        var round = _roundDataSO.roundDatas[_roundDataSO.currentRound];

        _spawnCount = 0;

        _spawnTimes.Clear();

        while (_spawnTimes.Count < round.planetDiggerCount)
        {
            _spawnTimes.Add(Random.Range(Time.time, Time.time + round.timeSec - 20f));
        }
        _spawnTimes.Sort();

        _nextSpawTime = Time.time + _spawnTimes[_spawnCount];
    }

    public void AddDigger()
    {
        _diggersCount += 1;
    }

    public void RemoveDigger()
    {
        _diggersCount -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        if (Time.time >= _nextSpawTime)
        {
            _nextSpawTime = Time.time + _spawnTimes[_spawnCount];
            InstantiateSpawnDigger();
        }

        if (_diggersCount > 0)
        {
            OnDiggerLand?.Invoke();
            _previouslyEnabled = true;
        }
        else
        {
            if (_previouslyEnabled)
                OnNoDiggers?.Invoke();
        }
    }

    void InstantiateSpawnDigger()
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
