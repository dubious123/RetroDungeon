using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    [SerializeField]
    float _cameraSize;
    float _cameraDeltaSize;
    float _maxCameraSize;
    float _minCameraSize;
    [SerializeField]
    double _holdDuration;
    double _maxHoldDuration;
    double _waitDuration;
    double _maxWaitDuration;
    [SerializeField]
    Define.CameraState _state;
    GameObject _player;
    GameObject _target;
    [SerializeField]
    Vector2 _dir;
    float _moveSpeedAuto;
    float _moveSpeedManual2D;
    [SerializeField]
    Vector3 _delta = new Vector3(0, 0, -20);
    float DeltaSize;
    [SerializeField]
    public GameObject Target { set { _target = value; } }
    public Define.CameraState State { get { return _state; } set { _state = value; } }
    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        _state = Define.CameraState.Auto;
        _moveSpeedManual2D = 6.0f;
        _maxHoldDuration = 0.5;
        _maxWaitDuration = 2;

        _maxCameraSize = 8.0f;
        _minCameraSize = 3.0f;
        _cameraSize = 5.0f;
        _cameraDeltaSize = 0f;
        
    }
    void LateUpdate()
    {
        if (_state == Define.CameraState.Auto)
        {
            _holdDuration = 0;
            _waitDuration += Timing.DeltaTime;
            if(_waitDuration > _maxWaitDuration)
            {
                UpdateTarget();
                UpdatePosition();
            }
        }
        else if(_state == Define.CameraState.Manual)
        {
            _waitDuration = 0;
            _holdDuration += Timing.DeltaTime;
            if (_holdDuration > _maxHoldDuration)
            {
                MoveCameraManual2D();
            }
        }
        UpdateSize();
    }
    #region Scroll Camera Movement
    public void UpdateCameraSize(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().y;
        if (value < 0)
        {
            _cameraDeltaSize = 1f;
        }
        else if (value > 0)
        {
            _cameraDeltaSize = -1f;
        }
        else
        {
            _cameraDeltaSize = 0f;
        }
    }
    public void UpdateSize()
    {
        _cameraSize += _cameraDeltaSize;
        if (_cameraSize < _minCameraSize)
        {
            _cameraSize = _minCameraSize;
        }
        else if(_cameraSize > _maxCameraSize)
        {
            _cameraSize = _maxCameraSize;
        }
        else
        {
            _camera.orthographicSize = _cameraSize;
        }
    }
    #endregion
    #region Auto Target Movement
    private void UpdateTarget()
    {
        if (_target == null)
        {
            if (_player == null)
            {
                _player = Managers.GameMgr.Player;
            }
            _target = _player;
        }
    }
    private void UpdatePosition()
    {
        //the farther the target is, the faster the camera moves
        CalculateDir();
        if(_dir.magnitude < 1.0f) { return; }
        CalculateMoveSpeed();
        MoveCameraAuto();
    }
    private void CalculateDir()
    {
        _dir = _target.transform.position - transform.position;
    }
    private void CalculateMoveSpeed()
    {
        _moveSpeedAuto = _dir.sqrMagnitude;
    }
    private void MoveCameraAuto()
    {
        transform.Translate(_dir.normalized * _moveSpeedAuto * Timing.DeltaTime);
    }
    #endregion
    #region Manual 2D Movement
    public void UpdateManualPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _state = Define.CameraState.Manual;
            _dir = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _state = Define.CameraState.Auto;
        }

    }
    private void MoveCameraManual2D()
    {
        transform.Translate(_dir.normalized * _moveSpeedManual2D * Timing.DeltaTime);
    }
    #endregion

}
