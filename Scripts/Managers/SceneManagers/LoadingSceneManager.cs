using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    private void Awake()
    {
        LoadingWindow.nextSceneName = "RegistLoginScene";
        UIManager.Instance.CreateOrShowWindow("LoadingWindow",UIManager.Instance.Canvas);

    }
}
