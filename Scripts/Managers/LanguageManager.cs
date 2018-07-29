using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class LanguageManager : Singleton<LanguageManager>
{

    public Dictionary<string, Hashtable> LanguageHashDic = new Dictionary<string, Hashtable>();
    /// <summary>
    /// 语言管理初始化
    /// </summary>
    public LanguageManager()
    {
        LoadLanguageToCache();
    }


    /// <summary>
    /// 加载Xml文件将信息存入对应哈希表（哈希表可从字典中获得）
    /// </summary>
    private void LoadLanguageToCache()
    {
        XmlDocument xmlDoc = new XmlDocument();
        string filePath = Application.dataPath + "/Resources/Language/Language.xml";
        if (File.Exists(filePath))
        {
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("Language");
            XmlNodeList languageTypeNodes = root.ChildNodes;
            for (int i = 0; i < languageTypeNodes.Count; i++)
            {
                Debug.Log(languageTypeNodes[i].Name);//English Chinese ...
                if (!LanguageHashDic.ContainsKey(languageTypeNodes[i].Name))
                {
                    LanguageHashDic.Add(languageTypeNodes[i].Name, new Hashtable());
                    XmlNodeList stringNodes = languageTypeNodes[i].SelectNodes("string");
                    for (int j = 0; j < stringNodes.Count; j++)
                    {
                        XmlElement xe = stringNodes[j] as XmlElement;
                        Debug.Log(xe.FirstChild.Value + xe.GetAttribute("name"));
                        Hashtable hash = LanguageHashDic[languageTypeNodes[i].Name];
                        hash.Add(xe.GetAttribute("name"), xe.FirstChild.Value);
                    }
                }
                else
                {
                    Debug.Log("The Language Has Been Loaded");
                }
            }
        }
        else
        {
            Debug.Log("The File Dont Exists");
        }
    }

    public override void ReleaseCache()
    {
        base.ReleaseCache();
        LanguageHashDic.Clear();
    }
}
