using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// 当前操作窗体
    /// </summary>
    public WindowBase currentWindow;
    /// <summary>
    /// 画布组件
    /// </summary>
    private Transform canvas;
    public Transform Canvas
    {
        get
        {
            GameObject canvasAndEventSystem = GameObject.Find("CanvasAndEventSystem");
            if (canvasAndEventSystem == null)
            {
                canvasAndEventSystem = GameObject.Find("CanvasAndEventSystem(Clone)");
                if (canvasAndEventSystem == null)
                {
                    GameObject cAERes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.UI, "CanvasAndEventSystem");
                    canvas = GameObject.Instantiate(cAERes).transform.Find("Canvas");
                    GameObject.DontDestroyOnLoad(cAERes);
                }
            }
            else
            {
                canvas = canvasAndEventSystem.transform.Find("Canvas");
            }
            return canvas;
        }
    }

    /// <summary>
    /// 窗口字典
    /// </summary>
    private Dictionary<string, WindowBase> windowsDic = new Dictionary<string, WindowBase>();



    /// <summary>
    /// 创建或展示窗口
    /// </summary>
    public WindowBase CreateOrShowWindow(string windowName, Transform parent)
    {
        WindowBase retWindowBase = null;
        if (windowsDic.ContainsKey(windowName))
        {
            windowsDic[windowName].gameObject.SetActive(true);
            if (windowsDic[windowName].transform.parent != parent)
            {
                windowsDic[windowName].transform.SetParent(parent);
            }
            retWindowBase = windowsDic[windowName];
        }
        else
        {
            GameObject windowGameObjectRes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Windows, windowName);
            if (windowGameObjectRes != null)
            {
                WindowBase windowBase = GameObject.Instantiate(windowGameObjectRes, parent).GetComponent<WindowBase>();
                if (windowBase != null)
                {
                    windowsDic.Add(windowName, windowBase);
                    retWindowBase = windowBase;
                }
                else
                {
                    Debug.Log("Without WindowBase On " + windowName);
                }
            }
            else
            {
                Debug.Log("Cant Find Window Resources :" + windowName);
            }
        }
        return retWindowBase;
    }
    /// <summary>
    /// 隐藏窗体
    /// </summary>
    /// <param name="windowName"></param>
    public void HideWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            if (windowsDic[windowName].gameObject.activeSelf)
            {
                windowsDic[windowName].gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }
    /// <summary>
    /// 关闭窗体
    /// </summary>
    public void CloseWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            if (windowsDic[windowName].GetComponentsInChildren<WindowBase>().Length > 1)
            {
                Debug.Log("Please Delete Children Firstly");
            }
            else
            {
                WindowBase deleteWindow = windowsDic[windowName];
                windowsDic.Remove(windowName);
                GameObject.Destroy(deleteWindow.gameObject);
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }
}
