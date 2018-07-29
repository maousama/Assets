using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class LoadingWindow : FixedWindow
{
    public override string Name
    {
        get
        {
            return "LoadingWndow";
        }
    }

    /// <summary>
    /// 异步对象
    /// </summary>
    private AsyncOperation async;
    /// <summary>
    /// 场景加载进度
    /// </summary>
    private int loadingProgress = 0;
    /// <summary>
    /// 进度条进度
    /// </summary>
    private float slowProgress = 0;
    /// <summary>
    /// 将跳转过去的场景名称
    /// </summary>
    public static string nextSceneName;
    /// <summary>
    /// 进度条进度属性
    /// </summary>
    public float SlowProgress
    {
        get
        {
            return slowProgress;
        }

        set
        {
            slowProgress = value;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(loadSceneEnumerator(nextSceneName));
    }


    private void Update()
    {
        Debug.Log(SlowProgress);
        //得到真实的进度
        loadingProgress = (int)(async.progress * 100);
        //如果真实进度>80则视为加载完毕
        if (loadingProgress > 88)
        {
            loadingProgress = 100;
        }
        //只要进度条值小于真实进度则前进
        if (SlowProgress <= loadingProgress)
        {
            SlowProgress += Time.deltaTime * 30;
            imageDic["Image_Bar"].fillAmount = slowProgress * 0.01f;
            //Bar.fillAmount = slowProgress * 0.01f;
            //LoadingText.text = slowProgress.ToString() + "%";
        }
        //如果进度条大与等于100时候则开放跳转
        if (SlowProgress >= 100)
        {
            if (async != null)
            {
                async.allowSceneActivation = true;
            }
        }
    }


    //注意这里返回值一定是 IEnumerator
    private IEnumerator loadSceneEnumerator(string sceneName)
    {
        //异步读取场景，并将此操作赋值给异步对象。
        async = SceneManager.LoadSceneAsync(sceneName);
        //读取完毕后禁止跳转场景
        async.allowSceneActivation = false;

        yield return null;

    }

    private IEnumerator loadSceneEnumerator1(string sceneName)
    {
        //异步读取场景，并将此操作赋值给异步对象。
        async = SceneManager.LoadSceneAsync(sceneName);
        //读取完毕后返回， 系统会自动进入C场景
        yield return null;
    }
}
