using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("������ �����")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("���������� ������")]
    [SerializeField] private int amountOfEnemies;

    [Header("����� ����� �������")]
    [SerializeField] private float _spawnTime;

    [Header("���� ��������� �� ��� ������ ������ �����, ������� ������ ���� ����� ������")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;


}
