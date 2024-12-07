using NTC.MonoCache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoCache
{
    private Transform _player;
    private Cinemachine.CinemachineVirtualCamera cinVirtCam;

    private void Awake()
    {
        cinVirtCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    protected override void Run()
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
