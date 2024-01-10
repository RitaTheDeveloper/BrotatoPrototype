using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    private void FixedUpdate()
    {
        //Move();
    }
    private void Update()
    {
        Move();
        // Rotation();
    }

    public void SetSpeed()
    {
        _speed = playerCharacteristics.CurrentMoveSpeed;
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);

        characterController.Move(move * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        //Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10f * Time.deltaTime);
        //transform.LookAt(move, Vector3.right);
        if (move != Vector3.zero)
        {
            //model.transform.LookAt(transform.position + move);
            //model.rotation *= startRotationModel;
            transform.LookAt(transform.position + move);
        }          
    }

    private void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectPoint = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(heightCorrectPoint);
        }
    }
}
