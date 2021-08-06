using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System;

public class CameraController : MonoBehaviour
{
    GameObject _player;
    GameObject _target;
    Vector2 _dir;
    float _moveSpeed;
    [SerializeField]
    Vector3 _delta = new Vector3(0, 0, -20);
    [SerializeField]

    public GameObject Target { set { _target = value; } }
    void LateUpdate()
    {
        UpdateTarget();
        UpdatePosition();
    }
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
    #region UpdatePosition
    private void UpdatePosition()
    {
        //the farther the target is, the faster the camera moves
        CalculateDir();
        if(_dir.magnitude < 1.0f) { return; }
        CalculateMoveSpeed();
        MoveCamera();
    }

    private void CalculateDir()
    {
        _dir = _target.transform.position - transform.position;
    }
    private void CalculateMoveSpeed()
    {
        _moveSpeed = _dir.sqrMagnitude;
    }
    private void MoveCamera()
    {
        transform.Translate(_dir.normalized * _moveSpeed * Timing.DeltaTime);
    }

    #endregion
}
