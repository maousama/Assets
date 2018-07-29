using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRegistMessageBox : MessageBox {
    public override string Name
    {
        get
        {
            return "LoginRegistMessageBox";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                
                case "Btn_Enter":
                    buttonsList[i].onClick.AddListener(EnterBtnClick);
                    break;
            }
        }
    }

    public void EnterBtnClick()
    {
        UIManager.Instance.CloseWindow("LoginRegistMessageBox");
    }
}
