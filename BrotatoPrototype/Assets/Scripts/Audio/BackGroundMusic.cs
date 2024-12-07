using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [Tooltip("������� ����������� ���������� �����")]
    [SerializeField] public ConditionClass endMusicCondition;

    [Tooltip("���� ��� �������")]
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