using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : Singleton<StateManager>
{
    ///// <summary>
    ///// 状态队列字典
    ///// </summary>
    //public Dictionary<string, Queue<IState>> stateQueueDic = new Dictionary<string, Queue<IState>>();

    ///// <summary>
    ///// 得到状态
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="owner"></param>
    ///// <returns></returns>
    //public T GetState<T>(ICharacter owner) where T : IState, new()
    //{
    //    T t;
    //    string stateName = typeof(T).ToString();
    //    if (!stateQueueDic.ContainsKey(stateName))
    //    {
    //        stateQueueDic.Add(stateName, new Queue<IState>());
    //    }
    //    if (stateQueueDic[stateName].Count > 0)
    //    {
    //        t = stateQueueDic[stateName].Dequeue() as T;
    //    }
    //    else
    //    {
    //        t = new T();
    //        t.InitState(owner);
    //    }
    //    return t;
    //}


    //public void PutState(IState state)
    //{
    //    if (!stateQueueDic.ContainsKey(state.Name))
    //    {
    //        stateQueueDic.Add(state.Name, new Queue<IState>());
    //    }
    //    stateQueueDic[state.Name].Enqueue(state);
    //}
}
