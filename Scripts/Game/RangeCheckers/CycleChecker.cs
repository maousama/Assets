using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CycleChecker : RangeChecker
{

    public CycleChecker(float radius, int checkLayerMask, Transform startTrans) : base(checkLayerMask, startTrans)
    {
        checkType = RangeCheckType.Cycle;

        this.radius = radius;

    }

    private float radius;


    public override List<Transform> Check(int layerMask = DefualtLayer)
    {
        int mask = layerMask == DefualtLayer ? this.checkLayerMask : layerMask;

        Collider[] colliders = Physics.OverlapSphere(startTr.position, radius,mask);

        List<Transform> beHitedTrans = new List<Transform>();

        foreach (Collider col in colliders)
        {
            beHitedTrans.Add(col.transform);
        }

        if (beHitedTrans == null)
        {
            beHitedTrans = new List<Transform>();
        }

        return beHitedTrans;

    }

    public override void DrawCheckArea()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(startTr.position, radius);

    }
}
