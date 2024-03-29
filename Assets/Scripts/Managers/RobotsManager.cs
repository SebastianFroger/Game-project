using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsManager : Singleton<RobotsManager>
{
    public UnitStatsSO unitStatsSO;
    public GameObject robotPrefab;

    private List<RobotControl> robots = new List<RobotControl>();

    public void InstantiateRobots()
    {
        StartCoroutine(InstantiateRobotsRO());
    }

    IEnumerator InstantiateRobotsRO()
    {
        yield return new WaitForSecondsRealtime(1f);

        var playerTrs = GlobalObjectsManager.Instance.player.transform;
        for (int i = 0; i < unitStatsSO.attackRobotCount.value; i++)
        {

            var randomVector = playerTrs.rotation.eulerAngles + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized * 10;
            var posAroundPlayer = Quaternion.Euler(randomVector) * new Vector3(0, Planet.Instance.GetRadius(), 0);
            var robot = MyObjectPool.Instance.GetInstance(robotPrefab, posAroundPlayer, Quaternion.identity);

            robots.Add(robot.GetComponent<RobotControl>());
        }
    }


    public void StopAllRobots()
    {
        foreach (var robot in robots)
        {
            robot.stopped = true;
        }
    }

    public void StartAllRobots()
    {
        foreach (var robot in robots)
        {
            robot.stopped = false;
        }
    }
}
