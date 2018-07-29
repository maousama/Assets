using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxCastChecker : RangeChecker
{
    public float width, distance;

    public BoxCastChecker(float width, float distance, int checkLayerMask, Transform startTrans) : base(checkLayerMask, startTrans)
    {
        checkType = RangeCheckType.BoxCast;
        this.width = width;
        this.distance = distance;
    }

    public override List<Transform> Check(int layerMask = DefualtLayer)
    {
        int mask = layerMask == DefualtLayer ? this.checkLayerMask : layerMask;

        
        Quaternion rotation = startTr.rotation;
        Collider[] colliders = Physics.OverlapBox(startTr.position + startTr.forward * (distance / 2), new Vector3(width / 2, width / 2, distance / 2), rotation, mask);
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

        Vector3 rightHightPoint = startTr.position + startTr.right * width / 2 + startTr.up * width / 2;
        Vector3 rightLowPoint = startTr.position + startTr.right * width / 2 - startTr.up * width / 2;
        Vector3 leftHightPoint = startTr.position - startTr.right * width / 2 + startTr.up * width / 2;
        Vector3 leftLowPoint = startTr.position - startTr.right * width / 2 - startTr.up * width / 2;

        Vector3 farRightHightPoint = startTr.position + startTr.right * width / 2 + startTr.up * width / 2 + startTr.forward * distance;
        Vector3 farRightLowPoint = startTr.position + startTr.right * width / 2 - startTr.up * width / 2 + startTr.forward * distance;
        Vector3 farLeftHightPoint = startTr.position - startTr.right * width / 2 + startTr.up * width / 2 + startTr.forward * distance;
        Vector3 farLeftLowPoint = startTr.position - startTr.right * width / 2 - startTr.up * width / 2 + startTr.forward * distance;

        Gizmos.DrawLine(rightHightPoint, leftHightPoint);
        Gizmos.DrawLine(leftHightPoint, leftLowPoint);
        Gizmos.DrawLine(leftLowPoint, rightLowPoint);
        Gizmos.DrawLine(rightLowPoint, rightHightPoint);


        Gizmos.DrawLine(farRightHightPoint, farLeftHightPoint);
        Gizmos.DrawLine(farLeftHightPoint, farLeftLowPoint);
        Gizmos.DrawLine(farLeftLowPoint, farRightLowPoint);
        Gizmos.DrawLine(farRightLowPoint, farRightHightPoint);


        Gizmos.DrawLine(rightHightPoint, farRightHightPoint);
        Gizmos.DrawLine(rightLowPoint, farRightLowPoint);

        Gizmos.DrawLine(leftHightPoint, farLeftHightPoint);
        Gizmos.DrawLine(leftLowPoint, farLeftLowPoint);
    }
}
