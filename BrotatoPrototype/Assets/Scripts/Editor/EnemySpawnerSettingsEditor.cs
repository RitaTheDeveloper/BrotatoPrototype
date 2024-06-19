
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(EnemySpawnerSettings))]
public class EnemySpawnerSettingsEditor : Editor
{
    private EnemySpawnerSettings ess;
    private SerializedProperty spawnPointType;
    private void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {

        //EditorGUILayout.PropertyField(enemy);
        
        base.OnInspectorGUI();
        //_ess.enemy = EditorGUILayout
        //if(_ess.spawnPointType is SpawnPointType.specificPoint)
        //{
        //    _ess.specificPoint = EditorGUILayout.Vector2Field("specificPoint", _ess.specificPoint);
        //}
        //EditorGUILayout.PropertyField(enemySpawnerSettings);
    }
}
