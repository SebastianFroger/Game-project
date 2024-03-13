using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlanetDiggerManager : Singleton<PlanetDiggerManager>
{
    public GameObject prefab;
    public UnityEvent OnDiggerLand;
    public UnityEvent OnNoDiggers;

    public RoundDataSO _roundDataSO;
    private float _nextSpawTime = 0f;
    private int _diggersCount = 0;
    private bool _previouslyEnabled;
    private RoundData _currentRound;

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
        if (_currentRound == null)
            _currentRound = _roundDataSO.roundDatas[_roundDataSO.currentRound];

        // when to spawn
        if (Time.time >= _nextSpawTime)
        {
            var roundData = _roundDataSO.roundDatas[_roundDataSO.currentRound];
            if (_nextSpawTime == 0)
            {
                _nextSpawTime = Time.time + Random.Range(Time.time + 1f, Time.time + (roundData.timeSec / roundData.planetDiggerCount));
                return;
            }

            InstantiateSpawnDigger();

            _nextSpawTime = Time.time + Random.Range(Time.time + 1f, Time.time + (roundData.timeSec / roundData.planetDiggerCount));
        }

        // events for nr of diggers
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
