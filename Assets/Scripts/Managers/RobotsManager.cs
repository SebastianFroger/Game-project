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

            var randomVector = playerTrs.rotation.eulerAngles + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized * 10;
            var posAroundPlayer = Quaternion.Euler(randomVector) * new Vector3(0, Planet.Instance.GetRadius(), 0);
            var robot = MyObjectPool.Instance.GetInstance(robotPrefab, posAroundPlayer, Quaternion.identity);

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
