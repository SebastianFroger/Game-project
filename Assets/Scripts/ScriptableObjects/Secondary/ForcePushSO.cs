using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ForcePushSO : UpgradeSO, ISecondary
{
    public LayerMask layerMask;
    public float force;
    public float size;
    public UnitStatsSO unitStats;
    public float batteryCost;
    public float upgradeAmount;
    public GameObject effect;

    public void Execute(Transform transform)
    {
        if (unitStats.currentShieldBattery.value < batteryCost) return;
        unitStats.currentShieldBattery.value -= batteryCost;

        MyObjectPool.Instance.GetInstance(effect, transform.position, Quaternion.identity);

        var colliders = Physics.OverlapSphere(transform.position, size, layerMask);
        foreach (var item in colliders)
        {
            CoroutineManager.Instance.StartCor(PushEnemy(item.gameObject, transform));
        }
    }

    public void Upgrade()
    {
        force += upgradeAmount;
    }

    IEnumerator PushEnemy(GameObject item, Transform transform)
    {
        var control = item.GetComponent<EnemyControl>();
        control.enabled = false;

        item.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 10);

        yield return new WaitForSeconds(1);

        control.enabled = true;
    }
}
