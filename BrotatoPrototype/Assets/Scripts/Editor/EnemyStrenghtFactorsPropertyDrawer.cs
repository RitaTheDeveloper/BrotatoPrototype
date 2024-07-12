using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyStrengthFactors))]
public class EnemyStrenghtFactorsPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);
        //EditorGUI.indentLevel++;
       // Rect labelPos = new Rect(position.x - 20, position.y, position.width, position.height);
        EditorGUILayout.LabelField("Enemy Strength Factors", EditorStyles.boldLabel);
      //  EditorGUI.LabelField(labelPos, "Enemy Strength Factors", EditorStyles.boldLabel);
        //EditorGUI.BeginProperty(position, label, property);

        
        SerializedProperty speedFactor = property.FindPropertyRelative("speedFactor");
        SerializedProperty damageFactor = property.FindPropertyRelative("damageFactor");
        SerializedProperty healthFactor = property.FindPropertyRelative("healthFactor");
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 70;
        EditorGUILayout.PropertyField(speedFactor, new GUIContent("speed"));
        EditorGUILayout.PropertyField(damageFactor, new GUIContent("damage"));
        EditorGUILayout.PropertyField(healthFactor, new GUIContent("health"));
        EditorGUILayout.EndHorizontal();
        // EditorGUI.EndProperty();
        EditorGUILayout.Space();
        //EditorGUI.indentLevel--;
    }
}
