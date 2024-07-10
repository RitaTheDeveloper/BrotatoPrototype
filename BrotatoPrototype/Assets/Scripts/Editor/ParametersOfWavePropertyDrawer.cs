using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ParametersOfWave))]
public class ParametersOfWavePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // EditorGUILayout.LabelField("Parameters o", EditorStyles.boldLabel);
        SerializedProperty time = property.FindPropertyRelative("waveTime");
        SerializedProperty amountOfGoldPerWave = property.FindPropertyRelative("amountOfGoldPerWave");
        SerializedProperty amountOfExpPerWave = property.FindPropertyRelative("amountOfExpPerWave");

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 50;
        EditorGUILayout.PropertyField(time, new GUIContent("time"));
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(amountOfGoldPerWave, new GUIContent("amountOfGoldPerWave"));
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(amountOfExpPerWave, new GUIContent("amountOfExpPerWave"));
        EditorGUILayout.EndHorizontal();
    }

}
