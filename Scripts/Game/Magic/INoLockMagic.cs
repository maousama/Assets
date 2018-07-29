using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class INoLockMagic : IMagic
{
    
    [HideInInspector]
    public Vector3 targetPos;
    [Space(10)]
    public float distance;

    [Tooltip("读秒过后自动消失，激发消失事件，产生销毁时创建的物体")]
    public float waitSecondForDestroy=100;

    [Tooltip("到达目的地后产生物体")]
    public GameObject CreateGoOnArrive;

    

    public Action OnArrive;
    


    protected override void Start()
    {
        base.Start();
        DestroyMagicEndFram(waitSecondForDestroy);
        targetPos = transform.forward * distance + transform.position;
        rig.velocity = (targetPos - transform.position).normalized * speed;
        CustomDefine();
    }

    protected override void Update()
    {
        base.Update();

        if (Vector3.Dot(targetPos-transform.position,transform.forward)<0)
        {
            DestroyMagicEndFram();
            if (CreateGoOnArrive != null)
            {
                CreateGo(CreateGoOnArrive);
            }
            OnArrive?.Invoke();
        } 
    }

    void CustomDefine()
    {
        
    }

}
