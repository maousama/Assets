using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    /// <summary>
    /// Resources资源缓存
    /// </summary>
    Hashtable resourcesCache = new Hashtable();


    #region Load 加载一个Resources资源
    /// <summary>
    /// 加载一个Resources资源
    /// </summary>
    /// <typeparam name="T">规定要得到的类型</typeparam>
    /// <param name="folderPath">文件夹路径</param>
    /// <param name="name">文件名</param>
    /// <param name="inCache">是否放入缓存</param>
    /// <returns></returns>
    public T Load<T>(string folderPath, string name, bool inCache = false) where T : Object
    {
        T t = null;
        if (resourcesCache.Contains(name))
        {
            t = resourcesCache[name] as T;
        }
        else
        {
            StringBuilder strPath = new StringBuilder();
            strPath.Append(folderPath);
            strPath.Append(name);
            if (Resources.Load(strPath.ToString()))
            {
                t = Resources.Load<T>(strPath.ToString());
                if (inCache)
                {
                    resourcesCache.Add(name, t);
                }
            }
            else
            {
                Debug.Log(strPath.ToString() + " Is Not Found");
                t = null;
            }
        }
        return t;
    }
    #endregion


    /// <summary>
    /// 释放资源
    /// </summary>
    public override void ReleaseCache()
    {
        base.ReleaseCache();
        resourcesCache.Clear();
    }

}
