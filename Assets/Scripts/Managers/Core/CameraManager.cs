using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    GameCamController _gameCamController;
    Camera _gameCam;
    public GameCamController GameCamController 
    { 
        get 
        { 
            if(_gameCamController == null) { _gameCamController = Camera.main.GetComponent<GameCamController>(); }
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
        Managers.ResourceMgr.Instantiate("Camera/Unit_Cam");
    }
    public void SetGameCameraTarget(GameObject target)
    {
        _gameCamController.Target = target;
    }
    public void Clear()
    {

    }
}
