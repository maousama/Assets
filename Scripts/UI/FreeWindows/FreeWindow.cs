using UnityEngine;
using System.Collections;

public abstract class FreeWindow : WindowBase
{
    protected override void OnAwake()
    {
        base.OnAwake();
        windowType = WindowType.FreeWindow;
    }
}
