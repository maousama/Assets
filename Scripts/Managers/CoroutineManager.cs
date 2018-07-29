using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoSingleton<CoroutineManager>
{
    public void StartOneCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    protected override void OnAwake()
    {

    }

    protected override void OnFixedUpdate()
    {

    }

    protected override void OnLateUpdate()
    {

    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {

    }
}
