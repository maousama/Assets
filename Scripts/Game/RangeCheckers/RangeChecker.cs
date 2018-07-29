using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeChecker {

    protected const int DefualtLayer = int.MinValue;

    public RangeChecker(int checkLayerMask, Transform startTr)
    {
        this.checkLayerMask = checkLayerMask;
        this.startTr = startTr;
    }

    public int checkLayerMask;

    public Transform startTr;

    public abstract List<Transform> Check(int layerMask=DefualtLayer);



    public RangeCheckType checkType;

    public abstract void DrawCheckArea();
}
