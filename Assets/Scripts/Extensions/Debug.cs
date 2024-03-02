using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugExt
{
    public static void LogError(MonoBehaviour mono, object msg)
    {
        UnityEngine.Debug.LogError($"{mono.gameObject.name} - {mono.GetType().Name} - {msg}");
    }

    public static void Log(MonoBehaviour mono, object msg)
    {
        UnityEngine.Debug.Log($"{mono.gameObject.name} - {mono.GetType().Name} - {msg}");
    }
}
