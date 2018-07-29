using UnityEngine;
using System.Collections;
using System;

//[RequireComponent()]
public abstract class IBuilding : MonoBehaviour
{
    public const float CannotWalkThrough = 1000;

    public abstract string Name { get; }

    public abstract float Weight { get; }

    private float hp = 1;
    /// <summary>
    /// 血量
    /// </summary>
    public float HP
    {
        get
        {
            return hp;
        }

        protected set
        {
            hp = value;
            if (hp < 1)
            {
                OnBroken();
                Destroy(gameObject);
            }
        }
    }


    /// <summary>
    /// 初始化所有属性
    /// </summary>
    protected abstract void InitAttributes();


    private void Awake()
    {
        InitAttributes();
        OnAwake();
    }

    protected abstract void OnAwake();


    private void Start()
    {
        OnStart();
    }

    protected abstract void OnStart();


    private void Update()
    {
        OnUpdate();
    }

    protected abstract void OnUpdate();

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected abstract void OnFixedUpdate();



    protected abstract void OnBroken();



}
