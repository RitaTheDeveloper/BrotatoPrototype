using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] WaveController currentWave;

    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _gameIsOver = false;
    }

    public void Lose()
    {
        _gameIsOver = true;
        currentWave.AllSpawnerOff();
        Debug.Log("Game over!");
    }
}
