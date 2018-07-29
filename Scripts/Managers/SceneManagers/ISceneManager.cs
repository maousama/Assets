using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISceneManager : MonoBehaviour
{

    public abstract void InitUI();



    private void Awake()
    {
        InitUI();
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
}
