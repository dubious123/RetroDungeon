using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    CameraController _gameCamController;
    Camera _gameCam;
    public CameraController GameCamController 
    { 
        get 
        { 
            if(_gameCamController == null) { _gameCamController = Camera.main.GetComponent<CameraController>(); }
            return _gameCamController; 
        } 
    }
    public Camera GameCam 
    { 
        get 
        { 
            if(_gameCam == null) { _gameCam = Camera.main; }
            return _gameCam; 
        } 
    }
    public void Init()
    {
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
