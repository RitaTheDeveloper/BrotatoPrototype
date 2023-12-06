using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private CharacterController characterController;
    private float yPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        yPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        Rotation();
        
    }
    private void LateUpdate()
    {
        
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);

        characterController.Move(move * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
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
