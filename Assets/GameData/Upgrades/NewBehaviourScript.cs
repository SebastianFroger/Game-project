using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public PlayerStatsSO playerStatsSO;
    public UpgradeSO upgradeSO;

    private void Start()
    {
        upgradeSO.ApplyUpgrade(playerStatsSO);
    }
}
