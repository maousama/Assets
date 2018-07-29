using UnityEngine;
using System.Collections;
using System;

public abstract class IStrategy
{
    public IStrategy(AI aI)
    {
        this.aI = aI;
    }
    private IStrategy() { }

    protected AI aI;

    public abstract StrategyType StrategyType { get; }

    public abstract void StrategyLoop();

    public void ChangeStrategy(IStrategy toStrategy)
    {
        aI.Strategy = toStrategy;
    }



}
