using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;
    private Cinemachine.CinemachineVirtualCamera cinVirtCam;

    private void Awake()
    {
        cinVirtCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if (!_player)
        {
            try
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
                cinVirtCam.Follow = _player;
            }
            catch
            {

            }
            
        } 
        
    }
}
