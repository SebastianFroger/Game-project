using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsManager : Singleton<RobotsManager>
{
    public UnitStatsSO unitStatsSO;
    public GameObject robotPrefab;

    private List<GameObject> robots = new();

    public void InstantiateRobots()
    {
        StartCoroutine(InstantiateRobotsRO());
    }

    IEnumerator InstantiateRobotsRO()
    {
        yield return new WaitForSecondsRealtime(1f);

        var playerTrs = GlobalObjectsManager.Instance.player.transform;
        for (int i = 0; i < unitStatsSO.numberOfAttackRobots; i++)
        {
            var randomVector = playerTrs.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized * 10;
            var robot = MyObjectPool.Instance.GetInstance(robotPrefab, randomVector, Quaternion.identity);

            robots.Add(robot);
        }
    }

    // reset robots enemies in attack range
    public void ResetRobots()
    {
        foreach (var robot in robots)
        {
            var miniRobotAttack = robot.GetComponentInChildren<MiniRobotAttack>();
            miniRobotAttack.ResetEnemies();
        }
    }
}
