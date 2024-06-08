using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Скорость поворота")]
    [SerializeField] private float _turnSpeed = 6f;
    private float basicSpeed = 11f;
    private float _speed;    
    private PlayerCharacteristics playerCharacteristics;
    private CharacterController characterController;
    private float yPosition;
    [SerializeField] Transform model;
    [Header("Сила наклона модельки")]
    [SerializeField] private float _tiltForce = 25f;
    [Header("Скорость наклона модельки")]
    [SerializeField] private float _tiltSpeed = 6f;
    private Quaternion startRotationModel;

    private Vector2 moveInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        startRotationModel = model.rotation;
    }

    private void Start()
    {
        SetSpeed();      
        yPosition = transform.position.y;
    }

    private void Update()
    {
        if (GameManager.instance.IsPlaying)
        {
            Move();
        }
    }

    public void SetSpeed()
    {
        _speed = playerCharacteristics.CurrentMoveSpeed / 100f * basicSpeed;
        if(_speed < 0)
        {
            _speed = 0f;
        }
    }

    public void SetMoveInput(Vector2 moveDirection)
    {
        moveInput = moveDirection;
    }

    private void Move()
    {        
        var x = moveInput.x;
        var z = moveInput.y;
        Vector3 move = new Vector3(x, 0f, z).normalized;

        characterController.Move(move * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        ModelRotation(move);
        if (move != Vector3.zero)
        {
            Rotation(move);
            //PlaySoundOfMoving();
        }
        else
        {
            //StopSoundOfMoving();
        }
    }
    
    private void Rotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _turnSpeed * Time.deltaTime);
        // rotation.x = 0f;
        // rotation.z = 0f;
        transform.rotation = rotation;
      
    }

    private void ModelRotation(Vector3 direction)
    {
        Quaternion target = Quaternion.Euler(new Vector3(Mathf.Abs(direction.x) + Mathf.Abs(direction.z), 0f, 0f) * _tiltForce);
        model.localRotation = Quaternion.Slerp(model.localRotation, target, Time.deltaTime * _tiltSpeed);
    }
    private void PlaySoundOfMoving()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMovement(true);
        }
    }

    private void StopSoundOfMoving()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMovement(false);
        }
    }

    public void PutPlayerInStartPosition(Vector3 playerStartingSpawnPoint)
    {
        //characterController.Move(playerStartingSpawnPoint);
        transform.position = playerStartingSpawnPoint;
    }

}
