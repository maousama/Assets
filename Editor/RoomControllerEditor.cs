using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomController))]
[ExecuteInEditMode]
public class RoomControllerEditor : Editor{
    RoomController roomController;

    float pillarWeight;
    float chestWeight;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        roomController = target as RoomController;

        pillarWeight = EditorGUILayout.FloatField("PillarWeight", roomController.PillarWeight);
        if (pillarWeight != roomController.PillarWeight)
        {
            roomController.PillarWeight = pillarWeight;
        }

        chestWeight= EditorGUILayout.FloatField("chestWeight", roomController.ChestWeight);
        if (chestWeight != roomController.ChestWeight)
        {
            roomController.ChestWeight = chestWeight;
        }


    }

}
