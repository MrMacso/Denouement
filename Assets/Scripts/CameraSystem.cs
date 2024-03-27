using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CinemachineVirtualCamera;
    [SerializeField] float FieldOfViewMax = 100f;
    [SerializeField] float FieldOfViewMin = 10f;
    [SerializeField] bool UseEdgeScrolling = false;
    [SerializeField] bool UseDragPan = false;

    bool _dragPanMoveActive = false;
    Vector2 _lastMousePosition= Vector2.zero;
    float _targetFieldOfView = 50f;

    static float MOVE_SPEED = 5f;
    static float ROTATE_SPEED = 100f;
    static int EDGE_SCROLL_SIZE = 15;

    private void Update()
    {
        HandleCameraMovement();

        if (UseEdgeScrolling)
            HandleCameraMovementEdgeScrolling();
        if (UseDragPan)
            HandleCameraMovementDragPan();

        HandleCameraRotation();
        HandleCameraZoom();
    }

    void HandleCameraMovement()
    {
        Vector3 _inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) _inputDir.z = 1f;          //UP
        if (Input.GetKey(KeyCode.S)) _inputDir.z = -1f;         //DOWN
        if (Input.GetKey(KeyCode.A)) _inputDir.x = -1f;         //LEFT
        if (Input.GetKey(KeyCode.D)) _inputDir.x = 1f;          //RIGHT

        Vector3 moveDir = transform.forward * _inputDir.z + transform.right * _inputDir.x;

        transform.position += moveDir * MOVE_SPEED * Time.deltaTime;
    }
    void HandleCameraMovementEdgeScrolling()
    {
        Vector3 _inputDir = new Vector3(0, 0, 0);
        
            //UP
            if (Input.mousePosition.y > Screen.height - EDGE_SCROLL_SIZE)
                _inputDir.z = 1f;
            //DOWN
            if (Input.mousePosition.y < EDGE_SCROLL_SIZE)
                _inputDir.z = -1f;
            //LEFT
            if (Input.mousePosition.x < EDGE_SCROLL_SIZE)
                _inputDir.x = -1f;
            //RIGHT
            if (Input.mousePosition.x > Screen.width - EDGE_SCROLL_SIZE)
                _inputDir.x = 1f;

        Vector3 moveDir = transform.forward * _inputDir.z + transform.right * _inputDir.x;

        transform.position += moveDir * MOVE_SPEED * Time.deltaTime;
    }
    void HandleCameraMovementDragPan()
    {
        Vector3 _inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            _dragPanMoveActive = true;
            _lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _dragPanMoveActive = false;
        }
        if (_dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - _lastMousePosition;

            float dragPanSpeed = 2f;
            _inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            _inputDir.z = mouseMovementDelta.y * dragPanSpeed;

            _lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = transform.forward * _inputDir.z + transform.right * _inputDir.x;

        transform.position += moveDir * MOVE_SPEED * Time.deltaTime;
    }
    void HandleCameraRotation()
    {
        float rotateDir = 0f;
        //ROTATE LEFT
        if (Input.GetKey(KeyCode.Q)) rotateDir = 1f;
        //ROTATE RIGHT
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;
        
        transform.eulerAngles += new Vector3(0, rotateDir * ROTATE_SPEED * Time.deltaTime, 0);
    }
    void HandleCameraZoom()
    {
        //ZOOM IN
        if(Input.mouseScrollDelta.y > 0)
        {
            _targetFieldOfView -= 5f;
        }
        //ZOOM OUT
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFieldOfView += 5f;
        }
        _targetFieldOfView = Mathf.Clamp(_targetFieldOfView, FieldOfViewMin, FieldOfViewMax);

        float zoomSpeed = 10f;
        CinemachineVirtualCamera.m_Lens.FieldOfView = 
            Mathf.Lerp(CinemachineVirtualCamera.m_Lens.FieldOfView, _targetFieldOfView, Time.deltaTime * zoomSpeed);
        
    }
}
