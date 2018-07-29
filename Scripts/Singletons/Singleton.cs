using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 普通类单例接口
/// </summary>
/// <typeparam name="T">子类自身类型</typeparam>
public abstract class Singleton<T> : IReleaseCache where T : new()
{
    /// <summary>
    /// 单例
    /// </summary>
    private static T instance;
    /// <summary>
    /// 锁
    /// </summary>
    private static readonly object singletonLock = new object();
    /// <summary>
    /// 单利访问器
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (singletonLock)
            {
                if (instance == null)
                {
                    instance = new T();
                }
            }
            return instance;
        }
    }
    /// <summary>
    /// 释放资源
    /// </summary>
    public virtual void ReleaseCache()
    {
        Debug.Log("ReleaseCache:" + typeof(T));
    }
}
