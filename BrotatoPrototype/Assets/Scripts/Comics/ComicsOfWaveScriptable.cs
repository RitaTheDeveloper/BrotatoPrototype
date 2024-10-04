using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComicsOfWaves", menuName = "Data/ComicsOfWaves", order = 51)]
public class ComicsOfWaveScriptable : ScriptableObject
{
    public ComicsOfWave[] comicsOfWaves;
}

[System.Serializable]
public class ComicsOfWave
{
    public string name;
    public int numberOfWave;
    public Sprite[] comicsSprites;
    public bool alreadyShown = false;


    public Sprite[] GetArrayOfComics()
    {
        return null;
    }
}
