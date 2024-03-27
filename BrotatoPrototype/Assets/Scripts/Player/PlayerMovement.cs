using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 6f;
    private float _speed;    
    private PlayerCharacteristics playerCharacteristics;
    private CharacterController characterController;
    private float yPosition;
    [SerializeField] Transform model;
    private Quaternion startRotationModel;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        startRotationModel = model.rotation;
    }

    private void Start()
    {        
        _speed = playerCharacteristics.CurrentMoveSpeed;        
        yPosition = transform.position.y;
    }

    private void Update()
    {
        Move();
    }

    public void SetSpeed()
    {
        _speed = playerCharacteristics.CurrentMoveSpeed;
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0f, z).normalized;

        characterController.Move(move * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

        if (move != Vector3.zero)
        {
            Rotation(move);
            PlaySoundOfMoving();
        }

    }
    
    private void Rotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _turnSpeed * Time.deltaTime);
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = rotation;
    }

    private void PlaySoundOfMoving()
    {

    }
}
