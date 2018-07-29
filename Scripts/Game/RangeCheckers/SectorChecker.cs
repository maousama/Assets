using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectorChecker : RangeChecker
{
    float angle, radius;

    public SectorChecker(float radius, float angle, int checkLayerMask, Transform startTrans) : base(checkLayerMask, startTrans)
    {
        checkType = RangeCheckType.Sector;

        this.radius = radius;
        this.angle = angle;
    }


    public override List<Transform> Check(int layerMask = DefualtLayer)
    {
        int mask = layerMask == DefualtLayer ? this.checkLayerMask : layerMask;

        Collider[] colliders = Physics.OverlapSphere(startTr.position, radius,mask);

        List<Transform> beHitedTrans = new List<Transform>();

        foreach (Collider col in colliders)
        {

            float angleBetween = Vector3.Angle(startTr.forward, col.transform.position - startTr.position);

            if (angleBetween < angle / 2)
            {
                beHitedTrans.Add(col.transform);
            }
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

        Vector3 dirLeft = Quaternion.AngleAxis(-angle / 2, startTr.up) * startTr.forward;
        Vector3 dirRight = Quaternion.AngleAxis(angle / 2, startTr.up) * startTr.forward;
        Vector3 dirTop = Quaternion.AngleAxis(angle / 2, startTr.right) * startTr.forward;
        Vector3 dirBottom = Quaternion.AngleAxis(-angle / 2, startTr.right) * startTr.forward;

        Gizmos.DrawLine(startTr.position, startTr.position + dirLeft * radius);
        Gizmos.DrawLine(startTr.position, startTr.position + dirRight * radius);
        Gizmos.DrawLine(startTr.position, startTr.position + dirTop * radius);
        Gizmos.DrawLine(startTr.position, startTr.position + dirBottom * radius);
        Gizmos.DrawWireSphere(startTr.position, radius);
    }
}
