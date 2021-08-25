using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    CameraController _gameCamController;
    Camera _gameCam;
    public CameraController GameCamController { get { return _gameCamController; } }
    public Camera GameCam { get { return _gameCam; } }
    public void Init()
    {
        _gameCam = Camera.main;
        _gameCamController = _gameCam.GetComponent<CameraController>();
    }
    public void InitGameCamera(GameObject player)
    {
        SetGameCameraTarget(player);
    }
    public void SetGameCameraTarget(GameObject target)
    {
        _gameCamController.Target = target;
    }
    
}
