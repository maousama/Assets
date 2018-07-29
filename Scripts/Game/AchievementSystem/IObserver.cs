using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObserver {
    private ISubject subject;

    public ISubject Subject
    {
        get
        {
            return subject;
        }
        set
        {
            subject = value;
        }
    }

    public abstract void ObserveUpdate();
    
}
