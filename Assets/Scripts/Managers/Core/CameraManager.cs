using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    CameraController _gameCam;
    CameraController GameCam { get { return _gameCam; } }
    public void Init()
    {
        _gameCam = Camera.main.GetComponent<CameraController>();
    }
    public void InitGameCamera(GameObject target)
    {
        SetGameCameraTarget(target);
    }
    public void SetGameCameraTarget(GameObject target)
    {
        _gameCam.Target = target;
    }
}
