using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ICharacter),true)]
[ExecuteInEditMode]
public class ICharacterEditor : Editor{

    ICharacter character;


    float maxHp;
    float hp;
    float atk;
    float agl;
    float hard;
    float dodgeCoolDown;
    bool canDodge;

    bool showInfo=true;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        showInfo = EditorGUILayout.Foldout(showInfo,"ShowInfo");
        if (showInfo)
        {
            DrawInfo();
        }
    }


    protected void DrawInfo()
    {

        character = target as ICharacter;

        EditorGUILayout.LabelField("Name",character.Name);

        string stateName = character.CurrentState == null ? "" : character.CurrentState.Name;

        EditorGUILayout.LabelField("CurrentState", stateName);


        maxHp = EditorGUILayout.FloatField("maxHp", character.MaxHP);
        if (maxHp != character.MaxHP)
        {
            character.MaxHP = maxHp;
        }

        hp = EditorGUILayout.FloatField("Hp", character.HP);
        if (hp != character.HP)
        {
            character.HP = hp;
        }

        atk = EditorGUILayout.FloatField("Atk", character.ATK);
        if (atk != character.ATK)
        {
            character.ATK = atk;
        }

        agl = EditorGUILayout.FloatField("Agl", character.AGL);
        if (agl != character.AGL)
        {
            character.AGL = agl;
        }

        hard = EditorGUILayout.FloatField("Hard", character.Hard);
        if (hard != character.Hard)
        {
            character.Hard = hard;
        }

        canDodge=EditorGUILayout.Toggle("CanDodge", character.CanDodge);
        if (canDodge != character.CanDodge)
        {
            character.CanDodge = canDodge;
        }

       

    }

}
