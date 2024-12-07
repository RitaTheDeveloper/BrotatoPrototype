using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [Tooltip("Условие пригрывания следующего трека")]
    [SerializeField] public ConditionClass endMusicCondition;

    [Tooltip("Трек для запуска")]
    [SerializeField] public Sound musicClip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool CanPlayNext()
    {
        return endMusicCondition.CanPlayNext();
    }
}