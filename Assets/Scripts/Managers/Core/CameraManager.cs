using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    CameraController _gameCam;
    public CameraController GameCam { get { return _gameCam; } }
    public void Init()
    {
        _gameCam = Camera.main.GetComponent<CameraController>();
    }
    public void InitGameCamera(GameObject player)
    {
        SetGameCameraTarget(player);
    }
    public void SetGameCameraTarget(GameObject target)
    {
        _gameCam.Target = target;
    }
    
}
