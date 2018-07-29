using UnityEngine;
using System.Collections;

public abstract class FixedWindow : WindowBase
{
    protected override void OnAwake()
    {
        base.OnAwake();
        windowType = WindowType.FixedWindow;
    }

}
