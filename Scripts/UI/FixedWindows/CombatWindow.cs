using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWindow : FixedWindow
{
    public Hero hero;
    public IMonster monster;

    public override string Name
    {
        get
        {
            return "CombatWindow";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        hero.OnHPChange += ShowHp;
        monster.OnHPChange += ShowMonsterHp;
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_0":
                    buttonsList[i].onClick.AddListener(Btn_0Click);
                    break;
                case "Btn_1":
                    buttonsList[i].onClick.AddListener(Btn_1Click);
                    break;
                case "Btn_2":
                    buttonsList[i].onClick.AddListener(Btn_2Click);
                    break;
            }
        }
    }

    private void Btn_0Click()
    {

    }
    private void Btn_1Click()
    {

    }
    private void Btn_2Click()
    {

    }
    private void ShowHp(float curhp, float maxhp)
    {
        imageDic["Image_HpBar"].fillAmount = curhp / maxhp;
    }
    private void ShowMonsterHp(float curmonsterhp, float maxmonsterhp)
    {
        imageDic["Image_MonsterHpBar"].fillAmount = curmonsterhp / maxmonsterhp;
    }
}
