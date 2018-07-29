using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ISubject
{
   private List<IObserver> observerList = new List<IObserver>();
    private System.Object param = null; //事件发生附加的参数
    /// <summary>
    /// 注册添加观察者
    /// </summary>
    /// <param name="observer"></param>
    public void Attach(IObserver observer)
    {
        observerList.Add(observer);
    }
    /// <summary>
    /// 移除观察者
    /// </summary>
    /// <param name="observer"></param>
    public void Detach(IObserver observer)
    {
        observerList.Remove(observer);
    }
    /// <summary>
    /// 通知所有观察者
    /// </summary>
    public void Notify()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].ObserveUpdate(); 
        }
    }
    public virtual void SetParam(Object Param)
    {
        param = Param;
    }
}
