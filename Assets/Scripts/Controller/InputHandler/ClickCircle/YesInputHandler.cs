using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YesInputHandler : MonoBehaviour
{
    public UnityEvent YesEvent;
    private void OnDisable()
    {
        YesEvent.RemoveAllListeners();
    }
    public void InvokeYes()
    {
        YesEvent.Invoke();
    }
}
