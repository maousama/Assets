using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuUtil
{
    [MenuItem("AssetBundle/CreateAssetBundleFile")]
    public static void AssetBundleCreateFile()
    {
        string path = Application.dataPath + "/../AssetBundles";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
