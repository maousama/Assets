using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基类
/// </summary>
public abstract class Strategy
{
    public abstract void AlgorithmInterface();
}


public class ConcreteStragegyA:Strategy
{
    public override void AlgorithmInterface()
    {
        Debug.Log("ConcreteStragegyA.AlgorithmInterface");
    }
}
public class ConcreteStragegyB : Strategy
{
    public override void AlgorithmInterface()
    {
        Debug.Log("ConcreteStragegyB.AlgorithmInterface");
    }
}
public class ConcreteStragegyC : Strategy
{
    public override void AlgorithmInterface()
    {
        Debug.Log("ConcreteStragegyC.AlgorithmInterface");
    }
}



public class Context
{
    Strategy m_strategy = null;
    public void SetStrategy(Strategy theStrategy)
    {
        m_strategy = theStrategy;
    }
    public void Sum()
    {
        m_strategy.AlgorithmInterface();
    }
}
