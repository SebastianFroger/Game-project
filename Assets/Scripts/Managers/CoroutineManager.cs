using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineManager : Singleton<CoroutineManager>
{
    public void StartCor(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}