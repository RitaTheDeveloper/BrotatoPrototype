
using UnityEngine;

[System.Serializable]
public class EnemySpawnerSettings
{    
    //[Header("префаб моба")]
    public EnemyController enemy;
    //[Header("кд спавна")]
    public float spawnCd = -1;
    //[Header("общее кол-во мобов за волну")]
    public int totalAmountOfEnemies = -1;
   // [Header("кол-во мобов в пачке")]
    public int amountOfEnemiesInPack = 1;
   //[Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    public float radiusOfPack = 0f;
   //[Header("задержка перед первым спавном")]
    public float startSpawnTime;
    //[Header("время до конца волны, чтобы закончить спавнить")]
    public float endSpawnTime;
    //[Header("радиус от игрока")]
    public float radiusOfPlayer = 20f;
   // [Header("спавнить в конкретной точке")]
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
