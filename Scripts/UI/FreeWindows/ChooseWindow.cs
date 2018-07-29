using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWindow : FixedWindow
{
    public override string Name
    {
        get
        {
            return "ChooseWindow ";
        }
    }

    public int index = 0;
    /// <summary>
    /// 角色预设体集合
    /// </summary>
    private GameObject[] prefabs = new GameObject[2];
    /// <summary>
    /// 场景中角色物体集合父物体
    /// </summary>
    private GameObject characters;
    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_Previous":
                    buttonsList[i].onClick.AddListener(PreviousBtnClick);
                    break;
                case "Btn_Next":
                    buttonsList[i].onClick.AddListener(NextBtnClick);
                    break;
                case "Btn_Enter":
                    buttonsList[i].onClick.AddListener(EnterBtnClick);
                    break;
                    
            }
        }
        characters = GameObject.Find("Characters");
        if (characters == null)
        {
            characters = new GameObject("Characters");
            characters.transform.position = Vector3.zero;

        }
        GameObject male = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Characters, "Character_Hero_Knight_Male");
        GameObject female = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Characters, "Character_Hero_Knight_Female");
        prefabs[0] = Instantiate(male, characters.transform);

        prefabs[1] = Instantiate(female, characters.transform);
        prefabs[0].transform.localScale = Vector3.one * 2f;
        prefabs[1].transform.localScale = Vector3.one * 2f;
    }
    private void EnterBtnClick()
    {
        Debug.Log("进入游戏");
    }
    private void PreviousBtnClick()
    {
        if (--index < 0)
        {
            index = 0;
            return;
        }
        ShowPlayer(index);
    }
    private void NextBtnClick()
    {
        if (++index == prefabs.Length)
        {
            index = prefabs.Length - 1;
            return;
        }
        ShowPlayer(index);
    }
    private void ShowPlayer(int _index)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (index == i)
            {
                prefabs[i].gameObject.SetActive(true);
            }
            else
            {
                prefabs[i].gameObject.SetActive(false);
            }
        }

    }

}
