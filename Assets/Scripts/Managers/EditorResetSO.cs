using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEngine;

public class EditorResetSO : MonoBehaviour
{
    public UnitHealthSO player;
    public IntVariable points;

    void OnDestroy()
    {
        player.currentHP = 0;
        points.Value = 0;
    }
}
