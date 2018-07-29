using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IMagic),true)]
[ExecuteInEditMode]
public class IMagicEditor :Editor
{
    
    IMagic magic;

    bool showTimeLine;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        magic = target as IMagic;

        showTimeLine=EditorGUILayout.Foldout(showTimeLine,"ShowTimeLine");
        if (showTimeLine)
        {
            DrawTimeLine();
        }
    }

    void DrawTimeLine()
    {
        int count;
        TimeLine timeLine = magic.TimeLine;
        count = EditorGUILayout.IntField("Size",timeLine.Count);
        if (count < timeLine.Count)
        {
            timeLine.RemoveLast(timeLine.Count - count);
        }
        else if (count > timeLine.Count)
        {
            timeLine.AddLast(count - timeLine.Count);
        }


        EditorGUI.indentLevel = 1;
        
        foreach (TimeLineEvent lineEvent in magic.TimeLine)
        {
            DrawTimeEvent(lineEvent);
        }
    }

    void DrawTimeEvent(TimeLineEvent timeLineEvent)
    {
      
        timeLineEvent.time=EditorGUILayout.FloatField("BeginTime",timeLineEvent.time);
        
        timeLineEvent.createGo=EditorGUILayout.ObjectField("CreateObj", timeLineEvent.createGo, typeof(GameObject),true) as GameObject;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }


}
