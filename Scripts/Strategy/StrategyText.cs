using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyText : MonoBehaviour
{

    private void Start()
    {

        UnitTest();


    }
    void UnitTest()
    {
        Context thecontext = new Context();
        thecontext.SetStrategy(new ConcreteStragegyA());
        thecontext.Sum();
        thecontext.SetStrategy(new ConcreteStragegyB());
        thecontext.Sum();
        thecontext.SetStrategy(new ConcreteStragegyC());
        thecontext.Sum();
    }
}
