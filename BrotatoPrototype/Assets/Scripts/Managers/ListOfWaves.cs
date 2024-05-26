using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfWaves : MonoBehaviour
{
    [SerializeField] private List<WaveController> _listOfWaves;

    public List<WaveController> GetListOfWaves { get => _listOfWaves; }
}
