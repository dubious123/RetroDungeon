using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCameraScroll : MonoBehaviour
{
    public void invoke()
    {
        Managers.InputMgr.GameController.ActivateCameraScroll();
    }
}
