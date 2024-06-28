
using UnityEngine;

[System.Serializable]
public class EnemySpawnerSettings
{    
    //[Header("������ ����")]
    public EnemyController enemy;
    //[Header("�� ������")]
    public float spawnCd = -1;
    //[Header("����� ���-�� ����� �� �����")]
    public int totalAmountOfEnemies = -1;
   // [Header("���-�� ����� � �����")]
    public int amountOfEnemiesInPack = 1;
   //[Header("���� ��������� �� ��� ������ ������ �����, ������� ������ ���� ����� ������")]
    public float radiusOfPack = 0f;
   //[Header("�������� ����� ������ �������")]
    public float startSpawnTime;
    //[Header("����� �� ����� �����, ����� ��������� ��������")]
    public float endSpawnTime;
    //[Header("������ �� ������")]
    public float radiusOfPlayer = 20f;
   // [Header("�������� � ���������� �����")]
    public bool isSpecificPoint = false;
    public Vector2 specificPoint;


    public float GetCdSpawn(float timeOfWave)
    {
        float result = (timeOfWave - startSpawnTime - endSpawnTime) / ((float)GetTotalAmountOfEnemies() / (float)amountOfEnemiesInPack);
        return result;
    }

    public int GetTotalAmountOfEnemies()
    {
        return totalAmountOfEnemies - totalAmountOfEnemies % amountOfEnemiesInPack;
    }

    public int GetTotalAmountOfEnemies(float timeOfWave)
    {
        return (int)((timeOfWave - startSpawnTime - endSpawnTime) / spawnCd * (float)amountOfEnemiesInPack);
    }

}
