using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
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
    [SerializeField]
    public GameObject Target { set { _target = value; } }
    public Define.CameraState State { get { return _state; } set { _state = value; } }
    private void Start()
    {
        _state = Define.CameraState.Auto;
        _moveSpeedManual2D = 4.0f;
    }
    void LateUpdate()
    {
        if (_state == Define.CameraState.Auto)
        {
            UpdateTarget();
            UpdatePosition();
        }
        else if(_state == Define.CameraState.Manual)
        {
            MoveCameraManual2D();
        }
    }
    #region Scroll Camera Movement

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
