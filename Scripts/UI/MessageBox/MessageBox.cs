using UnityEngine;
using System.Collections;

public abstract class MessageBox : WindowBase
{
    protected override void OnAwake()
    {
        base.OnAwake();
        windowType = WindowType.MessageBox;
    }

}
