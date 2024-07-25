using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemySpawnerSettings))]
public class EnemySpawnSettingsPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUILayout.LabelField("Enemy Spawner", EditorStyles.boldLabel);
        SerializedProperty enemy = property.FindPropertyRelative("enemy");
        SerializedProperty typeEnemy = property.FindPropertyRelative("typeEnemy");
        SerializedProperty tierType = property.FindPropertyRelative("tierType");
        SerializedProperty spawnCd = property.FindPropertyRelative("spawnCd");
        SerializedProperty totalAmountOfEnemies = property.FindPropertyRelative("totalAmountOfEnemies");
        SerializedProperty amountOfEnemiesInPack = property.FindPropertyRelative("amountOfEnemiesInPack");
        SerializedProperty radiusOfPack = property.FindPropertyRelative("radiusOfPack");

        SerializedProperty startSpawnTime = property.FindPropertyRelative("startSpawnTime");
        SerializedProperty endSpawnTime = property.FindPropertyRelative("endSpawnTime");
        SerializedProperty radiusOfPlayer = property.FindPropertyRelative("radiusOfPlayer");

        SerializedProperty isSpecificPoint = property.FindPropertyRelative("isSpecificPoint");
        SerializedProperty specificPoint = property.FindPropertyRelative("specificPoint");

        EditorGUILayout.PropertyField(enemy, new GUIContent("mob prefab"));

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(typeEnemy, new GUIContent("typeEnemy"));
        EditorGUILayout.PropertyField(tierType, new GUIContent("tierType"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(spawnCd, new GUIContent("spawnCd"));
        EditorGUILayout.PropertyField(totalAmountOfEnemies, new GUIContent("totalAmountOfEnemies"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(amountOfEnemiesInPack, new GUIContent("amountOfEnemiesInPack"));
        int amountOfPack = amountOfEnemiesInPack.intValue;
        if (amountOfPack != 1 && amountOfPack != 0)
        {
            EditorGUILayout.PropertyField(radiusOfPack, new GUIContent("radiusOfPack"));
        } 
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 150;
        EditorGUILayout.PropertyField(startSpawnTime, new GUIContent("startSpawnTime"));
        EditorGUILayout.PropertyField(endSpawnTime, new GUIContent("endSpawnTime"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.PropertyField(isSpecificPoint, new GUIContent("isSpecificPoint"));
        bool _isSpecificPoint = isSpecificPoint.boolValue;
        if(_isSpecificPoint)
        {
            EditorGUILayout.PropertyField(specificPoint, new GUIContent("specificPoint"));
        }
        else
        {
            EditorGUILayout.PropertyField(radiusOfPlayer, new GUIContent("radiusOfPlayer"));
        }
        EditorGUILayout.EndHorizontal();

    }
}
