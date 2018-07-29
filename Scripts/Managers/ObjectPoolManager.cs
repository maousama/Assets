using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    /// <summary>
    /// 对象池列表哈希存储器
    /// </summary>
    private Hashtable objectPoolHashtable = new Hashtable();
    
        

    #region GetGameObject 得到物体
    /// <summary>
    /// 得到物体
    /// </summary>
    /// <param name="folderPath">文件夹路径</param>
    /// <param name="name">物体名称</param>
    /// <returns></returns>
    public GameObject GetGameObject(string folderPath, string name)
    {
        GameObject returnObj = null;
        if (objectPoolHashtable.Contains(name))
        {
            Queue<GameObject> queue = objectPoolHashtable[name] as Queue<GameObject>;
            if (queue.Count > 0)
            {
                returnObj = queue.Dequeue();
                returnObj.SetActive(true);
            }
            else
            {
                if (ResourcesManager.Instance.Load<GameObject>(folderPath, name, true) != null)
                {
                    returnObj = GameObject.Instantiate(ResourcesManager.Instance.Load<GameObject>(folderPath, name, true));
                }
            }
        }
        else
        {
            objectPoolHashtable.Add(name, new Queue<GameObject>());
            if (ResourcesManager.Instance.Load<GameObject>(folderPath, name, true) != null)
            {
                returnObj = GameObject.Instantiate(ResourcesManager.Instance.Load<GameObject>(folderPath, name, true));
            }
        }
        return returnObj;
    }
    #endregion


    #region PutGameObject 向对象池中放入物体
    /// <summary>
    /// 向对象池中放入物体
    /// </summary>
    /// <param name="gameObject">物体</param>
    public void PutGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
            StringBuilder realNameStringBuilder = new StringBuilder();
            StringBuilder last7Str = new StringBuilder();
            last7Str.Append(gameObject.name);
            realNameStringBuilder.Append(gameObject.name);
            if (gameObject.name.Length > 7)
            {
                last7Str.Remove(0, gameObject.name.Length - 7);
                if(last7Str.ToString() == "(Clone)")
                {
                    realNameStringBuilder.Remove(gameObject.name.Length - 7, 7);
                }
            }
            string realName = realNameStringBuilder.ToString();

            if (!objectPoolHashtable.Contains(realName))
            {
                objectPoolHashtable.Add(realName, new Queue<GameObject>());
            }
            (objectPoolHashtable[realName] as Queue<GameObject>).Enqueue(gameObject);
        }
    }
    #endregion



    /// <summary>
    /// 打印所有队列信息
    /// </summary>
    public void CheckAllQueue()
    {
        foreach (string key in objectPoolHashtable.Keys)
        {
            Debug.Log(string.Format("{0}-{1}-{2}", key, (objectPoolHashtable[key] as Queue<GameObject>).Count, objectPoolHashtable.Count));
        }
    }

    /// <summary>
    /// 释放内存
    /// </summary>
    public override void ReleaseCache()
    {
        base.ReleaseCache();
        objectPoolHashtable.Clear();
    }
}
