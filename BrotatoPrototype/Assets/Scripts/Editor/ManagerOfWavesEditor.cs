using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ManagerOfWaves))]
public class ManagerOfWavesEditor : Editor
{
    SerializedProperty _listOfWaveSettings;
    ReorderableList list;

    private void OnEnable()
    {
        _listOfWaveSettings = serializedObject.FindProperty("_listOfWaveSettings");
        list = new ReorderableList(serializedObject, _listOfWaveSettings, true, true, true, true);
        list.drawHeaderCallback = DrawHeader;
    }


    private void DrawListItems(Rect rect, int index)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
    }

    private void DrawHeader(Rect rect)
    {
        string name = "Wave";
        EditorGUI.LabelField(rect, name);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        list.DoLayoutList();
    }
}
