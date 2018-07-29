using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteSubject : ISubject
{
    public string subjectState;

    public void SetState(string State)
    {
        subjectState = State;
        Notify();
    }
    public string GetState()
    {
        return subjectState;
    }


}
