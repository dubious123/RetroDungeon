using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestManager 
{
    public void Init()
    {

    }
    public void StartTest(InputAction.CallbackContext context)
    {
        Debug.Log("TestMode");
        Managers.ResourceMgr.Instantiate("Test/Canvas_Test");
    }
    public void Clear()
    {

    }
}
