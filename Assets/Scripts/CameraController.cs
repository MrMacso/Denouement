using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform CameraTransform; 
    [SerializeField] float MovementTime;

    float _movementSpeed = 1F;

    Vector3 _newPosition;
    Quaternion _newRotation;
    Vector3 _newZoom;
    Vector3 _dragStartPosition;
    Vector3 _dragCurrentPosition;

    float NORMAL_SPEED = 0.05f; 
    float FAST_SPEED = 0.15f; 
    static float ROTATION_AMOUNT = 1f;
    Vector3 ZOOM_AMOUNT = new Vector3(0, -1, 1);

    private void Awake()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = CameraTransform.localPosition;
    }

    void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        //CAMERA ZOOM
        if(Input.mouseScrollDelta.y != 0) 
        {
            _newZoom += Input.mouseScrollDelta.y * ZOOM_AMOUNT;
        }
        //CAMERA INPUT
        if(Input.GetMouseButtonDown(0)) 
        {
            Plane plane= new Plane(Vector3.up, Vector3.zero);

            Ray  ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry)) 
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);

                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }
    }

    void HandleKeyboardInput()
    {
        //CAMERA SPEED
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _movementSpeed = FAST_SPEED;
        }
        else
        {
            _movementSpeed = NORMAL_SPEED;
        }
        //CAMERA MOVEMENT
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += transform.forward * _movementSpeed;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += transform.forward * -_movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += transform.right * _movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += transform.right * -_movementSpeed;
        }
        //CAMERA ROTATION
        if(Input.GetKey(KeyCode.Q))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * ROTATION_AMOUNT);
        }
        if (Input.GetKey(KeyCode.E))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * -ROTATION_AMOUNT);
        }
        //CAMERA ZOOM
        if(Input.GetKey(KeyCode.R))
        {
            _newZoom += ZOOM_AMOUNT;
        }
        if (Input.GetKey(KeyCode.F))
        {
            _newZoom -= ZOOM_AMOUNT;
        }

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * MovementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * MovementTime);
        CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, _newZoom, Time.deltaTime * MovementTime);
    }
}
