using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUI : MonoBehaviour
{
    public UnityEvent GUIEvent = new UnityEvent();
    private void OnGUI()
    {
        GUIEvent.Invoke();
    }
}
